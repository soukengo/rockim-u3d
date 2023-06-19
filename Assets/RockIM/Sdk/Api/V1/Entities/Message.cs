namespace RockIM.Sdk.Api.V1.Entities
{
    public class Message
    {
        public ConversationID ConversationID { get; set; }
        public string ID { get; set; }

        public MessageContent Content { get; set; }
    }
}