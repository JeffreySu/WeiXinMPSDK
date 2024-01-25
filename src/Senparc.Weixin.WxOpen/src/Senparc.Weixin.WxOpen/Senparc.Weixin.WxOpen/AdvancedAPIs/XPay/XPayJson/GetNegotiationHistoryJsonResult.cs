using Newtonsoft.Json;
using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class GetNegotiationHistoryJsonResult : WxJsonResult
    {
        /// <summary>
        /// 协商历史
        /// </summary>
        public List<HistoryItem> history { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int total { get; set; }
    }

    /// <summary>
    /// 协商历史
    /// </summary>
    public class HistoryItem
    {
        /// <summary>
        /// 操作流水号
        /// </summary>
        public string log_id { get; set; }

        /// <summary>
        /// 当前投诉协商记录的操作人
        /// 增加了JsonProperty，因为operator为关键词
        /// </summary>
        [JsonProperty("operator")]
        public string operator_ { get; set; }

        /// <summary>
        /// 当前操作时间，格式为yyyy-mm-dd'T'HH:MM:ssXXX，其中XXX为时区偏移，例如：2023-11-28T11:11:49+08:00
        /// </summary>
        public string operate_time { get; set; }

        /// <summary>
        /// 当前投诉协商记录的操作类型，对应枚举： USER_CREATE_COMPLAINT: 用户提交投诉 USER_CONTINUE_COMPLAINT: 用户继续投诉 USER_RESPONSE: 用户留言 PLATFORM_RESPONSE: 平台留言 MERCHANT_RESPONSE: 商户留言 MERCHANT_CONFIRM_COMPLETE: 商户申请结单 USER_CREATE_COMPLAINT_SYSTEM_MESSAGE: 用户提交投诉系统通知 COMPLAINT_FULL_REFUNDED_SYSTEM_MESSAGE: 投诉单发起全额退款系统通知 USER_CONTINUE_COMPLAINT_SYSTEM_MESSAGE: 用户继续投诉系统通知 USER_REVOKE_COMPLAINT: 用户主动撤诉（只存在于历史投诉单的协商历史中） USER_COMFIRM_COMPLAINT: 用户确认投诉解决（只存在于历史投诉单的协商历史中） PLATFORM_HELP_APPLICATION: 平台催办 USER_APPLY_PLATFORM_HELP: 用户申请平台协助 MERCHANT_APPROVE_REFUND: 商户同意退款申请 MERCHANT_REFUSE_RERUND: 商户拒绝退款申请, 此时操作内容里展示拒绝原因 USER_SUBMIT_SATISFACTION: 用户提交满意度调查结果,此时操作内容里会展示满意度分数 SERVICE_ORDER_CANCEL: 服务订单已取消 SERVICE_ORDER_COMPLETE: 服务订单已完成 COMPLAINT_PARTIAL_REFUNDED_SYSTEM_MESSAGE: 投诉单发起部分退款系统通知 COMPLAINT_REFUND_RECEIVED_SYSTEM_MESSAGE: 投诉单退款到账系统通知 COMPLAINT_ENTRUSTED_REFUND_SYSTEM_MESSAGE: 投诉单受托退款系统通知
        /// </summary>
        public string operate_type { get; set; }

        /// <summary>
        /// 当前投诉协商记录的具体内容
        /// </summary>
        public string operate_details { get; set; }

        /// <summary>
        /// 投诉单执行操作时上传的资料凭证，包含用户、商户、微信支付客服等角色操作
        /// </summary>
        public List<ComplaintMediaItem> complaint_media_list { get; set; }
    }
}
