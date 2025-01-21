/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：RequestMessage3rdWxaAuth.cs
    文件功能描述：小程序订单页设置申请通知
    
    
    创建标识：mc7246 - 20240802
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 小程序订单页设置申请通知
    /// </summary>
    public class RequestMessageOrderPathApplyResultNotify : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.order_path_apply_result_notify; }
        }

        /// <summary>
        /// 送审结果列表
        /// </summary>
        public RequestMessageOrderPathApplyResultNotify_AppInfo list { get; set; }
    }

    public class RequestMessageOrderPathApplyResultNotify_AppInfo
    {
        /// <summary>
        /// 申请的appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        ///  送审结果，0 成功， 1 重复提审，-1 系统繁忙，-2 APPID非法
        /// </summary>
        public int ret_code { get; set; }

    }
}
