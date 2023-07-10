using System.Linq;
using RockIM.Api.Client.V1.Protocol.Socket;
using RockIM.Api.Client.V1.Types;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Internal.V1.Domain.Events;
using RockIM.Sdk.Internal.V1.Infra.Converter;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public class MessagePushProcessor : IPushProcessor
    {
        private readonly IEventBus _eventBus;

        public MessagePushProcessor(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Process(PushPacketHeader header, PushPacketBody body)
        {
            var message = Codec.Decode<IMMessagePacket>(body.Body.ToByteArray());
            _eventBus.Message.OnReceived(MessageConvert.MessageList(message.List.ToList()));
        }
    }
}