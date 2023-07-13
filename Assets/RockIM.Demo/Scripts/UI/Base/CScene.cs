using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using RockIM.Demo.Scripts.UI.Events;
using UnityEngine;

namespace RockIM.Demo.Scripts.UI.Base
{
    public abstract class CScene : MonoBehaviour
    {
        private readonly Dictionary<Type, CPanel> _panelMap = new Dictionary<Type, CPanel>();

        protected bool IsDestroyed;

        private void Awake()
        {
            var panels = Resources.FindObjectsOfTypeAll<CPanel>();
            foreach (var panel in panels)
            {
                panel.SetActive(panel.defaultOpen);
                _panelMap[panel.GetType()] = panel;
            }

            UIEventManager.Instance.OpenPanel += OnOpenUI;
            Init();
        }

        protected abstract void Init();


        [CanBeNull]
        public T GetPanel<T>() where T : CPanel
        {
            var typ = typeof(T);
            if (_panelMap.TryGetValue(typ, out var value))
            {
                return (T) value;
            }

            return null;
        }

        public virtual void OnDestroy()
        {
            IsDestroyed = true;
            UIEventManager.Instance.OpenPanel -= OnOpenUI;
        }

        private void OnOpenUI(Type type)
        {
            var exists = _panelMap.TryGetValue(type, out var panel);
            if (!exists)
            {
                return;
            }

            panel.SetActive(true);
        }
    }
}