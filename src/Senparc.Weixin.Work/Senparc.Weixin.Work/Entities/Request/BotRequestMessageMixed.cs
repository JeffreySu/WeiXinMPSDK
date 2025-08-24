/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageMixed.cs
    文件功能描述：接收图文混排消息
    
    
    创建标识：Wang Qian - 20250803

    修改标识: Wang Qian - 20250815
    修改描述: 修改为直接映射文档原始键名，便于无特性反序列化

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 图文混排消息，请求实体
    /// </summary>
    public class BotRequestMessageMixed : WorkBotRequestMessageBase
    {
        public override string msgtype { get; set; } = "mixed";

        /// <summary>
        /// 混排内容，对应 JSON 中的 mixed
        /// </summary>
        public Mixed mixed { get; set; }

        /// <summary>
        /// 与文档一致的 mixed 对象
        /// </summary>
        public class Mixed
        {
            /// <summary>
            /// 消息项集合，对应 mixed.msg_item
            /// </summary>
            public System.Collections.Generic.List<MsgItem> msg_item { get; set; }
        }

        /// <summary>
        /// mixed.msg_item 的元素
        /// </summary>
        public class MsgItem
        {
            /// <summary>
            /// 子消息类型：text 或 image
            /// </summary>
            public string msgtype { get; set; }

            /// <summary>
            /// 文本对象，仅当 msgtype=text 时存在
            /// </summary>
            public Text text { get; set; }

            /// <summary>
            /// 图片对象，仅当 msgtype=image 时存在
            /// </summary>
            public Image image { get; set; }
        }

        /// <summary>
        /// 文本对象
        /// </summary>
        public class Text
        {
            /// <summary>
            /// 文本内容
            /// </summary>
            public string content { get; set; }
        }

        /// <summary>
        /// 图片对象
        /// </summary>
        public class Image
        {
            /// <summary>
            /// 图片 URL
            /// </summary>
            public string url { get; set; }
        }
    }
}