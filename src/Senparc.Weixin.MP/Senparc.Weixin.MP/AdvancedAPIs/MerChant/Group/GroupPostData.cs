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

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    public class BaseGroupData
   {
       public GroupDetail group_detail { get; set; }
   }

    public class GroupDetail
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// 商品ID集合
        /// </summary>
        public string[] product_list { get; set; }
    }

    /// <summary>
    /// 修改分组属性需要Post的数据
    /// </summary>
    public class PropertyModGroup
    {
        /// <summary>
        /// 分组Id
        /// </summary>
        public int group_id { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string group_name { get; set; }
    }

    /// <summary>
    /// 修改分组商品需要Post的数据
    /// </summary>
    public class ProductModGroup
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public int group_id { get; set; }
        /// <summary>
        /// 分组的商品集合
        /// </summary>
        public List<Product> product { get; set; }
    }

    public class Product
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 修改操作(0-删除, 1-增加)
        /// </summary>
        public int mod_action { get; set; }
    }

    /// <summary>
    /// 增加分组
    /// </summary>
    public class AddGroupData : BaseGroupData
    {
    }
}
