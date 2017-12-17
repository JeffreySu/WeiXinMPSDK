/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：GroupMessageByTagId.cs
    文件功能描述：根据 TagId 群发所需的数据
    
    
    创建标识：Senparc - 20171217

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    /// <summary>
    /// 根据 TagId 群发筛选
    /// </summary>
    public class GroupMessageByTagId : BaseGroupMessageByFilter
    {
        public string tag_id { get; set; }
    }
}
