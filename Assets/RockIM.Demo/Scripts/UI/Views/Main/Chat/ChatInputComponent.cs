using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Events;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class ChatInputComponent : CComponent
    {
        public Button sendButton;


        private void OnEnable()
        {
            ChatUIEventManager.Instance.OnChatMenuSelected += OnChatMenuSelected;
        }

        private void OnDisable()
        {
            ChatUIEventManager.Instance.OnChatMenuSelected -= OnChatMenuSelected;
        }

        private void Start()
        {
            sendButton.onClick.AddListener(() =>
            {
                var req = new MessageSendReq(new GroupConversationID("", ""));
                ImSdk.Async(() => ImSdk.V1.Apis.Authorized.Message.Send(req), (result) => { });
            });
        }

        private static void OnChatMenuSelected(string key)
        {
            Debug.Log("OnChatMenuSelected: ChatInput");
        }
    }
}