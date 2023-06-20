using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Sdk.Api.V1.Dtos.Request
{
    public class MessageSendReq
    {
        public readonly TargetID TargetID;

        public readonly MessageContent Content;

        public MessageSendReq(TargetID targetID, MessageContent content)
        {
            TargetID = targetID;
            Content = content;
        }
    }
}