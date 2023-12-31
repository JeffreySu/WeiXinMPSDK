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
    
    文件名：CommonApi.cs
    文件功能描述：通用接口(用于和微信服务器通讯，一般不涉及自有网站服务器的通讯)
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150330
    修改描述：获取调用微信JS接口的临时票据中的AccessToken添加缓存
    
    修改标识：Senparc - 20150401
    修改描述：添加公众号第三方平台获取授权码接口
    
    修改标识：Senparc - 20150430
    修改描述：公众号第三方平台分离
 
    修改标识：Senparc - 20160721
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20161110
    修改描述：完善GetTicket系列方法备注

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20180928
    修改描述：添加Clear_quota

    修改标识：Senparc - 20191206
    修改描述：CommonApi.Token() 方法设置异常抛出机制

    修改标识：wtujvk - 20200416
    修改描述：v16.10.500 提供详细 CommonApi.GetToken() 报错信息（包括白名单异常）

    修改标识：dupeng0811 - 20230520
    修改描述：v16.18.11 新增“获取稳定版接口调用凭据”接口

----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/index.php?title=%E6%8E%A5%E5%8F%A3%E6%96%87%E6%A1%A3&oldid=103
    
 */

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// 通用接口
    /// 通用接口用于和微信服务器通讯，一般不涉及自有网站服务器的通讯
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, true)]
    public partial class CommonApi
    {
        #region 同步方法

        /// <summary>
        /// 获取凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static AccessTokenResult GetToken(string appid, string secret, string grant_type = "client_credential")
        {
            //注意：此方法不能再使用ApiHandlerWapper.TryCommonApi()，否则会循环
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type.AsUrlData(), appid.AsUrlData(), secret.AsUrlData());

            AccessTokenResult result = Get.GetJson<AccessTokenResult>(CommonDI.CommonSP, url);//此处为最原始接口，不再使用重试获取的封装

            if (Config.ThrownWhenJsonResultFaild && result.errcode != ReturnCode.请求成功)
            {
                throw new ErrorJsonResultException(
                    string.Format("微信请求发生错误（CommonApi.GetToken）！错误代码：{0}，说明：{1}",
                        (int)result.errcode, result.errmsg), null, result);
            }

            return result;
        }   
        
        /// <summary>
        /// 获取稳定版接口调用凭据
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">账号唯一凭证，即 AppID，可在「微信公众平台 - 设置 - 开发设置」页中获得。（需要已经成为开发者，且帐号没有异常状态）</param>
        /// <param name="secret">帐号唯一凭证密钥，即 AppSecret，获取方式同 appid</param>
        /// <param name="force_refresh">默认使用 false。
        /// 1. force_refresh = false 时为普通调用模式，access_token 有效期内重复调用该接口不会更新 access_token；
        /// 2. 当force_refresh = true 时为强制刷新模式，会导致上次获取的 access_token 失效，并返回新的 access_token</param>
        /// <returns></returns>
        public static AccessTokenResult GetStableAccessToken(string appid, string secret, string grant_type = "client_credential",bool force_refresh=false)
        {
            var url = Config.ApiMpHost + "/cgi-bin/stable_token";
            var data = new
            {
                grant_type= "client_credential",
                appid= appid,
                secret= secret,
                force_refresh= false
            };
            AccessTokenResult result = CommonJsonSend.Send<AccessTokenResult>(null, url, data, CommonJsonSendType.POST);
            if (Config.ThrownWhenJsonResultFaild && result.errcode != ReturnCode.请求成功)
            {
                throw new ErrorJsonResultException(
                    string.Format("微信请求发生错误（CommonApi.GetStableAccessToken）！错误代码：{0}，说明：{1}",
                        (int)result.errcode, result.errmsg), null, result);
            }

            return result;
        }

        //已经迁移到 UserApi 下
        ///// <summary>
        ///// 用户信息接口
        ///// </summary>
        ///// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///// <param name="openId"></param>
        ///// <returns></returns>
        //public static WeixinUserInfoResult GetUserInfo(string accessTokenOrAppId, string openId)
        //{
        //    return ApiHandlerWapper.TryCommonApi(accessToken =>
        //    {
        //        var url = string.Format(Config.ApiMpHost + "/cgi-bin/user/info?access_token={0}&openid={1}",
        //                                accessToken.AsUrlData(), openId.AsUrlData());
        //        WeixinUserInfoResult result = CommonJsonSend.Send<WeixinUserInfoResult>(null, url, null, CommonJsonSendType.GET);
        //        return result;

        //    }, accessTokenOrAppId);
        //}


        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicket(string appId, string secret, string type = "jsapi")
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, secret);
            return GetTicketByAccessToken(accessToken, type);
        }

        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicketByAccessToken(string accessTokenOrAppId, string type = "jsapi")
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                        accessToken.AsUrlData(), type.AsUrlData());

                JsApiTicketResult result = CommonJsonSend.Send<JsApiTicketResult>(null, url, null, CommonJsonSendType.GET);
                return result;

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取微信服务器的ip段
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static GetCallBackIpResult GetCallBackIp(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/getcallbackip?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetCallBackIpResult>(null, url, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 公众号调用或第三方平台帮公众号调用对公众号的所有api调用（包括第三方帮其调用）次数进行清零
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="appId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ClearQuota(string accessTokenOrAppId, string appId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/clear_quota?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    appid = appId
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】获取凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static async Task<AccessTokenResult> GetTokenAsync(string appid, string secret, string grant_type = "client_credential")
        {
            //注意：此方法不能再使用ApiHandlerWapper.TryCommonApi()，否则会循环
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type.AsUrlData(), appid.AsUrlData(), secret.AsUrlData());

            AccessTokenResult result = await Get.GetJsonAsync<AccessTokenResult>(CommonDI.CommonSP, url);//此处为最原始接口，不再使用重试获取的封装

            if (Config.ThrownWhenJsonResultFaild && result.errcode != ReturnCode.请求成功)
            {
                throw new ErrorJsonResultException(
                    string.Format("微信请求发生错误（CommonApi.GetToken）！错误代码：{0}，说明：{1}",
                        (int)result.errcode, result.errmsg), null, result);
            }

            return result;
        }
        
        /// <summary>
        /// 【异步方法】获取稳定版接口调用凭据
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">账号唯一凭证，即 AppID，可在「微信公众平台 - 设置 - 开发设置」页中获得。（需要已经成为开发者，且帐号没有异常状态）</param>
        /// <param name="secret">帐号唯一凭证密钥，即 AppSecret，获取方式同 appid</param>
        /// <param name="force_refresh">默认使用 false。
        /// 1. force_refresh = false 时为普通调用模式，access_token 有效期内重复调用该接口不会更新 access_token；
        /// 2. 当force_refresh = true 时为强制刷新模式，会导致上次获取的 access_token 失效，并返回新的 access_token</param>
        /// <returns></returns>
        public static async Task<AccessTokenResult> GetStableAccessTokenAsync(string appid, string secret, string grant_type = "client_credential",bool force_refresh=false)
        {
            var url = Config.ApiMpHost + "/cgi-bin/stable_token";
            var data = new
            {
                grant_type= "client_credential",
                appid= appid,
                secret= secret,
                force_refresh= false
            };

            AccessTokenResult result = await CommonJsonSend.SendAsync<AccessTokenResult>(null, url, data, CommonJsonSendType.POST);
            if (Config.ThrownWhenJsonResultFaild && result.errcode != ReturnCode.请求成功)
            {
                throw new ErrorJsonResultException(string.Format("微信请求发生错误（CommonApi.GetStableAccessTokenAsync）！错误代码：{0}，说明：{1}",
                        (int)result.errcode, result.errmsg), null, result);
            }

            return result;
        }

        //已经迁移到 UserApi 下
        ///// <summary>
        ///// 【异步方法】用户信息接口
        ///// </summary>
        ///// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///// <param name="openId"></param>
        ///// <returns></returns>
        //public static async Task<WeixinUserInfoResult> GetUserInfoAsync(string accessTokenOrAppId, string openId)
        //{
        //    return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
        //   {
        //       var url = string.Format(Config.ApiMpHost + "/cgi-bin/user/info?access_token={0}&openid={1}",
        //                               accessToken.AsUrlData(), openId.AsUrlData());
        //       var result = CommonJsonSend.SendAsync<WeixinUserInfoResult>(null, url, null, CommonJsonSendType.GET);
        //       return await result.ConfigureAwait(false);

        //   }, accessTokenOrAppId).ConfigureAwait(false);
        //}


        /// <summary>
        /// 【异步方法】获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetTicketAsync(string appId, string secret, string type = "jsapi")
        {
            var accessToken = await AccessTokenContainer.TryGetAccessTokenAsync(appId, secret).ConfigureAwait(false);
            return GetTicketByAccessToken(accessToken, type);
        }

        /// <summary>
        /// 【异步方法】获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetTicketByAccessTokenAsync(string accessTokenOrAppId, string type = "jsapi")
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                        accessToken.AsUrlData(), type.AsUrlData());

                var result = CommonJsonSend.SendAsync<JsApiTicketResult>(null, url, null, CommonJsonSendType.GET);
                return await result.ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取微信服务器的ip段
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static async Task<GetCallBackIpResult> GetCallBackIpAsync(string accessTokenOrAppId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/getcallbackip?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<GetCallBackIpResult>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】公众号调用或第三方平台帮公众号调用对公众号的所有api调用（包括第三方帮其调用）次数进行清零
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="appId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ClearQuotaAsync(string accessTokenOrAppId, string appId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/clear_quota?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    appid = appId
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

    }
}
