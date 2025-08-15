/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageStream.cs
    文件功能描述：接收流式消息
    
    
    创建标识：Wang Qian - 20250803

    修改标识: Wang Qian - 20250815
    修改描述: 修改为直接映射文档原始键名，便于无特性反序列化

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 流式消息，请求实体
    /// </summary>
    public class BotRequestMessageStream : WorkBotRequestMessageBase
    {
        /// <summary>
        /// 流式内容，对应 JSON 中的 stream
        /// </summary>
        public Stream stream { get; set; }

        /// <summary>
        /// 与文档一致的 stream 对象
        /// </summary>
        public class Stream
        {
            /// <summary>
            /// 流式消息 ID
            /// </summary>
            public string id { get; set; }
        }
    }
}