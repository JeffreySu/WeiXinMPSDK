/// <summary>
/// 服务人员注册请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_4_1.shtml
/// <summary>
public class GuidesRegisterRequestData
{

/// <summary>
/// 子商户ID
/// 服务人员所属商户的商户ID
/// 示例值：1234567890
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 企业ID
/// 商户的企业微信唯一标识
/// 示例值：1234567890
/// 可为空: True
/// </summary>
public string corpid { get; set; }

/// <summary>
/// 门店ID
/// 门店在微信支付商户平台的唯一标识（查找路径：登录商户平台—>营销中心—>门店管理，若无门店则需先创建门店）
/// 示例值：12345678
/// 可为空: True
/// </summary>
public int store_id { get; set; }

/// <summary>
/// 企业微信的员工ID
/// 员工在商户企业微信通讯录使用的唯一标识（企业微信的员工信息可通过接口从企业微信通讯录获取，具体请参考企业微信的API文档 ）
/// 示例值：robert
/// 可为空: True
/// </summary>
public string userid { get; set; }

/// <summary>
/// 企业微信的员工姓名
/// 员工在商户企业微信通讯录上的姓名,需使用微信支付平台公钥加密
/// 						该字段需进行加密处理，加密方法详见敏感信息加密说明。
/// 特殊规则：加密前字段长度限制为64个字节
/// 示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg== 
/// 
/// 可为空: True
/// </summary>
public string name { get; set; }

/// <summary>
/// 手机号码
/// 员工在商户企业微信通讯录上设置的手机号码，使用微信支付平台公钥加密
/// 该字段需进行加密处理，加密方法详见敏感信息加密说明。
/// 特殊规则：加密前字段长度限制为32个字节
/// 示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg== 
/// 
/// 可为空: True
/// </summary>
public string mobile { get; set; }

/// <summary>
/// 员工个人二维码
/// 员工在商户企业微信通讯录上的二维码串
/// 示例值：https://open.work.weixin.qq.com/wwopen/userQRCode?vcode=xxx
/// 可为空: True
/// </summary>
public string qr_code { get; set; }

/// <summary>
/// 头像URL
/// 员工在商户企业微信通讯录上头像的URL
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
