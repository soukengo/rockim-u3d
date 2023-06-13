namespace RockIM.Sdk.Api.V1
{
    public class Config
    {
        public string ServerUrl { get; set; }
        public string ProductId { get; set; }
        public string ProductKey { get; set; }


        public Config()
        {
        }

        public Config(string serverUrl, string productId, string productKey)
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