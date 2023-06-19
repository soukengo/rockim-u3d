using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Dtos.Request
{
    public class MessageListReq
    {
        public ConversationID ConversationID;

        public MessageDirection Direction;

        public string LastMsgId;

        public int Size = 20;
    }
}