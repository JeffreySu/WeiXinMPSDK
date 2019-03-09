/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：AccessTokenResult.cs
    文件功能描述：access_token请求后的JSON返回格式
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20160810
    修改描述：v4.1.4 AccessTokenResult添加序列化标签
    
    修改标识：Senparc - 20170702
    修改描述：加入 IAccessTokenResult 接口

----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// GetToken请求后的JSON返回格式
    /// </summary>
    [Serializable]
    public class AccessTokenResult : WorkJsonResult, IAccessTokenResult
    {
        /// <summary>
        /// 获取到的凭证。长度为64至512个字节
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 凭证的有效时间（秒）
        /// </summary>
        public int expires_in { get; set; }
    }
}
