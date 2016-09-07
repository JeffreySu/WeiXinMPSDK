/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：GetAuthorizerInfoResult.cs
    文件功能描述：获取授权方的账户信息返回结果


    创建标识：Senparc - 20150726

    修改标识：Senparc - 20150402
    修改描述：添加序列化特性[Serializable]

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 获取授权方的账户信息返回结果
    /// </summary>
    [Serializable]
    public class GetAuthorizerInfoResult : WxJsonResult
    {
        /// <summary>
        /// 授权方信息
        /// </summary>
        public AuthorizerInfo authorizer_info { get; set; }
        /// <summary>
        /// 授权信息
        /// </summary>
        public AuthorizationInfo authorization_info { get; set; }
    }

    [Serializable]
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
        /// <summary>
        /// 二维码图片的URL，开发者最好自行也进行保存
        /// </summary>
        public string qrcode_url { get; set; }

        public BusinessInfo business_info { get; set; }


    }

    [Serializable]
    public class ServiceTypeInfo
    {
        public ServiceType id { get; set; }
    }

    [Serializable]
    public class VerifyTypeInfo
    {
        public VerifyType id { get; set; }
    }

    [Serializable]
    public class BusinessInfo
    {
        public int open_pay { get; set; }
        public int open_shake { get; set; }
        public int open_card { get; set; }
        public int open_store { get; set; }
    }

    //[Obsolete("此类已过期，请使用AuthorizationInfo")]
    //public class AuthorizerInfo_AuthorizationInfo
    //{
    //    /// <summary>
    //    /// 授权方appid
    //    /// </summary>
    //    public string authorizer_appid { get; set; }


    //    /// <summary>
    //    /// 授权方令牌（在授权的公众号具备API权限时，才有此返回值）
    //    /// </summary>
    //    public string authorizer_access_token { get; set; }
    //    /// <summary>
    //    /// 有效期（在授权的公众号具备API权限时，才有此返回值）
    //    /// </summary>
    //    public int expires_in { get; set; }
    //    /// <summary>
    //    /// 刷新令牌（在授权的公众号具备API权限时，才有此返回值），刷新令牌主要用于公众号第三方平台获取和刷新已授权用户的access_token，只会在授权时刻提供，请妥善保存。 一旦丢失，只能让用户重新授权，才能再次拿到新的刷新令牌
    //    /// </summary>
    //    public string authorizer_refresh_token { get; set; }

    //    /// <summary>
    //    /// 公众号授权给开发者的权限集列表（请注意，当出现用户已经将消息与菜单权限集授权给了某个第三方，再授权给另一个第三方时，由于该权限集是互斥的，后一个第三方的授权将去除此权限集，开发者可以在返回的func_info信息中验证这一点，避免信息遗漏），
    //    /// 1到9分别代表：
    //    /// 消息与菜单权限集
    //    /// 用户管理权限集
    //    /// 帐号管理权限集
    //    /// 网页授权权限集
    //    /// 微信小店权限集
    //    /// 多客服权限集
    //    /// 业务通知权限集
    //    /// 微信卡券权限集
    //    /// 微信扫一扫权限集
    //    /// </summary>
    //    public List<AuthorizerInfo_FuncInfo> func_info { get; set; }
    //}

    //public class AuthorizerInfo_FuncInfo
    //{
    //    public AuthorizerInfo_FuncscopeCategory funcscope_category { get; set; }
    //}

    //public class AuthorizerInfo_FuncscopeCategory
    //{
    //    public FuncscopeCategory id { get; set; }
    //}
}

#region 实际返回json，文档中的json结构是错误的
//{"authorizer_info":
    //{"nick_name":"微微嗨测试二",
    //"head_img":"http:\/\/wx.qlogo.cn\/mmopen\/BUORmFJiapJ3LBJ6HnD0wnKMsVaP1W9jEOEZRzBSn8ZXs9aicxBxibaIdibxItbtqgj0sU4QfIRCAt8nxReDHRKjbVbUGPNq7w1B\/0",
    //"service_type_info":{"id":2},
    //"verify_type_info":{"id":-1},
    //"user_name":"gh_df67ac2cc491",
    //"alias":"WeiWeiHiTest2",
    //"qrcode_url":"http:\/\/mmbiz.qpic.cn\/mmbiz\/FVYUzJtc8bscHJYzg6Re85MP3VyCmibYe9Nes2npCiacqDbygnmSoODRktkV6BId92tvsapE83EELHwu06uNrIAA\/0",
    //"business_info":{"open_pay":0,"open_shake":0,"open_scan":0,"open_card":0,"open_store":0}
//},
//"authorization_info":
    //{"authorizer_appid":"wx7cfd56c9f047bf51",
        //"func_info":[{"funcscope_category":{"id":1}},
        //{"funcscope_category":{"id":2}},
        //{"funcscope_category":{"id":3}},
        //{"funcscope_category":{"id":4}},
        //{"funcscope_category":{"id":5}},
        //{"funcscope_category":{"id":6}},
        //{"funcscope_category":{"id":7}},
        //{"funcscope_category":{"id":8}},
        //{"funcscope_category":{"id":11}},
        //{"funcscope_category":{"id":12}},
        //{"funcscope_category":{"id":13}},
        //{"funcscope_category":{"id":10}}]
    //}
//}
#endregion