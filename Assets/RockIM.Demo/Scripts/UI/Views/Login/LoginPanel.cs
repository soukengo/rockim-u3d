using System;
using RockIM.Demo.Scripts.Logic.Managers;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Events;
using RockIM.Demo.Scripts.UI.Widgets;
using RockIM.Sdk.Api.V1.Dtos.Request;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Login
{
    public class LoginPanel : CPanel
    {
        public InputField authCodeInput;

        public Button loginButton;

        protected override void Init()
        {
            loginButton.onClick.AddListener(() =>
            {
                var authCode = authCodeInput.text;
                ImManager.Instance.Login(new LoginReq {AuthCode = authCode}, (result) =>
                {
                    if (!result.IsSuccess())
                    {
                        ToastManager.ShowToast("登录失败", true);
                        return;
                    }

                    ToastManager.ShowToast("登录成功");

                    LoginUIEventManager.Instance.OnLoginSuccess.Invoke();
                });
            });
        }
    }
}