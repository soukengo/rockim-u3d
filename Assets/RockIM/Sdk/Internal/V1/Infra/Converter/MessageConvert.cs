using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Internal.V1.Infra.Converter
{
    public abstract class MessageConvert
    {
        public static TargetID TargetId(RockIM.Api.Client.V1.Types.TargetID source)
        {
            var category = (TargetCategory) source.Category;
            return new TargetID(category, source.Value);
        }

        public static RockIM.Api.Client.V1.Types.TargetID ServerTargetId(TargetID source)
        {
            var category = (RockIM.Shared.Enums.MessageTarget.Types.Category) source.Category;
            return new RockIM.Api.Client.V1.Types.TargetID {Category = category, Value = source.Value};
        }
    }
}