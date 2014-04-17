using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities.Request
{
    class RequestMessageEvent_MassSendJobFinish : RequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.MASSSENDJOBFINISH; }
        }

        /// <summary>
        /// 返回状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// group_id下粉丝数；或者openid_list中的粉丝数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 过滤（过滤是指，有些用户在微信设置不接收该公众号的消息）后，准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
        /// </summary>
        public int FilterCount { get; set; }

        /// <summary>
        /// 发送成功的粉丝数
        /// </summary>
        public int SendCount { get; set; }

        /// <summary>
        /// 发送失败的粉丝数
        /// </summary>
        public int ErrorCount { get; set; }
    }
}
