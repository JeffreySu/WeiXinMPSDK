/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：OAuthOpenAPI.cs
    文件功能描述：代公众号发起网页授权
    
    
    创建标识：Senparc - 20150330
----------------------------------------------------------------*/

/*
    官方文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1419318587&lang=zh_CN
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs.OAuthOpen
{
    public static class OAuthOpenAPI
    {
        /// <summary>
        /// 获取验证地址
        /// 返回说明：
        /// 用户允许授权后，将会重定向到redirect_uri的网址上，并且带上code, state以及appid（redirect_uri?code=CODE&state=STATE&appid=APPID）
        /// 若用户禁止授权，则重定向后不会带上code参数，仅会带上state参数（redirect_uri?state=STATE）
        /// </summary>
        /// <param name="appId">公众号的appid</param>
        /// <param name="redirectUrl">重定向地址，需要urlencode，这里填写的应是服务开发方的回调地址</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写任意参数值，最多128字节</param>
        /// <param name="scope">授权作用域，拥有多个作用域用逗号（,）分隔</param>
        /// <param name="componentAppId">服务方的appid，在申请创建公众号服务成功后，可在公众号服务详情页找到</param>
        /// <param name="responseType">填code</param>
        /// <returns></returns>
        public static string GetOpenAuthorizeUrl(string appId, string redirectUrl, string state, OAuthScope scope,string componentAppId, string responseType = "code")
        {
            /* 在确保微信公众账号拥有授权作用域（scope参数）的权限的前提下（一般而言，已微信认证的服务号拥有snsapi_base和snsapi_userinfo），
             * 使用微信客户端打开以下链接（严格按照以下格式，包括顺序和大小写，并请将参数替换为实际内容）
             * 若提示“该链接无法访问”，请检查参数是否填写错误，是否拥有scope参数对应的授权作用域权限。
             */

            var url =
                string.Format(
                    "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}&component_appid={5}#wechat_redirect",
                    appId, redirectUrl.UrlEncode(), responseType, scope, state, componentAppId);
            
            return url;
        }
    }
}
