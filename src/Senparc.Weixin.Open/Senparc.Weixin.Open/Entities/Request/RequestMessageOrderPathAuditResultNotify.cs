/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessage3rdWxaAuth.cs
    文件功能描述：小程序订单页设置审核结果通知
    
    
    创建标识：mc7246 - 20240802
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 小程序订单页设置审核结果通知
    /// </summary>
    public class RequestMessageOrderPathAuditResultNotify : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.order_path_audit_result_notify; }
        }

        /// <summary>
        /// 申请结果列表
        /// </summary>
        public RequestMessageOrderPathAuditResultNotify_AppInfo list { get; set; }
    }

    public class RequestMessageOrderPathAuditResultNotify_AppInfo
    {
        /// <summary>
        /// 申请的appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 审核单id
        /// </summary>
        public long audit_id { get; set; }

        /// <summary>
        /// 结果 4成功
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public long apply_time { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public long audit_time { get; set; }

        /// <summary>
        /// 结果说明
        /// </summary>
        public string reason { get; set; }
    }
}
