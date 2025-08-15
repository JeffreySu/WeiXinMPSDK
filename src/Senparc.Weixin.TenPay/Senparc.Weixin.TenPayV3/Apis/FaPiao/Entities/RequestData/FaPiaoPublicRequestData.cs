#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：FapiaoPublicRequestData.cs
    文件功能描述：电子发票 - 公共API 请求数据
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.Fapiao
{
    /// <summary>
    /// 电子发票 - 检查子商户开票功能状态API 请求数据
    /// </summary>
    public class CheckFapiaoStatusRequestData
    {
        /// <summary>
        /// 子商户号
        /// <para>微信支付分配的子商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string sub_mchid { get; set; }
    }

    /// <summary>
    /// 电子发票 - 创建电子发票卡券模板API 请求数据
    /// </summary>
    public class CreateFapiaoCardTemplateRequestData
    {
        /// <summary>
        /// 商户号
        /// <para>微信支付分配的商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 发票卡券模板名称
        /// <para>发票卡券模板的名称</para>
        /// <para>示例值：电子发票</para>
        /// </summary>
        public string template_name { get; set; }

        /// <summary>
        /// 发票卡券模板描述
        /// <para>发票卡券模板的描述信息</para>
        /// <para>示例值：用于开具电子发票的卡券模板</para>
        /// </summary>
        public string template_description { get; set; }

        /// <summary>
        /// 发票类型
        /// <para>发票的类型</para>
        /// <para>示例值：NORMAL_INVOICE</para>
        /// </summary>
        public string invoice_type { get; set; }
    }

    /// <summary>
    /// 电子发票 - 查询电子发票API 请求数据
    /// </summary>
    public class QueryFapiaoRequestData
    {
        /// <summary>
        /// 发票申请单号
        /// <para>微信支付系统生成的发票申请单号</para>
        /// <para>示例值：2020112611140011234567890</para>
        /// </summary>
        public string fapiao_apply_id { get; set; }
    }

    /// <summary>
    /// 电子发票 - 获取抬头填写链接API 请求数据
    /// </summary>
    public class GetTitleUrlRequestData
    {
        /// <summary>
        /// 商户号
        /// <para>微信支付分配的商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 用户标识
        /// <para>用户在商户appid下的唯一标识</para>
        /// <para>示例值：oWmnN4xxxxxxxxxxe92NHiS8ABC</para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 回调链接
        /// <para>用户填写完成后的回调链接</para>
        /// <para>示例值：https://example.com/callback</para>
        /// </summary>
        public string redirect_url { get; set; }
    }

    /// <summary>
    /// 电子发票 - 获取用户填写的抬头API 请求数据
    /// </summary>
    public class GetUserTitleRequestData
    {
        /// <summary>
        /// 用户标识
        /// <para>用户在商户appid下的唯一标识</para>
        /// <para>示例值：oWmnN4xxxxxxxxxxe92NHiS8ABC</para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 场景值
        /// <para>获取抬头的场景值</para>
        /// <para>示例值：WITH_WECHATPAY</para>
        /// </summary>
        public string scene { get; set; }
    }
}
