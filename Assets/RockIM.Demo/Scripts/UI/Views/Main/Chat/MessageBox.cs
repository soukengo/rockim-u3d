using System.Collections.Generic;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class MessageBox : CComponent
    {
        public MessageList messageList;

        public InputBox inputBox;

        public void SwitchConversation(TargetID targetID)
        {
            messageList.SwitchConversation(targetID);
        }

        public void OnSendResult(Message message)
        {
            messageList.AppendMessage(message.TargetID, new List<Message> {message});
        }
    }
}