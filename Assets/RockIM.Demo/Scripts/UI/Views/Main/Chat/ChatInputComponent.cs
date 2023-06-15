using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class ChatInputComponent : MonoBehaviour
    {
        public Button sendButton;

        private void Start()
        {
            sendButton.onClick.AddListener(OnClickSendButton);
        }

        private static void OnClickSendButton()
        {
        }
    }
}