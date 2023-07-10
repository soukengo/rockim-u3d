using System;
using System.Collections.Generic;
using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Sdk.Api.V1.Events
{
    public class MessageEvents : EventsHolder
    {
        public event Action<List<Message>> Received;

        public void OnReceived(List<Message> obj)
        {
            Executor.Execute(() => Received?.Invoke(obj));
        }
    }
}