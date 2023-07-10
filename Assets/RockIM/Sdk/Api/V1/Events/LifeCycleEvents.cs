using System;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Api.V1.Events
{
    /// <summary>
    /// sdk生命周期相关事件
    /// </summary>
    public class LifeCycleEvents : EventsHolder
    {
        
        public event Action<User> LoginSuccess;

        public event Action<ConnectionStatus> ConnectionChange;

        public event Action<LogoutResp> Logout;

        public void OnConnectionChange(ConnectionStatus status)
        {
            Executor.Execute(() => ConnectionChange?.Invoke(status));
        }

        public void OnLoginSuccess(User user)
        {
            Executor.Execute(() => LoginSuccess?.Invoke(user));
        }

        public void OnLogout(LogoutResp obj)
        {
            Executor.Execute(() => Logout?.Invoke(obj));
        }
    }
}