using RockIM.Sdk.Framework.Network.Socket;
using RockIM.Sdk.Internal.V1.Domain.Entities;

namespace RockIM.Sdk.Internal.V1.Domain.Repository
{
    public interface ISocketRepository
    {
        void Connect(Socket config, SocketEvents events);
    }
}