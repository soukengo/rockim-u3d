using System;
using System.Threading;
using RockIM.Demo.Scripts.Framework;
using RockIM.Demo.Scripts.UI.Widgets;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Unity;
using UnityEngine;

namespace RockIM.Demo.Scripts.Logic
{
    public class ImManager : Singleton<ImManager>
    {
        static ImManager()
        {
            ImSdkV1.EventBus.LifeCycle.Connecting += () =>
            {
                Debug.Log("连接中");
                ToastManager.ShowToast("连接中");
            };
            ImSdkV1.EventBus.LifeCycle.Connected += () =>
            {
                Debug.Log("连接成功");
                ToastManager.ShowToast("连接成功");
            };
            ImSdkV1.EventBus.LifeCycle.DisConnected += () =>
            {
                Debug.Log("已断开连接");    
                ToastManager.ShowToast("已断开连接");
            };
        }

        public void Init(Config config, Action<APIResult<InitResp>> callback)
        {
            ImSdkUnity.Async(() => ImSdkV1.Init(config), callback);
        }

        public void Login(LoginReq req, Action<APIResult<LoginResp>> callback)
        {
            ImSdkUnity.Async(() => ImSdkV1.Apis.Auth.Login(req), callback);
        }
    }
}