/// <summary>
/// 申请退款请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_1_9.shtml
/// <summary>
public class RefundRequestData
{

/// <summary>
/// 子商户号
/// body子商户的商户号，由微信支付生成并下发。
/// 示例值：1900000109
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 微信支付订单号
/// body原支付交易对应的微信订单号
/// 示例值：1217752501201407033233368018
/// 可为空: True
/// </summary>
public string transaction_id { get; set; }

/// <summary>
/// 商户订单号
/// body原支付交易对应的商户订单号
/// 示例值：1217752501201407033233368018 多选一
/// 可为空: True
/// </summary>
public string out_trade_no { get; set; }

/// <summary>
/// 商户退款单号
/// body商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。
/// 示例值：1217752501201407033233368018
/// 可为空: True
/// </summary>
public string out_refund_no { get; set; }

/// <summary>
/// 退款原因
/// body若商户传入，会在下发给用户的退款消息中体现退款原因
/// 示例值：商品已售完
/// 可为空: True
/// </summary>
public string reason { get; set; }

/// <summary>
/// 退款结果回调url
/// body异步接收微信支付退款结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。 如果参数中传了notify_url，则商户平台上配置的回调地址将不会生效，优先回调当前传的这个地址。
/// 示例值：https://weixin.qq.com
/// 可为空: True
/// </summary>
public string notify_url { get; set; }

/// <summary>
/// 退款资金来源
/// body若传递此参数则使用对应的资金账户退款，否则默认使用未结算资金退款（仅对老资金流商户适用） 
/// 枚举值：
/// AVAILABLE：可用余额账户
/// 示例值：AVAILABLE
/// 可为空: True
/// </summary>
public string funds_account { get; set; }

/// <summary>
/// 金额信息
/// body订单金额信息
/// 可为空: True
/// </summary>
public Amount amount { get; set; }

/// <summary>
/// 退款商品
/// body指定商品退款需要传此参数，其他场景无需传递
/// 可为空: True
/// </summary>
public Goods_Detail[] goods_detail { get; set; }


 #region 子数据类型

/// <summary>
/// 金额信息
/// body订单金额信息
/// <summary>
public class Amount
{

/// <summary>
/// 退款金额
/// 退款金额，币种的最小单位，只能为整数，不能超过原订单支付金额。
/// 示例值：888
/// 可为空: True
/// </summary>
public int refund { get; set; }

/// <summary>
/// 退款出资账户及金额
/// 退款需要从指定账户出资时，传递此参数指定出资金额（币种的最小单位，只能为整数）。
/// 同时指定多个账户出资退款的使用场景需要满足以下条件：
///   1、未开通退款支出分离产品功能；
///   2、订单属于分账订单，且分账处于待分账或分账中状态。
/// 参数传递需要满足条件：
///   1、基本账户可用余额出资金额与基本账户不可用余额出资金额之和等于退款金额；
///   2、账户类型不能重复。
/// 上述任一条件不满足将返回错误
/// 可为空: True
/// </summary>
public From[] from { get; set; }

/// <summary>
/// 原订单金额
/// 原支付交易的订单总金额，币种的最小单位，只能为整数。
/// 示例值：888
/// 可为空: True
/// </summary>
public int total { get; set; }

/// <summary>
/// 退款币种
/// 符合ISO 4217标准的三位字母代码，目前只支持人民币：CNY。
/// 示例值：CNY
/// 可为空: True
/// </summary>
public string currency { get; set; }


 #region 子数据类型

/// <summary>
/// 退款出资账户及金额
/// 退款需要从指定账户出资时，传递此参数指定出资金额（币种的最小单位，只能为整数）。
/// 同时指定多个账户出资退款的使用场景需要满足以下条件：
///   1、未开通退款支出分离产品功能；
///   2、订单属于分账订单，且分账处于待分账或分账中状态。
/// 参数传递需要满足条件：
///   1、基本账户可用余额出资金额与基本账户不可用余额出资金额之和等于退款金额；
///   2、账户类型不能重复。
/// 上述任一条件不满足将返回错误
/// <summary>
public class From
{

/// <summary>
/// 出资账户类型
/// 下面枚举值多选一。
/// 枚举值：
/// AVAILABLE : 可用余额
/// UNAVAILABLE : 不可用余额
/// 示例值：AVAILABLE
/// 可为空: True
/// </summary>
public string account { get; set; }

/// <summary>
/// 出资金额
/// 对应账户出资金额
/// 示例值：444
/// 可为空: True
/// </summary>
public int amount { get; set; }



}

#endregion
}


/// <summary>
/// 退款商品
/// body指定商品退款需要传此参数，其他场景无需传递
/// <summary>
public class Goods_Detail
{

/// <summary>
/// 商户侧商品编码
/// 由半角的大小写字母、数字、中划线、下划线中的一种或几种组成
/// 示例值：1217752501201407033233368018
/// 可为空: True
/// </summary>
public string merchant_goods_id { get; set; }

/// <summary>
/// 微信支付商品编码
/// 微信支付定义的统一商品编号（没有可不传）
/// 示例值：1001
/// 可为空: True
/// </summary>
public string wechatpay_goods_id { get; set; }

/// <summary>
/// 商品名称
/// 商品的实际名称
/// 示例值：iPhone6s 16G
/// 可为空: True
/// </summary>
public string goods_name { get; set; }

/// <summary>
/// 商品单价
/// 商品单价金额，单位为分
/// 示例值：528800
/// 可为空: True
/// </summary>
public int unit_price { get; set; }

/// <summary>
/// 商品退款金额
/// 商品退款金额，单位为分
/// 示例值：528800
/// 可为空: True
/// </summary>
public int refund_amount { get; set; }

/// <summary>
/// 商品退货数量
/// 单品的退款数量
/// 示例值：1
/// 可为空: True
/// </summary>
public int refund_quantity { get; set; }



}

#endregion
}
