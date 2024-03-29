using System.Collections.Generic;
using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Sdk.Api.V1.Dtos.Request
{
    public class MessageSendReq
    {
        public readonly TargetID TargetID;

        public readonly MessageContent Content;
        
        public Dictionary<string, string> Payload;

        public MessageSendReq(TargetID targetID, MessageContent content)
        {
            TargetID = targetID;
            Content = content;
        }
    }
}