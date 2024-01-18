#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_Submit_Invoice_Title.cs
    文件功能描述：用户提交抬头后，商户会收到用户提交的事件
    
    
    创建标识：lishewen - 20210809

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;

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
