using System;
using System.Collections.Generic;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Framework;

namespace RockIM.Sdk.Api.V1.Events
{
    public class MessageEvents
    {
        public event Action<List<Message>> Received;

        public void OnReceived(List<Message> obj)
        {
            AsyncManager.Callback(() => Received?.Invoke(obj));
        }
    }
}