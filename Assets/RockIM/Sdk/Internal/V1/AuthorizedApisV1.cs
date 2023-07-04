using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Infra;
using RockIM.Sdk.Internal.V1.Infra.Http;
using RockIM.Sdk.Internal.V1.Service;

namespace RockIM.Sdk.Internal.V1
{
    public sealed class AuthorizedApisV1 : IAuthorizedApis
    {
        private SdkContext _context;


        private readonly IMessageApi _messageApi;

        public IMessageApi Message => _messageApi;

        public AuthorizedApisV1(SdkContext context)
        {
            _context = context;
            var httpManager = new HttpManager(context);
            _messageApi = new MessageService(context, new MessageRepository(httpManager));
        }
    }
}