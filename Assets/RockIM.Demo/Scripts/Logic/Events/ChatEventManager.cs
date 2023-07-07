using System;
using System.Collections.Generic;
using RockIM.Demo.Scripts.Framework;
using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Demo.Scripts.Logic.Events
{
    public class ChatEventManager : Singleton<ChatEventManager>
    {
        public Action<List<Message>> MessageReceived = delegate { };
    }
}