using RockIM.Sdk.Framework.Logger;

namespace RockIM.Sdk.Internal.V1.Context
{
    public abstract class LoggerContext
    {
        public static ILogger Logger = new NullLogger();
    }
}