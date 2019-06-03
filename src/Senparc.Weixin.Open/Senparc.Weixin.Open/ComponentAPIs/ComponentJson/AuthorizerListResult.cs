/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：AuthorizerListResult.cs
    文件功能描述：获取（刷新）授权公众号的令牌返回结果
    
    
    创建标识：mc7246 - 20190603

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 拉取所有已授权的帐号信息返回结果
    /// </summary>
    [Serializable]
    public class AuthorizerListResult : WxJsonResult
    {
        /// <summary>
        /// 授权的帐号总数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 当前查询的帐号基本信息列表
        /// </summary>
        public List<AuthorizerAccountInfo> list { get; set; }
    }

    //文档地址 https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/api/api_get_authorizer_list.html
    [Serializable]
    ///帐号基本信息
    public class AuthorizerAccountInfo
    {
        /// <summary>
        /// 已授权的 appid
        /// </summary>
        public string authorizer_appid { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// 授权的时间
        /// </summary>
        public int auth_time { get; set; }
    }
}