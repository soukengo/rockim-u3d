using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Sdk.Internal.V1.Domain.Entities;
using RockIM.Sdk.Internal.V1.Domain.Options;

namespace RockIM.Sdk.Internal.V1.Domain.Repository
{
    public interface IChatRoomRepository
    {
        public Result<Empty> Join(ChatRoomJoinOptions opts);
    }
}