using System.Collections.Generic;
using RockIM.Demo.Scripts.Framework;
using RockIM.Demo.Scripts.Logic.Models.Chat;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class ChatContext : Singleton<ChatContext>
    {
        public string CurrentMenuKey;

        public List<ChatMenuItem> Items = new List<ChatMenuItem>();
    }
}