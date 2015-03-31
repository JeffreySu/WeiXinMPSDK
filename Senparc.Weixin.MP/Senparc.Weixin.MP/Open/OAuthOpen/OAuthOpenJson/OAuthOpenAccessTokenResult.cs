/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：OAuthOpenAccessTokenResult.cs
    文件功能描述：代公众号发起网页授权获取OAuth AccessToken的结果
    
    
    创建标识：Senparc - 20150331
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.OAuth
{
    /// <summary>
    /// 代公众号发起网页授权获取OAuth AccessToken的结果
    /// 如果错误，返回结果{"errcode":40029,"errmsg":"invalid code"}
    /// </summary>
    public class OAuthOpenAccessTokenResult : WxJsonResult
    {
        /// <summary>
        /// 接口调用凭证
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// 授权用户唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        public string scope { get; set; }
    }
}
