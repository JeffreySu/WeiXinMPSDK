/// <summary>
/// 查询结算账号返回json
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_4.shtml
/// <summary>
public class GetSettlementReturnJson
{

/// <summary>
/// 账户类型 
/// 返回特约商户的结算账户类型。 
/// 枚举值：
/// 
/// 
/// ACCOUNT_TYPE_BUSINESS：对公银行账户 
/// ACCOUNT_TYPE_PRIVATE：经营者个人银行卡 
/// 
/// 示例值：ACCOUNT_TYPE_BUSINESS
/// 可为空: True
/// </summary>
public string account_type { get; set; }

/// <summary>
/// 开户银行 
/// 返回特约商户的结算账户-开户银行全称。 
/// 示例值：工商银行
/// 可为空: True
/// </summary>
public string account_bank { get; set; }

/// <summary>
/// 开户银行全称（含支行）
/// 返回特约商户的结算账户-开户银行全称（含支行）。 
/// 示例值：施秉县农村信用合作联社城关信用社
/// 可为空: True
/// </summary>
public string bank_name { get; set; }

/// <summary>
/// 开户银行联行号 
/// 返回特约商户的结算账户-联行号。 
/// 示例值：402713354941 
/// 可为空: True
/// </summary>
public string bank_branch_id { get; set; }

/// <summary>
/// 银行账号 
/// 返回特约商户的结算账户-银行账号，掩码显示。 
/// 示例值：62*************78
/// 可为空: True
/// </summary>
public string account_number { get; set; }

/// <summary>
/// 汇款验证结果 
/// 返回特约商户的结算账户-汇款验证结果。
///VERIFYING：系统汇款验证中，商户可发起提现尝试。
///VERIFY_SUCCESS：系统成功汇款，该账户可正常发起提现。
///VERIFY_FAIL：系统汇款失败，该账户无法发起提现，请检查修改。
/// 注：该字段，入驻后若没有修改过银行卡，除非汇款失败，否则是不返回的
/// 示例值：VERIFY_SUCCESS
/// 可为空: True
/// </summary>
public string verify_result { get; set; }

/// <summary>
/// 汇款验证失败原因
/// 如果汇款验证成功为空，汇款验证失败为具体原因。
/// 示例值：用户姓名/证件/手机不匹配，请核对后重试
/// 可为空: True
/// </summary>
public string verify_fail_reason { get; set; }



}
