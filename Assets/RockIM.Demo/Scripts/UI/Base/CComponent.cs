using UnityEngine;

namespace RockIM.Demo.Scripts.UI.Base
{
    public abstract class CComponent : MonoBehaviour
    {
        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
        }
    }
}