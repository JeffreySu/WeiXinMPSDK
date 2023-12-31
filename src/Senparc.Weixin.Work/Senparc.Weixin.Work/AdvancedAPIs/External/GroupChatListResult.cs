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
  
    文件名：GroupChatListResult.cs
    文件功能描述：获取客户群列表 返回结果
    
    
    创建标识：lishewen - 20200318


    修改标识：WangDrama - 20210630
    修改描述：v3.9.600 添加：外部联系人 - 客户群统计+联系客户+群直播+客户群事件 相关功能

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// 获取客户群列表 返回结果
    /// </summary>
    public class GroupChatListResult : WorkJsonResult
    {
        /// <summary>
        /// 客户群列表
        /// </summary>
        public Group_Chat_List[] group_chat_list { get; set; }
        /// <summary>
        /// 分页游标，下次请求时填写以获取之后分页的记录。如果该字段返回空则表示已没有更多数据
        /// </summary>
        public string next_cursor { get; set; }
    }

    public class Group_Chat_List
    {
        /// <summary>
        /// 客户群IDs
        /// </summary>
        public string chat_id { get; set; }
        /// <summary>
        /// 客户群状态。
        /// 0 - 正常
        /// 1 - 跟进人离职
        /// 2 - 离职继承中
        /// 3 - 离职继承完成
        /// </summary>
        public int status { get; set; }
    }
}
