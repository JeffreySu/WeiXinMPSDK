/// <summary>
/// 合单查询订单请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter5_1_11.shtml
/// <summary>
public class CombineGetOrderRequestData
{

/// <summary>
/// 合单商户订单号 
/// path
/// 								合单支付总订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一 。
/// 示例值：P20150806125346 
/// 可为空: True
/// </summary>
public string combine_out_trade_no { get; set; }



}
