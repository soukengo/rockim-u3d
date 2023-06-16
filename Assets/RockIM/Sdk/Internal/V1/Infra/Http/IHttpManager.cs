using Google.Protobuf;
using RockIM.Sdk.Internal.V1.Domain.Data;

namespace RockIM.Sdk.Internal.V1.Infra.Http
{
    public interface IHttpManager
    {
        public Result<T> Call<T>(string action, IMessage req) where T : IMessage, new();
    }
}