/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MakeOutInvoiceData.cs
    文件功能描述：统一开票post数据
    
    
    创建标识：Senparc - 20180930
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 统一开票接口-开具蓝票数据
    /// </summary>
    public class MakeOutInvoiceData
    {
        /// <summary>
        /// 用户的openid 用户知道是谁在开票
        /// </summary>
        public string wxopenid { get; set; }
        /// <summary>
        /// 订单号，企业自己内部的订单号码
        /// </summary>
        public string ddh { get; set; }
        /// <summary>
        /// 发票请求流水号，唯一识别开票请求的流水号
        /// </summary>
        public string fpqqlsh { get; set; }
        /// <summary>
        /// 纳税人识别码
        /// </summary>
        public string nsrsbh { get; set; }
        /// <summary>
        /// 纳税人名称
        /// </summary>
        public string nsrmc { get; set; }
        /// <summary>
        /// 纳税人地址
        /// </summary>
        public string nsrdz { get; set; }
        /// <summary>
        /// 纳税人电话
        /// </summary>
        public string nsrdh { get; set; }
        /// <summary>
        /// 纳税人开户行
        /// </summary>
        public string nsrbank { get; set; }
        /// <summary>
        /// 纳税人银行账号
        /// </summary>
        public string nsrbankid { get; set; }
        /// <summary>
        /// 购货方名称
        /// </summary>
        public string ghfmc { get; set; }
        /// <summary>
        /// 购货方识别号
        /// </summary>
        public string ghfnsrsbh { get; set; }
        /// <summary>
        /// 购货方地址
        /// </summary>
        public string ghfdz { get; set; }
        /// <summary>
        /// 购货方电话
        /// </summary>
        public string ghfdh { get; set; }
        /// <summary>
        /// 购货方开户行
        /// </summary>
        public string ghfbank { get; set; }
        /// <summary>
        /// 购货方银行帐号
        /// </summary>
        public string ghfbankid { get; set; }
        /// <summary>
        /// 开票人
        /// </summary>
        public string kpr { get; set; }
        /// <summary>
        /// 收款人
        /// </summary>
        public string skr { get; set; }
        /// <summary>
        /// 复核人
        /// </summary>
        public string fhr { get; set; }
        /// <summary>
        /// 价税合计
        /// </summary>
        public string jshj { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public string hjse { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 行业类型 0 商业 1其它
        /// </summary>
        public string hylx { get; set; }
        /// <summary>
        /// 发票行项目数据
        /// </summary>
        public List<string> invoicedetail_list { get; set; }
    }

    /// <summary>
    /// 发票行项目数据
    /// </summary>
    public class InvoiceDetailItem
    {
        /// <summary>
        /// 发票行性质 0 正常 1折扣 2 被折扣
        /// </summary>
        public Fphxz fphxz { get; set; }
        /// <summary>
        /// 19位税收分类编码
        /// </summary>
        public string spbm { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string ggxh { get; set; }
        /// <summary>
        /// 项目数量
        /// </summary>
        public string xmsl { get; set; }
        /// <summary>
        /// 项目单价
        /// </summary>
        public string xmdj { get; set; }
        /// <summary>
        /// 项目金额 不含税，单位元 两位小数
        /// </summary>
        public string xmje { get; set; }
        /// <summary>
        /// 税率 精确到两位小数 如0.01
        /// </summary>
        public string sl { get; set; }
        /// <summary>
        /// 税额 单位元 两位小数
        /// </summary>
        public string se { get; set; }
    }

    /// <summary>
    /// 统一开票接口-发票冲红数据
    /// </summary>
    public class ClearOutInvoiceData
    {
        /// <summary>
        /// 用户的openid 用户知道是谁在开票
        /// </summary>
        public string wxopenid { get; set; }
        /// <summary>
        /// 发票请求流水号，唯一识别开票请求的流水号
        /// </summary>
        public string fpqqlsh { get; set; }
        /// <summary>
        /// 纳税人识别码
        /// </summary>
        public string nsrsbh { get; set; }
        /// <summary>
        /// 纳税人名称
        /// </summary>
        public string nsrmc { get; set; }
        /// <summary>
        /// 原发票代码
        /// </summary>
        public string yfpdm { get; set; }
        /// <summary>
        /// 原发票号码
        /// </summary>
        public string yfphm { get; set; }
    }

    /// <summary>
    /// 将发票抬头信息录入到用户微信中数据
    /// </summary>
    public class GetUserTitleUrlData
    {
        public string title { get; set; }
        public string phone { get; set; }
        public string tax_no { get; set; }
        public string addr { get; set; }
        public string bank_type { get; set; }
        public string bank_no { get; set; }
        public int user_fill { get; set; }
        public string out_title_id { get; set; }
    }
}
