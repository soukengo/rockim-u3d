using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Demo.Scripts.Logic.Models.Chat
{
    public class Conversation
    {
        private const int MaxMessageCount = 50;

        public readonly TargetID TargetID;

        private readonly ConcurrentDictionary<string, DemoMessage> _messageDict =
            new ConcurrentDictionary<string, DemoMessage>();

        public List<DemoMessage> Messages = new List<DemoMessage>();


        public Conversation(TargetID targetID)
        {
            TargetID = targetID;
        }

        public bool AppendMessage(MessageDirection direction, List<DemoMessage> messages)
        {
            var overflow = false;
            foreach (var message in messages)
            {
                _messageDict[message.Message.ClientMsgId] = message;
            }

            var list = _messageDict.Values.ToList();
            list.Sort((v1, v2) => v1.Message.Time < v2.Message.Time ? -1 : 1);
            if (list.Count > MaxMessageCount)
            {
                overflow = true;
                var removeStart = 0;
                var removeCount = list.Count - MaxMessageCount;
                if (direction == MessageDirection.Newest)
                {
                    removeStart = MaxMessageCount;
                }

                var removeList = list.GetRange(removeStart, removeCount);

                foreach (var item in removeList)
                {
                    _messageDict.TryRemove(item.Message.ClientMsgId, out _);
                }

                list.RemoveRange(removeStart, removeCount);
            }

            Messages = list;
            return overflow;
        }
    }
}