using System.Collections.Generic;
using RockIM.Demo.Scripts.Logic.Events;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Events;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class RightPart : CComponent
    {
        public MessageBox messageBox;

        private readonly Dictionary<string, TargetID> _conversationIds = new Dictionary<string, TargetID>();

        private void OnEnable()
        {
            ChatUIEventManager.Instance.ChatMenuSelected += OnChatMenuSelected;
            ChatEventManager.Instance.MessageReceived += OnReceived;
        }

        private void OnDisable()
        {
            ChatUIEventManager.Instance.ChatMenuSelected -= OnChatMenuSelected;
            ChatEventManager.Instance.MessageReceived -= OnReceived;
        }

        protected override void Init()
        {
            var items = ChatContext.Instance.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var conversationID = new TargetID(TargetCategory.Group, item.key + "_" + item.bizId);
                if (i == 0)
                {
                    ChatContext.Instance.CurrentTargetID = conversationID;
                }

                ChatContext.Instance.GetOrCreateConversation(conversationID);
                _conversationIds[item.key] = conversationID;
            }
        }

        private void OnChatMenuSelected(string key)
        {
            var exists = _conversationIds.TryGetValue(key, out var conversationID);
            if (!exists)
            {
                return;
            }

            ChatContext.Instance.CurrentTargetID = conversationID;

            messageBox.SwitchConversation(conversationID);
        }

        private void OnSendSuccess(Message message)
        {
            messageBox.OnSendResult(message);
        }

        private void OnReceived(List<Message> list)
        {
            messageBox.OnReceived(list);
        }
    }
}