using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;

namespace RockIM.Sdk.Api.V1
{
    public interface IConversationApi
    {
        /// <summary>
        /// create a conversation
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public APIResult<ConversationCreateResp> Create(ConversationCreateReq req);

        /// <summary>
        /// list conversation
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public APIResult<ConversationListResp> List(ConversationListReq req);
    }
}