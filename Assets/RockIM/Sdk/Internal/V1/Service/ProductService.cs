using RockIM.Sdk.Api.v1.Dtos;
using RockIM.Sdk.Internal.V1.Domain.Entities;
using RockIM.Sdk.Internal.V1.Domain.Repository;

namespace RockIM.Sdk.Internal.V1.Service
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Result<ServerConfig> FetchConfig()
        {
            return _productRepository.FetchConfig();
        }
    }
}