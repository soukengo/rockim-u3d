using System.Collections.Generic;
using RockIM.Demo.Scripts.Logic;
using RockIM.Demo.Scripts.Logic.Models.Chat;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Views.Main.Chat;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main
{
    public class ChatPanel : CPanel
    {
        public List<ChatMenuItem> items = ChatContext.Instance.Items;

        public LeftPart leftPart;
        public RightPart rightPart;

        public Button collapseBtn;

        protected override void Init()
        {
            ChatContext.Instance.Reset();
            if (items.Count > 0)
            {
                ChatContext.Instance.CurrentMenuKey = items[0].key;
                ChatContext.Instance.Items = items;
            }

            collapseBtn.onClick.AddListener(() => { gameObject.SetActive(false); });
        }
        
    }
}