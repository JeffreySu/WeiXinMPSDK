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
    文件功能描述：小程序客服接口
    
    
    创建标识：Senparc - 20180815
    


----------------------------------------------------------------*/

/* 
   API地址：https://developers.weixin.qq.com/miniprogram/dev/api/custommsg/conversation.html
*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs
{

    /// <summary>
    /// 小程序客服接口
    /// </summary>
    public class CustomApi
    {
        #region 同步方法

        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="content">文本消息内容</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "CustomApi.SendText", true)]
        public static WxJsonResult SendText(string accessTokenOrAppId, string openId, string content,
            int timeOut = Config.TIME_OUT)
        {
            return Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendText(accessTokenOrAppId, openId, content,
             timeOut);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="mediaId">发送的图片的媒体ID，通过新增素材接口上传图片文件获得。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "CustomApi.SendImage", true)]
        public static WxJsonResult SendImage(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendImage(accessTokenOrAppId, openId, mediaId, timeOut);
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
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "CustomApi.SendLink", true)]
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
                return CommonJsonSend.Send(accessToken, Senparc.Weixin.MP.AdvancedAPIs.CustomApi.UrlFormat, data, timeOut: timeOut);

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
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "CustomApi.SendMiniProgramPage", true)]
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
                return CommonJsonSend.Send(accessToken, Senparc.Weixin.MP.AdvancedAPIs.CustomApi.UrlFormat, data, timeOut: timeOut);

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
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "CustomApi.SendTextAsync", true)]
        public static async Task<WxJsonResult> SendTextAsync(string accessTokenOrAppId, string openId, string content,
            int timeOut = Config.TIME_OUT)
        {
            return await Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendTextAsync(accessTokenOrAppId, openId, content,
             timeOut).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="mediaId">发送的图片的媒体ID，通过新增素材接口上传图片文件获得。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "CustomApi.SendImageAsync", true)]
        public static async Task<WxJsonResult> SendImageAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendImageAsync(accessTokenOrAppId, openId, mediaId, timeOut).ConfigureAwait(false);
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
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "CustomApi.SendLinkAsnyc", true)]
        public static async Task<WxJsonResult> SendLinkAsnyc(string accessTokenOrAppId, string openId, string title, string description, string url, string thumbUrl, int timeOut = Config.TIME_OUT)
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
                return await CommonJsonSend.SendAsync(accessToken, Senparc.Weixin.MP.AdvancedAPIs.CustomApi.UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

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
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "CustomApi.SendMiniProgramPageAsync", true)]
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
                return await CommonJsonSend.SendAsync(accessToken, Senparc.Weixin.MP.AdvancedAPIs.CustomApi.UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
