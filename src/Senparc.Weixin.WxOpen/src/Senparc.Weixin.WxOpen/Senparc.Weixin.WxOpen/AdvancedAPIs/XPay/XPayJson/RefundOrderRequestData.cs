using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 对使用jsapi接口下的单进行退款，此接口只是启动退款任务成功，启动后需要调用query_order接口来查询退款单状态，等状态变成退款完成后即为最终成功
    /// </summary>
    public class RefundOrderRequestData
    {
        /// <summary>
        /// 下单时的用户openid
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 下单时的单号，即jsapi接口传入的OutTradeNo，与wx_order_id字段二选一
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 支付单对应的微信侧单号，与order_id字段二选一
        /// </summary>
        public string wx_order_id { get; set; }

        /// <summary>
        /// 本次退款时需要传的单号，长度为[8,32]，字符只允许使用字母、数字、'_'、'-'
        /// </summary>
        public string refund_order_id { get; set; }

        /// <summary>
        /// 当前单剩余可退金额，单位分，可以通过调用query_order接口查到
        /// </summary>
        public int left_fee { get; set; }

        /// <summary>
        /// 本次退款金额，单位分，需要(0,left_fee]
        /// </summary>
        public int refund_fee { get; set; }

        /// <summary>
        /// 商家自定义数据，传入后可在query_order接口查询时原样返回，长度需要[0,1024]
        /// </summary>
        public string biz_meta { get; set; }

        /// <summary>
        /// 退款原因，当前仅支持以下值 0-暂无描述 1-产品问题，影响使用或效果不佳 2-售后问题，无法满足需求 3-意愿问题，用户主动退款 4-价格问题 5:其他原因
        /// </summary>
        public string refund_reason { get; set; }

        /// <summary>
        /// 退款来源，当前仅支持以下值 1-人工客服退款，即用户电话给客服，由客服发起退款流程 2-用户自己发起退款流程 3-其它
        /// </summary>
        public string req_from { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
