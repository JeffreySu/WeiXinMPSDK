#region Apache License Version 2.0
    /*----------------------------------------------------------------

    Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright(C) 2017 Senparc

    文件名：OAuthAPI.cs
    文件功能描述：OAuth


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法
----------------------------------------------------------------*/

/*
    官方文档：http://mp.weixin.qq.com/wiki/index.php?title=%E7%BD%91%E9%A1%B5%E6%8E%88%E6%9D%83%E8%8E%B7%E5%8F%96%E7%94%A8%E6%88%B7%E5%9F%BA%E6%9C%AC%E4%BF%A1%E6%81%AF#.E7.AC.AC.E4.B8.80.E6.AD.A5.EF.BC.9A.E7.94.A8.E6.88.B7.E5.90.8C.E6.84.8F.E6.8E.88.E6.9D.83.EF.BC.8C.E8.8E.B7.E5.8F.96code
 */

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public static class OAuthApi
    {
        #region 同步方法
        /*此接口不提供异步方法*/
        /// <summary>
        /// 获取验证地址
        /// </summary>
        /// <param name="appId">公众号的唯一标识</param>
        /// <param name="redirectUrl">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节</param>
        /// <param name="scope">应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息）</param>
        /// <param name="responseType">返回类型，请填写code（或保留默认）</param>
        /// <param name="addConnectRedirect">加上后可以解决40029-invalid code的问题（测试中）</param>
        /// <returns></returns>
        public static string GetAuthorizeUrl(string appId, string redirectUrl, string state, OAuthScope scope, string responseType = "code", bool addConnectRedirect = true)
        {
            var url =
                string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}{5}#wechat_redirect",
                                appId.AsUrlData(), redirectUrl.AsUrlData(), responseType.AsUrlData(), scope.ToString("g").AsUrlData(), state.AsUrlData(),
                                addConnectRedirect ? "&connect_redirect=1" : "");

            /* 这一步发送之后，客户会得到授权页面，无论同意或拒绝，都会返回redirectUrl页面。
             * 如果用户同意授权，页面将跳转至 redirect_uri/?code=CODE&state=STATE。这里的code用于换取access_token（和通用接口的access_token不通用）
             * 若用户禁止授权，则重定向后不会带上code参数，仅会带上state参数redirect_uri?state=STATE
             */
            return url;
        }

        /// <summary>
        /// 获取AccessToken（OAuth专用）
        /// </summary>
        /// <param name="appId">公众号的唯一标识</param>
        /// <param name="secret">公众号的appsecret</param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="grantType">填写为authorization_code（请保持默认参数）</param>
        /// <returns></returns>
        public static OAuthAccessTokenResult GetAccessToken(string appId, string secret, string code, string grantType = "authorization_code")
        {
            var url =
                string.Format(Config.ApiMpHost + "/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type={3}",
                                appId.AsUrlData(), secret.AsUrlData(), code.AsUrlData(), grantType.AsUrlData());

            return CommonJsonSend.Send<OAuthAccessTokenResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 刷新（OAuth专用）access_token（如果需要）
        /// </summary>
        /// <param name="appId">公众号的唯一标识</param>
        /// <param name="refreshToken">填写通过access_token获取到的refresh_token参数</param>
        /// <param name="grantType">填写refresh_token</param>
        /// <returns></returns>
        public static RefreshTokenResult RefreshToken(string appId, string refreshToken, string grantType = "refresh_token")
        {
            var url =
                string.Format(Config.ApiMpHost + "/sns/oauth2/refresh_token?appid={0}&grant_type={1}&refresh_token={2}",
                                appId.AsUrlData(), grantType.AsUrlData(), refreshToken.AsUrlData());

            return CommonJsonSend.Send<RefreshTokenResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="oauthAccessToken">调用接口凭证（OAuth专用）</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static OAuthUserInfo GetUserInfo(string oauthAccessToken, string openId, Language lang = Language.zh_CN)
        {
            var url = string.Format(Config.ApiMpHost + "/sns/userinfo?access_token={0}&openid={1}&lang={2}", oauthAccessToken.AsUrlData(), openId.AsUrlData(), lang.ToString("g").AsUrlData());
            return CommonJsonSend.Send<OAuthUserInfo>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 检验授权凭证（access_token）是否有效（OAuth专用）
        /// </summary>
        /// <param name="oauthAccessToken">调用接口凭证（OAuth专用）</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <returns></returns>
        public static WxJsonResult Auth(string oauthAccessToken, string openId)
        {
            var url = string.Format(Config.ApiMpHost + "/sns/auth?access_token={0}&openid={1}", oauthAccessToken.AsUrlData(), openId.AsUrlData());
            return CommonJsonSend.Send<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】获取AccessToken（OAuth专用）
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        public static async Task<OAuthAccessTokenResult> GetAccessTokenAsync(string appId, string secret, string code, string grantType = "authorization_code")
        {
            var url =
                string.Format(Config.ApiMpHost + "/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type={3}",
                                appId.AsUrlData(), secret.AsUrlData(), code.AsUrlData(), grantType.AsUrlData());

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<OAuthAccessTokenResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        ///【异步方法】刷新（OAuth专用）access_token（如果需要）
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="refreshToken">填写通过access_token获取到的refresh_token参数</param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        public static async Task<OAuthAccessTokenResult> RefreshTokenAsync(string appId, string refreshToken, string grantType = "refresh_token")
        {
            var url =
                string.Format(Config.ApiMpHost + "/sns/oauth2/refresh_token?appid={0}&grant_type={1}&refresh_token={2}",
                                appId.AsUrlData(), grantType.AsUrlData(), refreshToken.AsUrlData());

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<OAuthAccessTokenResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        ///【异步方法】 获取用户基本信息
        /// </summary>
        /// <param name="oauthAccessToken">调用接口凭证（OAuth专用）</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static async Task<OAuthUserInfo> GetUserInfoAsync(string oauthAccessToken, string openId, Language lang = Language.zh_CN)
        {
            var url = string.Format(Config.ApiMpHost + "/sns/userinfo?access_token={0}&openid={1}&lang={2}", oauthAccessToken.AsUrlData(), openId.AsUrlData(), lang.ToString("g").AsUrlData());
            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<OAuthUserInfo>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 【异步方法】检验授权凭证（access_token）是否有效（OAuth专用）
        /// </summary>
        /// <param name="oauthAccessToken">调用接口凭证（OAuth专用）</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> AuthAsync(string oauthAccessToken, string openId)
        {
            var url = string.Format(Config.ApiMpHost + "/sns/auth?access_token={0}&openid={1}", oauthAccessToken.AsUrlData(), openId.AsUrlData());
            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
        }
        #endregion
#endif
    }
}
