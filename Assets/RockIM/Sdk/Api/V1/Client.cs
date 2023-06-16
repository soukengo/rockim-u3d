using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Response;

namespace RockIM.Sdk.Api.V1
{
    public abstract class Client
    {
        /// <summary>
        /// 必须在初始化完成之后调用，否则调用接口返回 ErrorReasons.ClientUninitialized
        /// </summary>
        public abstract Apis Apis { get; }

        /// <summary>
        /// 初始化接口
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public abstract APIResult<InitResp> Init(Config config);
    }
}