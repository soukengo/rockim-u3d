using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Sdk.Internal.V1.Domain.Entities;

namespace RockIM.Sdk.Internal.V1.Domain.Repository
{
    public interface IProductRepository
    {
        public Result<ServerConfig> FetchConfig();
    }
}