using Senparc.NeuChar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 用户提交抬头后，商户会收到用户提交的事件
    /// </summary>
    public class RequestMessageEvent_Submit_Invoice_Title : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.submit_invoice_title; }
        }
        public string EventKey { get; set; }
        /// <summary>
        /// 抬头
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        public string tax_no { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        /// 银行类型
        /// </summary>
        public string bank_type { get; set; }
        /// <summary>
        /// 银行号码
        /// </summary>
        public string bank_no { get; set; }
        /// <summary>
        /// 附加字段
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// 抬头类型
        /// InvoiceUserTitlePersonType:个人抬头
        /// InvoiceUserTitleBusinessType:企业抬头
        /// </summary>
        public string title_type { get; set; }
    }
}
