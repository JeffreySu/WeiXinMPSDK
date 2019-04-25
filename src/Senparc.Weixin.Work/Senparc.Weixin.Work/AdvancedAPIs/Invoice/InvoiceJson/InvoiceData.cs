/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：InvoiceData.cs
    文件功能描述：开票平台接口post数据
    
    
    创建标识：Senparc - 20181009
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoiceUserData
    {
        /// <summary>
        /// 发票的金额，以分为单位
        /// </summary>
        public int fee { get; set; }
        /// <summary>
        /// 发票的抬头
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 发票的开票时间，为10位时间戳
        /// </summary>
        public int billing_time { get; set; }
        /// <summary>
        /// 发票的发票代码
        /// </summary>
        public string billing_no { get; set; }
        /// <summary>
        /// 发票的发票号码
        /// </summary>
        public string billing_code { get; set; }
        /// <summary>
        /// 商品详情结构
        /// </summary>
        public List<ProjectInfo> info { get; set; }
        /// <summary>
        /// 不含税金额，以分为单位
        /// </summary>
        public int fee_without_tax { get; set; }
        /// <summary>
        /// 税额，以分为单位
        /// </summary>
        public int tax { get; set; }
        /// <summary>
        /// 发票pdf文件上传到微信发票平台后，会生成一个发票
        /// </summary>
        public string s_pdf_media_id { get; set; }
        /// <summary>
        /// 其它消费附件的PDF
        /// </summary>
        public string s_trip_pdf_media_id { get; set; }
        /// <summary>
        /// 校验码，发票pdf右上角，开票日期下的校验码
        /// </summary>
        public string check_code { get; set; }
        /// <summary>
        /// 购买方纳税人识别号
        /// </summary>
        public string buyer_number { get; set; }
        /// <summary>
        /// 购买方地址、电话
        /// </summary>
        public string buyer_address_and_phone { get; set; }
        /// <summary>
        /// 购买方开户行及账号
        /// </summary>
        public string buyer_bank_account { get; set; }
        /// <summary>
        /// 销售方纳税人识别号
        /// </summary>
        public string seller_number { get; set; }
        /// <summary>
        /// 销售方地址、电话
        /// </summary>
        public string seller_address_and_phone { get; set; }
        /// <summary>
        /// 销售方开户行及账号
        /// </summary>
        public string seller_bank_account { get; set; }
        /// <summary>
        /// 备注，发票右下角处
        /// </summary>
        public string remarks { get; set; }
        /// <summary>
        /// 收款人，发票左下角处
        /// </summary>
        public string cashier { get; set; }
        /// <summary>
        /// 开票人，发票下方处
        /// </summary>
        public string maker { get; set; }
    }

    /// <summary>
    /// 商品详情结构
    /// </summary>
    public class ProjectInfo
    {
        /// <summary>
        /// 项目的名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 项目的数量
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 项目的单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 项目的单价
        /// </summary>
        public int price { get; set; }

    }

    /// <summary>
    /// 单张发票
    /// </summary>
    public class InvoiceItem
    {
        /// <summary>
        /// 发票卡券的 card_id
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 发票卡券的加密 code ，和 card_id 共同构成一张发票卡券的唯一标识
        /// </summary>
        public string encrypt_code { get; set; }
    }
}
