﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：OAuthAPI.cs
    文件功能描述：OAuth
    
    
    创建标识：Senparc - 20150812
    
    修改标识：Senparc - 20150726
    修改描述：修改GetAuthorizeUrl()方法
    
----------------------------------------------------------------*/

/*
    官方文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1419318590&token=cf4a37b85bce34cbb0fcae566d61c4aa71c593b7&lang=zh_CN
 */

using System.Linq;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Open.CommonAPIs;

namespace Senparc.Weixin.Open.OAuth
{
    /// <summary>
    /// 代公众号发起网页授权
    /// </summary>
    public static class OAuthApi
    {
        /// <summary>
        /// 获取验证地址
        /// </summary>
        /// <param name="appId">公众号的appid</param>
        /// <param name="componentAppId">第三方平台的appid</param>
        /// <param name="redirectUrl">重定向地址，需要urlencode，这里填写的应是服务开发方的回调地址</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写任意参数值，最多128字节</param>
        /// <param name="scope">授权作用域，拥有多个作用域用逗号（,）分隔。此处暂时只放一作用域。</param>
        /// <param name="responseType">默认，填code</param>
        /// <returns></returns>
        public static string GetAuthorizeUrl(string appId, string componentAppId, string redirectUrl, string state, OAuthScope[] scopes, string responseType = "code")
        {
            //此URL比MP中的对应接口多了&component_appid=component_appid参数
            var url =
                string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}&component_appid={5}#wechat_redirect",
                                appId, redirectUrl.UrlEncode(), responseType, string.Join(",", scopes.Select(z => z.ToString())), state, componentAppId);

            /* 这一步发送之后，客户会得到授权页面，无论同意或拒绝，都会返回redirectUrl页面。
             * 如果用户同意授权，页面将跳转至 redirect_uri?code=CODE&state=STATE&appid=APPID。这里的code用于换取access_token（和通用接口的access_token不通用）
             * 若用户禁止授权，则重定向后不会带上code参数，仅会带上state参数redirect_uri?state=STATE
             */
            return url;
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="componentAppid">服务开发方的appid</param>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="code">GetAuthorizeUrl()接口返回的code</param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        public static OAuthAccessTokenResult GetAccessToken(string appId, string componentAppid, string componentAccessToken, string code, string grantType = "authorization_code")
        {
            var url =
                string.Format("https://api.weixin.qq.com/sns/oauth2/component/access_token?appid={0}&code={1}&grant_type={2}&component_appid={3}&component_access_token={4}",
                                appId, code, grantType, componentAppid, componentAccessToken);

            /* 期望返回：
            {
            "access_token":"ACCESS_TOKEN",
            "expires_in":7200,
            "refresh_token":"REFRESH_TOKEN",
            "openid":"OPENID",
            "scope":"SCOPE"
            }
            
            出错返回：{"errcode":40029,"errmsg":"invalid code"}
            */
            return CommonJsonSend.Send<OAuthAccessTokenResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="refreshToken">填写通过access_token获取到的refresh_token参数</param>
        /// <param name="componentAccessToken"></param>
        /// <param name="grantType"></param>
        /// <param name="componentAppid"></param>
        /// <returns></returns>
        public static OAuthAccessTokenResult RefreshToken(string appId, string refreshToken, string componentAppid, string componentAccessToken, string grantType = "refresh_token")
        {
            var url = string.Format("https://api.weixin.qq.com/sns/oauth2/component/refresh_token?appid={0}&grant_type={1}&component_appid={2}&component_access_token={3}&refresh_token={4}",
                                appId, grantType, componentAppid, componentAccessToken, refreshToken);

            return CommonJsonSend.Send<OAuthAccessTokenResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static OAuthUserInfo GetUserInfo(string accessToken, string openId, Language lang = Language.zh_CN)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang={2}", accessToken, openId, lang);
            /*
             期望返回：{    "openid":" OPENID",    " nickname": NICKNAME,    "sex":"1",    "province":"PROVINCE"    "city":"CITY",    "country":"COUNTRY",     "headimgurl":    "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46",  "privilege":[ "PRIVILEGE1" "PRIVILEGE2"     ],     "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL" }
             错误时微信会返回JSON数据包如下（示例为openid无效）:{"errcode":40003,"errmsg":" invalid openid "}
             */
            return CommonJsonSend.Send<OAuthUserInfo>(null, url, null, CommonJsonSendType.GET);
        }

        //下面的方法在MP中有提供，开放平台的官方文档未提及
        ///// <summary>
        ///// 检验授权凭证（access_token）是否有效
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="openId">用户的唯一标识</param>
        ///// <returns></returns>
        //public static WxJsonResult Auth(string accessToken, string openId)
        //{
        //    var url = string.Format("https://api.weixin.qq.com/sns/auth?access_token={0}&openid={1}", accessToken, openId);
        //    return CommonJsonSend.Send<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
        //}
    }
}
