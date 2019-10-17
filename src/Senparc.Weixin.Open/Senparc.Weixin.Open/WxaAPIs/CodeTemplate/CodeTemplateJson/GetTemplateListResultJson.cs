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
    
    文件名：CodeResultJson.cs
    文件功能描述：代码模板列表返回结果
    
    
    创建标识：Senparc - 20171215


----------------------------------------------------------------*/


using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    [Serializable]
    public class GetTemplateListResultJson : WxJsonResult
    {
        /// <summary>
        /// 草稿列表
        /// </summary>
        public List<TemplateInfo> template_list { get; set; }
    }

    [Serializable]
    public class TemplateInfo
    {
        /// <summary>
        /// 开发者上传草稿的时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 模板版本号，开发者自定义字段
        /// </summary>
        public string user_version { get; set; }

        /// <summary>
        /// 模板描述，开发者自定义字段
        /// </summary>
        public string user_desc { get; set; }

        /// <summary>
        /// 草稿ID
        /// </summary>
        public int template_id { get; set; }
    }
}
