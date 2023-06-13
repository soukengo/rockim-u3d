namespace RockIM.Sdk.Internal.V1.Domain.Entities
{
    public class ServerConfig
    {
        public Socket Socket { get; set; }
    }

    public class Socket
    {
        public Tcp Tcp { get; set; }
        public WebSocket WebSocket { get; set; }
    }

    public class Tcp
    {
        public string Address { get; set; }
    }

    public class WebSocket
    {
        public string Address { get; set; }
    }
}