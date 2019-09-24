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
    
    文件名：StatisticsResultJson.cs
    文件功能描述：数据统计返回结果
    
    
    创建标识：Senparc - 20150512
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 数据统计返回结果
    /// </summary>
    public class StatisticsResultJson : WxJsonResult
    {
        /// <summary>
        /// 数据统计返回数据
        /// </summary>
        public List<Statistics_DataItem> data { get; set; }
    }

    public class Statistics_DataItem
    {
        /// <summary>
        /// 点击摇周边消息的次数
        /// </summary>
        public int click_pv { get; set; }
        /// <summary>
        /// 点击摇周边消息的人数
        /// </summary>
        public int click_uv { get; set; }
        /// <summary>
        /// 当天0点对应的时间戳
        /// </summary>
        public long ftime { get; set; }
        /// <summary>
        /// 摇周边的次数
        /// </summary>
        public int shake_pv { get; set; }
        /// <summary>
        /// 摇周边的人数
        /// </summary>
        public int shake_uv { get; set; }
    }
}