using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public class GroupConversationID : ConversationID
    {
        public string GroupType { get; }
        public string CustomId { get; }

        public GroupConversationID(string groupType, string customId) : base(ConversationCategory.Group,
            UniqueId(groupType, customId))
        {
            GroupType = groupType;
            CustomId = customId;
        }

        public static GroupConversationID FromValue(string value)
        {
            var pairs = DecodeUniqueId(value);
            return pairs.Length != 2 ? null : new GroupConversationID(pairs[0], pairs[1]);
        }
    }
}