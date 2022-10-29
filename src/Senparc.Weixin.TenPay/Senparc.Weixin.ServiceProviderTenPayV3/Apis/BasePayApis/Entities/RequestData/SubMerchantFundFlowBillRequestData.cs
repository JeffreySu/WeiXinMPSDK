/// <summary>
/// 申请单个子商户资金账单 请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_1_12.shtml
/// <summary>
public class SubMerchantFundFlowBillRequestData
{

/// <summary>
/// 子商户号
/// query下载指定子商户的账单。
/// 示例值：19000000001
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 账单日期
/// query格式yyyy-MM-dd
/// 示例值：2019-06-11
/// 可为空: True
/// </summary>
public string bill_date { get; set; }

/// <summary>
/// 资金账户类型
/// query枚举值：
/// BASIC：基本账户
/// OPERATION：运营账户
/// FEES：手续费账户
/// 示例值：BASIC
/// 可为空: True
/// </summary>
public string account_type { get; set; }

/// <summary>
/// 加密算法
/// query枚举值：
/// AEAD_AES_256_GCM：AEAD_AES_256_GCM加密算法 
/// SM4_GCM：SM4_GCM加密算法，密钥长度128bit
/// 示例值：AEAD_AES_256_GCM
/// 可为空: True
/// </summary>
public string algorithm { get; set; }

/// <summary>
/// 压缩格式
/// query不填则以不压缩的方式返回数据流
/// 枚举值：
/// GZIP：返回格式为.gzip的压缩包账单
/// 示例值：GZIP
/// 可为空: True
/// </summary>
public string tar_type { get; set; }



}
