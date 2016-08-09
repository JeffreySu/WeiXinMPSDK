/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RegisterData.cs
    文件功能描述：申请开通功能数据
    
    
    创建标识：Senparc - 20150914
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 申请开通功能数据
    /// </summary>
    public class RegisterData
    {
        public string name { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public List<string> qualification_cert_urls { get; set; }
        public string apply_reason { get; set; }
        public string industry_id { get; set; }

        /// <summary>
        /// industry_id默认留空，可以使用ConvertIndustryId方法进行转换
        /// </summary>
        public RegisterData()
        {
        }

        public RegisterData(IndustryId _industry_id)
        {
            industry_id = ConvertIndustryId(_industry_id);
        }

        /// <summary>
        /// 转换_industry_id到合法的提交参数格式
        /// </summary>
        /// <param name="_industry_id"></param>
        /// <returns></returns>
        public static string ConvertIndustryId(IndustryId _industry_id)
        {
             return ((int)_industry_id).ToString("0000");

        }
    }
}