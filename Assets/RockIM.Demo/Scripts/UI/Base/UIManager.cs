using RockIM.Demo.Scripts.UI.Widgets;
using UnityEngine;

namespace RockIM.Demo.Scripts.UI.Base
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        static UIManager()
        {
        }


        private static void RegisterComponents()
        {
            RegisterComponent<ToastManager>();
        }

        private static void RegisterComponent<T>() where T : MonoBehaviour
        {
            var target = new GameObject(nameof(T));
            var component = target.AddComponent<T>();
            DontDestroyOnLoad(target);
            DontDestroyOnLoad(component);
        }
    }
}