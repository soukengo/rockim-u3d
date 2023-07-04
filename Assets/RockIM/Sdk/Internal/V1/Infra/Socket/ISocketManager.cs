using System;
using RockIM.Sdk.Framework.Network.Socket;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public interface ISocketManager : IDisposable
    {
        public IChannel Connect(Domain.Entities.Socket config);
    }
}