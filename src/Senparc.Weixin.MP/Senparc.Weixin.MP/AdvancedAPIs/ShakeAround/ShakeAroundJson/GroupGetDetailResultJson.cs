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
    
    文件名：GroupGetDetailResultJson.cs
    文件功能描述：查询分组详情的返回结果
    
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
    public class GroupGetDetailResultJson : WxJsonResult 
    {
        /// <summary>
        /// 查询分组详情的返回结果
        /// </summary>
        public GroupGetDetail_Data data { get; set; }

       

           
        }
    public class GroupGetDetail_Data
    {
        /// <summary>
        /// 分组唯一标识，全局唯一
        /// </summary>
        public string group_id { get; set; }
        /// <summary>
        /// 分组名
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// 此分组现有的总设备数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 分组下的设备列表
        /// </summary>
        public List<GroupGetDetail_Devices> devices { get; set; }
    }
    public class GroupGetDetail_Devices
    {
        /// <summary>
        /// 设备编号，设备全局唯一ID
        /// </summary>
        public string device_id { get; set; }
        /// <summary>
        /// uuid
        /// </summary>
        public string uuid { get; set; }
        /// <summary>
        /// major
        /// </summary>
        public string major { get; set; }
        /// <summary>
        /// minor
        /// </summary>
        public string minor { get; set; }
        /// <summary>
        /// 设备的备注信息
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 设备关联的门店ID，关联门店后，在门店1KM的范围内有优先摇出信息的机会。门店相关信息具体可查看门店相关的接口文档    
        /// </summary>
        public string poi_id { get; set; }
    }
}
