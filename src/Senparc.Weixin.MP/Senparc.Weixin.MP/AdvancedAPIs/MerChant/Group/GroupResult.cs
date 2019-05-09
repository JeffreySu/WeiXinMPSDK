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

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 增加分组返回信息
    /// </summary>
    public class AddGroupResult : WxJsonResult
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public int group_id { get; set; }
    }

    /// <summary>
    /// 获取所有分组返回信息
    /// </summary>
    public class GetAllGroup
    {
        /// <summary>
        /// 分组集合
        /// </summary>
        public List<GroupsDetail> groups_detail { get; set; }
    }

    public class GroupsDetail
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public int group_id { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string group_name { get; set; }
    }

    public class GetByIdGroup : WxJsonResult
    {
        /// <summary>
        /// 分组信息
        /// </summary>
        public Group_Detail group_detail { get; set; }
    }

    public class Group_Detail
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public int group_id { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// 商品ID集合
        /// </summary>
        public string[] product_list { get; set; }
    }
}


