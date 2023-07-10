using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Events;

namespace RockIM.Sdk.Internal.V1
{
    public class EventApis : IEventApis
    {
        public LifeCycleEvents LifeCycle { get; }

        public MessageEvents Message { get; }

        public EventApis()
        {
            LifeCycle = new LifeCycleEvents();
            Message = new MessageEvents();
        }
    }
}