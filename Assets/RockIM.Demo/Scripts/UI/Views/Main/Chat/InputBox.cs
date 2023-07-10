using RockIM.Demo.Scripts.UI.Base;
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


        private void Start()
        {
            sendButton.onClick.AddListener(() =>
            {
                var content = input.text;
                if (content.Trim().Length == 0)
                {
                    return;
                }

                var conversationId = ChatContext.Instance.CurrentTargetID;

                var req = new MessageSendReq(conversationId, new TextMessageContent(content));
                input.text = "";
                ImSdkUnity.Async(() => ImSdkV1.Apis.Authorized.Message.Send(req), (result) =>
                {
                    if (!result.IsSuccess())
                    {
                        ToastManager.ShowToast(result.Message, true);
                        return;
                    }
                
                    // ChatUIEventManager.Instance.SendResult.Invoke(result.Data.Message);
                });
            });
        }
    }
}