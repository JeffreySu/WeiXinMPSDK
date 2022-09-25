/// <summary>
/// 提交申请单返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_1.shtml
/// <summary>
public class ApplyResultJson
{

    /// <summary>
    /// 微信支付申请单号 
    /// 微信支付分配的申请单号
    /// 示例值：2000002124775691
    /// 可为空: True
    /// </summary>
    public int applyment_id { get; set; }

}
