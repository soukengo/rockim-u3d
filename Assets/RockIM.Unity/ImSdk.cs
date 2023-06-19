using System;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Framework;
using RockIM.Unity.Framework;

namespace RockIM.Unity
{
    public abstract class ImSdkUnity : ImSdk
    {
        static ImSdkUnity()
        {
            AsyncManager.Handler = UnityAsyncHandler.Instance;
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