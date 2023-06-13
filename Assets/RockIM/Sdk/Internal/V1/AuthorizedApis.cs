using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Infra.Http;
using RockIM.Sdk.Internal.V1.Service;

namespace RockIM.Sdk.Internal.V1
{
    public class AuthorizedApis : IAuthorizedApis
    {
        private SdkConfig _config;

        private Authorization _authorization;

        private IHttpManager _httpManager;

        private IMessageApi _messageApi;

        public AuthorizedApis(SdkConfig config, Authorization authorization)
        {
            _config = config;
            _authorization = authorization;
            _httpManager = new HttpManager(config, authorization);
            _messageApi = new MessageService();
        }

        public IMessageApi Message()
        {
            throw new System.NotImplementedException();
        }
    }
}