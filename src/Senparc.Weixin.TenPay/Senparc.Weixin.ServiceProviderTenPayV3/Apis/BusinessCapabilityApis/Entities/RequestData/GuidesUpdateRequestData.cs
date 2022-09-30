/// <summary>
/// 服务人员信息更新请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_4_4.shtml
/// <summary>
public class GuidesUpdateRequestData
{

/// <summary>
/// 服务人员ID
/// path服务人员在支付即服务系统中的唯一标识 
/// 示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic
/// 
/// 可为空: True
/// </summary>
public string guide_id { get; set; }

/// <summary>
/// 子商户号
/// body子商户的商户号，由微信支付生成并下发。
/// 示例值：1234567890
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 服务人员姓名
/// body需更新的服务人员姓名，不更新无需传入，该字段请使用微信支付平台公钥加密，加密方法详见敏感信息加密说明 
/// 特殊规则：加密前字段长度限制为64个字节
/// 示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
/// 可为空: True
/// </summary>
public string name { get; set; }

/// <summary>
/// 服务人员手机号码
/// body需更新的服务人员手机号码，不更新无需传入，该字段请使用微信支付平台公钥加密，加密方法详见敏感信息加密说明 
/// 特殊规则：加密前字段长度限制为32个字节
/// 示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
/// 可为空: True
/// </summary>
public string mobile { get; set; }

/// <summary>
/// 服务人员二维码URL
/// body需更新的服务人员二维码，不更新无需传入，企业微信商家适用，个人微信商家不可用 
/// 示例值：https://open.work.weixin.qq.com/wwopen/userQRCode?vcode=xxx
/// 可为空: True
/// </summary>
public string qr_code { get; set; }

/// <summary>
/// 服务人员头像URL
/// body需更新的服务人员头像URL，不更新无需传入 
/// 示例值：http://wx.qlogo.cn/mmopen/ajNVdqHZLLA3WJ6DSZUfiakYe37PKnQhBIeOQBO4czqrnZDS79FH5Wm5m4X69TBicnHFlhiafvDwklOpZeXYQQ2icg/0
/// 可为空: True
/// </summary>
public string avatar { get; set; }

/// <summary>
/// 群二维码URL
/// 员工所在门店在企业微信配置的群活码的URL（可通过企业微信“获取客户群进群方式API”获取，请登录企业微信后查看API文档，若无查看权限可通过问卷提交需求 ）
/// 示例值：http://p.qpic.cn/wwhead/nMl9ssowtibVGyrmvBiaibzDtp/0
/// 可为空: True
/// </summary>
public string group_qrcode { get; set; }



}
