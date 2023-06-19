using System;
using System.Collections.Generic;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Dtos.Request;
using RockIM.Sdk.Api.V1.Dtos.Response;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Internal.V1.Service
{
    public class MessageService : IMessageApi
    {
        private static readonly string[] Contents = new string[]
        {
            "在这个示例中，我们假设你已经将Text组件添加到一个游戏对象上，并将该游戏对象作为脚本的引用。",
            "在Start方法中，我们首先将horizontalOverflow属性设置为Wrap，以允许文本自动换行。然后，将verticalOverflow属性设置为Truncate，以在垂直方向上修剪溢出的文本。",
            "最后，我们将Text组件的宽度限制为maxWidth，通过设置sizeDelta属性来调整文本框的大小。这样，当文本超过指定的宽度时，它会自动进行换行。",
            "确保在使用这段代码之前，将正确的Text组件和最大宽度（maxWidth）分配给脚本的公共字段。这样，当你运行游戏时，文本将根据指定的宽度进行自动换行。",
            "Inspector で下記の設定をすることで無限スクロールを実装できます。FancyScrollView の Loop をオンにするとセルが循環し、先頭のセルの前に末尾のセル、末尾のセルの後に先頭のセルが並ぶようになります。サンプルで使用されている Scroller を使うときは、 Movement Type を Unrestricted に設定することで、スクロール範囲が無制限になります。 1. と組み合わせることで無限スクロールを実現できます。実装例（Examples/03_InfiniteScroll）が含まれていますので、こちらも参考にしてください。FancyScrollRect および FancyGridView は無限スクロールをサポートしていません。"
        };

        public APIResult<MessageListResp> List(MessageListReq req)
        {
            var ret = new APIResult<MessageListResp>
            {
                Code = ResultCode.Success
            };

            var data = new MessageListResp();
            var list = new List<Message>();
            // for (var i = 0; i < req.Size; i++)
            // {
            //     var idx = new Random().Next(Contents.Length);
            //     list.Add(new Message
            //     {
            //         ConversationID = req.ConversationID,
            //         ID = Guid.NewGuid().ToString(),
            //         Content = new TextMessageContent(req.ConversationID + " : " + Contents[idx])
            //     });
            // }

            data.List = list;
            ret.Data = data;
            return ret;
        }

        public APIResult<MessageSendResp> Send(MessageSendReq req)
        {
            var ret = new APIResult<MessageSendResp>
            {
                Code = ResultCode.Success
            };
            var data = new MessageSendResp();
            data.Message = new Message
            {
                ConversationID = req.ConversationID,
                ID = Guid.NewGuid().ToString(),
                Content = req.Content,
            };
            ret.Data = data;
            return ret;
        }
    }
}