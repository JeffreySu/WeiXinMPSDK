/// <summary>
/// 服务人员查询返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_4_3.shtml
/// <summary>
public class GuidesQueruReturnJson
{

/// <summary>
/// 服务人员列表
/// 服务人员列表
/// 可为空: True
/// </summary>
public Data[] data { get; set; }

/// <summary>
/// 服务人员数量
/// 符合条件筛选的服务人员数量，当服务人员列表返回为空时，服务人员数量返回0；当服务人员列表有返回值时，服务人员数量按查询到的值返回。 
/// 示例值：10
/// 可为空: True
/// </summary>
public int total_count { get; set; }

/// <summary>
/// 最大资源条数
/// 该次请求可返回的最大资源条数，不大于10 
/// 示例值：5
/// 可为空: True
/// </summary>
public int limit { get; set; }

/// <summary>
/// 请求资源起始位置
/// 该次请求资源的起始位置，默认值为0 
/// 示例值：0
/// 可为空: True
/// </summary>
public int offset { get; set; }


 #region 子数据类型

/// <summary>
/// 服务人员列表
/// 服务人员列表
/// <summary>
public class Data
{

/// <summary>
/// 服务人员ID
/// 服务人员在服务人员系统中的唯一标识 
/// 示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic
/// 可为空: True
/// </summary>
public string guide_id { get; set; }

/// <summary>
/// 门店ID
/// 门店在微信支付商户平台的唯一标识
/// （查找路径：登录商户平台—>营销中心—>门店管理，若无门店则需先创建门店）
/// 示例值：12345678
/// 可为空: True
/// </summary>
public int store_id { get; set; }

/// <summary>
/// 服务人员姓名
/// 员工在商户个人/企业微信通讯录上的姓名,需使用微信支付平台公钥加密
/// 该字段需进行加密处理，加密方法详见敏感信息加密说明。
/// 特殊规则：加密前字段长度限制为64个字节
/// 示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg== 
/// 可为空: True
/// </summary>
public string name { get; set; }

/// <summary>
/// 服务人员手机号码
/// 员工在商户个人/企业微信通讯录上设置的手机号码，使用微信支付平台公钥加密
/// 该字段需进行加密处理，加密方法详见敏感信息加密说明。
/// 特殊规则：加密前字段长度限制为32个字节
/// 示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg== 
/// 可为空: True
/// </summary>
public string mobile { get; set; }

/// <summary>
/// 企业微信的员工ID
/// 员工在商户企业微信通讯录使用的唯一标识，使用企业微信商家时返回
/// 示例值：robert
/// 可为空: True
/// </summary>
public string userid { get; set; }

/// <summary>
/// 工号
/// 服务人员通过小程序注册时填写的工号，使用个人微信商家时返回 
/// 示例值：robert多选一
/// 可为空: True
/// </summary>
public string work_id { get; set; }



}

#endregion
}
