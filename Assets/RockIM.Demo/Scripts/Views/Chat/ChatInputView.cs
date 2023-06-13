using RockIM.Sdk;
using RockIM.Sdk.Api.V1;
using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.Views.Chat
{
    public class ChatInputView : MonoBehaviour
    {
        public Button sendButton;

        private void Start()
        {
            sendButton.onClick.AddListener(OnClickSendButton);
        }

        private static void OnClickSendButton()
        {
            ImSdk.Async(() => ImSdk.V1.Init(new Config("http://localhost:6002", "1000003",
                "gMY7qMyJRUEdxWdEGpKxV3uTV0BXcgyE")), (resp) => { Debug.Log("ret: " + resp); });
        }
    }
}