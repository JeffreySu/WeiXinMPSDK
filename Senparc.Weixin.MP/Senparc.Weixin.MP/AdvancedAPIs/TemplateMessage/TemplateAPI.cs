/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：TemplateAPI.cs
    文件功能描述：模板消息接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/17/304c1885ea66dbedf7dc170d84999a9d.html
 */

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
    public static class TemplateApi
    {
        public static SendTemplateMessageResult SendTemplateMessage<T>(string accessToken, string openId, string templateId, string topcolor,string url, T data)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";
            var msgData = new TempleteModel()
            {
                touser = openId,
                template_id = templateId,
                topcolor = topcolor,
                url = url,
                data = data
            };
            return CommonJsonSend.Send<SendTemplateMessageResult>(accessToken, urlFormat, msgData);
        }
    }
}