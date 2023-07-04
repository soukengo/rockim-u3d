using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Constants;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Api.V1.Exceptions;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Domain.Entities;
using RockIM.Sdk.Internal.V1.Service;

namespace RockIM.Sdk.Internal.V1
{
    public sealed class ClientV1 : IClient
    {
        private readonly SdkContext _context;

        private ApisV1 _apis;

        public IApis Apis
        {
            get
            {
                if (_apis == null)
                {
                    throw new BusinessException(ErrorReasons.ClientUninitialized, "未初始化");
                }

                return _apis;
            }
        }

        public IEventBus EventBus { get; }

        public ClientV1()
        {
            _context = new SdkContext();
            EventBus = new EventBus();
        }

        public APIResult<InitResp> Init(Config config)
        {
            _context.Config.APIConfig = new APIConfig(config.ServerUrl, config.ProductId, config.ProductKey);
            _apis = new ApisV1(_context,EventBus);
            var result = _apis.ProductService.FetchConfig();
            var data = result.Data;
            var ret = ResultConverter.Convert(result, (source) => new InitResp());
            if (!result.IsSuccess())
            {
                return ret;
            }

            _context.Config.ServerConfig = data;
            return ret;
        }
    }
}