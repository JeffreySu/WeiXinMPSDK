/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ProviderTokenResult.cs
    文件功能描述：获取应用提供商凭证返回格式
    
    
    创建标识：Senparc - 20150325
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 获取应用提供商凭证返回格式
    /// </summary>
    public class ProviderTokenResult : QyJsonResult
    {
        /// <summary>
        /// 服务提供商的accesstoken，可用于用户授权登录信息查询接口
        /// </summary>
        public string provider_access_token { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }
}
