using System;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Utils;

namespace RockIM.Sdk.Api.V1.Dtos
{
    [Serializable]
    public class APIResult<T>
    {
        public ResultCode Code { get; set; }

        public string Reason { get; set; }

        public string Message { get; set; }
        public T Data { get; set; }

        public MetaData Meta { get; set; }

        public APIResult()
        {
        }

        public APIResult(ResultCode code, string reason, string message, MetaData meta)
        {
            Code = code;
            Reason = reason;
            Message = message;
            Meta = meta;
        }

        public bool IsSuccess()
        {
            return Code == ResultCode.Success;
        }

        public override string ToString()
        {
            return JsonUtils.ToJson(this);
        }
    }

    [Serializable]
    public class MetaData
    {
        public string Version { get; set; }

        public string TraceId { get; set; }

        public MetaData()
        {
        }

        public MetaData(string version, string traceId)
        {
            Version = version;
            TraceId = traceId;
        }
    }
}