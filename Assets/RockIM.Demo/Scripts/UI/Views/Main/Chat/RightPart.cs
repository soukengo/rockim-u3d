using System.Collections.Generic;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Events;
using RockIM.Sdk.Api.V1.Entities;
using UnityEngine;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class RightPart : CComponent
    {
        public GameObject messageBoxPrefab;

        private readonly Dictionary<string, GameObject> _messageBoxMap = new Dictionary<string, GameObject>();

        private readonly Dictionary<ConversationID, MessageBox> _conversationMap =
            new Dictionary<ConversationID, MessageBox>();

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
                var obj = Instantiate(messageBoxPrefab, transform, false);
                var messageBox = obj.GetComponent<MessageBox>();
                var conversationID = new GroupConversationID(item.key, item.bizId);
                messageBox.SetConversationID(conversationID);
                _messageBoxMap[item.key] = obj;
                _conversationMap[conversationID] = messageBox;
                obj.SetActive(i == 0);
            }
        }

        private void OnChatMenuSelected(string key)
        {
            foreach (var item in _messageBoxMap)
            {
                item.Value.SetActive(item.Key.Equals(key));
            }
        }

        private void OnSendSuccess(Message message)
        {
            var exists = _conversationMap.TryGetValue(message.ConversationID, out var messageBox);
            if (!exists)
            {
                return;
            }

            messageBox.OnSendResult(message);
        }
    }
}