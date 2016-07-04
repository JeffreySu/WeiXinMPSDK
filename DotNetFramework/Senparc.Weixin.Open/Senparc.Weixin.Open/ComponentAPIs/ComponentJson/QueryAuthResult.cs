/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：QueryAuthResult.cs
    文件功能描述：使用授权码换取公众号的授权信息返回结果


    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 使用授权码换取公众号的授权信息返回结果
    /// </summary>
    [Serializable]
    public class QueryAuthResult : WxJsonResult
    {
        /// <summary>
        /// 授权信息
        /// </summary>
        public AuthorizationInfo authorization_info { get; set; }
    }

    /// <summary>
    /// 授权信息
    /// </summary>
    [Serializable]
    public class AuthorizationInfo
    {
        /// <summary>
        /// 授权方appid
        /// </summary>
        public string authorizer_appid { get; set; }
        /// <summary>
        /// 授权方令牌（在授权的公众号具备API权限时，才有此返回值）
        /// </summary>
        public string authorizer_access_token { get; set; }
        /// <summary>
        /// 有效期（在授权的公众号具备API权限时，才有此返回值）
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 刷新令牌（在授权的公众号具备API权限时，才有此返回值），刷新令牌主要用于公众号第三方平台获取和刷新已授权用户的access_token，只会在授权时刻提供，请妥善保存。 一旦丢失，只能让用户重新授权，才能再次拿到新的刷新令牌
        /// </summary>
        public string authorizer_refresh_token { get; set; }
        /// <summary>
        /// 公众号授权给开发者的权限集列表（请注意，当出现用户已经将消息与菜单权限集授权给了某个第三方，再授权给另一个第三方时，由于该权限集是互斥的，后一个第三方的授权将去除此权限集，开发者可以在返回的func_info信息中验证这一点，避免信息遗漏），
        /// 1到8分别代表：
        ///消息与菜单权限集
        ///用户管理权限集
        ///帐号管理权限集
        ///网页授权权限集
        ///微信小店权限集
        ///多客服权限集
        ///业务通知权限集
        ///微信卡券权限集
        /// </summary>
        public List<FuncscopeCategoryItem> func_info { get; set; }
    }

    [Serializable]
    public class FuncscopeCategoryItem
    {
        public AuthorizationInfo_FuncscopeCategory funcscope_category { get; set; }
    }

    [Serializable]
    public class AuthorizationInfo_FuncscopeCategory
    {
        public FuncscopeCategory id { get; set; }
    }
}
