using System;
using RockIM.Sdk.Api.v1;
using RockIM.Sdk.Api.V1.Constants;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Api.V1.Exceptions;

namespace RockIM.Sdk.Api.V1
{
    /// <summary>
    /// 接口列表
    /// </summary>
    public interface IApis
    {
        public IAuthApi Auth { get; }

        /// <summary>
        /// 必须在登录之后调用，否则调用接口返回 ErrorReasons.ClientUnauthorized
        /// </summary>
        public IAuthorizedApis Authorized { get; }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public APIResult<LogoutResp> Logout();
    }


    /// <summary>
    /// 需要登录认证的接口列表
    /// </summary>
    public interface IAuthorizedApis
    {
        public User Current { get; }

        public ConnectionStatus ConnectionStatus { get; }

        public IMessageApi Message { get; }
    }

    public class EmptyApis : IApis
    {
        private readonly Exception _exception = new BusinessException(ErrorReasons.ClientUninitialized, "未初始化");

        public EmptyApis()
        {
            Authorized = new EmptyAuthorizedApis(_exception);
        }

        public IAuthApi Auth => throw _exception;
        public IAuthorizedApis Authorized { get; }

        public APIResult<LogoutResp> Logout()
        {
            return new APIResult<LogoutResp>
                {Code = ResultCode.Success, Data = new LogoutResp {Reason = LogoutReason.Initiative}};
        }
    }

    public class EmptyAuthorizedApis : IAuthorizedApis
    {
        private readonly Exception _exception = new BusinessException(ErrorReasons.ClientUnauthorized, "未登录");

        public EmptyAuthorizedApis()
        {
        }

        public EmptyAuthorizedApis(Exception exception)
        {
            _exception = exception;
        }

        public IMessageApi Message => throw _exception;

        public User Current => null;
        public ConnectionStatus ConnectionStatus => ConnectionStatus.Disconnected;
    }
}