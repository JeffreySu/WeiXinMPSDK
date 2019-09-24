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
    
    文件名：GetWeAnalysisAppidDailySummaryTrendResultJson.cs
    文件功能描述：小程序“数据分析”接口 - 概况趋势 返回结果
    
    
    创建标识：Senparc - 20180101
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.DataCube
{
    /// <summary>
    /// 小程序“数据分析”接口 - 概况趋势 返回结果
    /// </summary>
    public class GetWeAnalysisAppidDailySummaryTrendResultJson: WxJsonResult
    {
        public List<GetWeAnalysisAppidDailySummaryTrendResultJson_list> list { get; set; }
    }

    /// <summary>
    /// 小程序“数据分析”接口 - 概况趋势 返回结果 - list
    /// </summary>
    public class GetWeAnalysisAppidDailySummaryTrendResultJson_list
    {
        /// <summary>
        /// 日期，如：20170313
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 累计用户数
        /// </summary>
        public int visit_total { get; set; }
        /// <summary>
        /// 转发次数
        /// </summary>
        public int share_pv { get; set; }
        /// <summary>
        /// 转发人数
        /// </summary>
        public int share_uv { get; set; }
    }

}
