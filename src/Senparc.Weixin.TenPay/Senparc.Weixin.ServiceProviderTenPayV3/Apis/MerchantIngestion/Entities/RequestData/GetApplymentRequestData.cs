/// <summary>
/// 查询申请单状态请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_2.shtml
/// <summary>
public class GetApplymentRequestData
{

/// <summary>
/// 业务申请编号 
/// path
/// 
/// 1、只能由数字、字母或下划线组成，建议前缀为服务商商户号。 
/// 2、服务商自定义的唯一编号。 
/// 3、每个编号对应一个申请单，每个申请单审核通过后生成一个微信支付商户号。 
/// 4、若申请单被驳回，可填写相同的“业务申请编号”，即可覆盖修改原申请单信息。 
/// 
/// 示例值：1900013511_10000 
/// 可为空: True
/// </summary>
public string business_code { get; set; }



}
