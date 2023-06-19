using System.Collections.Generic;
using RockIM.Demo.Scripts.Logic.Models.Chat;
using RockIM.Demo.Scripts.UI.Base;
using RockIM.Demo.Scripts.UI.Views.Main.Chat.Widgets;
using RockIM.Demo.Scripts.UI.Widgets;
using RockIM.Demo.Third.InfiniteScroll;
using RockIM.Sdk;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Unity;
using UnityEngine;

namespace RockIM.Demo.Scripts.UI.Views.Main.Chat
{
    public class MessageList : CComponent
    {
        [SerializeField] public InfiniteScroll scroll;

        private List<ImMessage> _list = new List<ImMessage>();

        public int maxCount = 50;

        public ConversationID ConversationID;

        private void Start()
        {
            scroll.OnFill += OnFillItem;
            scroll.OnHeight += OnHeightItem;
            scroll.OnPull += OnPullItem;
            PullMessage(InfiniteScroll.Direction.Bottom);
        }

        private void OnFillItem(int index, GameObject obj)
        {
            var item = _list[index];
            var mi = obj.GetComponent<MessageItem>();
            mi.SetContent(item.Message.Content.Content);
        }

        private int OnHeightItem(int index)
        {
            return _list[index].Height;
        }

        private void OnPullItem(InfiniteScroll.Direction direction)
        {
            Debug.Log("direction: "+ direction);
            PullMessage(direction);
        }

        // 拉取消息
        private void PullMessage(InfiniteScroll.Direction direction)
        {
            var lastMsgId = "";
            if (_list.Count > 0)
            {
                lastMsgId = _list[0].Message.ID;
            }

            var messageDirection =
                direction is InfiniteScroll.Direction.Left or InfiniteScroll.Direction.Top
                    ? MessageDirection.Oldest
                    : MessageDirection.Newest;
            var req = new MessageListReq
            {
                ConversationID = ConversationID,
                Direction = messageDirection,
                LastMsgId = lastMsgId
            };
            ImSdkUnity.Async(() => ImSdk.V1.Apis.Authorized.Message.List(req), (result) =>
            {
                if (!result.IsSuccess())
                {
                    ToastManager.ShowToast(result.Message, true);
                    return;
                }

                var list = result.Data?.List;
                if (list == null || list.Count == 0)
                {
                    return;
                }

                AppendMessage(list, direction);
            });
        }

        public void AppendMessage(List<Message> list,
            InfiniteScroll.Direction direction = InfiniteScroll.Direction.Bottom)
        {
            var mi = scroll.Prefab.GetComponent<MessageItem>();
            var newList = new List<ImMessage>();
            foreach (var item in list)
            {
                var content = item.Content.Content;
                var height = mi.SetContent(content);
                newList.Add(new ImMessage {Message = item, Height = (int) height});
            }

            ApplyMessage(direction, newList);
        }

        /// <summary>
        /// 将消息加载到ui上
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="newList"></param>
        private void ApplyMessage(InfiniteScroll.Direction direction, List<ImMessage> newList)
        {
            lock (_list)
            {
                var oldCount = _list.Count;
                var isFirst = oldCount == 0;
                // 第一次加载，初始化
                if (isFirst)
                {
                    _list = newList;
                    scroll.InitData(_list.Count);
                }
                else
                {
                    var refresh = false;
                    if (direction is InfiniteScroll.Direction.Left or InfiniteScroll.Direction.Top)
                    {
                        _list.InsertRange(0, newList);
                        if (_list.Count > maxCount)
                        {
                            _list.RemoveRange(maxCount, _list.Count - maxCount);
                            refresh = true;
                            scroll.InitData(_list.Count);
                        }
                    }
                    else
                    {
                        _list.AddRange(newList);
                        if (_list.Count > maxCount)
                        {
                            _list.RemoveRange(0, _list.Count - maxCount);
                            refresh = true;
                        }
                    }

                    if (refresh)
                    {
                        Debug.Log("超过数量，刷新页面");
                        scroll.InitData(_list.Count);
                    }
                    else
                    {
                        scroll.ApplyDataTo(_list.Count, newList.Count, direction);
                        scroll.MoveToSide(direction);
                    }
                }
            }
        }
    }
}