#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：MedicalAssistantJson.cs
    文件功能描述：MedicalAssistantJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.MedicalAssistant
{
    /// <summary>
    /// 微信就医助手消息请求，使用官方示例中的标准就医业务信息模型。
    /// </summary>
    public class SendChannelMessageRequest : SendChannelMessageRequest<MedicalAssistantBusinessInfo>
    {
    }

    /// <summary>
    /// 微信就医助手消息请求。
    /// </summary>
    /// <typeparam name="TBusinessInfo">当前消息状态对应的业务信息类型。</typeparam>
    public class SendChannelMessageRequest<TBusinessInfo>
        where TBusinessInfo : class
    {
        /// <summary>
        /// 消息状态，按微信就医助手对应业务节点的官方定义填写。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 接收消息用户的 OpenId。
        /// </summary>
        public string open_id { get; set; }

        /// <summary>
        /// 就医流程订单号；同一流程的多次状态推送必须保持一致。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 消息唯一标识；同一用户和订单下不可重复。
        /// </summary>
        public string msg_id { get; set; }

        /// <summary>
        /// 已开通微信就医助手能力的公众号 AppId。
        /// </summary>
        public string app_id { get; set; }

        /// <summary>
        /// 就医助手业务标识，官方固定值为 150。
        /// </summary>
        public int business_id { get; set; } = 150;

        /// <summary>
        /// 与 <see cref="status"/> 对应的业务信息；部分状态可不传。
        /// </summary>
        public TBusinessInfo business_info { get; set; }
    }

    /// <summary>
    /// 微信就医助手标准预约就诊业务信息。
    /// </summary>
    public class MedicalAssistantBusinessInfo
    {
        /// <summary>
        /// 患者在医院侧的标识。
        /// </summary>
        public string pat_hospital_id { get; set; }

        /// <summary>
        /// 患者姓名。
        /// </summary>
        public string pat_name { get; set; }

        /// <summary>
        /// 医生姓名。
        /// </summary>
        public string doc_name { get; set; }

        /// <summary>
        /// 科室名称。
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        /// 科室位置。
        /// </summary>
        public string department_location { get; set; }

        /// <summary>
        /// 预约时间，按官方接口要求的时间格式填写。
        /// </summary>
        public string appointment_time { get; set; }

        /// <summary>
        /// 业务补充说明。
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 普通用户点击消息后的跳转配置。
        /// </summary>
        public MedicalAssistantRedirectPage redirect_page { get; set; }

        /// <summary>
        /// 长辈模式用户点击消息后的跳转配置。
        /// </summary>
        public MedicalAssistantRedirectPage elder_redirect_page { get; set; }
    }

    /// <summary>
    /// 微信就医助手消息跳转配置。
    /// </summary>
    public class MedicalAssistantRedirectPage
    {
        /// <summary>
        /// 页面类型：<c>web</c> 或 <c>mini_program</c>。
        /// </summary>
        public string page_type { get; set; }

        /// <summary>
        /// <c>web</c> 页面地址。
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// <c>mini_program</c> 类型对应的小程序 AppId。
        /// </summary>
        public string app_id { get; set; }

        /// <summary>
        /// <c>mini_program</c> 类型对应的小程序完整页面路径。
        /// </summary>
        public string fullpath { get; set; }
    }
}
