using RockIM.Sdk.Api.v1;

namespace RockIM.Sdk.Api.V1
{
    /// <summary>
    /// 接口列表
    /// </summary>
    public abstract class Apis
    {
        public abstract IAuthApi Auth { get; protected set; }

        /// <summary>
        /// 必须在登录之后调用，否则调用接口返回 ErrorReasons.ClientUnauthorized
        /// </summary>
        public abstract AuthorizedApis Authorized { get; protected set; }
    }

    /// <summary>
    /// 需要登录认证的接口列表
    /// </summary>
    public abstract class AuthorizedApis
    {
        public abstract IMessageApi Message { get; }
    }
}