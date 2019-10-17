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
    /// GetPrivateTemplateJsonResult
    /// </summary>
    public class GetPrivateTemplateJsonResult : WxJsonResult
    {
        public List<GetPrivateTemplate_TemplateItem> template_list { get; set; }
    }

    public class GetPrivateTemplate_TemplateItem
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 模板标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 模板所属行业的一级行业
        /// </summary>
        public string primary_industry { get; set; }
        /// <summary>
        /// 模板所属行业的二级行业
        /// </summary>
        public string deputy_industry { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 模板示例
        /// </summary>
        public string example { get; set; }
        public IndustryCode ConvertToIndustryCode()
        {
            var enumName = string.Format("{0}_{1}", primary_industry,
                deputy_industry.Replace("|", "_").Replace("/", "_"));

            IndustryCode code;

            if (!Enum.TryParse(enumName,true,out code))
            {
                return IndustryCode.其它_其它;//没有成功，此处也可以抛出异常
            }
            return code;
        }
       
    }
}
