using UnityEngine;

namespace RockIM.Demo.Scripts.UI.Base
{
    public abstract class CComponent : MonoBehaviour
    {
        protected bool IsDestroyed;
        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
        }
        
        public virtual void OnDestroy()
        {
            IsDestroyed = true;
        }
    }
}