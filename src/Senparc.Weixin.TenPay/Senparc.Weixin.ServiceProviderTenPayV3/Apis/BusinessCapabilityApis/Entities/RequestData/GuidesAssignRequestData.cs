/// <summary>
/// 服务人员分配请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_4_2.shtml
/// <summary>
public class GuidesAssignRequestData
{

/// <summary>
/// 服务人员ID
/// path 服务人员在服务人员系统中的唯一标识 
/// 示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic
/// 可为空: True
/// </summary>
public string guide_id { get; set; }

/// <summary>
/// 子商户ID
/// 服务人员所属商户的商户ID
/// 示例值：1234567890
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 商户订单号
/// 商户系统内部订单号，要求32个字符内，仅支持使用字母、数字、中划线-、下划线_、竖线|、星号*这些英文半角字符的组合，请勿使用汉字或全角等特殊字符，且在同一个商户号下唯一。
/// 示例值：20150806125346
/// 可为空: True
/// </summary>
public string out_trade_no { get; set; }



}
