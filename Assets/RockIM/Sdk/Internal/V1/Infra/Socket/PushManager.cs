using System.Collections.Generic;
using RockIM.Api.Client.V1.Protocol.Socket;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Domain.Events;
using RockIM.Shared.Enums;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public class PushManager
    {
        private readonly SdkContext _context;

        private readonly IEventBus _eventBus;

        private readonly Dictionary<Comet.Types.PushOperation, IPushProcessor> _processors =
            new Dictionary<Comet.Types.PushOperation, IPushProcessor>();

        public PushManager(SdkContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
            _processors[Comet.Types.PushOperation.Messages] = new MessagePushProcessor(_eventBus);
        }

        public void OnReceived(Packet packet)
        {
            var header = Codec.Decode<PushPacketHeader>(packet.Header);
            var body = Codec.Decode<PushPacketBody>(packet.Body);
            var exists = _processors.TryGetValue(header.Operation, out var processor);
            if (!exists)
            {
                LoggerContext.Logger.Warn("Push Operation[{0}] un", header.Operation);
                return;
            }

            processor.Process(header, body);
        }
    }
}