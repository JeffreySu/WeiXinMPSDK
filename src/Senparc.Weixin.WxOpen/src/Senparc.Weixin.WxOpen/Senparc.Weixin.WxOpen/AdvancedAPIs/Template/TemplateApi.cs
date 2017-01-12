/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：TemplateAPI.cs
    文件功能描述：小程序的模板消息接口

----------------------------------------------------------------*/

/*
    API：https://mp.weixin.qq.com/debug/wxadoc/dev/api/notice.html#接口说明
 */

using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Template.TemplateJson;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Template
{
    /// <summary>
    /// 模板消息接口
    /// </summary>
    public static class TemplateApi
    {
        #region 同步请求

        /// <summary>
        /// 小程序模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="templateId"></param>
        /// <param name="data"></param>
        /// <param name="emphasisKeyword">模板需要放大的关键词，不填则默认无放大</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="formId">表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id</param>
        /// <param name="page">点击模板查看详情跳转页面，不填则模板无跳转</param>
        /// <returns></returns>
        public static WxJsonResult SendTemplateMessage(string accessTokenOrAppId, string openId, string templateId, object data, string formId, string page = null, string emphasisKeyword = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";
                var msgData = new TempleteModel()
                {
                    touser = openId,
                    template_id = templateId,
                    // topcolor = topcolor,
                    page = page,
                    form_id = formId,
                    data = data,
                    emphasis_keyword = emphasisKeyword,
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }



        #endregion

        #region 异步请求
        /// <summary>
        /// 【异步方法】小程序模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="templateId"></param>
        /// <param name="data"></param>
        /// <param name="emphasisKeyword">模板需要放大的关键词，不填则默认无放大</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="formId">表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id</param>
        /// <param name="page">点击模板查看详情跳转页面，不填则模板无跳转</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendTemplateMessageAsync(string accessTokenOrAppId, string openId, string templateId, object data, string formId, string page = null, string emphasisKeyword = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";
                var msgData = new TempleteModel()
                {
                    touser = openId,
                    template_id = templateId,
                    // topcolor = topcolor,
                    page = page,
                    form_id = formId,
                    data = data,
                    emphasis_keyword = emphasisKeyword,
                };
            
                return Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion
    }
}