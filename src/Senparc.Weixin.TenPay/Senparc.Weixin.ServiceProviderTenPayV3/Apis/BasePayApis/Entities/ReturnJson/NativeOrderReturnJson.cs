/// <summary>
/// Native下单返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_4_1.shtml
/// <summary>
public class NativeOrderReturnJson
{

/// <summary>
/// 二维码链接 
/// 此URL用于生成支付二维码，然后提供给用户扫码支付。 
/// 注意：code_url并非固定值，使用时按照URL格式转成二维码即可。 
/// 示例值：weixin://wxpay/bizpayurl/up?pr=NwY5Mz9&groupid=00 
/// 可为空: True
/// </summary>
public string code_url { get; set; }



}
