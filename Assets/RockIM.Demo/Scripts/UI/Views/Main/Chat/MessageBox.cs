using System.Collections.Generic;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class MessageBox : CComponent
    {
        private ConversationID _conversationID;

        public MessageList messageList;

        public InputBox inputBox;

        public void SetConversationID(ConversationID conversationID)
        {
            _conversationID = conversationID;
            messageList.ConversationID = conversationID;
            inputBox.ConversationID = conversationID;
        }

        public void OnSendResult(Message message)
        {
            messageList.AppendMessage(new List<Message> {message});
        }
    }
}