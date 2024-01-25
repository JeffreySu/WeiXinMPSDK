using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class GetComplaintListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 投诉列表
        /// </summary>
        public List<ComplaintItem> complaints { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int total { get; set; }
    }

    /// <summary>
    /// 投诉列表
    /// </summary>
    public class ComplaintItem
    {
        /// <summary>
        /// 投诉id
        /// </summary>
        public string complaint_id { get; set; }

        /// <summary>
        /// 投诉时间 格式为yyyy-mm-dd'T'HH:MM:ssXXX，其中XXX为时区偏移，例如：2023-11-28T11:11:49+08:00
        /// </summary>
        public string complaint_time { get; set; }

        /// <summary>
        /// 投诉内容
        /// </summary>
        public string complaint_detail { get; set; }

        /// <summary>
        /// 投诉状态 PENDING-待处理；PROCESSING-处理中；PROCESSED-已处理完成
        /// </summary>
        public string complaint_state { get; set; }

        /// <summary>
        /// 投诉人联系方式
        /// </summary>
        public string payer_phone { get; set; }

        /// <summary>
        /// 投诉人在商户AppID下的唯一标识
        /// </summary>
        public string payer_openid { get; set; }

        /// <summary>
        /// 投诉单关联订单信息
        /// </summary>
        public List<ComplaintOrderInfoItem> complaint_order_info { get; set; }

        /// <summary>
        /// 投诉单下所有订单是否已全部全额退款
        /// </summary>
        public bool complaint_full_refunded { get; set; }

        /// <summary>
        /// 投诉单是否有待回复的用户留言
        /// </summary>
        public bool incoming_user_response { get; set; }

        /// <summary>
        /// 用户投诉次数。用户首次发起投诉记为1次，用户每有一次继续投诉就加1
        /// </summary>
        public int user_complaint_times { get; set; }

        /// <summary>
        /// 户上传的投诉相关资料，包括图片凭证等
        /// </summary>
        public List<ComplaintMediaItem> complaint_media_list { get; set; }

        /// <summary>
        /// 用户发起投诉前选择的faq标题
        /// </summary>
        public string problem_description { get; set; }

        /// <summary>
        /// 问题类型为申请退款的单据是需要最高优先处理的单据。REFUND: 申请退款；SERVICE_NOT_WORK: 服务权益未生效；OTHERS: 其他类型
        /// </summary>
        public string problem_type { get; set; }

        /// <summary>
        /// 当问题类型为申请退款时, 有值, (单位:分)
        /// </summary>
        public int apply_refund_amount { get; set; }

        /// <summary>
        /// 用户标签列表，每一项内容为string。TRUSTED: 此类用户满足极速退款条件；HIGH_RISK: 高风险投诉，请按照运营要求优先妥善处理
        /// </summary>
        public List<string> user_tag_list { get; set; }

        /// <summary>
        /// 投诉单关联服务单信息
        /// </summary>
        public List<ComplaintServiceOrderInfoItem> service_order_info { get; set; }
    }

    /// <summary>
    /// complaint_order_info
    /// </summary>
    public class ComplaintOrderInfoItem
    {
        /// <summary>
        /// 投诉单关联的微信支付交易单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 渠道单号，query_order接口返回的channel_order_id
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 订单金额，单位（分）
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 商户单号，商家在拉走支付时传的单号
        /// </summary>
        public string wxa_out_trade_no { get; set; }

        /// <summary>
        /// 小程序侧单号
        /// </summary>
        public string wx_order_id { get; set; }

    }

    /// <summary>
    /// complaint_media_list
    /// </summary>
    public class ComplaintMediaItem
    {
        /// <summary>
        /// 体文件对应的业务类型，USER_COMPLAINT_IMAGE: 用户提交投诉时上传的图片凭证；OPERATION_IMAGE: 用户、商户、微信支付客服在协商解决投诉时，上传的图片凭证
        /// </summary>
        public string media_type { get; set; }

        /// <summary>
        /// 每一项的内容为string，媒体文件请求url
        /// </summary>
        public List<string> media_url { get; set; }
    }

    /// <summary>
    /// service_order_info
    /// </summary>
    public class ComplaintServiceOrderInfoItem
    {
        /// <summary>
        /// 微信支付服务订单号，每个微信支付服务订单号与商户号下对应的商户服务订单号一一对应
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 商户系统内部服务订单号（不是交易单号），与创建订单时一致
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 此处上传的是用户发起投诉时的服务单状态，不会实时更新。DOING: 服务订单进行中；REVOKED: 服务订单已取消；WAITPAY: 服务订单待支付；DONE: 服务订单已完成
        /// </summary>
        public string state { get; set; }
    }
}
