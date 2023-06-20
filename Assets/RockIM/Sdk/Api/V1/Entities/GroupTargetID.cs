using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public class GroupTargetID : TargetID
    {
        public string GroupType { get; }
        public string BizId { get; }

        public GroupTargetID(string groupType, string bizId) : base(TargetCategory.Group,
            UniqueId(groupType, bizId))
        {
            GroupType = groupType;
            BizId = bizId;
        }

        public static GroupTargetID FromValue(string value)
        {
            var pairs = DecodeUniqueId(value);
            return pairs.Length != 2 ? null : new GroupTargetID(pairs[0], pairs[1]);
        }
    }
}