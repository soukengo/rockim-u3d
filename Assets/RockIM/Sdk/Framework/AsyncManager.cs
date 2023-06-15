using System;

namespace RockIM.Sdk.Framework
{
    public abstract class AsyncManager
    {
        public static volatile IAsyncHandler Handler = new DefaultAsyncHandler();

        public static void Callback(Action action)
        {
            Handler?.Callback(action);
        }
    }
}