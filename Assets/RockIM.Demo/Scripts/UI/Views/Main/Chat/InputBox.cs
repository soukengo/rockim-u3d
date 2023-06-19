using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Events;
using RockIM.Demo.Scripts.UI.Widgets;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Unity;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class InputBox : CComponent
    {
        public InputField input;

        public Button sendButton;

        public ConversationID ConversationID;


        private void Start()
        {
            sendButton.onClick.AddListener(() =>
            {
                var content = input.text;
                if (content.Trim().Length == 0)
                {
                    return;
                }

                var req = new MessageSendReq(ConversationID, new TextMessageContent(content));
                input.text = "";
                ImSdkUnity.Async(() => ImSdk.V1.Apis.Authorized.Message.Send(req), (result) =>
                {
                    if (!result.IsSuccess())
                    {
                        ToastManager.ShowToast(result.Message, true);
                        return;
                    }

                    ChatUIEventManager.Instance.OnSendResult.Invoke(result.Data.Message);
                });
            });
        }
    }
}