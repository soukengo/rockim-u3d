using RockIM.Demo.Scripts.Logic.Models.Chat;
using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat.Widgets
{
    public class MessageSide : MonoBehaviour
    {
        public Image avatarImage;

        public Text nameText;

        public Text contentText;

        public int minWidth = 200;

        public int maxWidth = 800;


        public float CalculateHeight(DemoMessage message)
        {
            SetText(contentText, message.Message.Content.Content);
            return contentText.preferredHeight + 100;
        }

        public void SetMessage(DemoMessage message)
        {
            nameText.text = message.Message.Sender.Name;
            SetContent(message.Message.Content.Content);
        }
        
        public void SetContent(string content)
        {
            SetText(contentText, content);
        }

        private void SetText(Text target, string contentStr)
        {
            if (target is null)
            {
                return;
            }

            target.text = contentStr;

            //宽高都不缩放
            var textSizeHeight = Mathf.CeilToInt(target.preferredHeight);
            if (target.preferredWidth <= minWidth)
            {
                target.rectTransform.sizeDelta = new Vector2(minWidth, textSizeHeight);
                return;
            }

            //宽度缩放，高度不变
            if (target.preferredWidth <= maxWidth)
            {
                target.rectTransform.sizeDelta =
                    new Vector2(target.preferredWidth, target.rectTransform.sizeDelta.y);
                return;
            }

            //宽度最大，高度缩放
            //设置最优高度
            target.rectTransform.sizeDelta = new Vector2(maxWidth, textSizeHeight);
        }
    }
}