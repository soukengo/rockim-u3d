using RockIM.Demo.Scripts.Logic;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Views.Main.Control;

namespace RockIM.Demo.Scripts.UI.Views.Main
{
    public class ControlPanel : CPanel
    {
        public UserInfoBox userInfoBox;

        public ToolbarBox toolbarBox;

        protected override void Init()
        {
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            ImManager.Instance.Destroy();
        }
    }
}