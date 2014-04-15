using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// 模板消息接口
    /// </summary>
    public static class Template
    {
        public static WxJsonResult SendTemplateMessage<T>(string accessToken, string openId, string templateId, string topcolor, T data)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";
            var msgData = new TempleteModel()
            {
                template_id = templateId,
                topcolor = topcolor,
                touser = openId,
                data = data
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData);
        }
    }
}
