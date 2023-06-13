using System;
using System.Threading.Tasks;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Internal.V1;

namespace RockIM.Sdk
{
    public abstract class ImSdk
    {
        private static readonly Lazy<IClient> _v1 = new(new ClientV1());
        public static IClient V1 => _v1.Value;


        private ImSdk()
        {
        }

        public static void Async<T>(Func<T> syncMethod, Action<T> callback)
        {
            Task.Run(() => { callback(syncMethod()); });
        }
    }
}