using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace RockIM.Demo.Scripts.UI.Base
{
    public abstract class CScene : MonoBehaviour
    {
        private readonly Dictionary<Type, CPanel> _panelMap = new Dictionary<Type, CPanel>();

        private void Awake()
        {
            var panels = Resources.FindObjectsOfTypeAll<CPanel>();
            foreach (var panel in panels)
            {
                panel.SetActive(panel.defaultOpen);
                _panelMap[panel.GetType()] = panel;
            }

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
    }
}