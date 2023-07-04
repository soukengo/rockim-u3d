using RockIM.Sdk.Api.v1;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Constants;
using RockIM.Sdk.Api.V1.Exceptions;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Infra;
using RockIM.Sdk.Internal.V1.Infra.Http;
using RockIM.Sdk.Internal.V1.Service;

namespace RockIM.Sdk.Internal.V1
{
    public sealed class ApisV1 : IApis
    {
        private AuthorizedApisV1 _authorizedApis;

        internal readonly ProductService ProductService;
        public IAuthApi Auth { get; }

        public IAuthorizedApis Authorized
        {
            get
            {
                if (_authorizedApis == null)
                {
                    throw new BusinessException(ErrorReasons.ClientUnauthorized, "未登录");
                }

                return _authorizedApis;
            }
        }


        public ApisV1(SdkContext context)
        {
            IHttpManager httpManager = new HttpManager(context);
            ProductService = new ProductService(new ProductRepository(httpManager));
            Auth = new AuthService(context, new AuthRepository(httpManager),
                (auth) =>
                {
                    context.Authorization = auth;
                    _authorizedApis = new AuthorizedApisV1(context);
                });
        }
    }
}