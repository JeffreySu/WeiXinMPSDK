/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageUpdateAuthorized.cs
    文件功能描述：授权更新通知
    
    
    创建标识：Senparc - 20160813

    修改标识：Senparc - 20181226
    修改描述：v4.3.3 修改 DateTime 为 DateTimeOffset
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
        public DateTimeOffset AuthorizationCodeExpiredTime { get; set; }
    }
}
