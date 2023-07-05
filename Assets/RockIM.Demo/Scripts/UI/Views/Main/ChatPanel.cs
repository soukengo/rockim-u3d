using System;
using System.Collections.Generic;
using RockIM.Demo.Scripts.Logic;
using RockIM.Demo.Scripts.Logic.Models.Chat;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Views.Main.Chat;
using RockIM.Sdk;
using RockIM.Unity;

namespace RockIM.Demo.Scripts.UI.Views.Main
{
    public class ChatPanel : CPanel
    {
        public List<ChatMenuItem> items = ChatContext.Instance.Items;

        public LeftPart leftPart;
        public RightPart rightPart;


        protected override void Init()
        {
            if (items.Count > 0)
            {
                ChatContext.Instance.CurrentMenuKey = items[0].key;
                ChatContext.Instance.Items = items;
            }
        }

        private void OnDestroy()
        {
           ImManager.Instance.Destroy();
        }
    }
}