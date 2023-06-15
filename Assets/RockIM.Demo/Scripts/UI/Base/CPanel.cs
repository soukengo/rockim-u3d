using UnityEngine;

namespace RockIM.Demo.Scripts.UI.Base
{
    public abstract class CPanel : MonoBehaviour
    {
        public bool defaultOpen;

        private void Awake()
        {
            Init();
        }

        protected abstract void Init();

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}