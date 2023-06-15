using System;

namespace RockIM.Demo.Scripts.Models
{
    
    [Serializable]
    public class Server
    {
        public string Name;

        public string Url;

        public string ProductId;

        public string ProductKey;

        public Server(string name, string url, string productId, string productKey)
        {
            Name = name;
            Url = url;
            ProductId = productId;
            ProductKey = productKey;
        }
    }
}