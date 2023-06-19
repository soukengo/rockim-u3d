using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat.Widgets
{
    public class MessageItem : MonoBehaviour
    {
        public Image avatarImage;

        public Text nameText;

        public Text contentText;

        public int minWidth = 200;
        public int maxWidth = 800;

        private void Start()
        {
            
        }

        public float SetContent(string content)
        {
            SetTextSize(contentText, content);
            return contentText.preferredHeight + 100;
        }

        private void SetTextSize(Text targetText, string contentStr)
        {
            if (targetText is null)
            {
                return;
            }

            targetText.text = contentStr;

            //宽高都不缩放
            var textSizeHeight = Mathf.CeilToInt(targetText.preferredHeight);
            if (targetText.preferredWidth <= minWidth)
            {
                targetText.rectTransform.sizeDelta = new Vector2(minWidth, textSizeHeight);
                return;
            }

            //宽度缩放，高度不变
            if (targetText.preferredWidth <= maxWidth)
            {
                targetText.rectTransform.sizeDelta =
                    new Vector2(targetText.preferredWidth, targetText.rectTransform.sizeDelta.y);
                return;
            }

            //宽度最大，高度缩放
            //设置最大宽度
            targetText.rectTransform.sizeDelta = new Vector2(minWidth, targetText.rectTransform.sizeDelta.y);
            //设置最优高度
            targetText.rectTransform.sizeDelta = new Vector2(maxWidth, textSizeHeight);
        }
    }
}