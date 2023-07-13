using System;
using RockIM.Demo.Scripts.Framework;

namespace RockIM.Demo.Scripts.UI.Events
{
    public class UIEventManager : Singleton<UIEventManager>
    {
        public Action<Type> OpenPanel = delegate { };
    }
}