using RockIM.Sdk.Internal.V1.Domain.Entities;

namespace RockIM.Sdk.Internal.V1.Context
{
    public class SdkConfig
    {
        /// <summary>
        /// 接口配置
        /// </summary>
        public APIConfig APIConfig;

        /// <summary>
        /// 服务端配置
        /// </summary>
        public ServerConfig ServerConfig;
    }
}