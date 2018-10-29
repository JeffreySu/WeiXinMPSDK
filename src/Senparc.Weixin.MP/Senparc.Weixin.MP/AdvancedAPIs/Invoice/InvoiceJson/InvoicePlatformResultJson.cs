/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：InvoicePlatformResultJson.cs
    文件功能描述：开票平台返回结果
    
    
    创建标识：Senparc - 20180930
 
   
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
        public InvoiceReimburseUserData user_info { get; set; }
    }

    /// <summary>
    /// 批量查询报销发票信息返回信息
    /// </summary>
    public class GetInvoiceListResultJson:WxJsonResult
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
        public InvoiceReimburseUserData user_info { get; set; }
    }
}
