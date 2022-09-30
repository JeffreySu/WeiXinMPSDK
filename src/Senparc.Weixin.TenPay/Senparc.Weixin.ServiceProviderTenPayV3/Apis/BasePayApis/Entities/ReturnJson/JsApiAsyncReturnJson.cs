/// <summary>
/// JSAPI下单返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_1_1.shtml
/// <summary>
public class JsApiAsyncReturnJson
{

/// <summary>
/// 预支付交易会话标识 
/// 预支付交易会话标识。用于后续接口调用中使用，该值有效期为2小时 
/// 示例值：wx201410272009395522657a690389285100 
/// 可为空: True
/// </summary>
public string prepay_id { get; set; }



}
