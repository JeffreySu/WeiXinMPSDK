/// <summary>
/// 合单关闭订单请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter5_1_12.shtml
/// <summary>
public class CombineCloseOrderRequestData
{

/// <summary>
/// 合单商户appid 
/// 合单发起方的appid。 
/// 示例值：wxd678efh567hg6787 
/// 可为空: True
/// </summary>
public string combine_appid { get; set; }

/// <summary>
/// 合单商户订单号 
/// path
/// 								合单支付总订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。 
/// 示例值：P20150806125346 
/// 可为空: True
/// </summary>
public string combine_out_trade_no { get; set; }

/// <summary>
/// 子单信息
/// 最多支持子单条数：50
/// 可为空: True
/// </summary>
public Sub_Orders[] sub_orders { get; set; }


 #region 子数据类型

/// <summary>
/// 子单信息
/// 最多支持子单条数：50
/// <summary>
public class Sub_Orders
{

/// <summary>
/// 子单商户号 
/// 子单发起方商户号，必须与发起方appid有绑定关系。 
/// 示例值：1900000109 
/// 可为空: True
/// </summary>
public string mchid { get; set; }

/// <summary>
/// 子单商户订单号 
/// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。 
/// 示例值：20150806125346 
/// 可为空: True
/// </summary>
public string out_trade_no { get; set; }

/// <summary>
/// 二级商户号 
/// 二级商户商户号，由微信支付生成并下发。服务商子商户的商户号，被合单方。直连商户不用传二级商户号。 
/// 												 
/// 											示例值：1900000109
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
///  子商户应用ID 
/// 服务商模式下，sub_mchid对应的sub_appid
/// 示例值：wxd678efh567hg6999
/// 可为空: True
/// </summary>
public string  sub_appid { get; set; }



}

#endregion
}
