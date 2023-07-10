using RockIM.Sdk.Api.v1;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Infra;
using RockIM.Sdk.Internal.V1.Infra.Http;
using RockIM.Sdk.Internal.V1.Service;

namespace RockIM.Sdk.Internal.V1
{
    public sealed class ApisV1 : IApis
    {
        private readonly SdkContext _context;
        private readonly IEventBus _eventBus;

        private readonly IAuthorizedApis _emptyAuthorizedApis = new EmptyAuthorizedApis();

        private AuthorizedApisV1 _authorizedApis;

        internal readonly ProductService ProductService;
        public IAuthApi Auth { get; }

        public IAuthorizedApis Authorized => _authorizedApis ?? _emptyAuthorizedApis;


        public ApisV1(SdkContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
            IHttpManager httpManager = new HttpManager(context);
            ProductService = new ProductService(new ProductRepository(httpManager));
            Auth = new AuthService(context, new AuthRepository(httpManager),
                (auth) =>
                {
                    LoggerContext.Logger.Info("Auth Success: {0}", auth);
                    context.Authorization = auth;
                    _authorizedApis = new AuthorizedApisV1(context, _eventBus);
                    _eventBus.LifeCycle.OnLoginSuccess(auth.User);
                });
        }

        public APIResult<LogoutResp> Logout()
        {
            var resp = new LogoutResp {Reason = LogoutReason.Initiative};
            var auth = _context.Authorization;
            if (auth != null)
            {
                resp.Account = auth.User.Account;
            }

            ClearAuth();
            _eventBus.LifeCycle.OnLogout(resp);
            return new APIResult<LogoutResp> {Code = ResultCode.Success, Data = new LogoutResp()};
        }

        private void ClearAuth()
        {
            _authorizedApis?.Dispose();
            _authorizedApis = null;
        }

        public void Dispose()
        {
            ClearAuth();
        }
    }
}