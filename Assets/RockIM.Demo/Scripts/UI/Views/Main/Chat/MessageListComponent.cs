using System.Collections.Generic;
using Third.InfiniteScroll;
using UnityEngine;
using UnityEngine.UI;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class MessageListComponent : MonoBehaviour
    {
        [SerializeField] public InfiniteScroll scroll;

        [SerializeField] private int count = 100;

        [SerializeField] private int pullCount = 25;

        private readonly List<int> _list = new List<int>();

        private readonly List<int> _heights = new List<int>();

        void Start()
        {
            scroll.OnFill += OnFillItem;
            scroll.OnHeight += OnHeightItem;
            scroll.OnPull += OnPullItem;
            for (int i = 0; i < count; i++)
            {
                _list.Add(i);
                _heights.Add(Random.Range(100, 200));
            }

            scroll.InitData(_list.Count);
        }

        void OnFillItem(int index, GameObject item)
        {
            item.GetComponentInChildren<Text>().text = _list[index].ToString();
        }

        int OnHeightItem(int index)
        {
            // Debug.Log($"OnHeightItem: {index}");
            return _heights[index];
        }

        void OnPullItem(InfiniteScroll.Direction direction)
        {
            int index = _list.Count;
            if (direction == InfiniteScroll.Direction.Top)
            {
                for (int i = 0; i < pullCount; i++)
                {
                    _list.Insert(0, index);
                    _heights.Insert(0, Random.Range(100, 200));
                    index++;
                }
            }
            else
            {
                for (int i = 0; i < pullCount; i++)
                {
                    _list.Add(index);
                    _heights.Add(Random.Range(100, 200));
                    index++;
                }
            }

            scroll.ApplyDataTo(_list.Count, pullCount, direction);
        }
    }
}