using System;
using System.Collections.Generic;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Domain.Options;
using RockIM.Sdk.Internal.V1.Domain.Repository;

namespace RockIM.Sdk.Internal.V1.Service
{
    public class MessageService : IMessageApi
    {
        private readonly SdkContext _context;

        private readonly IMessageRepository _messageRepository;

        public MessageService(SdkContext context, IMessageRepository messageRepository)
        {
            _context = context;
            _messageRepository = messageRepository;
        }


        public APIResult<MessageListResp> List(MessageListReq req)
        {
            var ret = new APIResult<MessageListResp>
            {
                Code = ResultCode.Success
            };

            var data = new MessageListResp();
            var list = new List<Message>();
            // for (var i = 0; i < req.Size; i++)
            // {
            //     var idx = new Random().Next(Contents.Length);
            //     list.Add(new Message
            //     {
            //         ConversationID = req.ConversationID,
            //         ID = Guid.NewGuid().ToString(),
            //         Content = new TextMessageContent(req.ConversationID + " : " + Contents[idx])
            //     });
            // }

            data.List = list;
            ret.Data = data;
            return ret;
        }

        public APIResult<MessageSendResp> Send(MessageSendReq req)
        {
            var opts = new MessageSendOptions(req.TargetID, req.Content)
            {
                ClientMsgId = Guid.NewGuid().ToString(),
                Payload = req.Payload,
            };
            var result = _messageRepository.Send(opts);
            var currentUser = _context.Authorization.User;
            var ret = ResultConverter.Convert(result, (source) =>
            {
                var resp = new MessageSendResp();
                var message = new Message
                {
                    ID = "",
                    TargetID = opts.TargetID,
                    ClientMsgId = opts.ClientMsgId,
                    Sender = new MessageSender
                    {
                        Account = currentUser.Account,
                        Name = currentUser.Name,
                        AvatarUrl = currentUser.AvatarUrl
                    },
                    Content = opts.Content,
                    Status = MessageStatus.Sending,
                };
                resp.Message = message;
                return resp;
            });
            return ret;
        }
    }
}