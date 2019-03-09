/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：InvoiceResultJson.cs
    文件功能描述：查询报销发票信息返回信息返回结果
    
    
    创建标识：Senparc - 20181009
 
   
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 查询报销发票信息返回信息
    /// </summary>
    public class GetInvoiceInfoResultJson : WorkJsonResult
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
        public InvoiceUserData user_info { get; set; }
    }
}
