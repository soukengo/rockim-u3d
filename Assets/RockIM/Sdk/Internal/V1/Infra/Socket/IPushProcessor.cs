using RockIM.Api.Client.V1.Protocol.Socket;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public interface IPushProcessor
    {
        public void Process(PushPacketHeader header, PushPacketBody body);
    }
}