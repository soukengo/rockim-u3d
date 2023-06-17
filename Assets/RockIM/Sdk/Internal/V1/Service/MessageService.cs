using System;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;

namespace RockIM.Sdk.Internal.V1.Service
{
    public class MessageService : IMessageApi
    {
        public APIResult<MessageListResp> List(MessageListReq req)
        {
            throw new NotImplementedException();
        }

        public APIResult<MessageSendResp> Send(MessageSendReq req)
        {
            throw new NotImplementedException();
        }
    }
}