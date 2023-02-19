/// <summary>
/// 关闭订单请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_5_3.shtml
/// <summary>
public class AppletsCloseOrderRequestData
{

/// <summary>
/// 服务商户号 
/// 服务商户号，由微信支付生成并下发 
/// 示例值：1230000109 
/// 可为空: True
/// </summary>
public string sp_mchid { get; set; }

/// <summary>
/// 子商户号 
/// 子商户的商户号，由微信支付生成并下发。 
/// 示例值：1900000109 
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 商户订单号 
/// path 商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一。 
/// 示例值：1217752501201407033233368018 
/// 可为空: True
/// </summary>
public string out_trade_no { get; set; }



}
