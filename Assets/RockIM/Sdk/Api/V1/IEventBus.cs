using RockIM.Sdk.Api.V1.Events;

namespace RockIM.Sdk.Api.V1
{
    public interface IEventBus
    {
        public LifeCycleEvents LifeCycle { get; }

        public MessageEvents Message { get; }
    }
}