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

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 添加货架返回结果
    /// </summary>
    public class AddShelfResult : WxJsonResult
    {
        public int shelf_id { get; set; }//货架ID
    }

    /// <summary>
    /// 获取所有货架
    /// </summary>
    public class GetAllShelfResult : WxJsonResult
    {
        public List<ShelfItem> shelves { get; set; }
    }

    public class ShelfItem
    {
        public ShelfInfo shelf_info { get; set; }
        /// <summary>
        /// 货架banner
        /// </summary>
        public string shelf_banner { get; set; }
        /// <summary>
        /// 货架名称
        /// </summary>
        public string shelf_name { get; set; }
        /// <summary>
        /// 货架ID
        /// </summary>
        public int shelf_id { get; set; }
    }

    public class ShelfInfo
    {
        public List<Shelf_ModuleInfo> module_infos { get; set; }
    }

    public class Shelf_ModuleInfo
    {
        public Shelf_GroupInfo group_infos { get; set; }
        public Shelf_Group group_info { get; set; }
        public int eid { get; set; }
    }

    public class Shelf_GroupInfo
    {
        public List<Shelf_Group> groups { get; set; }
        public string img_background { get; set; }
    }

    public class Shelf_Group
    {
        public int group_id { get; set; }
        public Shelf_Filter filter { get; set; }
    }

    public class Shelf_Filter
    {
        public int count { get; set; }
    }

    /// <summary>
    /// 根据货架ID获取货架信息
    /// </summary>
    public class GetByIdShelfResult : WxJsonResult
    {
        public ShelfInfo shelf_info { get; set; }
        /// <summary>
        /// 货架banner
        /// </summary>
        public string shelf_banner { get; set; }
        /// <summary>
        /// 货架名称
        /// </summary>
        public string shelf_name { get; set; }
        /// <summary>
        /// 货架ID
        /// </summary>
        public int shelf_id { get; set; }
    }
}

