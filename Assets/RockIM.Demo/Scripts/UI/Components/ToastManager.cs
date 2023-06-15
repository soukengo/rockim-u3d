using UnityEngine;

namespace RockIM.Demo.Scripts.UI.Components
{
    public class ToastManager : MonoBehaviour
    {
        private const string PrefabPath = "Prefabs/Toolkits/Toast";

        private static ToastManager Instance { get; set; }

        private static readonly GameObject Prefab;

        private static readonly Transform Transform;


        static ToastManager()
        {
            Prefab = Resources.Load<GameObject>(PrefabPath);
            var target = new GameObject(nameof(ToastManager));
            Instance = target.AddComponent<ToastManager>();
            DontDestroyOnLoad(target);
            DontDestroyOnLoad(Instance);
            Transform = target.transform;
            var canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                Transform = canvas.transform;
            }
        }


        public static void ShowToast(string content, bool useMask = false, float duration = 1.5f)
        {
            Instance.NewToast(content, useMask, duration);
        }

        private void NewToast(string content, bool useMask, float duration)
        {
            var toast = Instantiate(Prefab, Transform, false);
            var sc = toast.GetComponent<Toast>();
            sc.mask.enabled = useMask;
            sc.message.text = content;
            toast.SetActive(true);
            StartCoroutine(Hide(toast, duration));
        }

        private static System.Collections.IEnumerator Hide(Object toast, float duration)
        {
            // 等待指定的持续时间
            yield return new WaitForSeconds(duration);

            // 销毁Toast对象
            Destroy(toast);
        }
    }
}