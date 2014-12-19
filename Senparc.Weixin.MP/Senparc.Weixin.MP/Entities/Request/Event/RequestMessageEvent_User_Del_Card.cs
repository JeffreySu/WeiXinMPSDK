using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_User_Del_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 删除卡券
        /// </summary>
        public override Event Event
        {
            get { return Event.user_del_card; }
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
