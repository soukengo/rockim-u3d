using RockIM.Demo.Scripts.Logic.Models.Chat;
using UnityEngine;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat.Widgets
{
    public class MessageItem : MonoBehaviour
    {
        public MessageSide left;

        public MessageSide right;


        public float CalculateHeight(DemoMessage message)
        {
            return message.IsSelf ? right.CalculateHeight(message) : left.CalculateHeight(message);
        }

        public void SetMessage(DemoMessage message)
        {
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            if (message.IsSelf)
            {
                right.gameObject.SetActive(true);
                right.SetMessage(message);
                return;
            }
            left.gameObject.SetActive(true);
            left.SetMessage(message);
        }
    }
}