/// <summary>
/// 修改结算账号API请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_3.shtml
/// <summary>
public class UpdateAccountInfoRequestData
{

    /// <summary>
    /// 特约商户/二级商户号 
    /// path请填写本服务商负责进件的特约商户/二级商户号。 
    /// 特殊规则：长度最小8个字节。
    /// 示例值：1511101111 
    /// 可为空: True
    /// </summary>
    public string sub_mchid { get; set; }

    /// <summary>
    /// 账户类型 
    /// 根据特约商户/二级商户号的主体类型，可选择的账户类型如下： 
    /// 
    /// 1、小微主体：经营者个人银行卡
    /// 2、个体工商户主体：经营者个人银行卡/ 对公银行账户
    /// 3、企业主体：对公银行账户
    /// 4、党政、机关及事业单位主体：对公银行账户
    /// 5、其他组织主体：对公银行账户
    /// 
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
    ///  开户名称 
    /// body1、不需要修改开户名称时，可以不填写或填写当前绑定的结算银行卡户名；
    /// 2、支持将开户名称修改为当前商户对应的主体名称（对公银行账户）或经营者名称（个人银行账户），支持修改开户名称中括号的全半角；
    /// 3、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
    /// VyOMa+SncfM4lLha65dsxZ/xYW1zqBVVp6/W5mNkolESJU9fqgMt0lxjtuiWdhR+qUjnC2dTfuJuCOZs/Qi6kmicogGFjDC9ZxzFpdjR7AidWDuCIId5WRnRN8lGUcVyxctZZ4WcxxL2ADq57h7dZoFxNgyRYR4Y6q37LpYDccmYO5SiCkUP3rMX1CrTwKJysVhHij62HiU/P/yScImgdKrc+/MBWb1O6TT2RgwU3U6IwSZRWx4QH4EmYBLAQTdcEyUz2wuDmPA4nMSeXJVyzKl/WB+QYBh4Yj+BLT0HkA2IbTRyGX1U2wvv3N/w59Xq0pWYSXMHlmxhle2Cqj/7Cw==
    /// 
    /// 可为空: False
    /// </summary>
    public string[1, 1024] account_name { get; set; }

    /// <summary>
    /// 开户银行 
    /// 请填写开户银行名称
    /// 
    /// 			对私银行调用：查询支持个人业务的银行列表API
    /// 对公银行调用：查询支持对公业务的银行列表API。
    /// 示例值：工商银行
    /// 
    /// 可为空: True
    /// </summary>
    public string account_bank { get; set; }

    /// <summary>
    /// 开户银行省市编码 
    /// 需至少精确到市，详细参见省市区编号对照表。
    /// 示例值：110000 
    /// 可为空: True
    /// </summary>
    public string bank_address_code { get; set; }

    /// <summary>
    /// 开户银行全称（含支行） 
    /// 1、根据开户银行查询接口中的“是否需要填写支行”判断是否需要填写。如为其他银行，开户银行全称（含支行）和开户银行联行号二选一。
    /// 2、详细需调用查询支行列表API查看查询结果。
    /// 示例值：中国工商银行股份有限公司北京市分行营业部
    /// 可为空: False
    /// </summary>
    public string bank_name { get; set; }

    /// <summary>
    /// 开户银行联行号 
    /// 1、根据开户银行查询接口中的“是否需要填写支行”判断是否需要填写。如为其他银行，开户银行全称（含支行）和开户银行联行号二选一。 
    /// 											2、详细需调用查询支行列表API查看查询结果。
    /// 示例值：402713354941 
    /// 可为空: False
    /// </summary>
    public string bank_branch_id { get; set; }

    /// <summary>
    /// 银行账号 
    ///  1、数字，长度遵循系统支持的开户银行对照表中对公/对私卡号长度要求
    /// 2、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
    /// 示例值：d+xT+MQCvrLHUVDWv/8MR/dB7TkXM2YYZlokmXzFsWs35NXUot7C0NcxIrUF5FnxqCJHkNgKtxa6RxEYyba1+VBRLnqKG2fSy/Y5qDN08Ej9zHCwJjq52Wg1VG8MRugli9YMI1fI83KGBxhuXyemgS/hqFKsfYGiOkJqjTUpgY5VqjtL2N4l4z11T0ECB/aSyVXUysOFGLVfSrUxMPZy6jWWYGvT1+4P633f+R+ki1gT4WF/2KxZOYmli385ZgVhcR30mr4/G3HBcxi13zp7FnEeOsLlvBmI1PHN4C7Rsu3WL8sPndjXTd75kPkyjqnoMRrEEaYQE8ZRGYoeorwC+w== 
    /// 可为空: True
    /// </summary>
    public string account_number { get; set; }



}
