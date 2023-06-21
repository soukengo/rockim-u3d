namespace RockIM.Sdk.Api.V1.Enums
{
    public enum MessageStatus
    {
        // 发送中
        Sending = 0,

        // 发送失败
        Failed = 1,

        // 发送成功
        Success = 2,

        // 已撤回
        Revoked = 3,
    }
}