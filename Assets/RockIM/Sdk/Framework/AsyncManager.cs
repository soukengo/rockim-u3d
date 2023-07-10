using System;

namespace RockIM.Sdk.Framework
{
    public abstract class AsyncManager
    {
        public static volatile IAsyncExecutor Executor = new DefaultAsyncExecutor();

        public static void Execute(Action action)
        {
            Executor?.Execute(action);
        }
    }
}