/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：RequestMessageUpdateAuthorized.cs
    文件功能描述：授权更新通知
    
    
    创建标识：Senparc - 20160813
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 授权更新通知
    /// </summary>
    public class RequestMessageUpdateAuthorized : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.updateauthorized; }
        }

        /// <summary>
        /// 公众号appid
        /// </summary>
        public string AuthorizerAppid { get; set; }
        /// <summary>
        /// 授权码（code）
        /// </summary>
        public string AuthorizationCode { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime AuthorizationCodeExpiredTime { get; set; }
    }
}
