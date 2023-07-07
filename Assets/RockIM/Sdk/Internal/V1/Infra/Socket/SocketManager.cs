using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using RockIM.Api.Client.V1.Protocol.Socket;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Framework.Network.Socket;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Sdk.Utils;
using Network = RockIM.Shared.Enums.Network;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public class SocketManager : ISocketManager
    {
        private int _reqId;

        private const int ProtocolVersion = 1;

        private const int HeartbeatInterval = 10 * 1000;

        private const int ReConnectInterval = 3 * 1000;

        private Domain.Entities.Socket _currentConfig;

        private IChannel _current;

        private string _currentTicket;

        private readonly SdkContext _context;

        private readonly IEventBus _eventBus;

        private readonly CancellationTokenSource _shutdown;

        private CancellationTokenSource _close;

        private long _lastHeartbeat;
        private long _lastHeartbeatReply;
        private long _lastConnectTime;

        private readonly PushManager _pushManager;


        public bool IsConnected => _current is {IsConnected: true};

        public bool IsConnecting => _current is {IsConnecting: true};

        public SocketManager(SdkContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
            _shutdown = new CancellationTokenSource();
            _close = new CancellationTokenSource();

            _pushManager = new PushManager(_context, _eventBus);
        }


        public void Connect(Domain.Entities.Socket config)
        {
            if (IsConnecting || IsConnected)
            {
                return;
            }

            _currentConfig = config;
            _currentTicket = Guid.NewGuid().ToString();
            _eventBus.LifeCycle.OnConnecting();
            var parser = new PacketParser();
            var events = new SocketEvents();

            events.Connected += () => { OnConnected(_currentTicket); };
            events.Closed += () => { OnClosed(_currentTicket); };
            events.Received += (p) => { OnReceived(_currentTicket, p); };
            events.ReceivedError += (e) => { OnReceivedError(_currentTicket, e); };
            IChannel ch = new Channel(config.WebSocket.Address, new WebSocketAdapter(parser), events,
                LoggerContext.Logger);
            _current = ch;

            _close = new CancellationTokenSource();
            _lastConnectTime = DateUtils.NowTs();
            ch.ConnectAsync();
        }


        private void ReConnect()
        {
            _current.CloseAsync();
            var now = DateUtils.NowTs();

            var delay = ReConnectInterval - (now - _lastConnectTime);
            if (delay > 0)
            {
                Thread.Sleep((int) delay);
            }

            Connect(_currentConfig);
        }

        private void SendPacket<T>(Operation operation, IMessage data, Action<Result<T>> callback)
            where T : IMessage, new()
        {
            if (_current == null)
            {
                LoggerContext.Logger.Warn("SendPacket error,socket not connected");
                return;
            }

            var reqId = Interlocked.Increment(ref _reqId);

            var dataBytes = Codec.Encode(data);

            var headerBytes = Codec.Encode(new RequestPacketHeader
            {
                Operation = operation,
                RequestId = reqId
            });
            var bodyBytes = Codec.Encode(new RequestPacketBody
            {
                Body = ByteString.CopyFrom(dataBytes),
            });
            var packet = new Packet(reqId.ToString(), ProtocolVersion, (byte) Network.Types.PacketType.Request,
                headerBytes,
                bodyBytes);
            _current.SendAsync(packet, (p) =>
            {
                if (p is not Packet newPacket)
                {
                    LoggerContext.Logger.Warn("invalid packet {0}", p);
                    return;
                }

                callback(ResponsePacketDecoder.Decode<T>(newPacket));
            });
        }

        /// <summary>
        /// Socket Connected
        /// Need to Send Auth Packet
        /// </summary>
        /// <param name="ticket"></param>
        private void OnConnected(string ticket)
        {
            LoggerContext.Logger.Info("OnConnected {0}", ticket);
            if (!ticket.Equals(_currentTicket))
            {
                return;
            }

            var data = new AuthRequest
            {
                ProductId = _context.Config.APIConfig.ProductId,
                Token = _context.Authorization.AccessToken
            };
            SendPacket<AuthResponse>(Operation.Auth, data, (result) =>
            {
                if (!result.IsSuccess())
                {
                    LoggerContext.Logger.Warn("Auth Socket Error {}", result);
                    return;
                }

                Task.Run(HeartbeatLoop);
                _eventBus.LifeCycle.OnConnected();
            });
        }

        /// <summary>
        /// Socket Closed
        /// </summary>
        /// <param name="ticket"></param>
        private void OnClosed(string ticket)
        {
            LoggerContext.Logger.Info("OnClosed {0}", ticket);
            if (!ticket.Equals(_currentTicket))
            {
                return;
            }

            _close.Cancel();
            _eventBus.LifeCycle.OnDisConnected();

            // 已经shutdown，不再重连
            if (_shutdown.Token.IsCancellationRequested)
            {
                return;
            }

            // 触发重连
            ReConnect();
        }

        /// <summary>
        /// Received Some Data From Socket
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="packet"></param>
        private void OnReceived(string ticket, IPacket packet)
        {
            LoggerContext.Logger.Debug("OnReceived {0} {1}", ticket, packet);
            if (!ticket.Equals(_currentTicket))
            {
                return;
            }

            var p = (Packet) packet;
            if (Network.Types.PacketType.Push == (Network.Types.PacketType) p.Type)
            {
                _pushManager.OnReceived(p);
            }
        }

        private void OnReceivedError(string ticket, Exception e)
        {
            if (!ticket.Equals(_currentTicket))
            {
                return;
            }

            LoggerContext.Logger.Warn("OnReceivedError {0} {1}", ticket, e.StackTrace);
        }

        /// <summary>
        /// Send Heartbeat
        /// </summary>
        private void HeartbeatLoop()
        {
            do
            {
                var now = DateUtils.NowTs();
                var delay = HeartbeatInterval - (now - _lastHeartbeat);
                if (delay > 0)
                {
                    Thread.Sleep((int) delay);
                    continue;
                }

                LoggerContext.Logger.Debug("Heartbeat request");
                _lastHeartbeat = DateUtils.NowTs();
                SendPacket<HeartBeatResponse>(Operation.Heartbeat, new HeartBeatRequest(), (result) =>
                {
                    LoggerContext.Logger.Debug("Heartbeat reply");
                    if (!result.IsSuccess())
                    {
                        LoggerContext.Logger.Warn("Heartbeat Error {}", result);
                        return;
                    }

                    _lastHeartbeatReply = DateUtils.NowTs();
                });
            } while (!_close.Token.IsCancellationRequested && !_shutdown.Token.IsCancellationRequested);
        }

        public void Dispose()
        {
            _close?.Cancel();
            _current?.CloseAsync();
            _current = null;
            _shutdown?.Cancel();
        }
    }
}