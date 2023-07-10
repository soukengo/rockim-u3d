using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RockIM.Sdk.Framework.Logger;

namespace RockIM.Sdk.Framework.Network.Socket
{
    public class Channel : IChannel
    {
        private const int DefaultConnectTimeout = 3;

        private const int DefaultSendTimeout = 10;

        private readonly ISocketAdapter _adapter;
        private readonly Uri _uri;
        private readonly Dictionary<string, TaskCompletionSource<IPacket>> _responses;

        private readonly SocketEvents _events;

        private readonly object _responsesLock = new object();

        private readonly TimeSpan _sendTimeoutSec;

        public bool IsConnected => _adapter.IsConnected;

        public bool IsConnecting => _adapter.IsConnecting;

        private readonly ILogger _logger;


        public Channel(string address, ISocketAdapter adapter, SocketEvents events, ILogger logger,
            int sendTimeoutSec = DefaultSendTimeout)
        {
            _adapter = adapter;
            _uri = new UriBuilder(address).Uri;
            _responses = new Dictionary<string, TaskCompletionSource<IPacket>>();
            _sendTimeoutSec = TimeSpan.FromSeconds(sendTimeoutSec);
            _events = events;
            _logger = logger;

            _adapter.Events.Connected += OnConnected;
            _adapter.Events.Closed += OnClosed;
            _adapter.Events.Received += OnReceived;
            _adapter.Events.ReceivedError += OnReceivedError;
        }

        public async void CloseAsync()
        {
            try
            {
                await _adapter.CloseAsync();
            }
            catch (Exception e)
            {
                _logger.Warn("CloseAsync Exception: {0}", e);
            }
        }

        public async void ConnectAsync(int connectTimeoutSec = DefaultConnectTimeout)
        {
            try
            {
                await _adapter.ConnectAsync(_uri, connectTimeoutSec);
            }
            catch (Exception e)
            {
                _logger.Warn("ConnectAsync Exception: {0}", e);
            }

            if (!IsConnected)
            {
                _events.OnClosed();
            }
        }

        async void IChannel.SendAsync(IPacket proto, Action<IPacket> callback)
        {
            try
            {
                var ret = await SendAsync(proto);
                if (ret == null)
                {
                    return;
                }

                callback(ret);
            }
            catch (Exception e)
            {
                _logger.Warn("SendAsync Exception: {0}", e);
            }
        }

        private void OnConnected()
        {
            _events.OnConnected();
        }

        private void OnClosed()
        {
            lock (_responsesLock)
            {
                foreach (var response in _responses)
                {
                    response.Value.TrySetCanceled();
                }

                _responses.Clear();
            }

            _events.OnClosed();
        }

        private void OnReceived(IPacket proto)
        {
            try
            {
                if (string.IsNullOrEmpty(proto.ID))
                {
                    _events.OnReceived(proto);
                    return;
                }

                lock (_responsesLock)
                {
                    // Handle message response.
                    if (_responses.ContainsKey(proto.ID))
                    {
                        var completer = _responses[proto.ID];
                        _responses.Remove(proto.ID);

                        completer.SetResult(proto);
                    }
                    else
                    {
                        _events.OnReceived(proto);
                    }
                }
            }
            catch (Exception e)
            {
                _events.OnReceivedError(e);
            }
        }

        private void OnReceivedError(Exception e)
        {
            if (!_adapter.IsConnected)
            {
                lock (_responsesLock)
                {
                    foreach (var response in _responses)
                    {
                        response.Value.TrySetCanceled();
                    }

                    _responses.Clear();
                }
            }

            _events.OnReceivedError(e);
        }

        public async Task<IPacket> SendAsync(IPacket proto)
        {
            var buffer = proto.Data();
            var cts = new CancellationTokenSource(_sendTimeoutSec);
            if (string.IsNullOrEmpty(proto.ID))
            {
                await _adapter.SendAsync(new ArraySegment<byte>(buffer), cts.Token);
                return null; // No response required.
            }

            var completer = new TaskCompletionSource<IPacket>();
            lock (_responsesLock)
            {
                _responses[proto.ID] = completer;
            }

            cts.Token.Register(() =>
            {
                lock (_responsesLock)
                {
                    if (_responses.ContainsKey(proto.ID))
                    {
                        _responses.Remove(proto.ID);
                    }
                }

                // completer.TrySetResult();
            });

            await _adapter.SendAsync(new ArraySegment<byte>(buffer), cts.Token);
            return await completer.Task;
        }

        public void Dispose()
        {
            lock (_responsesLock)
            {
                _responses.Clear();
            }
        }
    }
}