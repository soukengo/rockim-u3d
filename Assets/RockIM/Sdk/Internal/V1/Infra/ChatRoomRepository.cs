using RockIM.Api.Client.V1.Protocol.Http;
using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Sdk.Internal.V1.Domain.Entities;
using RockIM.Sdk.Internal.V1.Domain.Options;
using RockIM.Sdk.Internal.V1.Domain.Repository;
using RockIM.Sdk.Internal.V1.Infra.Http;

namespace RockIM.Sdk.Internal.V1.Infra
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly IHttpManager _httpManager;

        public ChatRoomRepository(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public Result<Empty> Join(ChatRoomJoinOptions opts)
        {
            var ret = new Result<Empty>();
            var req = new ChatRoomJoinRequest()
            {
                Base = new APIRequest(),
                CustomGroupId = opts.CustomGroupId,
            };

            var result = _httpManager.Call<ChatRoomJoinResponse>(Action.ChatRoomJoin, req);
            ret.CopyForm(result);
            if (!ret.IsSuccess())
            {
                return ret;
            }

            ret.Data = new Empty();
            return ret;
        }
    }
}