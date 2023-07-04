using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using RockIM.Api.Client.V1.Protocol.Socket;
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

        private const int HeartbeatInterval = 10;

        private Domain.Entities.Socket _currentConfig;

        private IChannel _current;

        private string _currentTicket;

        private readonly SdkContext _context;

        private readonly EventBus _eventBus;

        private readonly CancellationTokenSource _shutdown;

        private CancellationTokenSource _close;

        private long _lastHeartbeat;

        public SocketManager(SdkContext context, EventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
            _shutdown = new CancellationTokenSource();
            _close = new CancellationTokenSource();
        }


        public IChannel Connect(Domain.Entities.Socket config)
        {
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
            ch.ConnectAsync();

            return ch;
        }


        private void SendPacket<T>(Operation operation, IMessage data, Action<Result<T>> callback)
            where T : IMessage, new()
        {
            if (_current == null)
            {
                LoggerContext.Logger.Error("SendPacket error,socket not connected");
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
            var packet = new Packet(ProtocolVersion, (byte) Network.Types.PacketType.Request, headerBytes, bodyBytes);
            _current.SendAsync(packet, (p) =>
            {
                if (p is not Packet newPacket)
                {
                    LoggerContext.Logger.Error("invalid packet {0}", p);
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
                    LoggerContext.Logger.Error("Auth Socket Error {}", result);
                    return;
                }

                Task.Run(HeartbeatLoop);
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
        }

        /// <summary>
        /// Received Some Data From Socket
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="packet"></param>
        private void OnReceived(string ticket, IPacket packet)
        {
            LoggerContext.Logger.Info("OnReceived {0} {1}", ticket, packet);
            if (!ticket.Equals(_currentTicket))
            {
                return;
            }
        }

        private void OnReceivedError(string ticket, Exception e)
        {
            if (!ticket.Equals(_currentTicket))
            {
                return;
            }

            LoggerContext.Logger.Info("OnReceivedError {0} {1}", ticket, e.StackTrace);
        }

        /// <summary>
        /// Send Heartbeat
        /// </summary>
        private void HeartbeatLoop()
        {
            do
            {
                var now = DateUtils.NowTs();
                if (now - _lastHeartbeat < TimeSpan.FromSeconds(HeartbeatInterval).Milliseconds)
                {
                    continue;
                }

                SendPacket<HeartBeatResponse>(Operation.Heartbeat, new HeartBeatRequest(), (result) =>
                {
                    if (!result.IsSuccess())
                    {
                        LoggerContext.Logger.Error("Heartbeat Error {}", result);
                        return;
                    }

                    _lastHeartbeat = DateUtils.NowTs();
                });
            } while (!_close.Token.IsCancellationRequested && !_shutdown.Token.IsCancellationRequested);
        }

        public void Dispose()
        {
            _close?.Cancel();
            _current?.CloseAsync();
            _shutdown?.Cancel();
        }
    }
}