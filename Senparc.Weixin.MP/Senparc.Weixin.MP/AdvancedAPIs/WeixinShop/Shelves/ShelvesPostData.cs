using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class ShelvesData
    {
        public ShelfData shelf_data { get; set; }
        public string shelf_banner { get; set; }//sku信息,格式"id1:vid1;id2:vid2",如商品为统一规格，则此处赋值为空字符串即可
        public string shelf_name { get; set; }//增加的库存数量
    }

    public class ShelfData
    {
        public ModuleInfos module_infos { get; set; }
    }

    public class ModuleInfos
    {
        public List<ModuleInfo> module_infos { get; set; }
    }

    public class ModuleInfo
    {
        public GroupInfo group_info { get; set; }//分组信息
        public int eid { get; set; }//控件1的ID
    }

    public class GroupInfo
    {
        public Filter filter { get; set; }
        public int group_id { get; set; }//分组ID
    }

    public class Filter
    {
        public int count { get; set; }//该控件展示商品个数
    }

    public class GroupInfos
    {
        public GroupList group_infos { get; set; }//分组数组
        public int eid { get; set; }//控件2的ID
    }

    public class GroupList
    {
        public List<Group> groups { get; set; }//分组ID
    }

    public class Group
    {
        public int group_id { get; set; }//分组ID
    }

    public class Group_Info
    {
        public group_Info group_info { get; set; }//分组信息
        public int eid { get; set; }//控件3的ID
    }

    public class group_Info
    {
        public int group_id { get; set; }//分组ID
        public string img { get; set; }//分组照片(图片需调用图片上传接口获得图片Url填写至此，否则添加货架失败，建议分辨率600*208)
    }

    public class Group_Infos
    {
        public group_Infos group_infos { get; set; }
        public int eid { get; set; }//控件4的ID
    }

    public class group_Infos
    {
        public List<group_Info> groups { get; set; }
    }

    public class groupinfos
    {
        public groupInfos group_infos { get; set; }
        public int eid { get; set; }//控件5的ID
    }

    public class groupInfos
    {
        public List<Group> groups { get; set; }
        public string img_background { get; set; }
    }

    /// <summary>
    /// 添加货架
    /// </summary>
    public class AddShelvesData : ShelvesData
    {
    }

    /// <summary>
    /// 修改货架
    /// </summary>
    public class ModShelvesData : ShelvesData
    {
        public int shelf_id { get; set; }//货架ID
    }
}

