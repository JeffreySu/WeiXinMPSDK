/// <summary>
/// 查询订单返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_4_2.shtml
/// <summary>
public class NativeOrderQueryReturnJson
{

/// <summary>
/// 服务商应用ID 
///  服务商申请的公众号或移动应用appid。 
/// 示例值：wx8888888888888888
/// 可为空: True
/// </summary>
public string sp_appid { get; set; }

/// <summary>
/// 服务商户号 
///  服务商户号，由微信支付生成并下发 
/// 示例值：1230000109
/// 可为空: True
/// </summary>
public string sp_mchid { get; set; }

/// <summary>
/// 子商户应用ID 
/// 子商户申请的公众号或移动应用appid。 
/// 示例值：wxd678efh567hg6999
/// 可为空: True
/// </summary>
public string sub_appid { get; set; }

/// <summary>
/// 子商户号 
/// 子商户的商户号，由微信支付生成并下发。 
/// 示例值：1900000109
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 商户订单号 
/// 商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一，详见【商户订单号】。 
/// 特殊规则：最小字符长度为6 
/// 示例值：1217752501201407033233368018
/// 可为空: True
/// </summary>
public string out_trade_no { get; set; }

/// <summary>
/// 微信支付订单号 
/// 微信支付系统生成的订单号。 
/// 示例值：1217752501201407033233368018 
/// 
/// 可为空: True
/// </summary>
public string transaction_id { get; set; }

/// <summary>
/// 交易类型 
/// 交易类型，枚举值： 
/// 				JSAPI：公众号支付 
/// 				NATIVE：扫码支付 
/// 				APP：APP支付 
/// 				MICROPAY：付款码支付 
/// 				MWEB：H5支付 
/// 				FACEPAY：刷脸支付 
/// 示例值：MICROPAY 
/// 可为空: True
/// </summary>
public string trade_type { get; set; }

/// <summary>
/// 交易状态 
/// 交易状态，枚举值： 
/// 				SUCCESS：支付成功 
/// 				REFUND：转入退款 
/// 				NOTPAY：未支付 
/// 				CLOSED：已关闭 
/// 				REVOKED：已撤销（仅付款码支付会返回） 
/// 				USERPAYING：用户支付中（仅付款码支付会返回） 
/// 				PAYERROR：支付失败（仅付款码支付会返回） 
/// 示例值：SUCCESS 
/// 可为空: True
/// </summary>
public string trade_state { get; set; }

/// <summary>
/// 交易状态描述 
/// 交易状态描述 
/// 示例值：支付成功
/// 可为空: True
/// </summary>
public string trade_state_desc { get; set; }

/// <summary>
/// 付款银行 
/// 银行类型，采用字符串类型的银行标识。 银行标识请参考《银行类型对照表》
/// 示例值：CMC
/// 可为空: True
/// </summary>
public string bank_type { get; set; }

/// <summary>
/// 附加数据 
/// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用，实际情况下只有支付完成状态才会返回该字段。 
/// 示例值：自定义数据   
/// 可为空: True
/// </summary>
public string attach { get; set; }

/// <summary>
/// 支付完成时间 
/// 支付完成时间，遵循rfc3339标准格式，格式为yyyy-MM-DDTHH:mm:ss+TIMEZONE，yyyy-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。 
/// 示例值：2018-06-08T10:34:56+08:00
/// 可为空: True
/// </summary>
public string success_time { get; set; }

/// <summary>
/// 支付者
///  支付者信息
/// 可为空: True
/// </summary>
public Payer payer { get; set; }

/// <summary>
/// 订单金额
/// 订单金额信息，当支付成功时返回该字段。 
/// 可为空: True
/// </summary>
public Amount amount { get; set; }

/// <summary>
/// 场景信息
/// 支付场景描述 
/// 可为空: True
/// </summary>
public Scene_Info scene_info { get; set; }

/// <summary>
/// 优惠功能
/// 优惠功能，享受优惠时返回该字段。 
/// 可为空: True
/// </summary>
public Promotion_Detail[] promotion_detail { get; set; }


 #region 子数据类型

/// <summary>
/// 支付者
///  支付者信息
/// <summary>
public class Payer
{

/// <summary>
/// 用户服务标识 
/// 用户在服务商appid下的唯一标识。 
/// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
/// 可为空: True
/// </summary>
public string sp_openid { get; set; }

/// <summary>
/// 用户子标识 
/// 用户在子商户appid下的唯一标识。如果返回sub_appid，那么sub_openid一定会返回 
/// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
/// 可为空: True
/// </summary>
public string sub_openid { get; set; }



}


/// <summary>
/// 订单金额
/// 订单金额信息，当支付成功时返回该字段。 
/// <summary>
public class Amount
{

/// <summary>
/// 总金额 
/// 订单总金额，单位为分。 
/// 示例值：100
/// 可为空: True
/// </summary>
public int  total { get; set; }

/// <summary>
/// 用户支付金额 
/// 用户支付金额，单位为分。（指使用优惠券的情况下，这里等于总金额-优惠券金额）
/// 示例值：100
/// 可为空: True
/// </summary>
public int  payer_total { get; set; }

/// <summary>
/// 货币类型 
/// CNY：人民币，境内商户号仅支持人民币。 
/// 示例值：CNY
/// 可为空: True
/// </summary>
public string currency { get; set; }

/// <summary>
/// 用户支付币种 
/// 用户支付币种 
/// 示例值：CNY
/// 可为空: True
/// </summary>
public string payer_currency { get; set; }



}


/// <summary>
/// 场景信息
/// 支付场景描述 
/// <summary>
public class Scene_Info
{

/// <summary>
/// 商户端设备号 
/// 商户端设备号（发起扣款请求的商户服务器设备号）。 
/// 示例值：013467007045764
/// 可为空: True
/// </summary>
public string device_id { get; set; }



}


/// <summary>
/// 优惠功能
/// 优惠功能，享受优惠时返回该字段。 
/// <summary>
public class Promotion_Detail
{

/// <summary>
/// 券ID 
/// 券ID 
/// 示例值：109519
/// 可为空: True
/// </summary>
public string coupon_id { get; set; }

/// <summary>
/// 优惠名称 
/// 优惠名称 
/// 示例值：单品惠-6
/// 可为空: True
/// </summary>
public string name { get; set; }

/// <summary>
/// 优惠范围 
/// GLOBAL：全场代金券 
/// SINGLE：单品优惠 
/// 示例值：GLOBAL
/// 可为空: True
/// </summary>
public string scope { get; set; }

/// <summary>
/// 优惠类型 
/// CASH：充值型代金券 
/// NOCASH：免充值型代金券 
/// 示例值：CASH
/// 可为空: True
/// </summary>
public string type { get; set; }

/// <summary>
/// 优惠券面额 
/// 优惠券面额 
/// 示例值：100
/// 可为空: True
/// </summary>
public int  amount { get; set; }

/// <summary>
/// 活动ID 
/// 活动ID 
/// 示例值：931386
/// 可为空: True
/// </summary>
public string stock_id { get; set; }

/// <summary>
/// 微信出资 
/// 微信出资，单位为分 
/// 示例值：0
/// 可为空: True
/// </summary>
public int  wechatpay_contribute { get; set; }

/// <summary>
/// 商户出资 
/// 商户出资，单位为分 
/// 示例值：0 
/// 可为空: True
/// </summary>
public int  merchant_contribute { get; set; }

/// <summary>
/// 其他出资 
/// 其他出资，单位为分 
/// 示例值：0
/// 可为空: True
/// </summary>
public int  other_contribute { get; set; }

/// <summary>
/// 优惠币种 
/// CNY：人民币，境内商户号仅支持人民币。 
/// 示例值：CNY
/// 可为空: True
/// </summary>
public string currency { get; set; }

/// <summary>
/// 单品列表
/// 单品列表信息 
/// 可为空: True
/// </summary>
public Goods_Detail[] goods_detail { get; set; }


 #region 子数据类型

/// <summary>
/// 单品列表
/// 单品列表信息 
/// <summary>
public class Goods_Detail
{

/// <summary>
/// 商品编码 
/// 商品编码 
/// 示例值：M1006
/// 可为空: True
/// </summary>
public string goods_id { get; set; }

/// <summary>
/// 商品数量 
/// 用户购买的数量 
/// 示例值：1 
/// 可为空: True
/// </summary>
public int  quantity { get; set; }

/// <summary>
/// 商品单价 
/// 商品单价，单位为分 
/// 示例值：100 
/// 可为空: True
/// </summary>
public int  unit_price { get; set; }

/// <summary>
/// 商品优惠金额 
/// 商品优惠金额 
/// 示例值：0  
/// 可为空: True
/// </summary>
public int  discount_amount { get; set; }

/// <summary>
/// 商品备注 
/// 商品备注信息 
/// 示例值：商品备注信息 
/// 可为空: True
/// </summary>
public string goods_remark { get; set; }



}

#endregion
}

#endregion
}
