namespace RockIM.Sdk.Internal.V1.Domain.Entities
{
    public class APIConfig
    {
        public string ServerUrl { get; set; }
        public string ProductId { get; set; }
        public string ProductKey { get; set; }


        public APIConfig()
        {
        }

        public APIConfig(string serverUrl, string productId, string productKey)
        {
            ServerUrl = serverUrl;
            ProductId = productId;
            ProductKey = productKey;
        }

        public override string ToString()
        {
            return
                $"{nameof(ServerUrl)}: {ServerUrl}, {nameof(ProductId)}: {ProductId}, {nameof(ProductKey)}: {ProductKey}";
        }
    }
}