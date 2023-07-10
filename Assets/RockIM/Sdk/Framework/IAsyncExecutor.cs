using System;

namespace RockIM.Sdk.Framework
{
    public interface IAsyncExecutor
    {
        public void Execute(Action action);
    }
}