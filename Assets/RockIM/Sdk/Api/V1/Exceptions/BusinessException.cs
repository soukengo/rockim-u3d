using System;

namespace RockIM.Sdk.Api.V1.Exceptions
{
    public class BusinessException : Exception
    {
        public string Reason { get; }


        public BusinessException(string reason, string message) : base(message)
        {
            Reason = reason;
        }
    }
}