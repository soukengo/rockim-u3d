using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Events;
using RockIM.Sdk;
using RockIM.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main.Control
{
    public class ToolbarBox : CComponent
    {
        public Button startChatBtn;

        public Button logoutBtn;

        protected override void Init()
        {
            startChatBtn!.onClick.AddListener(() =>
            {
                ChatUIEventManager.Instance.OpenChat.Invoke();
            });
            logoutBtn!.onClick.AddListener(() =>
            {
                ImSdkUnity.Async(() => ImSdkV1.Apis.Logout(), (result) =>
                {
                    SceneManager.LoadScene(SceneNames.Login);
                });
            });
        }
    }
}