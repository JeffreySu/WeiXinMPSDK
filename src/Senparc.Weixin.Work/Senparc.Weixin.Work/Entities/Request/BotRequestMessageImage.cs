/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageImage.cs
    文件功能描述：接收图片消息
    
    
    创建标识：Wang Qian - 20250803

    修改标识: Wang Qian - 20250815
    修改描述: 修改为直接映射文档原始键名，便于无特性反序列化

----------------------------------------------------------------*/


using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 图片消息，请求实体
    /// </summary>
    public class BotRequestMessageImage : WorkBotRequestMessageBase
    {
        public override string msgtype { get; set; } = "image";
        /// <summary>
        /// 图片内容，对应 JSON 中的 image
        /// </summary>
        public Image image { get; set; }

        /// <summary>
        /// 与文档一致的 image 对象
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