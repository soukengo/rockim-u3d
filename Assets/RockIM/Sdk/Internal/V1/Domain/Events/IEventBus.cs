namespace RockIM.Sdk.Internal.V1.Domain.Events
{
    public interface IEventBus
    {
        public LifeCycleEvents LifeCycle { get; }

        public MessageEvents Message { get; }
    }
}