using System;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Framework;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Unity.Framework;

namespace RockIM.Unity
{
    public abstract class ImSdkUnity : ImSdk
    {
        static ImSdkUnity()
        {
            AsyncManager.Executor = UnityAsyncExecutor.Instance;
            LoggerContext.Logger = new UnityLogger();
        }

        public new static APIResult<T> Call<T>(Func<APIResult<T>> syncMethod)
        {
            return ImSdk.Call(syncMethod);
        }

        public new static void Async<T>(Func<APIResult<T>> syncMethod, Action<APIResult<T>> callback)
        {
            ImSdk.Async(syncMethod, callback);
        }
    }
}