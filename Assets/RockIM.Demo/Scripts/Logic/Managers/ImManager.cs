using System;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Framework;
using RockIM.Unity.Framework;

namespace RockIM.Demo.Scripts.Logic.Managers
{
    public class ImManager
    {
        public static readonly ImManager Instance = new ImManager();

        private ImManager()
        {
            AsyncManager.Handler = UnityAsyncHandler.Instance;
        }

        public void Init(Config config, Action<APIResult<InitResp>> callback)
        {
            ImSdk.Async(() => ImSdk.V1.Init(config), callback);
        }

        public void Login(LoginReq req, Action<APIResult<LoginResp>> callback)
        {
            ImSdk.Async(() => ImSdk.V1.Apis.Auth.Login(req), callback);
        }
    }
}