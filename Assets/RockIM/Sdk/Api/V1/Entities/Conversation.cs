namespace RockIM.Sdk.Api.V1.Entities
{
    public class Conversation
    {
        public ConversationID ConversationID;

        public Conversation(ConversationID conversationID)
        {
            ConversationID = conversationID;
        }
    }
}