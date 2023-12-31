#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
  
    文件名：GroupChatListParam.cs
    文件功能描述：客户群列表查询参数
    
    
    创建标识：lishewen - 20200318

    修改标识：WangDrama - 20210630
    修改描述：v3.9.600 添加：外部联系人 - 客户群统计+联系客户+群直播+客户群事件 相关功能
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// 群状态
    /// </summary>
    public enum StatusFilter
    {
        普通列表 = 0,
        离职待继承 = 1,
        离职继承中 = 2,
        离职继承完成 = 3
    }
    /// <summary>
    /// 客户群列表查询参数
    /// </summary>
    public class GroupChatListParam
    {
        /// <summary>
        /// 群状态过滤。
        /// </summary>
        public StatusFilter status_filter { get; set; } = StatusFilter.普通列表;
        /// <summary>
        /// 群主过滤。如果不填，表示获取全部群主的数据
        /// </summary>
        public Owner_Filter owner_filter { get; set; }
        /// <summary>
        /// 用于分页查询的游标，字符串类型，由上一次调用返回，首次调用不填
        /// </summary>
        public string cursor { get; set; }
        /// <summary>
        /// 分页，预期请求的数据量，取值范围 1 ~ 1000s
        /// </summary>
        public int limit { get; set; }
    }

    public class Owner_Filter
    {
        /// <summary>
        /// 用户ID列表。最多100个
        /// </summary>
        public string[] userid_list { get; set; }
        /// <summary>
        /// 部门ID列表。最多100个
        /// </summary>
        public int[] partyid_list { get; set; }
    }

}
