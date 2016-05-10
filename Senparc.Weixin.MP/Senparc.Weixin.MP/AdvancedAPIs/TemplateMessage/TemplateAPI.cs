/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：TemplateAPI.cs
    文件功能描述：模板消息接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/17/304c1885ea66dbedf7dc170d84999a9d.html
 */

using System;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 模板消息接口
    /// </summary>
    public static class TemplateApi
    {
        /// <summary>
        /// 模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="templateId"></param>
        /// <param name="topcolor"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SendTemplateMessageResult SendTemplateMessage(string accessTokenOrAppId, string openId, string templateId, string topcolor, string url, object data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
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
                return CommonJsonSend.Send<SendTemplateMessageResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="industry_id1">公众号模板消息所属行业编号</param>
        /// <param name="industry_id2">公众号模板消息所属行业编号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>

        public static WxJsonResult SetIndustry(string accessTokenOrAppId, IndustryCode industry_id1, IndustryCode industry_id2, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/template/api_set_industry?access_token={0}";
                var msgData = new 
                {
                    industry_id1 = ((int)industry_id1).ToString(),
                    industry_id2 = ((int)industry_id2).ToString()
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>

        public static GetIndustryJsonResult GetIndustry(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/template/get_industry?access_token={0}";
                return CommonJsonSend.Send<GetIndustryJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获得模板ID
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="template_id_short">模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddtemplateJsonResult Addtemplate(string accessTokenOrAppId, string template_id_short, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token={0}";
                var msgData = new
                {
                    template_id_short = template_id_short
                    
                };
                return CommonJsonSend.Send<AddtemplateJsonResult>(accessToken, urlFormat, msgData, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>

        public static GetPrivateTemplateJsonResult GetPrivateTemplate(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/template/get_all_private_template?access_token={0}";
                return CommonJsonSend.Send<GetPrivateTemplateJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="template_id">公众帐号下模板消息ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult DelPrivateTemplate(string accessTokenOrAppId, string template_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/template/del_private_template?access_token={0}";
                var msgData = new
                {
                    template_id = template_id
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData, CommonJsonSendType.POST, timeOut: timeOut);
                    
            }, accessTokenOrAppId);
        }




       
    }
}