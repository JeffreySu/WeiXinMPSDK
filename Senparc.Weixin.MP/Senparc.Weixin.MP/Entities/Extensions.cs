﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public static class Extensions
    {
        /// <summary>
        /// 将RequestMessageEventBase转换成RequestMessageText类型，其中Content = requestMessage.EventKey
        /// </summary>
        /// <param name="requestMessageEvent"></param>
        /// <returns></returns>
        public static RequestMessageText ConvertToRequestMessageText(this IRequestMessageEventBase requestMessageEvent)
        {
            var requestMessage = requestMessageEvent;
            var requestMessageText = new RequestMessageText()
            {
                FromUserName = requestMessage.FromUserName,
                ToUserName = requestMessage.ToUserName,
                CreateTime = requestMessage.CreateTime,
                MsgId = requestMessage.MsgId
            };

            //判断是否具有EventKey属性
            if (requestMessageEvent is IRequestMessageEventKey)
            {
                requestMessageText.Content = (requestMessageEvent as IRequestMessageEventKey).EventKey;
            }
            else
            {
                requestMessageText.Content = "";
            }

            return requestMessageText;
        }
    }
}
