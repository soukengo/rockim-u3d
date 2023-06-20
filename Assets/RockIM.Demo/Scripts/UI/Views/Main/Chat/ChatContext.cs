using System.Collections.Generic;
using RockIM.Demo.Scripts.Framework;
using RockIM.Demo.Scripts.Logic.Models.Chat;
using RockIM.Sdk.Api.V1.Entities;
using Conversation = RockIM.Demo.Scripts.Logic.Models.Chat.Conversation;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class ChatContext : Singleton<ChatContext>
    {
        public string CurrentMenuKey;

        public List<ChatMenuItem> Items = new List<ChatMenuItem>();

        public ConversationID CurrentConversationID;

        private readonly Dictionary<ConversationID, Conversation> _conversations =
            new Dictionary<ConversationID, Conversation>();

        public Conversation CurrentConversation => GetConversation(CurrentConversationID);

        public Conversation GetConversation(ConversationID conversationID)
        {
            lock (_conversations)
            {
                if (conversationID == null)
                {
                    return null;
                }

                var exists = _conversations.TryGetValue(conversationID, out var value);
                return !exists ? null : value;
            }
        }

        public Conversation GetOrCreateConversation(ConversationID conversationID)
        {
            lock (_conversations)
            {
                var conversation = GetConversation(conversationID) ?? new Conversation(conversationID);
                _conversations[conversationID] = conversation;
                return conversation;
            }
        }
    }
}