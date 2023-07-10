using System;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public interface ISocketManager : IDisposable
    {
        bool IsConnected { get; }

        bool IsConnecting { get; }

        public void Connect(Domain.Entities.Socket config);
    }
}