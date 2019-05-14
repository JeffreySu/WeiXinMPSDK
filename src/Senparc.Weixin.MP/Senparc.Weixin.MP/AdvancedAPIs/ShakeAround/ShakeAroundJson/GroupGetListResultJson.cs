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
    
    文件名：GroupGetListResultJson.cs
    文件功能描述：查询分组列表的返回结果
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 查询分组列表的返回结果
    /// </summary>
    public class GroupGetListResultJson : WxJsonResult 
    {
        public GroupGetList_Data data { get; set; }
     }
    public class GroupGetList_Data
    {
        /// <summary>
        /// 此账号下现有的总分组数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 分组列表
        /// </summary>

        public List<GroupGetList_Groups> groups { get; set; }


    }
    public class GroupGetList_Groups
    {
        /// <summary>
        /// 分组唯一标识，全局唯一
        /// </summary>
        public string group_id { get; set; }
        /// <summary>
        /// 分组名
        /// </summary>
        public string group_name { get; set; }
    }
}
