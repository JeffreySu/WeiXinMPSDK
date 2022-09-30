/// <summary>
/// 商家小票管理返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_5_2.shtml
/// <summary>
public class ReceiptsManagementReturnJson
{

/// <summary>
/// 特约商户号
/// 开通或关闭“商家自定义小票”的特约商户商户号，由微信支付生成并下发。 
/// 示例值：1234567890 
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }



}
