using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public abstract class TargetID
    {
        private const string ValueSeparator = "#";

        public TargetCategory Category;

        public string Value;

        public TargetID(TargetCategory category, string value)
        {
            Category = category;
            Value = value;
        }

        protected static string UniqueId(params string[] pairs)
        {
            return string.Join(ValueSeparator, pairs);
        }

        protected static string[] DecodeUniqueId(string value)
        {
            return value.Split(ValueSeparator);
        }

        public override string ToString()
        {
            return $"{Category}:{Value}";
        }
    }
}