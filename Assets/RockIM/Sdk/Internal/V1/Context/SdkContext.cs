using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Sdk.Internal.V1.Domain.Entities;

namespace RockIM.Sdk.Internal.V1.Context
{
    public class SdkContext
    {
        public readonly SdkConfig Config;

        public Authorization Authorization;

        public SdkContext()
        {
            Config = new SdkConfig();
        }
    }
}