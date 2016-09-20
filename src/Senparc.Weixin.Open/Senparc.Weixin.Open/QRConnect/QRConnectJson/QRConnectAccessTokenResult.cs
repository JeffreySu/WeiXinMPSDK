/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：QRConnectAccessTokenResult.cs
    文件功能描述：获取QRConnect AccessToken的结果
    
    
    创建标识：Senparc - 20150820

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.QRConnect
{
    /// <summary>
    /// 刷新access_token有效期返回结果
    /// </summary>
    public class RefreshAccessTokenResult : WxJsonResult
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

    /// <summary>
    /// 获取OAuth AccessToken的结果
    /// 如果错误，返回结果{"errcode":40029,"errmsg":"invalid code"}
    /// </summary>
    public class QRConnectAccessTokenResult : RefreshAccessTokenResult
    {
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string unionid { get; set; }
    }
}
