using System;
using System.Collections.Generic;
using System.Linq;
using RockIM.Demo.Scripts.Managers;
using RockIM.Demo.Scripts.Models;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Components;
using RockIM.Sdk.Api.V1;
using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Login
{
    public class InitPanel : CPanel
    {
        [SerializeField] public List<Server> servers = new List<Server>();

        public Dropdown serverDropdown;

        public InputField serverUrlInput;
        public InputField productIdInput;
        public InputField productKeyInput;

        public Button confirmButton;

        public Action OnInitSuccess = () => { };

        protected override void Init()
        {
        }

        private void Start()
        {
            var serverOptions = servers.Select(t => new Dropdown.OptionData(t.Name)).ToList();
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
            serverUrlInput.text = server.Url;
            productIdInput.text = server.ProductId;
            productKeyInput.text = server.ProductKey;
        }

        /// <summary>
        /// 点击初始化按钮
        /// </summary>
        private void ClickInit()
        {
            var productId = productIdInput.text;
            var productKey = productKeyInput.text;
            var serverUrl = serverUrlInput.text;
            ImSdkManager.Instance.Init(new Config(serverUrl, productId, productKey), (result) =>
            {
                Debug.Log("初始化结果：" + result);
                if (!result.IsSuccess())
                {
                    ToastManager.ShowToast("初始化失败", true);
                    return;
                }

                ToastManager.ShowToast("初始化成功");
                Hide();
                // 通知初始化成功
                OnInitSuccess();
            });
        }
    }
}