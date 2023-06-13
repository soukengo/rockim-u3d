using System;
using System.Collections.Generic;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1.Dtos.Request;
using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.Views.Chat
{
    public class ChatMenuView : MonoBehaviour
    {
        public List<ChatMenuItem> items = new List<ChatMenuItem>();

        public GameObject itemPrefab;

        public GameObject menuContainer;

        private Dictionary<GameObject, ChatMenuItem> menuMap = new Dictionary<GameObject, ChatMenuItem>();


        void Start()
        {
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var obj = Instantiate(itemPrefab, menuContainer.transform, false);
                obj.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
                var toggle = obj.GetComponent<Toggle>();
                toggle.group = menuContainer.GetComponent<ToggleGroup>();
                toggle.isOn = i == 0;
                menuMap[obj] = item;
            }
        }

        void Update()
        {
        }
    }

    [Serializable]
    public class ChatMenuItem
    {
        public string key;

        public Sprite sprite;
    }
}