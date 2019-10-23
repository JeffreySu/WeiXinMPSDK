/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ProviderTokenResult.cs
    文件功能描述：获取应用提供商凭证返回格式
    
    
    创建标识：Senparc - 20150325

    修改标识：Senparc - 20160810
    修改描述：v4.1.4 ProviderTokenResult添加序列化标签

----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 获取应用提供商凭证返回格式
    /// </summary>
    [Serializable]
    public class ProviderTokenResult : WorkJsonResult
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
