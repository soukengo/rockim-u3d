using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Sdk.Api.V1.Dtos.Request
{
    public class MessageSendReq
    {
        public readonly ConversationID ConversationID;

        public readonly MessageContent Content;

        public MessageSendReq(ConversationID conversationID, MessageContent content)
        {
            ConversationID = conversationID;
            Content = content;
        }
    }
}