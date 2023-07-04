using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace RockIM.Sdk.Framework.Network.Socket
{
    /// <summary>
    /// An adapter which uses the WebSocket protocol with RockIM server.
    /// </summary>
    public class WebSocketStdlibAdapter : ISocketAdapter
    {
        private const int KeepAliveIntervalSec = 15;
        private const int MaxMessageReadSize = 1024 * 256;
        private const int SendTimeoutSec = 10;

        public SocketEvents Events { get; }

        /// <summary>
        /// If the WebSocket is connected.
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// If the WebSocket is connecting.
        /// </summary>
        public bool IsConnecting { get; private set; }

        private CancellationTokenSource _cancellationSource;
        private Uri _uri;
        private ClientWebSocket _webSocket;
        private readonly int _maxMessageReadSize;
        private readonly TimeSpan _sendTimeoutSec;

        private readonly IPacketParser<IPacket> _packetParser;

        public WebSocketStdlibAdapter(IPacketParser<IPacket> packetParser, int sendTimeoutSec = SendTimeoutSec,
            int maxMessageReadSize = MaxMessageReadSize)
        {
            _packetParser = packetParser;
            _maxMessageReadSize = maxMessageReadSize;
            _sendTimeoutSec = TimeSpan.FromSeconds(sendTimeoutSec);
            _webSocket = new ClientWebSocket();
            Events = new SocketEvents();
        }

        public WebSocketStdlibAdapter(IPacketParser<IPacket> packetParser, ClientWebSocket webSocket)
        {
            _packetParser = packetParser;
            // There is no way to override options so allow constructor to take a websocket that already has options.
            _webSocket = webSocket;
            Events = new SocketEvents();
        }

        /// <inheritdoc cref="ISocketAdapter.CloseAsync"/>
        public async Task CloseAsync()
        {
            if (_webSocket == null) return;

            if (_webSocket.State == WebSocketState.Open)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            }
            else if (_webSocket.State == WebSocketState.Connecting)
            {
                // cancel mid-connect
                _cancellationSource?.Cancel();
            }

            _webSocket = null;
            IsConnecting = false;
            IsConnected = false;
        }

        /// <inheritdoc cref="ISocketAdapter.ConnectAsync"/>
        public async Task ConnectAsync(Uri uri, int timeout)
        {
            if (_webSocket != null && _webSocket.State == WebSocketState.Open)
            {
                // Already connected so we can return.
                return;
            }

            _cancellationSource = new CancellationTokenSource();
            _uri = uri;
            _webSocket = new ClientWebSocket();
            IsConnecting = true;

            try
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
                var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cancellationSource.Token, cts.Token);
                await _webSocket.ConnectAsync(_uri, linkedCts.Token).ConfigureAwait(false);
                _ = ReceiveLoop(_webSocket, _cancellationSource.Token);
                Events.OnConnected();
                IsConnected = true;
            }
            catch (Exception)
            {
                IsConnected = false;
                throw;
            }
            finally
            {
                IsConnecting = false;
            }
        }

        /// <inheritdoc cref="ISocketAdapter.SendAsync"/>
        public Task SendAsync(ArraySegment<byte> buffer, CancellationToken canceller = default)
        {
            if (_webSocket == null || _webSocket.State != WebSocketState.Open)
            {
                throw new SocketException((int) SocketError.NotConnected);
            }

            canceller.ThrowIfCancellationRequested();

            try
            {
                var cts = new CancellationTokenSource(_sendTimeoutSec);
                var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(canceller, cts.Token);
                var t = _webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, linkedCts.Token);
                t.ConfigureAwait(false);
                return t;
            }
            catch
            {
                _ = CloseAsync();
                throw;
            }
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString() => $"WebSocketDriver(MaxMessageSize={_maxMessageReadSize}, Uri='{_uri}')";

        private async Task ReceiveLoop(WebSocket webSocket, CancellationToken canceller)
        {
            canceller.ThrowIfCancellationRequested();

            var buffer = new byte[_maxMessageReadSize];
            var bufferReadCount = 0;

            try
            {
                do
                {
                    var bufferSegment =
                        new ArraySegment<byte>(buffer, bufferReadCount, _maxMessageReadSize - bufferReadCount);
                    var result = await webSocket.ReceiveAsync(bufferSegment, canceller).ConfigureAwait(false);
                    if (result == null)
                    {
                        break;
                    }

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        break;
                    }

                    bufferReadCount += result.Count;
                    if (!result.EndOfMessage) continue;

                    try
                    {
                        Events.OnReceived(_packetParser.Parse(bufferSegment.Array));
                    }
                    catch (Exception e)
                    {
                        Events.OnReceivedError(e);
                    }

                    bufferReadCount = 0;
                } while (!canceller.IsCancellationRequested && _webSocket != null &&
                         _webSocket.State == WebSocketState.Open);
            }
            catch (Exception e)
            {
                Events.OnReceivedError(e);
            }
            finally
            {
                IsConnecting = false;
                IsConnected = false;
                Events.OnClosed();
            }
        }
    }
}