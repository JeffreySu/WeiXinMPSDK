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
    
    文件名：PageListResultJson.cs
    文件功能描述：批量查询页面统计数据的返回结果
    
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
    /// <summary>
    /// 批量查询页面统计数据的返回结果
    /// </summary>
    public class PageListResultJson : WxJsonResult 
    {
        public PageList_Data data { get; set; }

       
    }
    public class PageList_Data
    {
        public List<PageList_Pages> pages { get; set; }


        /// <summary>
        /// 所查询的日期时间戳，单位为秒
        /// </summary>
        public long date { get; set; }
        /// <summary>
        /// 页面总数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 所查询的结果页序号；返回结果按摇周边人数降序排序，每50条记录为一页
        /// </summary>
        public int page_index { get; set; }
    }
    public class PageList_Pages
    {
        /// <summary>
        /// 页面ID
        /// </summary>
        public int page_id { get; set; }
        /// <summary>
        /// 点击摇周边消息的次数
        /// </summary>
        public int click_pv { get; set; }
        /// <summary>
        /// 点击摇周边消息的人数
        /// </summary>
        public int click_uv { get; set; }
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
