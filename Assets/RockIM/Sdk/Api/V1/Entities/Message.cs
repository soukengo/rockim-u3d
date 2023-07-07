using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Entities
{
    public class Message
    {
        /// <summary>
        /// 服务端消息ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 客户端消息ID
        /// </summary>
        public string ClientMsgId { get; set; }

        /// <summary>
        ///  目标
        /// </summary>
        public TargetID TargetID { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public MessageSender Sender { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public MessageContent Content { get; set; }

        /// <summary>
        /// 消息状态
        /// </summary>
        public MessageStatus Status { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public long Sequence { get; set; }

        /// <summary>
        /// 消息版本号
        /// </summary>
        public long Version { get; set; }
    }
}