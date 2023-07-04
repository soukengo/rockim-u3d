using System;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Internal.V1;

namespace RockIM.Sdk
{
    public abstract class ImSdkV1 : ImSdk
    {
        private static readonly Lazy<IClient> V1Lazy = new(new ClientV1());
        private static IClient Client => V1Lazy.Value;

        public static IApis Apis => Client.Apis;

        public static IEventBus EventBus => Client.EventBus;

        public static APIResult<InitResp> Init(Config config)
        {
            return Client.Init(config);
        }
    }
}