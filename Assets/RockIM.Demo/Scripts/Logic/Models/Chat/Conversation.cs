using System.Collections.Generic;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Demo.Scripts.Logic.Models.Chat
{
    public class Conversation
    {
        public const int MaxMessageCount = 50;

        public TargetID TargetID;

        public List<DemoMessage> Messages = new List<DemoMessage>();

        public Conversation(TargetID targetID)
        {
            TargetID = targetID;
        }

        public bool AppendMessage(MessageDirection direction, List<DemoMessage> messages)
        {
            var overflow = false;
            if (direction == MessageDirection.Newest)
            {
                Messages.AddRange(messages);
            }
            else
            {
                messages.Reverse();
                Messages.InsertRange(0, messages);
            }

            if (Messages.Count > MaxMessageCount)
            {
                overflow = true;
            }

            return overflow;
        }
    }
}