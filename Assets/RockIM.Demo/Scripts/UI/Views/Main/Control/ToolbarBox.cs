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

        public Button joinChatRoomBtn;

        protected override void Init()
        {
            logoutBtn!.onClick.AddListener(() =>
            {
                ImSdkUnity.Async(() => ImSdkV1.Apis.Logout(),
                    (result) => { SceneManager.LoadScene(SceneNames.Login); });
            });

            startChatBtn!.onClick.AddListener(() => { UIEventManager.Instance.OpenPanel.Invoke(typeof(ChatPanel)); });

            joinChatRoomBtn!.onClick.AddListener(() =>
            {
                UIEventManager.Instance.OpenPanel.Invoke(typeof(ChatRoomJoinPanel));
            });
        }
    }
}