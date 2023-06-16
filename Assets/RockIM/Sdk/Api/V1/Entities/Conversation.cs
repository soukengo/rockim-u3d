using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public class Conversation
    {
        public ConversationID ID;
    }

    public class ConversationID
    {
        public ConversationCategory Category;

        public string Value;
    }
}