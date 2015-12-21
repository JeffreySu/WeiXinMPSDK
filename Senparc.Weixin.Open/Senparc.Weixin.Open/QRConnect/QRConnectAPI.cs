﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：QRConnectAPI.cs
    文件功能描述：微信扫码登录
    
    
    创建标识：Senparc - 20150820
    
----------------------------------------------------------------*/

/*
    官方文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1419316505&token=&lang=zh_CN
 */

using System.Linq;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Open.CommonAPIs;

namespace Senparc.Weixin.Open.QRConnect
{
    /// <summary>
    /// 代公众号发起网页授权
    /// </summary>
    public static class QRConnectAPI
    {
        /// <summary>
        /// 微信扫码登录网页授权
        /// </summary>
        /// <param name="appId">第三方应用唯一标识</param>
        /// <param name="redirectUrl">重定向地址，需要进行UrlEncode</param>
        /// <param name="state">用于保持请求和回调的状态，授权请求后原样带回给第三方。该参数可用于防止csrf攻击（跨站请求伪造攻击），建议第三方带上该参数，可设置为简单的随机数加session进行校验</param>
        /// <param name="scopes">应用授权作用域，拥有多个作用域用逗号（,）分隔，网页应用目前仅填写snsapi_login即可</param>
        /// <param name="responseType">填code</param>
        /// <returns></returns>
        public static string GetQRConnectUrl(string appId, string redirectUrl, string state, OAuthScope[] scopes, string responseType = "code")
        {
            //此URL比MP中的对应接口多了&component_appid=component_appid参数
            var url =
                string.Format("https://open.weixin.qq.com/connect/qrconnect?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect",
                                appId, redirectUrl.UrlEncode(), responseType, string.Join(",", scopes.Select(z => z.ToString())), state);

            /* 这一步发送之后，客户会得到授权页面，无论同意或拒绝，都会返回redirectUrl页面。
             * 用户允许授权后，将会重定向到redirect_uri的网址上，并且带上code和state参数redirect_uri?code=CODE&state=STATE
             * 若用户禁止授权，则重定向后不会带上code参数，仅会带上state参数redirect_uri?state=STATE
             */
            return url;
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="appId">应用唯一标识，在微信开放平台提交应用审核通过后获得</param>
        /// <param name="appSecret">应用密钥AppSecret，在微信开放平台提交应用审核通过后获得</param>
        /// <param name="code">GetQRConnectUrl()接口返回的code</param>
        /// <param name="grantType">填authorization_code</param>
        /// <returns></returns>
        public static QRConnectAccessTokenResult GetAccessToken(string appId, string appSecret, string code, string grantType = "authorization_code")
        {
            var url =
                string.Format(
                    "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type={3}",
                    appId, appSecret, code, grantType);

            /* 期望返回：
            { 
            "access_token":"ACCESS_TOKEN", 
            "expires_in":7200, 
            "refresh_token":"REFRESH_TOKEN",
            "openid":"OPENID", 
            "scope":"SCOPE",
            "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
            }
            
            出错返回：{"errcode":40029,"errmsg":"invalid code"}
            */
            return CommonJsonSend.Send<QRConnectAccessTokenResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        /// <param name="appId">第三方应用唯一标识</param>
        /// <param name="refreshToken">填写通过access_token获取到的refresh_token参数</param>
        /// <param name="grantType">填refresh_token</param>
        /// <returns></returns>
        public static RefreshAccessTokenResult RefreshToken(string appId, string refreshToken, string grantType = "refresh_token")
        {
            var url =
                string.Format(
                    "https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type={1}&refresh_token={2}",
                    appId, grantType, refreshToken);

            return CommonJsonSend.Send<RefreshAccessTokenResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <returns></returns>
        public static QRConnectUserInfo GetUserInfo(string accessToken, string openId)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}", accessToken, openId);
            /*
             期望返回：{    "openid":" OPENID",    " nickname": "NICKNAME",    "sex":"1",    "province":"PROVINCE"    "city":"CITY",    "country":"COUNTRY",     "headimgurl":    "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46",  "privilege":[ "PRIVILEGE1" "PRIVILEGE2"     ],     "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL" }
             错误时微信会返回JSON数据包如下（示例为openid无效）:{"errcode":40003,"errmsg":" invalid openid "}
             */
            return CommonJsonSend.Send<QRConnectUserInfo>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 检验授权凭证（access_token）是否有效
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">用户的唯一标识</param>
        /// <returns></returns>
        public static WxJsonResult Auth(string accessToken, string openId)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/auth?access_token={0}&openid={1}", accessToken, openId);
            return CommonJsonSend.Send<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
        }
    }
}
