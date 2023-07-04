using System;
using RockIM.Sdk.Api.v1;

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
    }

    /// <summary>
    /// 需要登录认证的接口列表
    /// </summary>
    public interface IAuthorizedApis
    {
        public abstract IMessageApi Message { get; }
    }
}