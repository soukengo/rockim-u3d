using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public class TextMessageContent : MessageContent
    {
        public TextMessageContent(string content) : base(content)
        {
        }

        public override MessageType Type()
        {
            return MessageType.Text;
        }
    }
}