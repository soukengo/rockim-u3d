using RockIM.Api.Client.V1.Protocol.Http;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Sdk.Internal.V1.Domain.Options;
using RockIM.Sdk.Internal.V1.Domain.Repository;
using RockIM.Sdk.Internal.V1.Infra.Http;

namespace RockIM.Sdk.Internal.V1.Infra
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHttpManager _httpManager;

        public AuthRepository(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public Result<Authorization> Login(LoginOptions opts)
        {
            var ret = new Result<Authorization>();
            var req = new ConfigFetchRequest
            {
                Base = new APIRequest()
            };

            var result = _httpManager.Call<LoginResponse>(Action.Login, req);
            var data = result.Data;
            ret.CopyForm(result);
            if (!ret.IsSuccess())
            {
                return ret;
            }

            ret.Data = new Authorization(data.Token);
            return ret;
        }
    }
}