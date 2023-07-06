using System.Collections.Generic;
using System.Linq;
using RockIM.Demo.Scripts.Logic;
using RockIM.Demo.Scripts.Logic.Models.Chat;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Events;
using RockIM.Demo.Scripts.UI.Widgets;
using RockIM.Sdk.Api.V1;
using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Login
{
    public class InitPanel : CPanel
    {
        [SerializeField] public List<ChatServer> servers = new List<ChatServer>();

        public Dropdown serverDropdown;

        public InputField serverUrlInput;
        public InputField productIdInput;
        public InputField productKeyInput;

        public Button confirmButton;

        protected override void Init()
        {
        }

        private void Start()
        {
            var serverOptions = servers.Select(t => new Dropdown.OptionData(t.name)).ToList();
            // 默认选中第一个
            serverDropdown.options = serverOptions;
            if (serverOptions.Count > 0)
            {
                SelectServer(0);
            }

            serverDropdown.onValueChanged.AddListener(SelectServer);

            confirmButton.onClick.AddListener(ClickInit);
        }

        /// <summary>
        /// 选择服务器
        /// </summary>
        /// <param name="idx"></param>
        private void SelectServer(int idx)
        {
            if (idx >= servers.Count)
            {
                return;
            }

            var server = servers[idx];
            serverUrlInput.text = server.url;
            productIdInput.text = server.productId;
            productKeyInput.text = server.productKey;
        }

        /// <summary>
        /// 点击初始化按钮
        /// </summary>
        private void ClickInit()
        {
            var productId = productIdInput.text;
            var productKey = productKeyInput.text;
            var serverUrl = serverUrlInput.text;

            ImManager.Instance.Init(new Config(serverUrl, productId, productKey), (result) =>
            {
                if (!result.IsSuccess())
                {
                    ToastManager.ShowToast("初始化失败", true);
                    return;
                }
            
                ToastManager.ShowToast("初始化成功");
                Hide();
                // 通知初始化成功
                LoginUIEventManager.Instance.OnInitSuccess.Invoke();
            });
        }
    }
}