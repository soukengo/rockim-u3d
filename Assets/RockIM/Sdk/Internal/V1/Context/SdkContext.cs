using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Internal.V1.Domain.Data;

namespace RockIM.Sdk.Internal.V1.Context
{
    public class SdkContext
    {
        public readonly SdkConfig Config;

        public Authorization Authorization;

        public ConnectionStatus ConnectionStatus = ConnectionStatus.Disconnected;


        public SdkContext()
        {
            Config = new SdkConfig();
        }
    }
}