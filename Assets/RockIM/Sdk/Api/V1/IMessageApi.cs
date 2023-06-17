using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;

namespace RockIM.Sdk.Api.V1
{
    /// <summary>
    /// 消息相关接口
    /// </summary>
    public interface IMessageApi
    {
        public APIResult<MessageListResp> List(MessageListReq req);
        public APIResult<MessageSendResp> Send(MessageSendReq req);
    }
}