using System;
using RockIM.Demo.Scripts.Framework;

namespace RockIM.Demo.Scripts.UI.Events
{
    public class LoginUIEventManager : Singleton<LoginUIEventManager>
    {
        public Action OnInitSuccess = delegate { };
        public Action OnLoginSuccess = delegate { };
    }
}