/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：BizLicenseJsonResult.cs
    文件功能描述：OCR 营业执照识别返回结果
    
    
    创建标识：yaofeng - 20231204

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    /// <summary>
    /// 营业执照识别
    /// </summary>
    public class BizLicenseJsonResult : WxJsonResult
    {
        /// <summary>
        /// 注册号
        /// </summary>
        public string reg_num { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string serial { get; set; }

        /// <summary>
        /// 法定代表人姓名
        /// </summary>
        public string legal_representative { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string enterprise_name { get; set; }

        /// <summary>
        /// 组成形式
        /// </summary>
        public string type_of_organization { get; set; }

        /// <summary>
        /// 经营场所/企业住所
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 公司类型
        /// </summary>
        public string type_of_enterprise { get; set; }

        /// <summary>
        /// 经营范围
        /// </summary>
        public string business_scope { get; set; }

        /// <summary>
        /// 注册资本
        /// </summary>
        public string registered_capital { get; set; }

        /// <summary>
        /// 实收资本
        /// </summary>
        public string paid_in_capital { get; set; }

        /// <summary>
        /// 营业期限
        /// </summary>
        public string valid_period { get; set; }

        /// <summary>
        /// 注册日期/成立日期
        /// </summary>
        public string registered_date { get; set; }

        /// <summary>
        /// 营业执照位置
        /// </summary>
        public Position cert_position { get; set; }
    }
}
