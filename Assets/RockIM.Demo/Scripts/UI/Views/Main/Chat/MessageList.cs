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

        private void Start()
        {
            scroll.OnFill += OnFillItem;
            scroll.OnHeight += OnHeightItem;
            scroll.OnPull += OnPullItem;
            PullMessage(ChatContext.Instance.CurrentTargetID, MessageDirection.Oldest);
        }

        private void OnFillItem(int index, GameObject obj)
        {
            var count = 0;
            var conversation = ChatContext.Instance.CurrentConversation;
            if (conversation is {Messages: { }})
            {
                count = conversation.Messages.Count;
            }

            if (count <= index)
            {
                return;
            }

            var item = conversation.Messages[index];
            var mi = obj.GetComponent<MessageItem>();
            mi.SetMessage(item);
        }

        private int OnHeightItem(int index)
        {
            var count = 0;
            var conversation = ChatContext.Instance.CurrentConversation;
            if (conversation is {Messages: { }})
            {
                count = conversation.Messages.Count;
            }

            if (count <= index)
            {
                return 0;
            }

            var item = conversation.Messages[index];
            return item.Height;
        }

        private void OnPullItem(InfiniteScroll.Direction direction)
        {
            var messageDirection =
                direction is InfiniteScroll.Direction.Left or InfiniteScroll.Direction.Top
                    ? MessageDirection.Oldest
                    : MessageDirection.Newest;
            PullMessage(ChatContext.Instance.CurrentTargetID, messageDirection);
        }

        // 拉取消息
        private void PullMessage(TargetID targetID, MessageDirection direction)
        {
            var conversation = ChatContext.Instance.GetConversation(targetID);
            if (conversation == null)
            {
                return;
            }

            var lastMsgId = "";
            if (conversation.Messages is {Count: > 0})
            {
                lastMsgId = conversation.Messages[0].Message.ID;
            }


            var req = new MessageListReq
            {
                TargetID = conversation.TargetID,
                Direction = direction,
                LastMsgId = lastMsgId
            };
            ImSdkUnity.Async(() => ImSdkV1.Apis.Authorized.Message.List(req), (result) =>
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

                AppendMessage(conversation.TargetID, list, direction);
            });
        }

        public void SwitchConversation(TargetID targetID)
        {
            var conversation = ChatContext.Instance.GetOrCreateConversation(targetID);

            if (conversation.Messages.Count == 0)
            {
                scroll.RecycleAll();
                PullMessage(targetID, MessageDirection.Newest);
            }
            else
            {
                ApplyMessage(MessageDirection.Newest, conversation.Messages.Count, conversation.Messages.Count, true);
            }
        }

        public void AppendMessage(TargetID targetID, List<Message> list,
            MessageDirection direction = MessageDirection.Newest)
        {
            var mi = scroll.Prefab.GetComponent<MessageItem>();
            var newList = new List<DemoMessage>();
            var currentUser = ImSdkV1.Apis.Authorized.Current.Account;
            foreach (var item in list)
            {
                var dm = new DemoMessage {Message = item, IsSelf = currentUser.Equals(item.Sender.Account)};
                var height = mi.CalculateHeight(dm);
                dm.Height = (int) height;
                newList.Add(dm);
            }

            var conversation = ChatContext.Instance.GetOrCreateConversation(targetID);

            var isFirst = conversation.Messages.Count == 0;
            var overflow = conversation.AppendMessage(direction, newList);

            ApplyMessage(direction, conversation.Messages.Count, newList.Count, overflow || isFirst);
        }

        /// <summary>
        /// 将消息加载到ui上
        /// </summary>
        /// <param name="messageDirection"></param>
        /// <param name="totalCount"></param>
        /// <param name="newCount"></param>
        /// <param name="refresh"></param>
        private void ApplyMessage(MessageDirection messageDirection, int totalCount, int newCount, bool refresh)
        {
            var direction = InfiniteScroll.Direction.Bottom;
            if (messageDirection == MessageDirection.Oldest)
            {
                direction = InfiniteScroll.Direction.Top;
            }

            if (refresh)
            {
                scroll.RecycleAll();
                if (totalCount == 0)
                {
                    return;
                }

                scroll.InitData(totalCount);
            }
            else
            {
                scroll.ApplyDataTo(totalCount, newCount, direction);
            }

            scroll.MoveToSide(direction);
        }
    }
}