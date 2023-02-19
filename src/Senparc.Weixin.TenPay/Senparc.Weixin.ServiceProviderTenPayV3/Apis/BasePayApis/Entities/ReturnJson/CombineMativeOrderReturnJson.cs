/// <summary>
/// 合单Native下单返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter5_1_5.shtml
/// <summary>
public class CombineMativeOrderReturnJson
{

/// <summary>
/// 二维码链接
/// 二维码链接
/// 示例值：weixin://pay.weixin.qq.com/bizpayurl/up?pr=NwY5Mz9&groupid=00 
/// 可为空: True
/// </summary>
public string code_url { get; set; }



}
