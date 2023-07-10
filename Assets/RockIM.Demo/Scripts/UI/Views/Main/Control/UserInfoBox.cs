using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Widgets;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main.Control
{
    public class UserInfoBox : CComponent
    {
        public Text nameText;

        public Image avatarImage;

        public Text statusText;

        public Image statusImage;

        protected override void Init()
        {
            ImSdkV1.EventApis.LifeCycle.ConnectionChange += OnStatusChange;
            ImSdkV1.EventApis.LifeCycle.LoginSuccess += SetUserInfo;
        }

        private void OnDisable()
        {
            ImSdkV1.EventApis.LifeCycle.ConnectionChange -= OnStatusChange;
            ImSdkV1.EventApis.LifeCycle.LoginSuccess -= SetUserInfo;
        }

        private void Start()
        {
            SetUserInfo(ImSdkV1.Apis.Authorized.Current);
            SetStatus(ImSdkV1.Apis.Authorized.ConnectionStatus);
        }

        private void OnStatusChange(ConnectionStatus status)
        {
            Debug.Log(status.GetDisplayName());
            ToastManager.ShowToast(status.GetDisplayName());
            SetStatus(status);
        }

        private void SetUserInfo(User user)
        {
            if (IsDestroyed)
            {
                return;
            }

            if (user == null)
            {
                return;
            }

            nameText.text = user.Name;
        }

        private void SetStatus(ConnectionStatus? status)
        {
            if (IsDestroyed)
            {
                return;
            }

            if (status == null)
            {
                status = ConnectionStatus.Disconnected;
            }

            statusText.text = status.Value.GetDisplayName();

            statusImage.color = status switch
            {
                ConnectionStatus.Connecting => Color.gray,
                ConnectionStatus.Connected => Color.green,
                _ => Color.red
            };
        }
    }
}