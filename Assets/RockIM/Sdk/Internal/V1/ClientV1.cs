using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Domain.Entities;
using RockIM.Sdk.Internal.V1.Service;

namespace RockIM.Sdk.Internal.V1
{
    public sealed class ClientV1 : IClient
    {
        private SdkContext _context;

        private ApisV1 _apis;

        private readonly IApis _emptyApis = new EmptyApis();

        public IApis Apis => _apis ?? _emptyApis;

        public IEventApis EventApis { get; }

        private readonly EventBus _eventBus;

        public ClientV1()
        {
            _context = new SdkContext();
            EventApis = new EventApis();
            _eventBus = new EventBus(EventApis);
        }

        public APIResult<InitResp> Init(Config config)
        {
            _context.Config.APIConfig = new APIConfig(config.ServerUrl, config.ProductId, config.ProductKey);
            _apis = new ApisV1(_context, _eventBus);
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

        public void Dispose()
        {
            _apis?.Dispose();
            _apis = null;
            _context = new SdkContext();
        }
    }
}