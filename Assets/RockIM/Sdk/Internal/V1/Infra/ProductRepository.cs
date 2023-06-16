using RockIM.Api.Client.V1.Protocol.Http;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Internal.V1.Domain.Data;
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
            var ret = new Result<ServerConfig>();
            var req = new ConfigFetchRequest
            {
                Base = new APIRequest()
            };

            var result = _httpManager.Call<ConfigFetchResponse>(Action.Config, req);
            var data = result.Data;
            ret.CopyForm(result);
            if (!ret.IsSuccess())
            {
                return ret;
            }

            var config = new ServerConfig()
            {
                Socket = new Socket()
                {
                    Tcp = new Tcp()
                    {
                        Address = data.Socket.Tcp?.Address
                    },
                    WebSocket = new WebSocket()
                    {
                        Address = data.Socket.Ws?.Address,
                    }
                }
            };
            ret.Data = config;
            return ret;
        }
    }
}