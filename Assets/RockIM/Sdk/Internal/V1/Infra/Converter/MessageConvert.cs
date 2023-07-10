using System.Collections.Generic;
using System.Linq;
using RockIM.Api.Client.V1.Types;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Shared.Enums;
using Message = RockIM.Shared.Enums.Message;
using TargetID = RockIM.Sdk.Api.V1.Entities.TargetID;

namespace RockIM.Sdk.Internal.V1.Infra.Converter
{
    public abstract class MessageConvert
    {
        public static TargetID TargetId(RockIM.Api.Client.V1.Types.TargetID source)
        {
            var category = (TargetCategory) source.Category;
            return new TargetID(category, source.Value);
        }

        public static RockIM.Api.Client.V1.Types.TargetID ServerTargetId(TargetID source)
        {
            var category = (MessageTarget.Types.Category) source.Category;
            return new RockIM.Api.Client.V1.Types.TargetID {Category = category, Value = source.Value};
        }

        public static MessageSender Sender(IMMessageSender source)
        {
            return new MessageSender
            {
                Account = source.Account,
                Name = source.Name,
                AvatarUrl = source.AvatarUrl,
            };
        }

        public static MessageContent Content(IMMessageBody source)
        {
            return new TextMessageContent(source.Content.ToStringUtf8());
        }

        public static MessageStatus Status(Message.Types.Status source)
        {
            return (MessageStatus) source;
        }

        public static Api.V1.Entities.Message Message(IMMessage source)
        {
            return new Api.V1.Entities.Message
            {
                ID = source.MsgId,
                ClientMsgId = source.Body.ClientMsgId,
                TargetID = TargetId(source.Target),
                Sender = Sender(source.Sender),
                Content = Content(source.Body),
                Status = Status(source.Status),
                Time = source.Body.Timestamp,
                Sequence = source.Sequence,
                Version = source.Version,
            };
        }

        public static List<Api.V1.Entities.Message> MessageList(List<IMMessage> source)
        {
            return source.Select(Message).ToList();
        }
    }
}