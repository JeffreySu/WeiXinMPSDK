/// <summary>
/// 合单H5下单请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter5_1_2.shtml
/// <summary>
public class CombineH5OrderRequestData
{

/// <summary>
/// 合单商户appid 
///  合单发起方的appid。
/// 示例值：wxd678efh567hg6787 
/// 可为空: True
/// </summary>
public string combine_appid { get; set; }

/// <summary>
/// 合单商户号 
/// 合单发起方商户号，服务商和电商模式下，传服务商商户号。
/// 示例值：1900000109
/// 可为空: True
/// </summary>
public string combine_mchid { get; set; }

/// <summary>
/// 合单商户订单号 
/// 合单支付总订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一 。
/// 示例值：P20150806125346
/// 可为空: True
/// </summary>
public string combine_out_trade_no { get; set; }

/// <summary>
/// 场景信息
/// 支付场景信息描述，为了方便问题定位，H5支付场景下，该字段必填
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
/// 交易起始时间 
/// 订单生成时间，遵循rfc3339标准格式，格式为yyyy-MM-DDTHH:mm:ss+TIMEZONE，yyyy-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒125毫秒。
/// 示例值：2019-12-31T15:59:59+08:00
/// 可为空: True
/// </summary>
public string time_start { get; set; }

/// <summary>
/// 交易结束时间 
/// 订单失效时间，遵循rfc3339标准格式，格式为yyyy-MM-DDTHH:mm:ss+TIMEZONE，yyyy-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。
/// 示例值：2019-12-31T15:59:59+08:00 
/// 
/// 可为空: True
/// </summary>
public string time_expire { get; set; }

/// <summary>
/// 通知地址 
/// 接收微信支付异步通知回调地址，通知url必须为直接可访问的URL，不能携带参数。 
/// 格式: URL 
/// 示例值：https://yourapp.com/notify 
/// 可为空: True
/// </summary>
public string notify_url { get; set; }


 #region 子数据类型

/// <summary>
/// 场景信息
/// 支付场景信息描述，为了方便问题定位，H5支付场景下，该字段必填
/// <summary>
public class Scene_Info
{

/// <summary>
/// 商户端设备号 
/// 终端设备号（门店号或收银设备ID） 。
/// 								为了方便问题定位，H5支付场景下，该字段必填
/// 示例值：POS1:123 
/// 可为空: True
/// </summary>
public string device_id { get; set; }

/// <summary>
/// 用户终端IP 
/// 用户的客户端IP，支持IPv4和IPv6两种格式的IP地址。
/// 格式: ip(ipv4+ipv6) 
/// 获取用户IP指引
/// 示例值：14.17.22.32
/// 可为空: True
/// </summary>
public string payer_client_ip { get; set; }

/// <summary>
/// H5场景信息
/// H5场景信息
/// 可为空: True
/// </summary>
public H5_Info h5_info { get; set; }


 #region 子数据类型

/// <summary>
/// H5场景信息
/// H5场景信息
/// <summary>
public class H5_Info
{

/// <summary>
/// 场景类型
/// 场景类型，枚举值：
/// 										iOS：IOS移动应用； 
/// 										Android：安卓移动应用； 
/// 										Wap：WAP网站应用；
/// 示例值：iOS
/// 可为空: True
/// </summary>
public string type { get; set; }

/// <summary>
/// 应用名称
/// 应用名称
/// 示例值：王者荣耀
/// 可为空: True
/// </summary>
public string app_name { get; set; }

/// <summary>
/// 网站URL
/// 网站URL
/// 示例值：https://pay.qq.com
/// 可为空: True
/// </summary>
public string app_url { get; set; }

/// <summary>
/// iOS平台BundleID
/// iOS平台BundleID
/// 示例值：com.tencent.wzryiOS
/// 可为空: True
/// </summary>
public string bundle_id { get; set; }

/// <summary>
/// Android平台PackageName
/// Android平台PackageName
/// 示例值：com.tencent.tmgp.sgame
/// 可为空: True
/// </summary>
public string package_name { get; set; }



}

#endregion
}


/// <summary>
/// 子单信息
/// 最多支持子单条数：50 
/// <summary>
public class Sub_Orders
{

/// <summary>
/// 子单商户号 
/// 子单发起方商户号，必须与发起方appid有绑定关系。服务商和电商模式下，传服务商商户号。
/// 示例值：1900000109 
/// 可为空: True
/// </summary>
public string mchid { get; set; }

/// <summary>
/// 附加数据 
/// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。 
/// 示例值：深圳分店 
/// 可为空: True
/// </summary>
public string attach { get; set; }

/// <summary>
/// 订单金额
/// 订单金额信息
/// 可为空: True
/// </summary>
public Amount amount { get; set; }

/// <summary>
/// 子单商户订单号 
/// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。 
/// 特殊规则：最小字符长度为6 
/// 示例值：20150806125346
/// 可为空: True
/// </summary>
public string out_trade_no { get; set; }

/// <summary>
/// 订单优惠标记 
/// 订单优惠标记，使用代金券或立减优惠功能时需要的参数，说明详见代金券或立减优惠 
/// 示例值：WXG 
/// 可为空: True
/// </summary>
public string goods_tag { get; set; }

/// <summary>
/// 二级商户号 
/// 二级商户商户号，由微信支付生成并下发。 服务商子商户的商户号，被合单方。直连商户不用传二级商户号。 
/// 注意：仅适用于电商平台 服务商
/// 示例值：1900000109
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 商品描述 
/// 商品简单描述。需传入应用市场上的APP名字-实际商品名称，例如：天天爱消除-游戏充值。
/// 示例值：腾讯充值中心-QQ会员充值 
/// 可为空: True
/// </summary>
public string description { get; set; }

/// <summary>
/// 结算信息
/// 结算信息
/// 可为空: True
/// </summary>
public Settle_Info settle_info { get; set; }

/// <summary>
///  子商户应用ID 
/// 子商户申请的应用ID，全局唯一。sub_mchid对应的sub_appid
/// 示例值：wxd678efh567hg6999
/// 可为空: True
/// </summary>
public  string[1,32]  sub_appid { get; set; }


 #region 子数据类型

/// <summary>
/// 订单金额
/// 订单金额信息
/// <summary>
public class Amount
{

/// <summary>
/// 标价金额 
/// 子单金额，单位为分 
/// 										境外场景下，标价金额要超过商户结算币种的最小单位金额，例如结算币种为美元，则标价金额必须大于1美分
/// 示例值：100
/// 可为空: True
/// </summary>
public long total_amount { get; set; }

/// <summary>
/// 标价币种 
/// 符合ISO 4217标准的三位字母代码，人民币：CNY 。
/// 示例值：CNY
/// 可为空: True
/// </summary>
public string currency { get; set; }



}


/// <summary>
/// 结算信息
/// 结算信息
/// <summary>
public class Settle_Info
{

/// <summary>
/// 是否指定分账 
/// 是否分账，枚举值：
/// true：是 
/// false：否 
/// 示例值：true
/// 可为空: True
/// </summary>
public bool profit_sharing { get; set; }

/// <summary>
/// 补差金额 
/// SettleInfo.profit_sharing为true时，该金额才生效。 
/// 注意：单笔订单最高补差金额为5000元
/// 示例值：10
/// 可为空: True
/// </summary>
public long subsidy_amount { get; set; }



}

#endregion
}

#endregion
}
