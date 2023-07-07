using System.Collections.Generic;
using System.Linq;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;

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
            var category = (RockIM.Shared.Enums.MessageTarget.Types.Category) source.Category;
            return new RockIM.Api.Client.V1.Types.TargetID {Category = category, Value = source.Value};
        }

        public static MessageSender Sender(RockIM.Api.Client.V1.Types.IMMessageSender source)
        {
            return new MessageSender
            {
                Account = source.Account,
                Name = source.Name,
                AvatarUrl = source.AvatarUrl,
            };
        }

        public static MessageContent Content(RockIM.Api.Client.V1.Types.IMMessageBody source)
        {
            return new TextMessageContent(source.Content.ToStringUtf8());
        }

        public static MessageStatus Status(RockIM.Shared.Enums.Message.Types.Status source)
        {
            return (MessageStatus) source;
        }

        public static Message Message(RockIM.Api.Client.V1.Types.IMMessage source)
        {
            return new Message
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

        public static List<Message> MessageList(List<RockIM.Api.Client.V1.Types.IMMessage> source)
        {
            return source.Select(Message).ToList();
        }
    }
}