using System;

namespace RockIM.Sdk.Framework
{
    public interface IAsyncHandler
    {
        public void Callback(Action action);
    }
}