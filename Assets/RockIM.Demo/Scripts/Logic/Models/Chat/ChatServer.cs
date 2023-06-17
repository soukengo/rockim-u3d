using System;

namespace RockIM.Demo.Scripts.Logic.Models.Chat
{
    
    [Serializable]
    public class ChatServer
    {
        public string name;

        public string url;

        public string productId;

        public string productKey;

        public ChatServer(string name, string url, string productId, string productKey)
        {
            this.name = name;
            this.url = url;
            this.productId = productId;
            this.productKey = productKey;
        }
    }
}