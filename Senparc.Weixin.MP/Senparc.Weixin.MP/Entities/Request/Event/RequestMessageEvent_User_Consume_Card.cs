/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：RequestMessageEvent_User_Del_Card.cs
    文件功能描述：事件之核销卡券
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_User_Consume_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 核销卡券
        /// </summary>
        public override Event Event
        {
            get { return Event.user_consume_card; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// code 序列号。自定义code 及非自定义code的卡券被领取后都支持事件推送。
        /// </summary>
        public string UserCardCode { get; set; }
    }
}
