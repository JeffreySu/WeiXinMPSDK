#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：CustomAPI.cs
    文件功能描述：小程序订阅消息接口
    
    
    创建标识：Senparc - 20191014
    
        
    修改标识：Senparc - 20190917
    修改描述：v3.6.0 支持新版本 MessageHandler 和 WeixinContext，支持使用分布式缓存储存上下文消息


----------------------------------------------------------------*/

/* 
   API地址：https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/subscribe-message.html
            https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/subscribe-message/subscribeMessage.send.html
*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs
{

    /// <summary>
    /// 小程序订阅消息接口
    /// </summary>
    public class MessageApi
    {
        #region 同步方法

        /// <summary>
        /// 发送订阅消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="toUser">接收者（用户）的 openid</param>
        /// <param name="templateId">所需下发的订阅模板id</param>
        /// <param name="data">模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }</param>
        /// <param name="page">点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar）。该字段不填则模板无跳转。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "MessageApi.SendSubscribe", true)]
        public static WxJsonResult SendSubscribe(string accessTokenOrAppId, string toUser, string templateId, TemplateMessageData data, string page = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/subscribe/send?access_token={0}";
                var submitData = new
                {
                    touser = toUser,
                    template_id = templateId,
                    page = page,
                    data = data
                };
                return CommonJsonSend.Send(accessToken, urlFormat, submitData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】发送订阅消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="toUser">接收者（用户）的 openid</param>
        /// <param name="templateId">所需下发的订阅模板id</param>
        /// <param name="data">模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }</param>
        /// <param name="page">点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar）。该字段不填则模板无跳转。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "MessageApi.SendSubscribe", true)]
        public static async Task<WxJsonResult> SendSubscribeAsync(string accessTokenOrAppId, string toUser, string templateId, TemplateMessageData data, string page = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/subscribe/send?access_token={0}";
                var submitData = new
                {
                    touser = toUser,
                    template_id = templateId,
                    page = page,
                    data = data
                };

                return await CommonJsonSend.SendAsync(accessToken, urlFormat, submitData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion
    }
}
