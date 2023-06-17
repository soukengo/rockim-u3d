using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Events;
using RockIM.Demo.Scripts.UI.Views.Login;
using UnityEngine.SceneManagement;

namespace RockIM.Demo.Scripts.UI.Views
{
    public class LoginView : CScene
    {
        protected override void Init()
        {
            var loginPanel = GetPanel<LoginPanel>();
            LoginUIEventManager.Instance.OnInitSuccess = () => { loginPanel!.Show(); };
            LoginUIEventManager.Instance.OnLoginSuccess = () => { SceneManager.LoadScene(SceneNames.Main); };
        }
    }
}