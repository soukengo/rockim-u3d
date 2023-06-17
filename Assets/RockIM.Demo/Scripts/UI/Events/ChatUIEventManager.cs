using System;
using RockIM.Demo.Scripts.Framework;

namespace RockIM.Demo.Scripts.UI.Events
{
    public class ChatUIEventManager : Singleton<ChatUIEventManager>
    {
        public Action<string> OnChatMenuSelected = delegate { };
    }
}