﻿/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：OAuthAccessTokenResult.cs
    文件功能描述：获取OAuth AccessToken的结果
    
    
    创建标识：Senparc - 20150712
    
    修改标识：Senparc - 20161216
    修改描述：v2.3.5 添加序列化特性

    修改标识：Senparc - 20241129
    修改描述：v4.21.8 返回值添加参数 is_snapshotuser 和 unionid
    
----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.OAuthAPIs
{
    /// <summary>
    /// 获取OAuth AccessToken的结果
    /// 如果错误，返回结果{"errcode":40029,"errmsg":"invalid code"}
    /// </summary>
    [Serializable]
    public class OAuthAccessTokenResult : WxJsonResult
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
        /// <summary>
        /// 用户统一标识（针对一个微信开放平台账号下的应用，同一用户的 unionid 是唯一的），只有当scope为"snsapi_userinfo"时返回
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 是否为快照页模式虚拟账号，只有当用户是快照页模式虚拟账号是返回，值为1
        /// </summary>
        public int? is_snapshotuser { get; set; }
    }
}
