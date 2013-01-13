using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Senparc.Weixin.MP.Sample.Service
{
    using Senparc.Weixin.MP.Entities;

    public class LocationService
    {
        public ResponseMessageNews GetResponseMessage(RequestMessageLocation requestMessage)
        {
            var strongresponseMessage =
                                ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.News) as
                                ResponseMessageNews;
            strongresponseMessage.Content =
                string.Format("您刚才发送了地理位置信息。Location_X：{0}，Location_Y：{1}，Scale：{2}，标签：{3}",
                              requestMessage.Location_X, requestMessage.Location_Y,
                              requestMessage.Scale, requestMessage.Label);
            return strongresponseMessage;
        }
    }
}