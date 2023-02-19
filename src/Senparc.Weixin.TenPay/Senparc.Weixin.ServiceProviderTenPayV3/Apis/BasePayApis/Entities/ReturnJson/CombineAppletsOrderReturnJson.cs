/// <summary>
/// 合单小程序下单返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter5_1_4.shtml
/// <summary>
public class CombineAppletsOrderReturnJson
{

/// <summary>
/// 预支付交易会话标识 
/// 数字和字母。微信生成的预支付会话标识，用于后续接口调用使用，该值有效期为2小时。 
/// 示例值：up_wx201410272009395522657a690389285100 
/// 可为空: True
/// </summary>
public string prepay_id { get; set; }



}
