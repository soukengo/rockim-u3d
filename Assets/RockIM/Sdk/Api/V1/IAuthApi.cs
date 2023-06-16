using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;

namespace RockIM.Sdk.Api.v1
{
    /// <summary>
    /// 登录相关接口
    /// </summary>
    public interface IAuthApi
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public APIResult<LoginResp> Login(LoginReq req);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public APIResult<LoginResp> Logout();
    }
}