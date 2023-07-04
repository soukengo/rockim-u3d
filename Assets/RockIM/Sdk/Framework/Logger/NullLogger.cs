namespace RockIM.Sdk.Framework.Logger
{
    public class NullLogger : ILogger
    {
        public static readonly ILogger Instance = new NullLogger();

        public void Debug(string format, params object[] args)
        {
        }

        public void Error(string format, params object[] args)
        {
        }

        public void Info(string format, params object[] args)
        {
        }

        public void Warn(string format, params object[] args)
        {
        }
    }
}