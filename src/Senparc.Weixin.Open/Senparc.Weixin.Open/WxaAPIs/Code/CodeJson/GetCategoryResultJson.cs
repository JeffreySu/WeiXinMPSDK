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
    
    文件名：GetCategoryResultJson.cs
    文件功能描述：各级类目名称和ID返回结果
    
    
    创建标识：Senparc - 20170726


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
    public class GetCategoryResultJson : WxJsonResult
    {
        public List<CategroyInfo> category_list { get; set; }
    }

    [Serializable]
    public class CategroyInfo
    {
        /// <summary>
        /// 一级类目名称
        /// </summary>
        public string first_class { get; set; }

        /// <summary>
        /// 二级类目名称
        /// </summary>
        public string second_class { get; set; }

        /// <summary>
        /// 三级类目名称
        /// </summary>
        public string third_class { get; set; }

        /// <summary>
        /// 一级类目的ID编号
        /// </summary>
        public int first_id { get; set; }

        /// <summary>
        /// 二级类目的ID编号
        /// </summary>
        public int second_id { get; set; }

        /// <summary>
        /// 三级类目的ID编号
        /// </summary>
        public int third_id { get; set; }
    }
}
