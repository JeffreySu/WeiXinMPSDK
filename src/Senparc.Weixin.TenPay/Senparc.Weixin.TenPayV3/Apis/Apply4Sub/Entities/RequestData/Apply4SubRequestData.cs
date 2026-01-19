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
  
    文件名：Apply4SubRequestData.cs
    文件功能描述：服务商进件 - API 请求数据
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Sub
{
    /// <summary>
    /// 服务商进件 - 提交申请单API 请求数据
    /// </summary>
    public class Apply4SubApplymentRequestData
    {
        /// <summary>
        /// 业务申请编号
        /// <para>服务商自定义的商户唯一编号</para>
        /// <para>示例值：APPLYMENT_00000000001</para>
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 主体类型
        /// <para>SUBJECT_TYPE_INDIVIDUAL：个体户；SUBJECT_TYPE_ENTERPRISE：企业；SUBJECT_TYPE_INSTITUTIONS：党政、机关及事业单位；SUBJECT_TYPE_OTHERS：其他组织</para>
        /// <para>示例值：SUBJECT_TYPE_ENTERPRISE</para>
        /// </summary>
        public string organization_type { get; set; }

        /// <summary>
        /// 营业执照信息
        /// <para>主体为企业/个体工商户时，必填</para>
        /// </summary>
        public Apply4SubBusinessLicenseInfo business_license_info { get; set; }

        /// <summary>
        /// 经营者/法人身份证件
        /// <para>个体工商户：经营者身份证件；企业：法人身份证件</para>
        /// </summary>
        public Apply4SubIdDocInfo id_doc_info { get; set; }

        /// <summary>
        /// 最终受益人信息
        /// <para>若经营者/法人不是最终受益人，需要提供最终受益人信息</para>
        /// </summary>
        public Apply4SubUboInfo ubo_info { get; set; }

        /// <summary>
        /// 账户信息
        /// <para>收款账户信息</para>
        /// </summary>
        public Apply4SubAccountInfo account_info { get; set; }

        /// <summary>
        /// 联系人信息
        /// <para>超级管理员信息</para>
        /// </summary>
        public Apply4SubContactInfo contact_info { get; set; }

        /// <summary>
        /// 销售场景
        /// <para>销售商品/提供服务的场景</para>
        /// </summary>
        public Apply4SubSalesSceneInfo sales_scene_info { get; set; }

        /// <summary>
        /// 商户简称
        /// <para>UTF8格式，长度不超过20个字符</para>
        /// <para>示例值：张三餐厅</para>
        /// </summary>
        public string merchant_shortname { get; set; }

        /// <summary>
        /// 特殊资质
        /// <para>根据所属行业，提供特殊资质</para>
        /// </summary>
        public string[] qualifications { get; set; }

        /// <summary>
        /// 补充材料
        /// <para>可上传多个图片凭证，请填写通过图片上传API预先上传图片生成好的MediaID</para>
        /// </summary>
        public string[] business_addition_pics { get; set; }

        /// <summary>
        /// 补充说明
        /// <para>可填写512字以内的补充说明</para>
        /// <para>示例值：特殊情况，说明原因</para>
        /// </summary>
        public string business_addition_msg { get; set; }
    }

    /// <summary>
    /// 营业执照信息
    /// </summary>
    public class Apply4SubBusinessLicenseInfo
    {
        /// <summary>
        /// 营业执照照片
        /// <para>通过图片上传API预先上传图片生成好的MediaID</para>
        /// <para>示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KE53JXs</para>
        /// </summary>
        public string license_copy { get; set; }

        /// <summary>
        /// 营业执照注册号
        /// <para>营业执照上的注册号或统一社会信用代码</para>
        /// <para>示例值：123456789012345678</para>
        /// </summary>
        public string license_number { get; set; }

        /// <summary>
        /// 商户名称
        /// <para>营业执照上的商户名称</para>
        /// <para>示例值：腾讯科技有限公司</para>
        /// </summary>
        public string merchant_name { get; set; }

        /// <summary>
        /// 个体户经营者姓名
        /// <para>个体工商户营业执照上经营者姓名</para>
        /// <para>示例值：张三</para>
        /// </summary>
        public string legal_person { get; set; }
    }

    // 其他数据类定义参考Ecommerce模块中的相似结构，这里简化处理

    /// <summary>
    /// 身份证件信息
    /// </summary>
    public class Apply4SubIdDocInfo
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
    /// 最终受益人信息
    /// </summary>
    public class Apply4SubUboInfo
    {
        public string id_type { get; set; }
        public string id_card_copy { get; set; }
        public string id_card_national { get; set; }
        public string name { get; set; }
        public string id_number { get; set; }
        public string id_period_begin { get; set; }
        public string id_period_end { get; set; }
    }

    /// <summary>
    /// 账户信息
    /// </summary>
    public class Apply4SubAccountInfo
    {
        public string bank_account_type { get; set; }
        public string account_name { get; set; }
        public string account_bank { get; set; }
        public string bank_address_code { get; set; }
        public string bank_branch_id { get; set; }
        public string account_number { get; set; }
    }

    /// <summary>
    /// 联系人信息
    /// </summary>
    public class Apply4SubContactInfo
    {
        public string contact_type { get; set; }
        public string contact_name { get; set; }
        public string contact_id_number { get; set; }
        public string mobile_phone { get; set; }
        public string contact_email { get; set; }
    }

    /// <summary>
    /// 销售场景信息
    /// </summary>
    public class Apply4SubSalesSceneInfo
    {
        public string store_name { get; set; }
        public string store_url { get; set; }
        public string store_qr_code { get; set; }
        public string mini_program_sub_appid { get; set; }
    }

    /// <summary>
    /// 查询申请状态 - 通过申请单号
    /// </summary>
    public class QueryApply4SubApplymentByIdRequestData
    {
        public string applyment_id { get; set; }
    }

    /// <summary>
    /// 查询申请状态 - 通过业务申请编号
    /// </summary>
    public class QueryApply4SubApplymentByOutRequestNoRequestData
    {
        public string out_request_no { get; set; }
    }

    /// <summary>
    /// 修改结算账号
    /// </summary>
    public class ModifyApply4SubSettlementRequestData
    {
        public string sub_mchid { get; set; }
        public Apply4SubAccountInfo account_info { get; set; }
    }

    /// <summary>
    /// 查询结算账号
    /// </summary>
    public class QueryApply4SubSettlementRequestData
    {
        public string sub_mchid { get; set; }
    }
}



