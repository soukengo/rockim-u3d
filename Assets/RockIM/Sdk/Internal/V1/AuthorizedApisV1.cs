using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Service;

namespace RockIM.Sdk.Internal.V1
{
    public sealed class AuthorizedApisV1 : AuthorizedApis
    {
        private SdkContext _context;


        private readonly IMessageApi _messageApi;

        public override IMessageApi Message => _messageApi;

        public AuthorizedApisV1(SdkContext context)
        {
            _context = context;
            _messageApi = new MessageService();
        }
    }
}