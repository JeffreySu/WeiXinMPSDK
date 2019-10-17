/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：InvoicePlatformData.cs
    文件功能描述：开票平台接口post数据
    
    
    创建标识：Senparc - 20180930
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 发票模板对象
    /// </summary>
    public class InvoiceInfo
    {
        /// <summary>
        /// 发票卡券模板基础信息
        /// </summary>
        public InvoiceBaseInfo base_info { get; set; }
        /// <summary>
        /// 收款方（开票方）全称，显示在发票详情内。故建议一个收款方对应一个发票卡券模板
        /// </summary>
        public string payee { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string type { get; set; }
    }

    /// <summary>
    /// 发票卡券模板基础信息
    /// </summary>
    public class InvoiceBaseInfo
    {
        /// <summary>
        /// 发票商家 LOGO
        /// </summary>
        public string logo_url { get; set; }
        /// <summary>
        /// 收款方（显示在列表），上限为 9 个汉字，建议填入商户简称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 开票平台自定义入口名称，与 custom_url 字段共同使用，长度限制在 5 个汉字内
        /// </summary>
        public string custom_url_name { get; set; }
        /// <summary>
        /// 开票平台自定义入口跳转外链的地址链接 , 发票外跳的链接会带有发票参数，用于标识是从哪张发票跳出的链接
        /// </summary>
        public string custom_url { get; set; }
        /// <summary>
        /// 显示在入口右侧的 tips ，长度限制在 6 个汉字内
        /// </summary>
        public string custom_url_sub_title { get; set; }
        /// <summary>
        /// 营销场景的自定义入口
        /// </summary>
        public string promotion_url_name { get; set; }
        /// <summary>
        /// 入口跳转外链的地址链接，发票外跳的链接会带有发票参数，用于标识是从那张发票跳出的链接
        /// </summary>
        public string promotion_url { get; set; }
        /// <summary>
        /// 显示在入口右侧的 tips ，长度限制在 6 个汉字内
        /// </summary>
        public string promotion_url_sub_title { get; set; }
    }

    /// <summary>
    /// 将电子发票卡券插入用户卡包数据
    /// </summary>
    public class InsertCardToBagData
    {
        /// <summary>
        /// 发票order_id，既商户给用户授权开票的订单号
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 发票card_id
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 该订单号授权时使用的appid，一般为商户appid
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 发票具体内容
        /// </summary>
        public CardExtInfo card_ext { get; set; }
    }

    /// <summary>
    /// 发票具体内容
    /// </summary>
    public class CardExtInfo
    {
        /// <summary>
        /// 随机字符串，防止重复
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 用户信息结构体
        /// </summary>
        public UserCard user_card { get; set; }
    }

    /// <summary>
    /// 用户信息结构体
    /// </summary>
    public class UserCard
    {
        public InvoicePlatformUserData invoice_user_data { get; set; }
    }

    /// <summary>
    /// 发票信息基础公用信息（开票平台，报销方）
    /// </summary>
    public abstract class InvoiceBaseUseData
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
    /// 开票平台将电子发票卡券插入用户卡包时的发票信息
    /// </summary>
    public class InvoicePlatformUserData: InvoiceBaseUseData
    {
        /// <summary>
        /// 发票pdf文件上传到微信发票平台后，会生成一个发票
        /// </summary>
        public string s_pdf_media_id { get; set; }
        /// <summary>
        /// 其它消费附件的PDF
        /// </summary>
        public string s_trip_pdf_media_id { get; set; }
    }
    /// <summary>
    /// 报销方查询报销发票时的发票信息
    /// </summary>
    public class InvoiceReimburseUserData : InvoiceBaseUseData
    {
        /// <summary>
        /// 这张发票对应的PDF_URL
        /// </summary>
        public string pdf_url { get; set; }
        /// <summary>
        /// 其它消费凭证附件对应的URL，如行程单、水单等
        /// </summary>
        public string trip_pdf_url { get; set; }
        /// <summary>
        /// 发票报销状态，与<see cref="Reimburse_Status"/>对应
        /// </summary>
        public string reimburse_status { get; set; }
        /// <summary>
        /// 尝试转换<see cref="reimburse_status"/>为对应枚举，如果转换失败，则返回null
        /// </summary>
        public Reimburse_Status? Status
        {
            get
            {
                if (System.Enum.TryParse(this.reimburse_status, out Reimburse_Status status))
                {
                    return status;
                }
                return null;
            }
        }
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
