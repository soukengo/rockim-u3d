using System;
using RockIM.Sdk.Framework;

namespace RockIM.Sdk.Internal.V1.Domain.Events
{
    public class LifeCycleEvents : RockIM.Sdk.Api.V1.Events.LifeCycleEvents
    {
        public event Action AuthExpired;

        protected override IAsyncExecutor Executor { get; }

        public LifeCycleEvents(IAsyncExecutor executor)
        {
            Executor = executor;
        }

        public void OnAuthExpired()
        {
            Executor.Execute(() => AuthExpired?.Invoke());
        }
    }
}