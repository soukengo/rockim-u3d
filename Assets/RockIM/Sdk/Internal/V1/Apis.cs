using RockIM.Sdk.Api.v1;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Infra;
using RockIM.Sdk.Internal.V1.Infra.Http;
using RockIM.Sdk.Internal.V1.Service;

namespace RockIM.Sdk.Internal.V1
{
    public class Apis : IApis
    {
        private IHttpManager _httpManager;
        private IAuthApi _authApi;

        private IAuthorizedApis _authorizedApis;

        internal ProductService productService;

        public Apis(SdkConfig config)
        {
            _httpManager = new HttpManager(config);
            productService = new ProductService(new ProductRepository(_httpManager));
        }

        public IAuthApi Auth()
        {
            return _authApi;
        }

        public IAuthorizedApis Authorized()
        {
            return _authorizedApis;
        }
    }
}