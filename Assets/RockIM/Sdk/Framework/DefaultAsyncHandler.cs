using System;

namespace RockIM.Sdk.Framework
{
    public class DefaultAsyncHandler : IAsyncHandler
    {
        public void Callback(Action action)
        {
            action();
        }
    }
}