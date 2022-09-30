/// <summary>
/// 申请资金账单请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter5_1_18.shtml
/// <summary>
public class CombineFundFlowBillRequestData
{

/// <summary>
/// 账单日期 
/// query 格式yyyy-MM-dd 
/// 								仅支持三个月内的账单下载申请。 
/// 示例值：2019-06-11 
/// 可为空: True
/// </summary>
public string bill_date { get; set; }

/// <summary>
/// 资金账户类型 
/// query 不填则默认是BASIC 
/// 								枚举值：
/// 								BASIC：基本账户 
/// 								OPERATION：运营账户 
/// 								FEES：手续费账户 
/// 示例值：BASIC 
/// 可为空: True
/// </summary>
public string account_type { get; set; }

/// <summary>
/// 压缩类型 
/// query 不填则默认是数据流 
/// 								枚举值：
/// 								GZIP：返回格式为.gzip的压缩包账单 
/// 示例值：GZIP 
/// 可为空: True
/// </summary>
public string tar_type { get; set; }



}
