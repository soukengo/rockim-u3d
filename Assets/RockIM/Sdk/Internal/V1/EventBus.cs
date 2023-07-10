using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Framework;
using RockIM.Sdk.Internal.V1.Domain.Events;

namespace RockIM.Sdk.Internal.V1
{
    public class EventBus : IEventBus
    {
        public LifeCycleEvents LifeCycle { get; }

        public MessageEvents Message { get; }

        private readonly IEventApis _eventApis;

        public EventBus(IEventApis eventApis)
        {
            var executor = new DefaultAsyncExecutor();
            _eventApis = eventApis;
            LifeCycle = new LifeCycleEvents(executor);
            Message = new MessageEvents(executor);
            Bind();
        }

        private void Bind()
        {
            LifeCycle.LoginSuccess += _eventApis.LifeCycle.OnLoginSuccess;
            LifeCycle.Logout += _eventApis.LifeCycle.OnLogout;
            LifeCycle.ConnectionChange += _eventApis.LifeCycle.OnConnectionChange;

            Message.Received += _eventApis.Message.OnReceived;
        }
    }
}