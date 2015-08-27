using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class GroupData
   {
       public GroupDetail group_detail { get; set; }
   }

    public class GroupDetail
    {
        public string group_name { get; set; }//分组名称
        public string[] product_list { get; set; }//商品ID集合
    }

    /// <summary>
    /// 修改分组属性需要Post的数据
    /// </summary>
    public class PropertyModGroup
    {
        public int group_id { get; set; }//分组Id
        public string group_name { get; set; }//分组名称
    }

    /// <summary>
    /// 修改分组商品需要Post的数据
    /// </summary>
    public class ProductModGroup
    {
        public int group_id { get; set; }//分组ID
        public List<Product> product { get; set; }//分组的商品集合
    }

    public class Product
    {
        public string product_id { get; set; }//商品ID
        public int mod_action { get; set; }//修改操作(0-删除, 1-增加)
    }

    /// <summary>
    /// 增加分组
    /// </summary>
    public class AddGroupData : GroupData
    {
    }
}
