#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：UrlApi.cs
    文件功能描述：UrlApi 微信接口封装


    创建标识：Senparc - 20160707

    修改标识：Senparc - 20160621
    修改描述：修改命名空间
              其改为Senparc.Weixin.MP.AdvancedAPIs

    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Url;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 长短链接接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount,true)]
    public class UrlApi
    {
        /*
        接口地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1443433600&token=&lang=zh_CN
                    将一条长链接转成短链接。
        主要使用场景： 开发者用于生成二维码的原链接（商品、支付二维码等）太长导致扫码速度和成功率下降，将原长链接通过此接口转成短链接再生成二维码将大大提升扫码速度和成功率。
        */
        #region 同步方法
        
        
        ///  <summary>
        /// 将一条长链接转成短链接。
        ///  </summary>
        ///  <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///  <param name="action">此处填long2short，代表长链接转短链接</param>
        ///  <param name="longUrl">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        /// <param name="timeOut">请求超时时间</param>
        public static ShortUrlResult ShortUrl(string accessTokenOrAppId, string action, string longUrl, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/shorturl?access_token={0}";
                var data = new
                {
                    action = action,
                    long_url = longUrl
                };
                return CommonJsonSend.Send<ShortUrlResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 将不超过 4KB 的长信息转换为短 key
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="longData">待转换的长信息，最大 4KB。</param>
        /// <param name="expireSeconds">短 Key 有效期（秒）。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GenerateShortenJsonResult GenerateShorten(string accessTokenOrAppId, string longData,
            int expireSeconds = 2592000, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/shorten/gen?access_token={0}";
                var data = new
                {
                    long_data = longData,
                    expire_seconds = expireSeconds
                };

                return CommonJsonSend.Send<GenerateShortenJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 使用短 key 还原长信息
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="shortKey">短 Key。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static FetchShortenJsonResult FetchShorten(string accessTokenOrAppId, string shortKey, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/shorten/fetch?access_token={0}";
                return CommonJsonSend.Send<FetchShortenJsonResult>(accessToken, urlFormat,
                    new { short_key = shortKey }, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
         ///  <summary>
        /// 将一条长链接转成短链接。
        ///  </summary>
        ///  <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///  <param name="action">此处填long2short，代表长链接转短链接</param>
        ///  <param name="longUrl">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        /// <param name="timeOut">请求超时时间</param>
        public static async Task<ShortUrlResult> ShortUrlAsync(string accessTokenOrAppId, string action, string longUrl, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/shorturl?access_token={0}";
                var data = new
                {
                    action = action,
                    long_url = longUrl
                };
                return await Senparc.Weixin .CommonAPIs .CommonJsonSend.SendAsync<ShortUrlResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】将不超过 4KB 的长信息转换为短 key
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="longData">待转换的长信息，最大 4KB。</param>
        /// <param name="expireSeconds">短 Key 有效期（秒）。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GenerateShortenJsonResult> GenerateShortenAsync(string accessTokenOrAppId, string longData,
            int expireSeconds = 2592000, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/shorten/gen?access_token={0}";
                var data = new
                {
                    long_data = longData,
                    expire_seconds = expireSeconds
                };

                return await CommonJsonSend.SendAsync<GenerateShortenJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】使用短 key 还原长信息
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="shortKey">短 Key。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<FetchShortenJsonResult> FetchShortenAsync(string accessTokenOrAppId, string shortKey, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/shorten/fetch?access_token={0}";
                return await CommonJsonSend.SendAsync<FetchShortenJsonResult>(accessToken, urlFormat,
                    new { short_key = shortKey }, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
