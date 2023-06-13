using RockIM.Sdk.Api.v1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;

namespace RockIM.Sdk.Api.v1
{
    public interface IAuthApi
    {
        public Result<LoginResp> Login(LoginReq req);
        public Result<LoginResp> Logout();
    }
}