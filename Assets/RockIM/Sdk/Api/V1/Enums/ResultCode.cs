using System;

namespace RockIM.Sdk.Api.V1.Enums
{
    public enum ResultCode
    {
        Success = 200,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        ClientClosed = 499,
        InternalError = 500,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
    }

    public static class ResultCodeExtensions
    {
        public static ResultCode FormInt(int code)
        {
            if (!Enum.IsDefined(typeof(ResultCode), code))
            {
                return ResultCode.InternalError;
            }

            return (ResultCode) code;
        }
    }
}