/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotRequestMessageBase.cs
    文件功能描述：企业微信智能机器人接收到请求的消息基类
    
    
    创建标识：Wang Qian - 20250802

    修改标识: Wang Qian - 20250815
    修改描述: 修改消息基类，直接映射文档原始键名，便于无特性反序列化
----------------------------------------------------------------*/
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{
    public class WorkBotRequestMessageBase
    {
        /// <summary>
        /// 机器人消息的唯一标识
        /// </summary>
        public string msgid { get; set; }

        /// <summary>
        /// 智能体（机器人）ID
        /// </summary>
        public string aibotid { get; set; }

        /// <summary>
        /// 会话ID
        /// </summary>
        public string chatid { get; set; }

        /// <summary>
        /// 会话类型，例：group
        /// </summary>
        public string chattype { get; set; }

        /// <summary>
        /// 发送方信息
        /// </summary>
        public From from { get; set; }

        /// <summary>
        /// 消息类型，例：text
        /// </summary>
        public string msgtype { get; set; }

        /// <summary>
        /// 与文档一致的 from 对象
        /// </summary>
        public class From
        {
            /// <summary>
            /// 成员UserId
            /// </summary>
            public string userid { get; set; }
        }
    }
}