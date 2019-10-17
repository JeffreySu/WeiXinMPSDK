/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GroupsJson.cs
    文件功能描述：获取用户分组列表返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Groups
{
    public class GroupsJson : WxJsonResult
    {
        public List<GroupsJson_Group> groups { get; set; }
    }

    public class GroupsJson_Group
    {
        public int id { get; set; }
        public string name { get; set; }
        /// <summary>
        /// 此属性在CreateGroupResult的Json数据中，创建结果中始终为0
        /// </summary>
        public int count { get; set; }
    }
}
