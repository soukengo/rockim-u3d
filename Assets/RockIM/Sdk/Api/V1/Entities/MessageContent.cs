using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public abstract class MessageContent
    {
        public string Content { get; }

        protected MessageContent(string content)
        {
            Content = content;
        }

        public abstract MessageType Type();
    }
}