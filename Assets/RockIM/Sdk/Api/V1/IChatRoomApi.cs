using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;

namespace RockIM.Sdk.Api.V1
{
    public interface IChatRoomApi
    {
        public APIResult<ChatRoomJoinResp> Join(ChatRoomJoinReq req);
    }
}