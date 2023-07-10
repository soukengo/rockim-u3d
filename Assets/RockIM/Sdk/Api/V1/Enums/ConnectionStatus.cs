namespace RockIM.Sdk.Api.V1.Enums
{
    public enum ConnectionStatus
    {
        Connecting,
        Connected,
        Disconnected,
    }

    static class ConnectionStatusExtensions
    {
        public static string GetDisplayName(this ConnectionStatus color)
        {
            switch (color)
            {
                case ConnectionStatus.Connecting:
                    return "连接中";
                case ConnectionStatus.Connected:
                    return "已连接";
                case ConnectionStatus.Disconnected:
                    return "未连接";
                default:
                    return string.Empty;
            }
        }
    }
}