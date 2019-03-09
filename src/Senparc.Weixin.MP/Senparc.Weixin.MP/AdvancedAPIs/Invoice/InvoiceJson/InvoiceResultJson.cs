/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：InvoiceResultJson.cs
    文件功能描述：电子票据返回结果
    
    
    创建标识：Senparc - 20180930
 
   
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 查询支付后开票信息返回结果
    /// </summary>
    public class GetPayMchResultJson : WxJsonResult
    {
        /// <summary>
        /// 授权页字段
        /// </summary>
        public PayMchInfoData paymch_info { get; set; }
    }

    /// <summary>
    /// 查询授权页字段信息返回结果
    /// </summary>
    public class AuthFieldResultJson : WxJsonResult
    {
        /// <summary>
        /// 授权页字段
        /// </summary>
        public AuthFieldData auth_field { get; set; }
    }

    /// <summary>
    /// 查询开票信息返回结果
    /// </summary>
    public class AuthDataResultJson : WxJsonResult
    {
        /// <summary>
        /// 订单授权状态
        /// </summary>
        public string invoice_status { get; set; }
        /// <summary>
        /// 授权时间，为十位时间戳（utc+8）
        /// </summary>
        public int auth_time { get; set; }
        /// <summary>
        /// 用户授权信息结构体，仅在type=1时出现
        /// </summary>
        public UserAuthInfo user_auth_info { get; set; }
    }

    /// <summary>
    /// 获取授权页链接返回结果
    /// </summary>
    public class AuthUrlResultJson : WxJsonResult
    {
        /// <summary>
        /// 授权链接
        /// </summary>
        public string auth_url { get; set; }
        /// <summary>
        /// source为wxa时才有
        /// </summary>
        public string appid { get; set; }
    }

    /// <summary>
    /// 获取授权页链接返回结果
    /// </summary>
    public class GetBillAuthUrlResultJson : WxJsonResult
    {
        /// <summary>
        /// 授权链接
        /// </summary>
        public string auth_url { get; set; }
        /// <summary>
        /// 过期时间，单位为秒，授权链接会在一段时间之后过期
        /// </summary>
        public int expire_time { get; set; }
    }

    /// <summary>
    /// 将发票抬头信息录入到用户微信中返回结果
    /// </summary>
    public class GetUserTitleUrlResultJson : WxJsonResult
    {
        /// <summary>
        /// 添加抬头卡链接
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 获取用户抬头（方式二）:商户扫描用户的发票抬头二维码返回结果
    /// </summary>
    public class ScanTitleResultJson : WxJsonResult
    {
        /// <summary>
        /// 抬头类型
        /// </summary>
        public int title_type { get; set; }
        public string title { get; set; }
        public string phone { get; set; }
        public string tax_no { get; set; }
        public string addr { get; set; }
        public string bank_type { get; set; }
        public string bank_no { get; set; }
    }

    /// <summary>
    /// 用户授权信息结构体
    /// </summary>
    public class UserAuthInfo
    {
        /// <summary>
        /// 个人类型发票的授权信息结构体
        /// </summary>
        public UserFiledInfo user_field { get; set; }
        /// <summary>
        /// 单位类型发票的授权信息结构体
        /// </summary>
        public BizFieldInfo biz_field { get; set; }


    }

    /// <summary>
    /// 单位类型发票的授权信息结构体
    /// </summary>
    public class BizFieldInfo
    {
        public string title { get; set; }
        /// <summary>
        /// 单位联系电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 单位税号
        /// </summary>
        public string tax_no { get; set; }
        /// <summary>
        /// 单位注册地址
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        /// 单位开户银行
        /// </summary>
        public string bank_type { get; set; }
        /// <summary>
        /// 单位开户银行账号
        /// </summary>
        public string bank_no { get; set; }
        /// <summary>
        /// 商户自定义信息结构体
        /// </summary>
        public List<CustomFieldItem> custom_field { get; set; }
    }

    /// <summary>
    /// 个人类型发票的授权信息结构体
    /// </summary>
    public class UserFiledInfo
    {
        /// <summary>
        /// 个人抬头
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 个人联系电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 个人邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 商户自定义信息结构体
        /// </summary>
        public List<CustomFieldItem> custom_field { get; set; }
    }

    /// <summary>
    /// 查询已开发票返回信息
    /// </summary>
    public class QueryInvoiceResultJson : WxJsonResult
    {
        /// <summary>
        /// 发票详情
        /// </summary>
        public InvoiceDetail invoicedetail { get; set; }
    }

    /// <summary>
    /// 发票详情
    /// </summary>
    public class InvoiceDetail
    {
        /// <summary>
        /// 发票请求流水号，唯一查询发票的流水号
        /// </summary>
        public string fpqqlsh { get; set; }
        /// <summary>
        /// 校验码，位于电子发票右上方，开票日期下
        /// </summary>
        public string jym { get; set; }
        /// <summary>
        /// 校验码
        /// </summary>
        public string kprq { get; set; }
        /// <summary>
        /// 发票代码
        /// </summary>
        public string fpdm { get; set; }
        /// <summary>
        /// 发票号码
        /// </summary>
        public string fphm { get; set; }
        /// <summary>
        /// 发票url
        /// </summary>
        public string pdfurl { get; set; }
    }

    /// <summary>
    /// 查询商户联系方式返回信息
    /// </summary>
    public class GetContactResultJson : WxJsonResult
    {
        /// <summary>
        /// 联系方式信息
        /// </summary>
        public Contact contact { get; set; }
    }

    /// <summary>
    /// 联系方式信息
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 开票超时时间
        /// </summary>
        public int time_out { get; set; }
    }
}
