using RockIM.Sdk.Internal.V1.Context;

namespace RockIM.Sdk.Internal.V1.Service
{
    public class SocketService
    {
        private readonly SdkContext _context;

        public SocketService(SdkContext context)
        {
            _context = context;
        }

        public void Connect()
        {
        }

        public void Reconnect()
        {
        }

        public bool IsConnected()
        {
            return false;
        }
    }
}