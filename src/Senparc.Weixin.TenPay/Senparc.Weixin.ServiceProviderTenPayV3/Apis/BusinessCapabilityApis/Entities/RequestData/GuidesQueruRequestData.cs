/// <summary>
/// 服务人员查询请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_4_3.shtml
/// <summary>
public class GuidesQueruRequestData
{

/// <summary>
/// 子商户ID
/// query服务人员所属商户的商户ID
/// 示例值：1234567890
/// 
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 门店ID
/// query门店在微信支付商户平台的唯一标识 
/// 示例值：1234
/// 
/// 可为空: True
/// </summary>
public int store_id { get; set; }

/// <summary>
/// 企业微信的员工ID
/// query员工在商户企业微信通讯录使用的唯一标识，企业微信商家可传入该字段查询单个服务人员信息；不传则查询整个门店下的服务人员信息 
/// 示例值：robert
/// 
/// 可为空: True
/// </summary>
public string userid { get; set; }

/// <summary>
/// 手机号码
/// query服务人员通过小程序注册时填写的手机号码，企业微信/个人微信商家可传入该字段查询单个服务人员信息；不传则查询整个门店下的服务人员信息。
/// 						该字段需进行加密处理，加密方法详见敏感信息加密说明 
/// 特殊规则：加密前字段长度限制为32个字节
/// 示例值：str2WoWy8uUiWM7xahvNv0lV3C+nn3t4QlKNnyr+iwlk2FoMcn/lxrR6YdKKFI6NFJkC5oyvzNwc1MDzIgjLR0W6bJKiwWeOV3Fp0x+VIoRDONal1Mgb6VTYg7lvACEqdwmVkvbt4/oEWaWP62njMGGe0fMiBSAvao3LrcsOwDvN3E9kiaw5XQZMP/rUdWTFfy5THuohcQobGMrxclHKAnwAHU8CWfkziW5crUc3Z83uMVcFQu38y9EcWR12FJ3jip5nyVKiqCF4iDSN+JRXjWsWlqTZ0Y8Q+piBCS5ACusK6nz7mKQeypi9fKjAggRfvNFPf/bNxPvork4mMVgZkA==
/// 
/// 可为空: True
/// </summary>
public string mobile { get; set; }

/// <summary>
/// 工号
/// query服务人员通过小程序注册时填写的工号，个人微信商家可传入该字段查询单个服务人员信息；不传则查询整个门店下的服务人员信息 
/// 示例值：robert
/// 
/// 可为空: True
/// </summary>
public string work_id { get; set; }

/// <summary>
/// 最大资源条数
/// query商家自定义字段，该次请求可返回的最大资源条数，不大于10 
/// 示例值：5
/// 
/// 可为空: True
/// </summary>
public int limit { get; set; }

/// <summary>
/// 请求资源起始位置
/// query商家自定义字段，该次请求资源的起始位置，默认值为0 
/// 						
/// 示例值：0
/// 
/// 可为空: True
/// </summary>
public int offset { get; set; }



}
