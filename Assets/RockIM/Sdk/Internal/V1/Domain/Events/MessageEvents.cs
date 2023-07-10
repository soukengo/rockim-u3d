using RockIM.Sdk.Framework;

namespace RockIM.Sdk.Internal.V1.Domain.Events
{
    public class MessageEvents : Api.V1.Events.MessageEvents
    {
        protected override IAsyncExecutor Executor { get; }

        public MessageEvents(IAsyncExecutor executor)
        {
            Executor = executor;
        }
    }
}