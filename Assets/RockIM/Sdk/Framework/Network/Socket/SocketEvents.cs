using System;

namespace RockIM.Sdk.Framework.Network.Socket
{
    public sealed class SocketEvents
    {
        public event Action Connected = () => { };

        public event Action Closed = () => { };

        public event Action<IPacket> Received = (p) => { };

        public event Action<Exception> ReceivedError = (e) => { };

        public void OnConnected()
        {
            Connected?.Invoke();
        }

        public void OnClosed()
        {
            Closed?.Invoke();
        }

        public void OnReceived(IPacket obj)
        {
            Received?.Invoke(obj);
        }

        public void OnReceivedError(Exception obj)
        {
            ReceivedError?.Invoke(obj);
        }
    }
}