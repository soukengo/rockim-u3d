using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Internal.V1.Domain.Options;
using RockIM.Sdk.Internal.V1.Domain.Repository;

namespace RockIM.Sdk.Internal.V1.Service
{
    public class ChatRoomService : IChatRoomApi
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatRoomService(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        public APIResult<ChatRoomJoinResp> Join(ChatRoomJoinReq req)
        {
            var result = _chatRoomRepository.Join(new ChatRoomJoinOptions {CustomGroupId = req.CustomGroupId});
            return ResultConverter.Convert(result, (source) => new ChatRoomJoinResp());
        }
    }
}