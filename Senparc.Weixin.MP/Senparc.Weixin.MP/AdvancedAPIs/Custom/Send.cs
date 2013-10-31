using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs.Custom
{
    public static class Send
    {
        private const string URL_FORMAT = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";

        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static WxJsonResult SendText(string accessToken, string openId, string content)
        {
            var data = new
            {
                touser = openId,
                msgtype = "text",
                text = new
                {
                    content = content
                }
            };
            return CommonJsonSend.Send(accessToken, URL_FORMAT, accessToken);
        }
    }
}
