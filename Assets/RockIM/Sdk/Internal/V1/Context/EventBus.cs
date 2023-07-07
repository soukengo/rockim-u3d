using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Events;

namespace RockIM.Sdk.Internal.V1.Context
{
    public class EventBus : IEventBus
    {
        public LifeCycleEvents LifeCycle { get; }

        public MessageEvents Message { get; }

        public EventBus()
        {
            LifeCycle = new LifeCycleEvents();
            Message = new MessageEvents();
        }
    }
}