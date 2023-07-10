using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Response;

namespace RockIM.Sdk.Api.V1
{
    public interface IClient
    {
        /// <summary>
        /// 必须在初始化完成之后调用，否则调用接口返回 ErrorReasons.ClientUninitialized
        /// </summary>
        public IApis Apis { get; }

        /// <summary>
        /// 事件中心，用于注册sdk的各类事件回调
        /// </summary>
        public IEventApis EventApis { get; }

        /// <summary>
        /// 初始化客户端
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public APIResult<InitResp> Init(Config config);

        /// <summary>
        /// 销毁客户端
        /// </summary>
        public void Dispose();
    }
}