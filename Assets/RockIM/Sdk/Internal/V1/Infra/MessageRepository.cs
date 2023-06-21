using Google.Protobuf;
using RockIM.Api.Client.V1.Protocol.Http;
using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Sdk.Internal.V1.Domain.Entities;
using RockIM.Sdk.Internal.V1.Domain.Options;
using RockIM.Sdk.Internal.V1.Domain.Repository;
using RockIM.Sdk.Internal.V1.Infra.Converter;
using RockIM.Sdk.Internal.V1.Infra.Http;
using RockIM.Shared.Enums;

namespace RockIM.Sdk.Internal.V1.Infra
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IHttpManager _httpManager;

        public MessageRepository(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public Result<Empty> Send(MessageSendOptions opts)
        {
            var ret = new Result<Empty>();
            var req = new MessageSendRequest()
            {
                Base = new APIRequest(),
                Target = MessageConvert.ServerTargetId(opts.TargetID),
                ClientMsgId = opts.ClientMsgId,
                MessageType = (Message.Types.MessageType) opts.Content.Type(),
                Content = ByteString.CopyFromUtf8(opts.Content.Content),
            };
            if (opts.Payload != null)
            {
                foreach (var item in opts.Payload)
                {
                    req.Payload.Add(item.Key, item.Value);
                }
            }

            var result = _httpManager.Call<MessageSendResponse>(Action.MessageSend, req);
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