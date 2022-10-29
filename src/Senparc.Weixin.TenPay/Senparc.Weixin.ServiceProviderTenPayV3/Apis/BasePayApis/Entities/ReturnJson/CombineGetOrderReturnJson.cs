/// <summary>
/// 合单查询订单返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter5_1_11.shtml
/// <summary>
public class CombineGetOrderReturnJson
{

/// <summary>
/// 合单商户appid 
/// 
/// 								合单发起方的appid。
/// 示例值：wxd678efh567hg6787 
/// 可为空: True
/// </summary>
public string combine_appid { get; set; }

/// <summary>
/// 合单商户号 
/// 合单发起方商户号。 
/// 示例值：1900000109 
/// 可为空: True
/// </summary>
public string combine_mchid { get; set; }

/// <summary>
/// 合单商户订单号 
/// 合单支付总订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
/// 示例值：P20150806125346 
/// 可为空: True
/// </summary>
public string combine_out_trade_no { get; set; }

/// <summary>
/// 场景信息
/// 支付场景信息描述
/// 可为空: True
/// </summary>
public Scene_Info scene_info { get; set; }

/// <summary>
/// 子单信息
/// 最多支持子单条数：50
/// 可为空: True
/// </summary>
public Sub_Orders[] sub_orders { get; set; }

/// <summary>
/// 支付者
/// 支付者信息
/// 可为空: True
/// </summary>
public Combine_Payer_Info combine_payer_info { get; set; }


 #region 子数据类型

/// <summary>
/// 场景信息
/// 支付场景信息描述
/// <summary>
public class Scene_Info
{

/// <summary>
/// 商户端设备号 
/// 终端设备号（门店号或收银设备ID） 。
/// 示例值：POS1:1 
/// 可为空: True
/// </summary>
public string device_id { get; set; }



}


/// <summary>
/// 子单信息
/// 最多支持子单条数：50
/// <summary>
public class Sub_Orders
{

/// <summary>
/// 子单商户号 
/// 子单发起方商户号，必须与发起方Appid有绑定关系。 
/// 示例值：1900000109 
/// 可为空: True
/// </summary>
public string mchid { get; set; }

/// <summary>
/// 交易类型 
/// 枚举值： 
/// NATIVE：扫码支付 
/// JSAPI：公众号支付 
/// APP：APP支付
/// MWEB：H5支付 
/// 示例值： JSAPI
/// 可为空: True
/// </summary>
public string trade_type { get; set; }

/// <summary>
/// 交易状态 
/// 枚举值： 
/// SUCCESS：支付成功 
/// REFUND：转入退款 
/// NOTPAY：未支付 
/// CLOSED：已关闭 
/// USERPAYING：用户支付中
/// PAYERROR：支付失败(其他原因，如银行返回失败) 
/// 												ACCEPT：已接收，等待扣款
/// 示例值： SUCCESS
/// 可为空: True
/// </summary>
public string trade_state { get; set; }

/// <summary>
/// 付款银行 
/// 银行类型，采用字符串类型的银行标识。 
/// 示例值：CMC 
/// 可为空: True
/// </summary>
public string bank_type { get; set; }

/// <summary>
/// 附加数据 
/// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。 
/// 示例值：深圳分店 
/// 可为空: True
/// </summary>
public string attach { get; set; }

/// <summary>
/// 支付完成时间 
/// 订单支付时间，遵循rfc3339标准格式，格式为yyyy-MM-DDTHH:mm:ss.sss+TIMEZONE，yyyy-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。 
/// 示例值： 2015-05-20T13:29:35.120+08:00 
/// 可为空: True
/// </summary>
public string success_time { get; set; }

/// <summary>
/// 微信订单号 
/// 微信支付订单号。 
/// 示例值：1009660380201506130728806387
/// 可为空: True
/// </summary>
public string transaction_id { get; set; }

/// <summary>
/// 子单商户订单号 
/// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。 
/// 示例值：20150806125346 
/// 可为空: True
/// </summary>
public string out_trade_no { get; set; }

/// <summary>
/// 二级商户号 
/// 二级商户商户号，由微信支付生成并下发。服务商子商户的商户号，被合单方。直连商户不用传二级商户号。 
/// 												 
/// 											示例值：1900000109
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
///  子商户应用ID 
/// 服务商模式下，sub_mchid对应的sub_appid
/// 示例值：wxd678efh567hg6999
/// 可为空: True
/// </summary>
public  string[1,32]  sub_appid { get; set; }

/// <summary>
///  用户子标识 
/// 服务商模式下，sub_appid 对应的 openid
/// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
/// 可为空: True
/// </summary>
public  string[1, 128]  sub_openid { get; set; }

/// <summary>
/// 订单金额
/// 订单金额信息 
/// 可为空: True
/// </summary>
public Amount amount { get; set; }

/// <summary>
/// 优惠功能
/// 优惠功能，子单有核销优惠券时有返回 
/// 可为空: True
/// </summary>
public Promotion_Detail[] promotion_detail { get; set; }


 #region 子数据类型

/// <summary>
/// 订单金额
/// 订单金额信息 
/// <summary>
public class Amount
{

/// <summary>
/// 标价金额 
/// 子单金额，单位为分。
/// 示例值：100 
/// 可为空: True
/// </summary>
public long total_amount { get; set; }

/// <summary>
/// 标价币种 
/// 符合ISO 4217标准的三位字母代码，人民币：CNY。 
/// 示例值：CNY 
/// 可为空: True
/// </summary>
public string currency { get; set; }

/// <summary>
/// 现金支付金额 
/// 订单现金支付金额。 
/// 示例值： 10 
/// 可为空: True
/// </summary>
public long payer_amount { get; set; }

/// <summary>
/// 现金支付币种 
/// 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY。 
/// 示例值： CNY 
/// 可为空: True
/// </summary>
public string payer_currency { get; set; }



}


/// <summary>
/// 优惠功能
/// 优惠功能，子单有核销优惠券时有返回 
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
/// 示例值：GLOBALSINGLE
/// 可为空: True
/// </summary>
public string scope { get; set; }

/// <summary>
/// 优惠类型
/// CASH：充值型代金券 
///NOCASH：免充值型代金券 
/// 示例值：CASH
/// 可为空: True
/// </summary>
public string type { get; set; }

/// <summary>
/// 优惠券金额
/// 当前子单中享受的优惠券金额 
/// 示例值：100
/// 可为空: True
/// </summary>
public int amount { get; set; }

/// <summary>
/// 活动ID
/// 活动ID，批次ID 
/// 示例值：931386
/// 可为空: True
/// </summary>
public string stock_id { get; set; }

/// <summary>
/// 微信出资
/// 单位为分 
/// 示例值：100
/// 可为空: True
/// </summary>
public int wechatpay_contribute { get; set; }

/// <summary>
/// 商户出资
/// 单位为分 
/// 示例值：100
/// 可为空: True
/// </summary>
public int merchant_contribute { get; set; }

/// <summary>
/// 其他出资
/// 单位为分 
/// 示例值：100
/// 可为空: True
/// </summary>
public int other_contribute { get; set; }

/// <summary>
/// 优惠币种
/// CNY：人民币，境内商户号仅支持人民币。 
/// 示例值：CNY
/// 可为空: True
/// </summary>
public string currency { get; set; }

/// <summary>
/// 单品列表
/// 单品列表
/// 可为空: True
/// </summary>
public Goods_Detail[] goods_detail { get; set; }


 #region 子数据类型

/// <summary>
/// 单品列表
/// 单品列表
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
/// 商品数量 
/// 示例值：1
/// 可为空: True
/// </summary>
public int quantity { get; set; }

/// <summary>
/// 商品价格
/// 商品价格 
/// 示例值：100
/// 可为空: True
/// </summary>
public int unit_price { get; set; }

/// <summary>
/// 商品优惠金额
/// 商品优惠金额 
/// 示例值：1
/// 可为空: True
/// </summary>
public int discount_amount { get; set; }

/// <summary>
/// 商品备注
/// 商品备注 
/// 示例值：商品备注信息
/// 可为空: True
/// </summary>
public string goods_remark { get; set; }



}

#endregion
}

#endregion
}


/// <summary>
/// 支付者
/// 支付者信息
/// <summary>
public class Combine_Payer_Info
{

/// <summary>
/// 用户标识 
/// 使用合单appid获取的对应用户openid。是用户在商户appid下的唯一标识。 获取用户openid指引
/// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o 
/// 可为空: True
/// </summary>
public string openid { get; set; }



}

#endregion
}
