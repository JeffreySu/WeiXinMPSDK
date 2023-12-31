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
    
    文件名：WxaAuth.cs
    文件功能描述：小程序认证
    
    
    创建标识：Yaofeng - 20231130

----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.Sec
{
    /// <summary>
    /// 认证数据
    /// </summary>
    public class AuthData
    {
        /// <summary>
        /// 企业为1，个体工商户 为12，个人是15，详情参考： https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/basic-info-management/getAccountBasicInfo.html#%E8%B0%83%E7%94%A8%E6%96%B9%E5%BC%8F
        /// </summary>
        public int customer_type { get; set; }

        /// <summary>
        /// 认证任务id，打回重审调用reauth时为必填
        /// </summary>
        public string taskid { get; set; }

        /// <summary>
        /// 联系人信息
        /// </summary>
        public WxaAuthAuthData_ContactInfo contact_info { get; set; }

        /// <summary>
        /// 发票信息，如果是服务商代缴模式，不需要改参数
        /// </summary>
        public WxaAuthAuthData_InvoiceInfo invoice_info { get; set; }

        /// <summary>
        /// 非个人类型必填。主体资质材料 media_id 支持jpg,jpeg .bmp.gif .png格式，仅支持一张图片
        /// </summary>
        public string qualification { get; set; }

        /// <summary>
        /// 主体资质其他证明材料 media_id 支持jpg,jpeg .bmp.gif .png格式，最多上传10张图片
        /// </summary>
        public List<string> qualification_other { get; set; }

        /// <summary>
        /// 小程序账号名称
        /// </summary>
        public string account_name { get; set; }

        /// <summary>
        /// 小程序账号名称命名类型 1：基于自选词汇命名 2：基于商标命名
        /// </summary>
        public int account_name_type { get; set; }

        /// <summary>
        /// 名称命中关键词-补充材料 media_id 支持jpg,jpeg .bmp.gif .png格式，支持上传多张图片
        /// </summary>
        public List<string> account_supplemental { get; set; }

        /// <summary>
        /// 支付方式 1：消耗服务商预购包 2：小程序开发者自行支付
        /// </summary>
        public int pay_type { get; set; }

        /// <summary>
        /// 认证类型为个人类型时可以选择要认证的身份，从/wxa/sec/authidentitytree 里获取，填叶节点的name
        /// </summary>
        public string auth_identification { get; set; }

        /// <summary>
        /// 填了auth_identification则必填。身份证明材料 media_id （1）基于不同认证身份上传不同的材料；（2）认证类型=1时选填，支持上传10张图片（3）支持jpg,jpeg .bmp.gif .png格式
        /// </summary>
        public List<string> auth_ident_material { get; set; }

        /// <summary>
        /// 第三方联系电话
        /// </summary>
        public string third_party_phone { get; set; }

        /// <summary>
        /// 选择服务商代缴模式时必填。服务市场appid，该服务市场账号主体必须与服务商账号主体一致
        /// </summary>
        public string service_appid { get; set; }
    }

    /// <summary>
    /// 联系人信息
    /// </summary>
    public class WxaAuthAuthData_ContactInfo
    {
        /// <summary>
        /// 认证联系人姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 认证联系人邮箱
        /// </summary>
        public string email { get; set; }
    }

    /// <summary>
    /// 发票信息，如果是服务商代缴模式，不需要改参数
    /// </summary>
    public class WxaAuthAuthData_InvoiceInfo
    {
        /// <summary>
        /// 发票类型 1: 不开发票 2: 电子发票 3: 增值税专票
        /// </summary>
        public int invoice_type { get; set; }

        /// <summary>
        /// 发票类型=2时必填 电子发票开票信息
        /// </summary>
        public WxaAuthAuthData_InvoiceInfo_Electronic electronic { get; set; }

        /// <summary>
        /// 发票类型=3时必填 增值税专票开票信息
        /// </summary>
        public WxaAuthAuthData_InvoiceInfo_Vat vat { get; set; }

        /// <summary>
        /// 发票抬头，需要和认证主体名称一样
        /// </summary>
        public string invoice_title { get; set; }
    }

    /// <summary>
    /// 发票类型=2时必填 电子发票开票信息
    /// </summary>
    public class WxaAuthAuthData_InvoiceInfo_Electronic
    {
        /// <summary>
        /// 纳税识别号（15位、17、18或20位）
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 发票备注（选填）
        /// </summary>
        public string desc { get; set; }
    }

    /// <summary>
    /// 发票类型=3时必填 增值税专票开票信息
    /// </summary>
    public class WxaAuthAuthData_InvoiceInfo_Vat
    {
        /// <summary>
        /// 企业电话
        /// </summary>
        public string enterprise_phone { get; set; }

        /// <summary>
        /// 纳税识别号（15位、17、18或20位）
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 企业注册地址
        /// </summary>
        public string enterprise_address { get; set; }

        /// <summary>
        /// 企业开户银行
        /// </summary>
        public string bank_name { get; set; }

        /// <summary>
        /// 企业银行账号
        /// </summary>
        public string bank_account { get; set; }

        /// <summary>
        /// 发票邮寄地址邮编
        /// </summary>
        public string mailing_address { get; set; }

        /// <summary>
        /// 街道地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 县区
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// 发票备注（选填）
        /// </summary>
        public string desc { get; set; }
    }
}