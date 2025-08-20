/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageText.cs
    文件功能描述：接收文本消息
    
    
    创建标识：Wang Qian - 20250803

    修改标识: Wang Qian - 20250815
    修改描述: 修改为直接映射文档原始键名，便于无特性反序列化

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 文本消息，请求实体
    /// </summary>
    public class BotRequestMessageText : WorkBotRequestMessageBase
    {
        public override string msgtype { get; set; } = "text";

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public Text text { get; set; }

        /// <summary>
        /// 与文档一致的 text 对象
        /// </summary>
        public class Text
        {
            /// <summary>
            /// 文本内容
            /// </summary>
            public string content { get; set; }
        }
    }
}
