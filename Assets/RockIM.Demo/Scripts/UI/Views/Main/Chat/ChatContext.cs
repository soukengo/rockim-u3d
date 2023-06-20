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

        public TargetID CurrentTargetID;

        private readonly Dictionary<TargetID, Conversation> _conversations =
            new Dictionary<TargetID, Conversation>();

        public Conversation CurrentConversation => GetConversation(CurrentTargetID);

        public Conversation GetConversation(TargetID targetID)
        {
            lock (_conversations)
            {
                if (targetID == null)
                {
                    return null;
                }

                var exists = _conversations.TryGetValue(targetID, out var value);
                return !exists ? null : value;
            }
        }

        public Conversation GetOrCreateConversation(TargetID targetID)
        {
            lock (_conversations)
            {
                var conversation = GetConversation(targetID) ?? new Conversation(targetID);
                _conversations[targetID] = conversation;
                return conversation;
            }
        }
    }
}