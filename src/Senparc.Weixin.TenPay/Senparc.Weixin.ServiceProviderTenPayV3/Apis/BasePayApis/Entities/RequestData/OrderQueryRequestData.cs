/// <summary>
/// 查询订单请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_1_2.shtml
/// <summary>
public class OrderQueryRequestData
{

/// <summary>
/// 服务商户号 
/// query 服务商户号，由微信支付生成并下发 
/// 示例值：1230000109 
/// 可为空: True
/// </summary>
public string sp_mchid { get; set; }

/// <summary>
/// 子商户号 
/// query 子商户的商户号，由微信支付生成并下发。 
/// 示例值：1900000109 
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 微信支付订单号 
/// path 
/// 微信支付系统生成的订单号
/// 示例值：1217752501201407033233368018 
/// 
/// 可为空: True
/// </summary>
public string transaction_id { get; set; }



}
