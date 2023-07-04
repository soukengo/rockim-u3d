using System;
using System.Threading;
using System.Threading.Tasks;

namespace RockIM.Sdk.Framework.Network.Socket
{
    public interface ISocketAdapter
    {
        SocketEvents Events { get; }

        bool IsConnected { get; }

        bool IsConnecting { get; }

        Task CloseAsync();

        Task ConnectAsync(Uri uri, int timeout);

        Task SendAsync(ArraySegment<byte> buffer, CancellationToken canceller = default(CancellationToken));
    }
}