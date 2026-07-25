#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License
is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：XPayIncrementJson.cs
    文件功能描述：XPayIncrementJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 下载虚拟支付 iOS 月结账单请求。
    /// </summary>
    public class DownloadIosSettlementBillRequestData
    {
        /// <summary>
        /// 开始月份，格式为 YYYYMM。
        /// </summary>
        public string start_month { get; set; }

        /// <summary>
        /// 结束月份，格式为 YYYYMM。
        /// </summary>
        public string end_month { get; set; }
    }

    /// <summary>
    /// 虚拟支付 iOS 月结账单信息。
    /// </summary>
    public class IosSettlementBillItem
    {
        /// <summary>
        /// 账单月份，格式为 YYYYMM。
        /// </summary>
        public string month { get; set; }

        /// <summary>
        /// 账单临时下载链接；链接会过期，应及时下载。
        /// </summary>
        public string bill_url { get; set; }
    }

    /// <summary>
    /// 下载虚拟支付 iOS 月结账单结果。
    /// </summary>
    public class DownloadIosSettlementBillJsonResult : WxJsonResult
    {
        /// <summary>
        /// 月结账单列表。
        /// </summary>
        public IList<IosSettlementBillItem> bill_list { get; set; }
    }

    /// <summary>
    /// 虚拟支付商户管控原因和解除路径。
    /// </summary>
    public class XPayRecoverySpecification
    {
        /// <summary>
        /// 管控原因对应单据号，可与管控流水通知中的 business_code 关联。
        /// </summary>
        public string limitation_case_id { get; set; }

        /// <summary>
        /// 管控原因类型。
        /// </summary>
        public string limitation_reason_type { get; set; }

        /// <summary>
        /// 管控原因简要描述。
        /// </summary>
        public string limitation_reason { get; set; }

        /// <summary>
        /// 管控原因详细说明。
        /// </summary>
        public string limitation_reason_describe { get; set; }

        /// <summary>
        /// 该原因影响的能力。官方参数表将其标记为字符串，返回示例却为数组，因此保留为对象以兼容两种结构。
        /// </summary>
        public object relate_limitations { get; set; }

        /// <summary>
        /// 未被标准枚举覆盖的其他受限能力说明。
        /// </summary>
        public string other_relate_limitations { get; set; }

        /// <summary>
        /// 微信支付建议的处理和解除路径。
        /// </summary>
        public string recover_way { get; set; }

        /// <summary>
        /// 解除路径的补充参数，例如尽调单号、申诉单号或经营类型确认单号。
        /// </summary>
        public string recover_way_param { get; set; }

        /// <summary>
        /// 微信支付提供的进一步帮助页面地址。
        /// </summary>
        public string recover_help_url { get; set; }

        /// <summary>
        /// 管控生效方式。
        /// </summary>
        public string limitation_action_type { get; set; }

        /// <summary>
        /// 延迟管控时预计开始时间。
        /// </summary>
        public string limitation_start_date { get; set; }

        /// <summary>
        /// 管控实际生效时间。
        /// </summary>
        public string limitation_date { get; set; }
    }

    /// <summary>
    /// 查询虚拟支付商户管控原因结果。
    /// </summary>
    public class QueryPunishmentReasonsJsonResult : WxJsonResult
    {
        /// <summary>
        /// 小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 小程序昵称。
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 微信支付商户号。
        /// </summary>
        public string merchant_code { get; set; }

        /// <summary>
        /// 商户被管控能力列表。
        /// </summary>
        public IList<string> limited_functions { get; set; }

        /// <summary>
        /// 商户其他被管控能力说明。
        /// </summary>
        public string other_limited_functions { get; set; }

        /// <summary>
        /// 被管控原因及解除路径列表。
        /// </summary>
        public IList<XPayRecoverySpecification> recovery_specifications { get; set; }
    }
}
