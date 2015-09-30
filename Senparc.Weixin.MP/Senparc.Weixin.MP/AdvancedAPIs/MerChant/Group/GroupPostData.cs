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
