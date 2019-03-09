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
    
    文件名：Semantic_RemindResult.cs
    文件功能描述：语意理解接口提醒服务（remind）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 提醒服务（remind）
    /// </summary>
    public class Semantic_RemindResult : BaseSemanticResultJson
    {
        public Semantic_Remind semantic { get; set; }
    }

    public class Semantic_Remind : BaseSemanticIntent
    {
        public Semantic_Details_Remind details { get; set; }
    }

    public class Semantic_Details_Remind
    {
        /// <summary>
        /// 时间
        /// </summary>
        public Semantic_DateTime datetime { get; set; }
        /// <summary>
        /// 事件
        /// </summary>
        public string @event { get; set; }
        /// <summary>
        /// 类别：0提醒；1闹钟  注：提醒有具体事件，闹钟没有具体事件
        /// </summary>
        public int remind_type { get; set; }
    }
}
