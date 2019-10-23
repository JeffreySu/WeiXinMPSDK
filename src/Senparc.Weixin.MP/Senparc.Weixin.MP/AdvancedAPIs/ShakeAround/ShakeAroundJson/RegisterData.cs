#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
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