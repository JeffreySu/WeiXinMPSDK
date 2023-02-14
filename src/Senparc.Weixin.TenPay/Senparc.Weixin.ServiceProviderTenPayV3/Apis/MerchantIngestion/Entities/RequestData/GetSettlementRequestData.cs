/// <summary>
/// 查询结算账号请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_4.shtml
/// <summary>
public class GetSettlementRequestData
{

/// <summary>
/// 特约商户/二级商户号
/// path 请输入本服务商进件、已签约的特约商户/二级商户号。
/// 示例值：1900006491 
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }



}
