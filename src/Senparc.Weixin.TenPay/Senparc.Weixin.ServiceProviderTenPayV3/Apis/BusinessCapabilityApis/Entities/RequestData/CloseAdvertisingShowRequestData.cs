/// <summary>
/// 关闭广告展示请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_5_5.shtml
/// <summary>
public class CloseAdvertisingShowRequestData
{

/// <summary>
/// 特约商户号
/// 需要关闭广告展示的特约商户号，由微信支付生成并下发。
/// 示例值：1900000109
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }



}
