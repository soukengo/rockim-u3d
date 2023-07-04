using System;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Framework;
using RockIM.Sdk.Internal.V1.Context;

namespace RockIM.Sdk.Api.V1.Events
{
    /// <summary>
    /// sdk生命周期相关事件
    /// </summary>
    public sealed class LifeCycleEvents
    {
        public event Action Connecting;

        public event Action Connected;

        public event Action DisConnected;

        public event Action<LogoutReason> Logout;

        public void OnConnecting()
        {
            LoggerContext.Logger.Info("通知：OnConnecting");
            AsyncManager.Callback(() => Connecting?.Invoke());
        }

        public void OnConnected()
        {
            LoggerContext.Logger.Info("通知：OnConnected");
            AsyncManager.Callback(() => Connected?.Invoke());
        }

        public void OnDisConnected()
        {
            LoggerContext.Logger.Info("通知：OnDisConnected");
            AsyncManager.Callback(() =>
            {
                DisConnected?.Invoke();
            });
        }

        public void OnLogout(LogoutReason obj)
        {
            AsyncManager.Callback(() => Logout?.Invoke(obj));
        }
    }
}