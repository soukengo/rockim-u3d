using RockIM.Sdk.Api.v1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Response;

namespace RockIM.Sdk.Api.V1
{
    public interface IClient
    {
        /// <summary>
        ///  初始化
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public Result<InitResp> Init(Config config);

        /// <summary>
        /// 接口列表
        /// </summary>
        /// <returns></returns>
        public IApis Apis();
    }

    public interface IAuthorizedApis
    {
        /// <summary>
        /// 消息相关接口
        /// </summary>
        /// <returns></returns>
        public IMessageApi Message();
    }
}