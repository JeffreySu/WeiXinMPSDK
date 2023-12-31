/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessage3rdWxaAuth.cs
    文件功能描述：微信认证推送通知
    
    
    创建标识：Senparc - 20231211
----------------------------------------------------------------*/

namespace Senparc.Weixin.Open
{
    public class RequestMessage3rdWxaAuth : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.notify_3rd_wxa_auth; }
        }
        /// <summary>
        /// 小程序appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 状态码，见各API返回码。当task_status为0, 6, 9, 12时有事件通知
        /// </summary>
        public int task_status { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 审核单ID
        /// </summary>
        public string taskid { get; set; }

        /// <summary>
        /// 审核单状态，当apply_status变为2、3、4、5时会有事件通知
        /// </summary>
        public int apply_status { get; set; }

        public DispatchInfo dispatch_info { get; set; }
    }

    /// <summary>
    /// 审核机构信息
    /// </summary>
    public class DispatchInfo
    {
        /// <summary>
        /// 审核机构名称（当apply_status=2时有效）
        /// </summary>
        public string provider { get; set; }

        /// <summary>
        /// 审核机构联系方式（当apply_status=2时有效）
        /// </summary>
        public string contact { get; set; }

        /// <summary>
        /// 派单时间戳（秒）（当apply_status=2时有效）
        /// </summary>
        public string dispatch_time { get; set; }
    }
}
