using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public class PersonConversationID : ConversationID
    {
        public string Account => Value;

        public PersonConversationID(string account) : base(ConversationCategory.Person, account)
        {
        }
    }
}