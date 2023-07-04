using RockIM.Sdk.Internal.V1.Domain.Data;

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