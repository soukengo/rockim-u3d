using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public class PersonTargetID : TargetID
    {
        public string Account => Value;

        public PersonTargetID(string account) : base(TargetCategory.Person, account)
        {
        }
    }
}