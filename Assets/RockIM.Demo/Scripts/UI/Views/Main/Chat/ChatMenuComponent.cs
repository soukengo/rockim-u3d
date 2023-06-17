using System.Collections.Generic;
using RockIM.Demo.Scripts.Logic.Models.Chat;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Events;
using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class ChatMenuComponent : CComponent
    {
        public List<ChatMenuItem> items = new List<ChatMenuItem>();

        public GameObject itemPrefab;

        public GameObject menuContainer;

        private readonly Dictionary<int, string> _menuMap = new Dictionary<int, string>();


        void Start()
        {
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var obj = Instantiate(itemPrefab, menuContainer.transform, false);
                _menuMap[obj.GetInstanceID()] = item.key;

                obj.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
                var toggle = obj.GetComponent<Toggle>();
                toggle.isOn = false;
                var group = menuContainer.GetComponent<ToggleGroup>();
                toggle.onValueChanged.AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        var activeToggle = group.GetFirstActiveToggle();
                        if (_menuMap.TryGetValue(activeToggle.gameObject.GetInstanceID(), out var key))
                        {
                            OnSelectMenuItem(key);
                        }
                    }
                });
                toggle.group = group;
                toggle.isOn = i == 0;
            }
        }


        void Update()
        {
        }

        private static void OnSelectMenuItem(string key)
        {
            ChatContext.Instance.CurrentMenuKey = key;
            ChatUIEventManager.Instance.OnChatMenuSelected?.Invoke(key);
        }
    }
}