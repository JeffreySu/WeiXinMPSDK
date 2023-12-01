using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryOrderJsonResult : WxJsonResult
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        public Order order { get; set; }
    }

    /// <summary>
    /// 订单信息
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public long update_time { get; set; }
        /// <summary>
        /// 当前状态 0-订单初始化（未创建成功，不可用于支付）1-订单创建成功 2-订单已经支付，待发货 3-订单发货中 4-订单已发货 5-订单已经退款 6-订单已经关闭（不可再使用） 7-订单退款失败 8-用户退款完成 9-回收广告金完成 10-分账回退完成
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 业务类型0-短剧
        /// </summary>
        public int biz_type { get; set; }
        /// <summary>
        /// 订单金额，单位分
        /// </summary>
        public int order_fee { get; set; }
        /// <summary>
        /// 订单优惠金额，单位分(暂无此字段)
        /// </summary>
        public int coupon_fee { get; set; }
        /// <summary>
        /// 用户支付金额
        /// </summary>
        public int paid_fee { get; set; }
        /// <summary>
        /// 订单类型0-支付单 1-退款单
        /// </summary>
        public int order_type { get; set; }
        /// <summary>
        /// 当类型为退款单时表示退款金额，单位分
        /// </summary>
        public int refund_fee { get; set; }
        /// <summary>
        /// 支付/退款时间，unix秒级时间戳
        /// </summary>
        public long paid_time { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public long provide_time { get; set; }
        /// <summary>
        /// 订单创建时传的信息
        /// </summary>
        public string biz_meta { get; set; }
        /// <summary>
        /// 环境类型1-现网 2-沙箱
        /// </summary>
        public int env_type { get; set; }
        /// <summary>
        /// 下单时米大师返回的token
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 支付单类型时表示此单经过退款还剩余的金额，单位分
        /// </summary>
        public int left_fee { get; set; }
        /// <summary>
        /// 微信内部单号
        /// </summary>
        public string wx_order_id { get; set; }
        /// <summary>
        /// 渠道单号，为用户微信支付详情页面上的商户单号
        /// </summary>
        public string channel_order_id { get; set; }
        /// <summary>
        /// 微信支付交易单号，为用户微信支付详情页面上的交易单号
        /// </summary>
        public string wxpay_order_id { get; set; }
        /// <summary>
        /// 结算时间的秒级时间戳，大于0表示结算成功
        /// </summary>
        public long sett_time { get; set; }
        /// <summary>
        /// 结算状态0-未开始结算 1-结算中 2-结算成功
        /// </summary>
        public int sett_state { get; set; }

    }
}
