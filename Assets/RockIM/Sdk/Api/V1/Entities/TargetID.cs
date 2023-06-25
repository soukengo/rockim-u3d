using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public class TargetID
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        public readonly TargetCategory Category;

        /// <summary>
        /// 群聊为customGroupId，私聊为用户account
        /// </summary>
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