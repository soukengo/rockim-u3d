using System;
using RockIM.Demo.Scripts.Framework;
using RockIM.Demo.Scripts.Logic.Events;
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
        static ImManager()
        {
            ImSdkV1.EventApis.Message.Received += (list) => { ChatEventManager.Instance.MessageReceived.Invoke(list); };
        }

        public void Init(Config config, Action<APIResult<InitResp>> callback)
        {
            ImSdkUnity.Async(() => ImSdkV1.Init(config), callback);
        }

        public void Login(LoginReq req, Action<APIResult<LoginResp>> callback)
        {
            ImSdkUnity.Async(() => ImSdkV1.Apis.Auth.Login(req), callback);
        }

        public void Destroy()
        {
            ImSdkV1.Dispose();
        }
    }
}