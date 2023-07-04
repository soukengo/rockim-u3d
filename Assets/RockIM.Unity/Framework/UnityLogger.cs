using RockIM.Sdk.Framework.Logger;

namespace RockIM.Unity.Framework
{
    public class UnityLogger : ILogger
    {
        public void Debug(string format, params object[] args) => UnityEngine.Debug.LogFormat(format, args);

        public void Error(string format, params object[] args) => UnityEngine.Debug.LogErrorFormat(format, args);

        public void Info(string format, params object[] args) => UnityEngine.Debug.LogFormat(format, args);

        public void Warn(string format, params object[] args) => UnityEngine.Debug.LogWarningFormat(format, args);
    }
}