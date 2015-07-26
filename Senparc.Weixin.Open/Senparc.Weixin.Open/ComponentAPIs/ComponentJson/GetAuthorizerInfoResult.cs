/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：GetAuthorizerInfoResult.cs
    文件功能描述：获取授权方的账户信息返回结果
    
    
    创建标识：Senparc - 20150726
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.Entities;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 获取授权方的账户信息返回结果
    /// </summary>
    public class GetAuthorizerInfoResult : WxJsonResult
    {
        /// <summary>
        /// 授权方信息
        /// </summary>
        public AuthorizerInfo authorizer_info { get; set; }
        /// <summary>
        /// 二维码图片的URL，开发者最好自行也进行保存
        /// </summary>
        public string qrcode_url { get; set; }
        /// <summary>
        /// 授权信息
        /// </summary>
        public AuthorizerInfo_AuthorizationInfo authorization_info { get; set; }
    }

    public class AuthorizerInfo
    {
        /// <summary>
        /// 授权方昵称
        /// </summary>
        public string nick_name { get; set; }
        /// <summary>
        /// 授权方头像
        /// </summary>
        public string head_img { get; set; }
        /// <summary>
        /// 授权方公众号类型，0代表订阅号，1代表由历史老帐号升级后的订阅号，2代表服务号
        /// </summary>
        public ServiceTypeInfo service_type_info { get; set; }
        /// <summary>
        /// 授权方认证类型，-1代表未认证，0代表微信认证，1代表新浪微博认证，2代表腾讯微博认证，3代表已资质认证通过但还未通过名称认证，4代表已资质认证通过、还未通过名称认证，但通过了新浪微博认证，5代表已资质认证通过、还未通过名称认证，但通过了腾讯微博认证
        /// </summary>
        public VerifyTypeInfo verify_type_info { get; set; }
        /// <summary>
        /// 授权方公众号的原始ID
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 授权方公众号所设置的微信号，可能为空
        /// </summary>
        public string alias { get; set; }
    }

    public class ServiceTypeInfo
    {
        public ServiceType id { get; set; }
    }

    public class VerifyTypeInfo
    {
        public VerifyType id { get; set; }
    }

    public class AuthorizerInfo_AuthorizationInfo
    {
        /// <summary>
        /// 授权方appid
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 公众号授权给开发者的权限集列表（请注意，当出现用户已经将消息与菜单权限集授权给了某个第三方，再授权给另一个第三方时，由于该权限集是互斥的，后一个第三方的授权将去除此权限集，开发者可以在返回的func_info信息中验证这一点，避免信息遗漏），
        /// 1到9分别代表：
        /// 消息与菜单权限集
        /// 用户管理权限集
        /// 帐号管理权限集
        /// 网页授权权限集
        /// 微信小店权限集
        /// 多客服权限集
        /// 业务通知权限集
        /// 微信卡券权限集
        /// 微信扫一扫权限集
        /// </summary>
        public List<AuthorizerInfo_FuncInfo> func_info { get; set; }
    }

    public class AuthorizerInfo_FuncInfo
    {
        public AuthorizerInfo_FuncscopeCategory funcscope_category { get; set; }
    }

    public class AuthorizerInfo_FuncscopeCategory
    {
        public FuncscopeCategory id { get; set; }
    }
}
