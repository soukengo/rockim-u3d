using RockIM.Api.Client.V1.Protocol.Http;
using RockIM.Sdk.Api.v1.Dtos;
using RockIM.Sdk.Internal.V1.Domain.Entities;
using RockIM.Sdk.Internal.V1.Domain.Repository;
using RockIM.Sdk.Internal.V1.Infra.Http;
using Socket = RockIM.Sdk.Internal.V1.Domain.Entities.Socket;

namespace RockIM.Sdk.Internal.V1.Infra
{
    public class ProductRepository : IProductRepository
    {
        private readonly IHttpManager _httpManager;

        public ProductRepository(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public Result<ServerConfig> FetchConfig()
        {
            var result = new Result<ServerConfig>();
            var req = new ConfigFetchRequest
            {
                Base = new APIRequest
                {
                    ProductId = "10001"
                }
            };

            var resp = new ConfigFetchResponse();
            var ret = _httpManager.Call("/client/v1/product/config/fetch", req, resp);
            result.CopyForm(ret);
            if (!result.IsSuccess())
            {
                return result;
            }

            var config = new ServerConfig()
            {
                Socket = new Socket()
                {
                    Tcp = new Tcp()
                    {
                        Address = ret.Data.Socket.Tcp?.Address
                    },
                    WebSocket = new WebSocket()
                    {
                        Address = ret.Data.Socket.Ws?.Address,
                    }
                }
            };
            result.Data = config;
            return result;
        }
    }
}