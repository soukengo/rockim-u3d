using System.Collections.Generic;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Events;
using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class RightPart : CComponent
    {
        public MessageBox messageBox;

        private readonly Dictionary<string, ConversationID> _conversationIds = new Dictionary<string, ConversationID>();

        private void OnEnable()
        {
            ChatUIEventManager.Instance.OnChatMenuSelected += OnChatMenuSelected;
            ChatUIEventManager.Instance.OnSendResult += OnSendSuccess;
        }

        private void OnDisable()
        {
            ChatUIEventManager.Instance.OnChatMenuSelected -= OnChatMenuSelected;
            ChatUIEventManager.Instance.OnSendResult += OnSendSuccess;
        }

        void Start()
        {
            var items = ChatContext.Instance.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var conversationID = new GroupConversationID(item.key, item.bizId);
                if (i == 0)
                {
                    ChatContext.Instance.CurrentConversationID = conversationID;
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

            ChatContext.Instance.CurrentConversationID = conversationID;

            messageBox.SwitchConversation(conversationID);
        }

        private void OnSendSuccess(Message message)
        {
            messageBox.OnSendResult(message);
        }
    }
}