using System;
using System.Threading.Tasks;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Framework;
using RockIM.Sdk.Internal.V1;

namespace RockIM.Sdk
{
    public abstract class ImSdk
    {
        private static readonly Lazy<IClient> V1Lazy = new(new ClientV1());
        public static IClient V1 => V1Lazy.Value;

        public static void Async<T>(Func<T> syncMethod, Action<T> callback)
        {
            Task.Run(() =>
            {
                var ret = syncMethod();
                AsyncManager.Callback(() => callback(ret));
            });
        }
    }
}