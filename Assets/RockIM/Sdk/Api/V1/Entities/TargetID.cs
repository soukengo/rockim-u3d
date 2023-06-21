using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public class TargetID
    {
        public readonly TargetCategory Category;

        public readonly string Value;

        public TargetID(TargetCategory category, string value)
        {
            Category = category;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Category}:{Value}";
        }
    }
}