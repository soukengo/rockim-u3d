using System;
using RockIM.Demo.Scripts.Framework;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Unity;

namespace RockIM.Demo.Scripts.Logic
{
    public class ImManager : Singleton<ImManager>
    {
        public void Init(Config config, Action<APIResult<InitResp>> callback)
        {
            ImSdkUnity.Async(() => ImSdk.V1.Init(config), callback);
        }

        public void Login(LoginReq req, Action<APIResult<LoginResp>> callback)
        {
            ImSdkUnity.Async(() => ImSdk.V1.Apis.Auth.Login(req), callback);
            ImSdk.V1.EventBus.LifeCycle.Connecting += () => { };
        }
    }
}