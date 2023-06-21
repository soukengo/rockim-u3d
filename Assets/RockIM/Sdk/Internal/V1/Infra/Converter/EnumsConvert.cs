using RockIM.Sdk.Api.V1.Enums;
using RockIM.Shared.Enums;

namespace RockIM.Sdk.Internal.V1.Infra.Converter
{
    public class EnumsConvert
    {
        public TargetCategory TargetCategory(MessageTarget.Types.Category source)
        {
            return (TargetCategory) source;
        }
    }
}