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
  
    文件名：EcommerceSubMerchantRequestData.cs
    文件功能描述：电商收付通 - 二级商户管理 请求数据
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 电商收付通 - 二级商户进件API 请求数据
    /// </summary>
    public class SubMerchantApplymentRequestData
    {
        /// <summary>
        /// 业务申请编号
        /// <para>服务商自定义的商户唯一编号</para>
        /// <para>示例值：APPLYMENT_00000000001</para>
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 主体类型
        /// <para>SUBJECT_TYPE_INDIVIDUAL：个体户；SUBJECT_TYPE_ENTERPRISE：企业</para>
        /// <para>示例值：SUBJECT_TYPE_ENTERPRISE</para>
        /// </summary>
        public string organization_type { get; set; }

        /// <summary>
        /// 营业执照信息
        /// <para>主体为企业/个体工商户时，必填</para>
        /// </summary>
        public BusinessLicenseInfo business_license_info { get; set; }

        /// <summary>
        /// 经营者/法人身份证件
        /// <para>个体工商户：经营者身份证件；企业：法人身份证件</para>
        /// </summary>
        public IdDocInfo id_doc_info { get; set; }

        /// <summary>
        /// 最终受益人信息
        /// <para>若经营者/法人不是最终受益人，需要提供最终受益人信息</para>
        /// </summary>
        public UboInfo ubo_info { get; set; }

        /// <summary>
        /// 账户信息
        /// <para>收款账户信息</para>
        /// </summary>
        public AccountInfo account_info { get; set; }

        /// <summary>
        /// 联系人信息
        /// <para>超级管理员信息</para>
        /// </summary>
        public ContactInfo contact_info { get; set; }

        /// <summary>
        /// 销售场景
        /// <para>销售商品/提供服务的场景</para>
        /// </summary>
        public SalesSceneInfo sales_scene_info { get; set; }

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
    public class BusinessLicenseInfo
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

    /// <summary>
    /// 身份证件信息
    /// </summary>
    public class IdDocInfo
    {
        /// <summary>
        /// 身份证件类型
        /// <para>IDENTIFICATION_TYPE_IDCARD：中国大陆居民身份证</para>
        /// <para>示例值：IDENTIFICATION_TYPE_IDCARD</para>
        /// </summary>
        public string id_doc_type { get; set; }

        /// <summary>
        /// 身份证人像面照片
        /// <para>通过图片上传API预先上传图片生成好的MediaID</para>
        /// <para>示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KE53JXs</para>
        /// </summary>
        public string id_card_copy { get; set; }

        /// <summary>
        /// 身份证国徽面照片
        /// <para>通过图片上传API预先上传图片生成好的MediaID</para>
        /// <para>示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KE53JXs</para>
        /// </summary>
        public string id_card_national { get; set; }

        /// <summary>
        /// 身份证姓名
        /// <para>证件上的姓名，需进行加密</para>
        /// <para>示例值：张三</para>
        /// </summary>
        public string id_card_name { get; set; }

        /// <summary>
        /// 身份证号码
        /// <para>证件上的号码，需进行加密</para>
        /// <para>示例值：110000199001011234</para>
        /// </summary>
        public string id_card_number { get; set; }

        /// <summary>
        /// 身份证有效期开始时间
        /// <para>格式为yyyy-MM-dd</para>
        /// <para>示例值：2019-06-06</para>
        /// </summary>
        public string card_period_begin { get; set; }

        /// <summary>
        /// 身份证有效期结束时间
        /// <para>格式为yyyy-MM-dd，长期有效填写：长期</para>
        /// <para>示例值：2026-06-06</para>
        /// </summary>
        public string card_period_end { get; set; }
    }

    /// <summary>
    /// 最终受益人信息
    /// </summary>
    public class UboInfo
    {
        /// <summary>
        /// 证件类型
        /// <para>IDENTIFICATION_TYPE_IDCARD：中国大陆居民身份证</para>
        /// <para>示例值：IDENTIFICATION_TYPE_IDCARD</para>
        /// </summary>
        public string id_type { get; set; }

        /// <summary>
        /// 身份证人像面照片
        /// <para>通过图片上传API预先上传图片生成好的MediaID</para>
        /// <para>示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KE53JXs</para>
        /// </summary>
        public string id_card_copy { get; set; }

        /// <summary>
        /// 身份证国徽面照片
        /// <para>通过图片上传API预先上传图片生成好的MediaID</para>
        /// <para>示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KE53JXs</para>
        /// </summary>
        public string id_card_national { get; set; }

        /// <summary>
        /// 证件姓名
        /// <para>证件上的姓名，需进行加密</para>
        /// <para>示例值：李四</para>
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 证件号码
        /// <para>证件上的号码，需进行加密</para>
        /// <para>示例值：110000199001011235</para>
        /// </summary>
        public string id_number { get; set; }

        /// <summary>
        /// 证件有效期开始时间
        /// <para>格式为yyyy-MM-dd</para>
        /// <para>示例值：2019-06-06</para>
        /// </summary>
        public string id_period_begin { get; set; }

        /// <summary>
        /// 证件有效期结束时间
        /// <para>格式为yyyy-MM-dd，长期有效填写：长期</para>
        /// <para>示例值：2026-06-06</para>
        /// </summary>
        public string id_period_end { get; set; }
    }

    /// <summary>
    /// 账户信息
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// 账户类型
        /// <para>ACCOUNT_TYPE_BUSINESS：对公银行账户；ACCOUNT_TYPE_PRIVATE：经营者个人银行卡</para>
        /// <para>示例值：ACCOUNT_TYPE_BUSINESS</para>
        /// </summary>
        public string bank_account_type { get; set; }

        /// <summary>
        /// 开户名称
        /// <para>开户名称，需进行加密</para>
        /// <para>示例值：腾讯科技有限公司</para>
        /// </summary>
        public string account_name { get; set; }

        /// <summary>
        /// 开户银行
        /// <para>开户银行</para>
        /// <para>示例值：工商银行</para>
        /// </summary>
        public string account_bank { get; set; }

        /// <summary>
        /// 开户银行省市编码
        /// <para>开户银行省市编码</para>
        /// <para>示例值：110000</para>
        /// </summary>
        public string bank_address_code { get; set; }

        /// <summary>
        /// 开户银行联行号
        /// <para>开户银行联行号</para>
        /// <para>示例值：102100000000</para>
        /// </summary>
        public string bank_branch_id { get; set; }

        /// <summary>
        /// 银行账号
        /// <para>银行账号，需进行加密</para>
        /// <para>示例值：1234567890</para>
        /// </summary>
        public string account_number { get; set; }
    }

    /// <summary>
    /// 联系人信息
    /// </summary>
    public class ContactInfo
    {
        /// <summary>
        /// 联系人类型
        /// <para>LEGAL：法人；SUPER：经办人</para>
        /// <para>示例值：LEGAL</para>
        /// </summary>
        public string contact_type { get; set; }

        /// <summary>
        /// 联系人姓名
        /// <para>联系人姓名，需进行加密</para>
        /// <para>示例值：张三</para>
        /// </summary>
        public string contact_name { get; set; }

        /// <summary>
        /// 联系人身份证件号码
        /// <para>联系人身份证件号码，需进行加密</para>
        /// <para>示例值：110000199001011234</para>
        /// </summary>
        public string contact_id_number { get; set; }

        /// <summary>
        /// 手机号码
        /// <para>联系人手机号码，需进行加密</para>
        /// <para>示例值：13800138000</para>
        /// </summary>
        public string mobile_phone { get; set; }

        /// <summary>
        /// 邮箱
        /// <para>联系人邮箱，需进行加密</para>
        /// <para>示例值：abc@abc.com</para>
        /// </summary>
        public string contact_email { get; set; }
    }

    /// <summary>
    /// 销售场景信息
    /// </summary>
    public class SalesSceneInfo
    {
        /// <summary>
        /// 店铺名称
        /// <para>店铺名称</para>
        /// <para>示例值：张三餐厅</para>
        /// </summary>
        public string store_name { get; set; }

        /// <summary>
        /// 店铺链接
        /// <para>店铺链接</para>
        /// <para>示例值：https://www.qq.com</para>
        /// </summary>
        public string store_url { get; set; }

        /// <summary>
        /// 店铺二维码
        /// <para>通过图片上传API预先上传图片生成好的MediaID</para>
        /// <para>示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KE53JXs</para>
        /// </summary>
        public string store_qr_code { get; set; }

        /// <summary>
        /// 小程序AppID
        /// <para>微信小程序AppID</para>
        /// <para>示例值：wx1234567890123456</para>
        /// </summary>
        public string mini_program_sub_appid { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 查询二级商户进件申请状态API 请求数据
    /// </summary>
    public class QuerySubMerchantApplymentRequestData
    {
        /// <summary>
        /// 申请单号
        /// <para>微信支付分配的申请单号</para>
        /// <para>示例值：2000002124775691</para>
        /// </summary>
        public string applyment_id { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 通过业务申请编号查询申请状态API 请求数据
    /// </summary>
    public class QuerySubMerchantApplymentByOutRequestNoRequestData
    {
        /// <summary>
        /// 业务申请编号
        /// <para>服务商自定义的商户唯一编号</para>
        /// <para>示例值：APPLYMENT_00000000001</para>
        /// </summary>
        public string out_request_no { get; set; }
    }
}

