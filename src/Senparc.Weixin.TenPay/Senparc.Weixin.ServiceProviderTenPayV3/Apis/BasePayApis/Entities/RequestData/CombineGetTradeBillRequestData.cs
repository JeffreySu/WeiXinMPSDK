/// <summary>
/// 申请交易账单请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter5_1_17.shtml
/// <summary>
public class CombineGetTradeBillRequestData
{

/// <summary>
/// 账单日期 
/// query 格式yyyy-MM-dd 
/// 仅支持三个月内的账单下载申请。 
/// 示例值：2019-06-11 
/// 可为空: True
/// </summary>
public string bill_date { get; set; }

/// <summary>
/// 子商户号 
/// query 商户是服务商： 
/// ● 不填则默认返回服务商下的交易或退款数据。 
/// ● 如需下载某个子商户下的交易或退款数据，则该字段必填。 
/// 示例值：1900000001 
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 账单类型 
/// query 不填则默认是ALL 
/// 枚举值：
/// ALL：返回当日所有订单信息（不含充值退款订单） 
/// SUCCESS：返回当日成功支付的订单（不含充值退款订单） 
/// REFUND：返回当日退款订单（不含充值退款订单） 
/// 示例值：ALL
/// 可为空: True
/// </summary>
public string bill_type { get; set; }

/// <summary>
/// 压缩类型 
/// query 不填则默认是数据流 
/// 枚举值：
/// GZIP：返回格式为.gzip的压缩包账单 
/// 示例值：GZIP
/// 可为空: True
/// </summary>
public string tar_type { get; set; }



}
