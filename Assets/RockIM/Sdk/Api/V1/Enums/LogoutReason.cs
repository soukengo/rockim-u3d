namespace RockIM.Sdk.Api.V1.Enums
{
    public enum LogoutReason
    {
        /// <summary>
        /// 未知原因
        /// </summary>
        Unknown,

        /// <summary>
        /// 主动退出
        /// </summary>
        Initiative,

        /// <summary>
        /// 授权过期
        /// </summary>
        AuthExpired,

        /// <summary>
        /// 被挤下线
        /// </summary>
        KickOff,
    }
}