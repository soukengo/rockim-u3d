using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Components;
using RockIM.Demo.Scripts.UI.Views.Login;

namespace RockIM.Demo.Scripts.UI.Views
{
    public class LoginView : CScene
    {
        protected override void Init()
        {
            var initPanel = GetPanel<InitPanel>();
            var loginPanel = GetPanel<LoginPanel>();
            initPanel!.OnInitSuccess = () => { loginPanel!.Show(); };
        }
    }
}