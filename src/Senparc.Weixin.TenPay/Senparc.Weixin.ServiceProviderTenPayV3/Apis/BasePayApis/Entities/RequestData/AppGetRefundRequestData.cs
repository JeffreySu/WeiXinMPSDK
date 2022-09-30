/// <summary>
/// 查询单笔退款请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_2_10.shtml
/// <summary>
public class AppGetRefundRequestData
{

/// <summary>
/// 商户退款单号
/// path商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。
/// 示例值：1217752501201407033233368018
/// 可为空: True
/// </summary>
public string out_refund_no { get; set; }

/// <summary>
/// 子商户号
/// query子商户的商户号，由微信支付生成并下发。
/// 示例值：1900000109
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }



}
