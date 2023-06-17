using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Sdk.Api.V1.Dtos.Request
{
    public class MessageSendReq
    {
        public ConversationID ConversationID { get; }

        public MessageSendReq(ConversationID conversationID)
        {
            ConversationID = conversationID;
        }
    }
}