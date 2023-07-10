using System;

namespace RockIM.Sdk.Framework
{
    public class DefaultAsyncExecutor : IAsyncExecutor
    {
        public void Execute(Action action)
        {
            action();
        }
    }
}