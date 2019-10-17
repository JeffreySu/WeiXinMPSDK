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
    
    文件名：RelationSearchResultJson.cs
    文件功能描述：查询设备与页面的关联关系返回结果
    
    
    创建标识：Senparc - 20160216
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 查询设备与页面的关联关系返回结果
    /// </summary>
    public class RelationSearchResultJson : WxJsonResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public RelationSearchDate data { get; set; }
    }

    public class RelationSearchDate
    {
        public List<RelationItem> relations { get; set; }

        /// <summary>
        /// 设备或页面的关联关系总数
        /// </summary>
        public int total_count { get; set; }
    }

    /// <summary>
    /// 设备与页面的关联关系
    /// </summary>
    public class RelationItem : DeviceApply_Data_Device_Identifiers
    {
        /// <summary>
        /// 摇周边页面唯一ID
        /// </summary>
        public long page_id { get; set; }
    }
}