/// <summary>
/// 申请资金账单返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_2_7.shtml
/// <summary>
public class AppFundFlowBillReturnJson
{

/// <summary>
/// 哈希类型 
/// 枚举值：
/// SHA1：SHA1值 
/// 示例值：SHA1
/// 可为空: True
/// </summary>
public string hash_type { get; set; }

/// <summary>
/// 哈希值 
/// 原始账单（gzip需要解压缩）的摘要值，用于校验文件的完整性。 
/// 示例值：79bb0f45fc4c42234a918000b2668d689e2bde04 
/// 可为空: True
/// </summary>
public string hash_value { get; set; }

/// <summary>
/// 账单下载地址 
/// 供下一步请求账单文件的下载地址，该地址30s内有效。 
/// 示例值：https://api.mch.weixin.qq.com/v3/billdownload/file?token=xxx 
/// 可为空: True
/// </summary>
public string download_url { get; set; }



}
