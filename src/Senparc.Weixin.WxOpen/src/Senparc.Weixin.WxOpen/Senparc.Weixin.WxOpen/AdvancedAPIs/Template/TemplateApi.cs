#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：TemplateAPI.cs
    文件功能描述：小程序的模板消息接口

    修改标识：Senparc - 20170225
    修改描述：v1.2.1 修改模板消息URL

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

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
        #region 同步方法

        /// <summary>
        /// 小程序模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="templateId"></param>
        /// <param name="data"></param>
        /// <param name="emphasisKeyword">模板需要放大的关键词，不填则默认无放大</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="formId">表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id</param>
        /// <param name="page">点击模板查看详情跳转页面，不填则模板无跳转</param>
        /// <returns></returns>
        public static WxJsonResult SendTemplateMessage(string accessTokenOrAppId, string openId, string templateId,
            object data, string formId, string page = null, string emphasisKeyword = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token={0}";
                var msgData = new TempleteModel()
                {
                    touser = openId,
                    template_id = templateId,
                    // topcolor = topcolor,
                    //page = page,
                    form_id = formId,
                    data = data,
                    emphasis_keyword = emphasisKeyword,
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);

            /*
            示例：
            {
              "touser": "OPENID",  
              "template_id": "TEMPLATE_ID", 
              "page": "index",          
              "form_id": "FORMID",         
              "data": {
                  "keyword1": {
                      "value": "339208499", 
                      "color": "#173177"
                  }, 
                  "keyword2": {
                      "value": "2015年01月05日 12:30", 
                      "color": "#173177"
                  }, 
                  "keyword3": {
                      "value": "粤海喜来登酒店", 
                      "color": "#173177"
                  } , 
                  "keyword4": {
                      "value": "广州市天河区天河路208号", 
                      "color": "#173177"
                  } 
              },
              "emphasis_keyword": "keyword1.DATA" 
            }
          */
        }


        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】小程序模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
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
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token={0}";
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
            
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion
#endif
    }
}
 