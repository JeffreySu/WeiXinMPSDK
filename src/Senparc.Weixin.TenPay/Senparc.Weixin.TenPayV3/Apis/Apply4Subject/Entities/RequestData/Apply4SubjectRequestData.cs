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
  
    文件名：Apply4SubjectRequestData.cs
    文件功能描述：商户开户意愿确认 - API 请求数据
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Subject
{
    /// <summary>
    /// 商户开户意愿确认 - 提交申请单API 请求数据
    /// </summary>
    public class Apply4SubjectApplymentRequestData
    {
        /// <summary>
        /// 业务申请编号
        /// <para>服务商自定义的商户唯一编号</para>
        /// <para>示例值：APPLYMENT_00000000001</para>
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 商户主体信息
        /// <para>商户的主体资质信息</para>
        /// </summary>
        public Apply4SubjectInfo subject_info { get; set; }

        /// <summary>
        /// 法人身份信息
        /// <para>法人的身份证信息</para>
        /// </summary>
        public Apply4SubjectIdentityInfo identity_info { get; set; }

        /// <summary>
        /// 联系人信息
        /// <para>联系人信息</para>
        /// </summary>
        public Apply4SubjectContactInfo contact_info { get; set; }
    }

    /// <summary>
    /// 商户主体信息
    /// </summary>
    public class Apply4SubjectInfo
    {
        public string subject_type { get; set; }
        public string business_license_copy { get; set; }
        public string business_license_number { get; set; }
        public string merchant_name { get; set; }
        public string company_address { get; set; }
    }

    /// <summary>
    /// 法人身份信息
    /// </summary>
    public class Apply4SubjectIdentityInfo
    {
        public string id_doc_type { get; set; }
        public string id_card_copy { get; set; }
        public string id_card_national { get; set; }
        public string id_card_name { get; set; }
        public string id_card_number { get; set; }
        public string card_period_begin { get; set; }
        public string card_period_end { get; set; }
    }

    /// <summary>
    /// 联系人信息
    /// </summary>
    public class Apply4SubjectContactInfo
    {
        public string contact_name { get; set; }
        public string contact_id_number { get; set; }
        public string mobile_phone { get; set; }
        public string contact_email { get; set; }
    }

    /// <summary>
    /// 撤销申请单
    /// </summary>
    public class CancelApply4SubjectApplymentRequestData
    {
        public string applyment_id { get; set; }
    }

    /// <summary>
    /// 查询申请状态 - 通过申请单号
    /// </summary>
    public class QueryApply4SubjectApplymentByIdRequestData
    {
        public string applyment_id { get; set; }
    }

    /// <summary>
    /// 查询申请状态 - 通过业务申请编号
    /// </summary>
    public class QueryApply4SubjectApplymentByOutRequestNoRequestData
    {
        public string out_request_no { get; set; }
    }
}



