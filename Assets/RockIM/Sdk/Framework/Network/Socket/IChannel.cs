using System;

namespace RockIM.Sdk.Framework.Network.Socket
{
    public interface IChannel
    {
        bool IsConnected { get; }

        bool IsConnecting { get; }

        void CloseAsync();

        void ConnectAsync(int connectTimeout = 3);

        void SendAsync(IPacket proto, Action<IPacket> callback);
    }
}