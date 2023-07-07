using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Demo.Scripts.Logic.Models.Chat
{
    public class DemoMessage
    {
        public Message Message;

        public int Height;

        /// <summary>
        /// 是否是当前用户发的消息
        /// </summary>
        public bool IsSelf;
    }
}