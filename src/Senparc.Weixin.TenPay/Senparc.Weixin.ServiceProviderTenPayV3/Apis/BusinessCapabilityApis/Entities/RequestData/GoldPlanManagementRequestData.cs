/// <summary>
/// 点金计划管理请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_5_1.shtml
/// <summary>
public class GoldPlanManagementRequestData
{

/// <summary>
/// 特约商户号
/// 
/// 											开通或关闭点金计划的特约商户商户号，由微信支付生成并下发。 
/// 示例值：1234567890
/// 可为空: True
/// </summary>
public string sub_mchid { get; set; }

/// <summary>
/// 操作类型
/// 
/// 											开通或关闭点金计划的动作，枚举值： 
/// 											OPEN：表示开通点金计划 
/// 											CLOSE：表示关闭点金计划 
/// 示例值：OPEN
/// 可为空: True
/// </summary>
public string operation_type { get; set; }



}
