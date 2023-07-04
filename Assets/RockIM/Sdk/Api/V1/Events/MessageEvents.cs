using System;
using System.Collections.Generic;
using RockIM.Sdk.Api.V1.Entities;

namespace RockIM.Sdk.Api.V1.Events
{
    public class MessageEvents
    {
        public event Action<List<Message>> Received;

        protected virtual void OnReceived(List<Message> obj)
        {
            Received?.Invoke(obj);
        }
    }
}