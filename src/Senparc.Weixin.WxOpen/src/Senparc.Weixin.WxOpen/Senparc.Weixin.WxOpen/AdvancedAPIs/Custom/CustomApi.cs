#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
    
    文件名：CustomAPI.cs
    文件功能描述：小程序客服接口
    
    
    创建标识：Senparc - 20180815

    修改标识：Senparc - 20210719
    修改描述：v3.12.2 修复小程序客服接口和公众号混用的问题
    
    修改标识：Senparc - 20230709
    修改描述：v3.16.0 客服接口支持长文本自动切割后连续发送

----------------------------------------------------------------*/

/* 
   API地址：https://developers.weixin.qq.com/miniprogram/dev/api/custommsg/conversation.html
*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.NeuChar.Helpers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs
{

    /// <summary>
    /// 小程序客服接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public class CustomApi
    {
        /* 商户客户参数文档： https://developers.weixin.qq.com/miniprogram/introduction/custom.html#%E6%8E%A5%E6%94%B6%E6%B6%88%E6%81%AF%E6%8E%A8%E9%80%81 */

        /// <summary>
        /// 客服消息统一请求地址格式
        /// </summary>
        public static readonly string UrlFormat_Send = Config.ApiMpHost + "/cgi-bin/message/custom/send?access_token={0}";
        public static readonly string UrlFormat_Send_Business = Config.ApiMpHost + "/cgi-bin/message/custom/business/send?access_token={0}";
        public static readonly string UrlFormat_Typing = Config.ApiMpHost + "/cgi-bin/message/custom/typing?access_token={0}";
        public static readonly string UrlFormat_Typing_Business = Config.ApiMpHost + "/cgi-bin/message/custom/business/typing?access_token={0}";


        /// <summary>
        /// 根据 BusinessId 获取 Send 接口的 UrlFormat
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        private static string GetSendUrlFormat(string businessId = null)
        {
            return businessId.IsNullOrEmpty() ? UrlFormat_Send : UrlFormat_Send_Business;
        }

        /// <summary>
        /// 根据 BusinessId 获取 Typing 接口的 UrlFormat
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        private static string GetTypingUrlFormat(string businessId = null)
        {
            return businessId.IsNullOrEmpty() ? UrlFormat_Typing : UrlFormat_Typing_Business;
        }

        #region 同步方法

        /// <summary>
        /// 发送文本信息
        /// <para>发送文本消息时，支持添加可跳转小程序的文字连接。</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="content">文本消息内容</param>
        /// <param name="businessId">添加 businessId 参数，则发送到子商户</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult SendText(string accessTokenOrAppId, string openId, string content, string businessId = null,
            int timeOut = Config.TIME_OUT)
        {
            object data = null;
            data = new
            {
                touser = openId,
                msgtype = "text",
                text = new
                {
                    content = content
                },
                businessid = businessId
            };

            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = GetSendUrlFormat(businessId);
                var jsonSetting = new JsonSetting() { IgnoreNulls = true };
                return CommonJsonSend.Send(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="mediaId">发送的图片的媒体ID，通过新增素材接口上传图片文件获得。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult SendImage(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            object data = null;
            data = new
            {
                touser = openId,
                msgtype = "image",
                image = new
                {
                    media_id = mediaId
                }
            };

            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send(accessToken, UrlFormat_Send, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图文链接
        /// <para>每次可以发送一个图文链接</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="title">消息标题</param>
        /// <param name="description">图文链接消息</param>
        /// <param name="url">图文链接消息被点击后跳转的链接</param>
        /// <param name="thumbUrl">[官方文档未给说明]</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SendLink(string accessTokenOrAppId, string openId, string title, string description, string url, string thumbUrl, int timeOut = Config.TIME_OUT)
        {
            object data = new
            {
                touser = openId,
                msgtype = "link",
                link = new
                {
                    title = title,
                    description = description,
                    url = url,
                    thumb_url = thumbUrl
                }
            };

            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send(accessToken, UrlFormat_Send, data, timeOut: timeOut);

            }, accessTokenOrAppId);

        }


        /// <summary>
        /// 发送小程序卡片
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="title">消息标题</param>
        /// <param name="pagePath">小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar</param>
        /// <param name="thumbMediaId">小程序消息卡片的封面， image类型的media_id，通过新增素材接口上传图片文件获得，建议大小为520*416</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SendMiniProgramPage(string accessTokenOrAppId, string openId, string title, string pagePath, string thumbMediaId, int timeOut = Config.TIME_OUT)
        {
            object data = new
            {
                touser = openId,
                msgtype = "miniprogrampage",
                miniprogrampage = new
                {
                    title = title,
                    pagepath = pagePath,
                    url = thumbMediaId,
                    thumb_media_id = thumbMediaId
                }
            };

            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send(accessToken, UrlFormat_Send, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 客服输入状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="touser">普通用户（openid）</param>
        /// <param name="typingStatus">"Typing"：对用户下发“正在输入"状态 "CancelTyping"：取消对用户的”正在输入"状态</param>
        /// <param name="businessId">添加 businessId 参数，则发送到子商户</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult GetTypingStatus(string accessTokenOrAppId, string touser, string typingStatus, string businessId = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = GetTypingUrlFormat(businessId);

                var data = new
                {
                    touser = touser,
                    command = typingStatus,
                    businessid = businessId
                };

                var jsonSetting = new JsonSetting() { IgnoreNulls = true };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="content">文本消息内容</param>
        /// <param name="businessId">添加 businessId 参数，则发送到子商户</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="limitedBytes">最大允许发送限制，如果超出限制，则分多条发送</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendTextAsync(string accessTokenOrAppId, string openId, string content, string businessId = null, int timeOut = Config.TIME_OUT, int limitedBytes = 2048)
        {
            object data = null;
            data = new
            {
                touser = openId,
                msgtype = "text",
                text = new
                {
                    content = content
                },
                businessid = businessId
            };

            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                //尝试超长内容发送
                var trySendResult = await MessageHandlerHelper.TrySendLimistedText(accessTokenOrAppId,
                    content, limitedBytes,
                    c => SendTextAsync(accessTokenOrAppId, openId, c, businessId, timeOut, limitedBytes));

                if (trySendResult != null)
                {
                    return trySendResult;
                }

                var urlFormat = GetSendUrlFormat(businessId);
                var jsonSetting = new JsonSetting() { IgnoreNulls = true };
                return await CommonJsonSend.SendAsync(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="mediaId">发送的图片的媒体ID，通过新增素材接口上传图片文件获得。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendImageAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            object data = null;
            data = new
            {
                touser = openId,
                msgtype = "image",
                image = new
                {
                    media_id = mediaId
                }
            };

            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                return await CommonJsonSend.SendAsync(accessToken, UrlFormat_Send, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发送图文链接
        /// <para>每次可以发送一个图文链接</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="title">消息标题</param>
        /// <param name="description">图文链接消息</param>
        /// <param name="url">图文链接消息被点击后跳转的链接</param>
        /// <param name="thumbUrl">[官方文档未给说明]</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendLinkAsync(string accessTokenOrAppId, string openId, string title, string description, string url, string thumbUrl, int timeOut = Config.TIME_OUT)
        {
            object data = new
            {
                touser = openId,
                msgtype = "link",
                link = new
                {
                    title = title,
                    description = description,
                    url = url,
                    thumb_url = thumbUrl
                }
            };

            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                return await CommonJsonSend.SendAsync(accessToken, UrlFormat_Send, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);

        }


        /// <summary>
        /// 【异步方法】发送小程序卡片
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="title">消息标题</param>
        /// <param name="pagePath">小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar</param>
        /// <param name="thumbMediaId">小程序消息卡片的封面， image类型的media_id，通过新增素材接口上传图片文件获得，建议大小为520*416</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendMiniProgramPageAsync(string accessTokenOrAppId, string openId, string title, string pagePath, string thumbMediaId, int timeOut = Config.TIME_OUT)
        {
            object data = new
            {
                touser = openId,
                msgtype = "miniprogrampage",
                miniprogrampage = new
                {
                    title = title,
                    pagepath = pagePath,
                    url = thumbMediaId,
                    thumb_media_id = thumbMediaId
                }
            };

            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                return await CommonJsonSend.SendAsync(accessToken, UrlFormat_Send, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】客服输入状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="touser">普通用户（openid）</param>
        /// <param name="typingStatus">"Typing"：对用户下发“正在输入"状态 "CancelTyping"：取消对用户的”正在输入"状态</param>
        /// <param name="businessId">添加 businessId 参数，则发送到子商户</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> GetTypingStatusAsync(string accessTokenOrAppId, string touser, string typingStatus, string businessId = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = GetTypingUrlFormat(businessId);

                var data = new
                {
                    touser = touser,
                    command = typingStatus,
                    businessid = businessId
                };

                var jsonSetting = new JsonSetting() { IgnoreNulls = true };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);

            }, accessTokenOrAppId);
        }

        #endregion
    }
}
