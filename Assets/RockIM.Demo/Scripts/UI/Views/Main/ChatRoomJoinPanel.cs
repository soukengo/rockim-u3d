using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Widgets;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Unity;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main
{
    public class ChatRoomJoinPanel : CPanel
    {
        public InputField chatRoomIdInput;

        public Button cancelBtn;

        public Button joinBtn;

        protected override void Init()
        {
            cancelBtn.onClick.AddListener(() => { gameObject.SetActive(false); });

            joinBtn.onClick.AddListener(() =>
            {
                var chatRoomId = chatRoomIdInput.text;
                if (chatRoomId.Length == 0)
                {
                    ToastManager.ShowToast("请输入房间ID");
                    return;
                }

                var req = new ChatRoomJoinReq()
                {
                    CustomGroupId = chatRoomId
                };
                ImSdkUnity.Async(() => ImSdkV1.Apis.Authorized.ChatRoom.Join(req), (result) =>
                {
                    if (!result.IsSuccess())
                    {
                        ToastManager.ShowToast("操作失败 :" + result.Message);
                        return;
                    }

                    ToastManager.ShowToast("加入成功");
                    Hide();
                });
            });
        }
    }
}