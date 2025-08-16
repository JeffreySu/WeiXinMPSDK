/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageEventBase.cs
    文件功能描述：企业微信智能机器人事件消息基类
    
    
    创建标识：Wang Qian - 20250804

    修改标识：Wang Qian - 20250815
    修改描述：修改事件消息基类，使其与文档一致，便于无特性反序列化
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 企业微信智能机器人事件消息基类（键名与文档一致，便于无特性反序列化）
    /// </summary>
    public class BotRequestMessageEventBase : WorkBotRequestMessageBase
    {
        /// <summary>
        /// 消息创建时间（Unix 秒），对应 create_time
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 发送方信息：包含 corpid 和 userid，对应 from
        /// </summary>
        public new From from { get; set; }

        public Event @event { get; set; }

        /// <summary>
        /// from 对象
        /// </summary>
        public new class From
        {
            /// <summary>
            /// 企业 id，对应 corpid（若为企业内部智能机器人不返回）
            /// </summary>
            public string corpid { get; set; }

            /// <summary>
            /// 成员 UserId，对应 userid
            /// </summary>
            public string userid { get; set; }
        }

        public class Event
        {
            public string eventtype { get; set; }
        }

    }
}