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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// GetIndustryJsonResult
    /// </summary>
    public class GetIndustryJsonResult : WxJsonResult
    {
        /// <summary>
        /// 帐号设置的主营行业
        /// </summary>
        public GetIndustry_Item primary_industry { get; set; }
        /// <summary>
        /// 帐号设置的副营行业
        /// </summary>
        public GetIndustry_Item secondary_industry { get; set; }


        
    }
    /// <summary>
    /// GetIndustry_Item
    /// </summary>
    public class GetIndustry_Item
    {
        /// <summary>
        /// 主行业
        /// </summary>
        public string first_class { get; set; }
        /// <summary>
        /// 副行业
        /// </summary>
        public string second_class { get; set; }

        /// <summary>
        /// 将当前对象转成IndustryCode
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IndustryCode ConvertToIndustryCode()
        {
            var enumName = string.Format("{0}_{1}", first_class,
                second_class.Replace("|", "_").Replace("/", "_"));

            IndustryCode code;

            if (!Enum.TryParse(enumName,true,out code))
            {
                return IndustryCode.其它_其它;//没有成功，此处也可以抛出异常
            }
            return code;
        }
    }
}
