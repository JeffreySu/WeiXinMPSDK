/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：InvoicePlatformResultJson.cs
    文件功能描述：开票平台返回结果
    
    
    创建标识：Senparc - 20180930
 
    修改标识：Senparc - 20181030
    修改描述：更新User_Info

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 获取自身的开票平台识别码返回信息
    /// </summary>
    public class SetUrlResultJson : WxJsonResult
    {
        /// <summary>
        /// 该开票平台专用的授权链接
        /// </summary>
        public string invoice_url { get; set; }
    }

    /// <summary>
    /// 创建发票卡券模板返回信息
    /// </summary>
    public class CreateCardResultJson : WxJsonResult
    {
        /// <summary>
        /// 当错误码为 0 时，返回发票卡券模板的编号，用于后续该商户发票生成后，作为必填参数在调用插卡接口时传入
        /// </summary>
        public string card_id { get; set; }
    }

    /// <summary>
    /// 上传PDF返回信息
    /// </summary>
    public class SetPDFResultJson : WxJsonResult
    {
        /// <summary>
        /// 64位整数，在 将发票卡券插入用户卡包 时使用用于关联pdf和发票卡券，s_media_id有效期有3天，3天内若未将s_media_id关联到发票卡券，pdf将自动销毁
        /// </summary>
        public string s_media_id { get; set; }
    }

    /// <summary>
    /// 查询已上传的PDF文件返回信息
    /// </summary>
    public class GetPDFResultJson : WxJsonResult
    {
        /// <summary>
        /// pdf 的 url ，两个小时有效期
        /// </summary>
        public string pdf_url { get; set; }
        /// <summary>
        /// pdf_url 过期时间， 7200 秒
        /// </summary>
        public int pdf_url_expire_time { get; set; }
    }

    /// <summary>
    /// 将电子发票卡券插入用户卡包返回信息
    /// </summary>
    public class InsertCardResultJson : WxJsonResult
    {
        /// <summary>
        /// 发票code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 获得发票用户的openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
        /// </summary>
        public string unionid { get; set; }
    }

    /// <summary>
    /// 查询报销发票信息返回信息
    /// </summary>
    public class GetInvoiceInfoResultJson : WxJsonResult
    {
        /// <summary>
        /// 发票 id
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 发票的有效期起始时间
        /// </summary>
        public int begin_time { get; set; }
        /// <summary>
        /// 发票的有效期截止时间
        /// </summary>
        public int end_time { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 发票的类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 发票的收款方
        /// </summary>
        public string payee { get; set; }
        /// <summary>
        /// 发票详情
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 用户可在发票票面看到的主要信息
        /// </summary>
        public User_Info user_info { get; set; }
    }

    /// <summary>
    /// 批量查询报销发票信息返回信息
    /// </summary>
    public class GetInvoiceListResultJson : WxJsonResult
    {
        /// <summary>
        /// 发票信息列表
        /// </summary>
        public List<InvoiceItemInfo> item_list { get; set; }
    }

    /// <summary>
    /// 发票信息
    /// </summary>
    public class InvoiceItemInfo
    {
        /// <summary>
        /// 发票 id
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 发票的有效期起始时间
        /// </summary>
        public int begin_time { get; set; }
        /// <summary>
        /// 发票的有效期截止时间
        /// </summary>
        public int end_time { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 发票的类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 发票的收款方
        /// </summary>
        public string payee { get; set; }
        /// <summary>
        /// 发票详情
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 用户可在发票票面看到的主要信息
        /// </summary>
        public User_Info user_info { get; set; }
    }
    

    public class User_Info
    {
        public int fee { get; set; }
        public string title { get; set; }
        public int billing_time { get; set; }
        public string billing_no { get; set; }
        public string billing_code { get; set; }
        public Info[] info { get; set; }
        public bool accept { get; set; }
        public int fee_without_tax { get; set; }
        public int tax { get; set; }
        public string pdf_url { get; set; }
        public string trip_pdf_url { get; set; }
        public Reimburse_Status reimburse_status { get; set; }
        public string check_code { get; set; }
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

    public class Info
    {
        public string name { get; set; }
        public int num { get; set; }
        public string unit { get; set; }
        public int price { get; set; }
    }

}
