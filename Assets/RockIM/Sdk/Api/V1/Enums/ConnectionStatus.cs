namespace RockIM.Sdk.Api.V1.Enums
{
    public enum ConnectionStatus
    {
        Connecting,
        Connected,
        Disconnected,
    }

    public static class ConnectionStatusExtensions
    {
        public static string GetDisplayName(this ConnectionStatus color)
        {
            return color switch
            {
                ConnectionStatus.Connecting => "连接中",
                ConnectionStatus.Connected => "已连接",
                ConnectionStatus.Disconnected => "未连接",
                _ => string.Empty
            };
        }
    }
}