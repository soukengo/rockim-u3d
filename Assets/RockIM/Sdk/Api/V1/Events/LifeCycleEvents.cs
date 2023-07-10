using System;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Framework;

namespace RockIM.Sdk.Api.V1.Events
{
    /// <summary>
    /// sdk生命周期相关事件
    /// </summary>
    public sealed class LifeCycleEvents
    {
        public event Action<User> LoginSuccess;

        public event Action<ConnectionStatus> ConnectionChange;

        public event Action<LogoutResp> Logout;

        public void OnConnectionChange(ConnectionStatus status)
        {
            AsyncManager.Callback(() => ConnectionChange?.Invoke(status));
        }

        public void OnLoginSuccess(User user)
        {
            AsyncManager.Callback(() => LoginSuccess?.Invoke(user));
        }

        public void OnLogout(LogoutResp obj)
        {
            AsyncManager.Callback(() => Logout?.Invoke(obj));
        }
    }
}