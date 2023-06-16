using System;
using RockIM.Sdk.Api.v1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Sdk.Internal.V1.Domain.Options;
using RockIM.Sdk.Internal.V1.Domain.Repository;

namespace RockIM.Sdk.Internal.V1.Service
{
    public class AuthService : IAuthApi
    {
        private readonly SdkContext _context;

        private readonly IAuthRepository _authRepository;

        private readonly Action<Authorization> _onAuthSuccess;

        public AuthService(SdkContext context, IAuthRepository authRepository, Action<Authorization> onAuthSuccess)
        {
            _context = context;
            _authRepository = authRepository;
            _onAuthSuccess = onAuthSuccess;
        }

        public APIResult<LoginResp> Login(LoginReq req)
        {
            var result = _authRepository.Login(new LoginOptions
            {
                AuthCode = req.AuthCode
            });
            var data = result.Data;
            var ret = ResultConverter.Convert(result, (source) => new LoginResp());
            if (!ret.IsSuccess())
            {
                return ret;
            }

            _onAuthSuccess(data);
            return ret;
        }

        public APIResult<LoginResp> Logout()
        {
            throw new NotImplementedException();
        }
    }
}