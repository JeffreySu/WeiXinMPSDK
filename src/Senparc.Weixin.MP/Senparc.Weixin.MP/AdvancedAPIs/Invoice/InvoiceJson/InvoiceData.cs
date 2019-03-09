/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：InvoiceData.cs
    文件功能描述：电子票据post数据
    
    
    创建标识：Senparc - 20180930
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 获取授权页链接数据
    /// </summary>
    public class GetBillAuthUrlData
    {
        /// <summary>
        /// 财政局id，需要找财政局提供
        /// </summary>
        public string s_pappid { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 订单金额，以分为单位
        /// </summary>
        public int money { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public int timestamp { get; set; }
        /// <summary>
        /// 开票来源，web：公众号开票，app：app开票
        /// </summary>
        public SourceType source { get; set; }
        /// <summary>
        /// 授权成功后跳转页面
        /// </summary>
        public string redirect_url { get; set; }
        /// <summary>
        /// Api_ticket，参考获取api_ticket接口获取
        /// </summary>
        public string ticket { get; set; }
    }

    /// <summary>
    /// 开票授权页字段
    /// </summary>
    public class PayMchInfoData
    {
        /// <summary>
        /// 微信支付商户号
        /// </summary>
        public string mchid { get; set; }
        /// <summary>
        /// 开票平台id，需要找开票平台提供
        /// </summary>
        public string s_pappid { get; set; }
    }

    /// <summary>
    /// 用户授权数据
    /// </summary>
    public class AuthFieldData
    {
        /// <summary>
        /// 授权页个人发票字段
        /// </summary>
        public UserFiledData user_field { get; set; }
        /// <summary>
        /// 授权页单位发票字段
        /// </summary>
        public BizField biz_field { get; set; }
    }

    /// <summary>
    /// 授权页个人发票字段
    /// </summary>
    public class UserFiledData
    {
        /// <summary>
        /// 是否填写抬头，0为否，1为是
        /// </summary>
        public int show_title { get; set; }
        /// <summary>
        /// 是否填写电话号码，0为否，1为是
        /// </summary>
        public int show_phone { get; set; }
        /// <summary>
        /// 是否填写邮箱，0为否，1为是
        /// </summary>
        public int show_email { get; set; }
        /// <summary>
        /// 自定义字段
        /// </summary>
        public List<CustomFieldItem> custom_field { get; set; }
    }

    /// <summary>
    /// 授权页单位发票字段
    /// </summary>
    public class BizField
    {
        /// <summary>
        /// 是否填写抬头，0为否，1为是
        /// </summary>
        public int show_title { get; set; }
        /// <summary>
        /// 是否填写税号，0为否，1为是
        /// </summary>
        public int show_tax_no { get; set; }
        /// <summary>
        /// 是否填写单位地址，0为否，1为是
        /// </summary>
        public int show_addr { get; set; }
        /// <summary>
        /// 是否填写电话号码，0为否，1为是
        /// </summary>
        public int show_phone { get; set; }
        /// <summary>
        /// 是否填写开户银行，0为否，1为是
        /// </summary>
        public int show_bank_type { get; set; }
        /// <summary>
        /// 是否填写银行帐号，0为否，1为是
        /// </summary>
        public int show_bank_no { get; set; }
        /// <summary>
        /// 自定义字段
        /// </summary>
        public List<CustomFieldItem> custom_field { get; set; }
    }

    /// <summary>
    /// 自定义字段
    /// </summary>
    public class CustomFieldItem
    {
        /// <summary>
        /// 自定义字段名称，最长5个字
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 自定义填写项用户填写的信息
        /// </summary>
        public string value { get; set; }
    }

    /// <summary>
    /// 获取授权页链接数据
    /// </summary>
    public class GetAuthUrlData
    {
        /// <summary>
        /// 开票平台在微信的标识号，商户需要找开票平台提供
        /// </summary>
        public string s_pappid { get; set; }
        /// <summary>
        /// 订单id，在商户内单笔开票请求的唯一识别号
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 订单金额，以分为单位
        /// </summary>
        public int money { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public int timestamp { get; set; }
        /// <summary>
        /// 开票来源，app：app开票，web：微信h5开票，wxa：小程序开发票，wap：普通网页开票
        /// </summary>
        public SourceType source { get; set; }
        /// <summary>
        /// 授权成功后跳转页面
        /// </summary>
        public string redirect_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 授权类型
        /// </summary>
        public AuthType type { get; set; }
    }
}
