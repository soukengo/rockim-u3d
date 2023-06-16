using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Sdk.Internal.V1.Domain.Options;

namespace RockIM.Sdk.Internal.V1.Domain.Repository
{
    public interface IAuthRepository
    {
        public Result<Authorization> Login(LoginOptions opts);
    }
}