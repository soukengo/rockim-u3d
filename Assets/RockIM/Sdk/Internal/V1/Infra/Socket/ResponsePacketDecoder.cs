using Google.Protobuf;
using RockIM.Api.Client.V1.Protocol.Socket;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Shared;
using RockIM.Shared.Enums;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public abstract class ResponsePacketDecoder
    {
        public static Result<T> Decode<T>(Packet packet) where T : IMessage, new()
        {
            if (Network.Types.PacketType.Response != (Network.Types.PacketType) packet.Type)
            {
                return new Result<T>
                {
                    Meta = new MetaData(),
                    Code = ResultCode.InternalError,
                };
            }

            var header = packet.ResponseHeader;
            var body = Codec.Decode<ResponsePacketBody>(packet.Body);

            var result = new Result<T>
            {
                Meta = new MetaData
                {
                    TraceId = header.TraceId,
                },
            };
            if (!header.Success)
            {
                result.Code = ResultCode.InternalError;
                if (body.Body == null)
                {
                    return result;
                }

                var err = Codec.Decode<Error>(body.Body.ToByteArray());
                result.Code = ResultCodeExtensions.FormInt(err.Code);
                result.Reason = err.Reason;
                result.Message = err.Message;

                return result;
            }

            result.Code = ResultCode.Success;
            result.Data = Codec.Decode<T>(body.Body.ToByteArray());
            return result;
        }
    }
}