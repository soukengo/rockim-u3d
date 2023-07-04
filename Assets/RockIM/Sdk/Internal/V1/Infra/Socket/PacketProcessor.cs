using RockIM.Shared.Enums;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public interface IPacketProcessor
    {
        public void Process(Network.Types.PacketType packetType, Packet packet);
    }
}