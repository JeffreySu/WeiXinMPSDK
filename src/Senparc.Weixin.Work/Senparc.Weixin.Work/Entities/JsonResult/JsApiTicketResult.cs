/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：JsApiTicketResult.cs
    文件功能描述：jsapi_ticket请求后的JSON返回格式
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 201813
    修改描述：添加可序列化标签
----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// jsapi_ticket请求后的JSON返回格式
    /// </summary>
    [Serializable]
    public class JsApiTicketResult : WorkJsonResult
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }
}
