/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotChatObjects.cs
    文件功能描述：封装企业微信智能机器人接收到请求的消息基类中的JSON对象
    
    
    创建标识：Wang Qian - 20250802
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Work.Entities.BotChatObjects
{
        /// <summary>
        /// 会话类型，single\group，分别表示：单聊\群聊
        /// </summary>
        public enum ChatType
        {
            Single,
            Group
        }

        /// <summary>
        /// 对应from,该事件触发者的信息
        /// </summary>
        public class From
        {
            /// <summary>
            /// 对应from.userid，该事件触发者的userid
            /// </summary>
            public string UserId { get; set; }
        }
    
        /// <summary>
        /// 对应msgtype，消息类型
        /// </summary>
        public enum MsgType
        {
            Text,
            Image,
            Mixed,
            Stream, // 流式消息,
            Unknown // 未知消息类型
        }

        /// <summary>
        /// 让Text和Image继承这个接口，方便MsgItem使用
        /// </summary>
        public interface IContent
        {
        }

        /// <summary>
        /// 对应text，文字消息内容
        /// </summary>
        public class Text : IContent
        {
            public string Content { get; set; }
        }
        
        /// <summary>
        /// 对应image，图片消息内容
        /// </summary>
        public class Image : IContent
        {
            /// <summary>
            /// 对应image.url，图片的下载url
            /// </summary>
            public string Url { get; set; }
        }

        /// <summary>
        /// 对应mixed.msg_item
        public class MsgItem
        {
            public MsgType MsgType { get; set; }
            public IContent Content { get; set; }
        }

        /// <summary>
        /// 对应mixed，混合消息内容
        /// </summary>
        public class Mixed
        {
            /// <summary>
            /// 对应mixed.msg_item JSON数组，图文混排消息内容
            /// </summary>
            public List<MsgItem> MsgItem { get; set; } = new List<MsgItem>
            {
                new MsgItem { MsgType = MsgType.Text, Content = new Text() },
                new MsgItem { MsgType = MsgType.Image, Content = new Image() }
            };
        }

        /// <summary>
        /// 对应stream，包含stream.id
        /// </summary>
        public class Stream
        {
            public string Id { get; set; }
        }
}