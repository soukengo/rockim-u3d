using Google.Protobuf;
using RockIM.Sdk.Api.v1.Dtos;

namespace RockIM.Sdk.Internal.V1.Infra.Http
{
    public interface IHttpManager
    {
        public Result<T> Call<T>(string action, IMessage req, T reply) where T : IMessage;
    }
}