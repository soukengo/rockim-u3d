using RockIM.Sdk.Framework;

namespace RockIM.Sdk.Api.V1.Events
{
    public abstract class EventsHolder
    {
        protected virtual IAsyncExecutor Executor => AsyncManager.Executor;
    }
}