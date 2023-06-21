using System.Collections.Generic;
using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Sdk.Internal.V1.Domain.Options
{
    public class MessageSendOptions
    {
        public readonly TargetID TargetID;

        public readonly MessageContent Content;

        public string ClientMsgId;

        public Dictionary<string, string> Payload;

        public MessageSendOptions(TargetID targetID, MessageContent content)
        {
            TargetID = targetID;
            Content = content;
        }
    }
}