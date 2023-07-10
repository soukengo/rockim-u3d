using System;
using RockIM.Demo.Scripts.Framework;
using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Demo.Scripts.UI.Events
{
    public class ChatUIEventManager : Singleton<ChatUIEventManager>
    {
        public Action OpenChat = delegate { };

        public Action<string> ChatMenuSelected = delegate { };

        public Action<Message> SendResult = delegate { };
    }
}