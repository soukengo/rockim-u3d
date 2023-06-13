using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.v1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Domain.Entities;
using RockIM.Sdk.Internal.V1.Service;

namespace RockIM.Sdk.Internal.V1
{
    public class ClientV1 : IClient
    {
        private readonly SdkConfig _config;

        private Apis _apis;
        private ProductService _productService;

        public ClientV1()
        {
            _config = new SdkConfig();
        }

        public Result<InitResp> Init(Config config)
        {
            var result = new Result<InitResp>();
            _config.APIConfig = new APIConfig(config.ServerUrl, config.ProductId, config.ProductKey);
            _apis = new Apis(_config);
            var ret = _apis.productService.FetchConfig();
            result.CopyForm(ret);
            if (!result.IsSuccess())
            {
                return result;
            }

            _config.ServerConfig = ret.Data;
            result.Data = new InitResp();
            return result;
        }

        public IApis Apis()
        {
            return _apis;
        }
    }
}