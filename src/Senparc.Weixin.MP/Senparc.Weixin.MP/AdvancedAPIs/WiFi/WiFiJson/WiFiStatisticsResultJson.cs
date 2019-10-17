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
    
    文件名：WiFiStatisticsResultJson.cs
    文件功能描述：数据统计返回结果
    
    
    创建标识：Senparc - 20150709
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.WiFi
{
    /// <summary>
    /// 数据统计返回结果
    /// </summary>
    public class GetStatisticsResult : WxJsonResult
    {
        public List<GetStatistics_Data> date { get; set; }
    }

    public class GetStatistics_Data
    {
        /// <summary>
        /// 门店ID，-1为总统计
        /// </summary>
        public string shop_id { get; set; }
        /// <summary>
        /// 统计时间，单位为毫秒
        /// </summary>
        public long statis_time { get; set; }
        /// <summary>
        /// 微信连wifi成功人数
        /// </summary>
        public int total_user { get; set; }
        /// <summary>
        /// 商家主页访问人数
        /// </summary>
        public int homepage_uv { get; set; }
        /// <summary>
        /// 新增公众号关注人数
        /// </summary>
        public int new_fans { get; set; }
        /// <summary>
        /// 累计公众号关注人数
        /// </summary>
        public int total_fans { get; set; }
    }
}
