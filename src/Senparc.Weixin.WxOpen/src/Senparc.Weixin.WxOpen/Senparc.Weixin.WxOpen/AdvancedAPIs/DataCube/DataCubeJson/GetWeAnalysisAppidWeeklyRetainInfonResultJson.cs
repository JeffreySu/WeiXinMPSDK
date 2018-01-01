#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
    
    文件名：GetWeAnalysisAppidWeeklyRetainInfonResultJson.cs
    文件功能描述：小程序“数据分析”接口 - 访问留存：周留存 返回结果
    
    
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
    /// 小程序“数据分析”接口 - 访问留存：周留存 返回结果
    /// </summary>
    public class GetWeAnalysisAppidWeeklyRetainInfonResultJson : WxJsonResult
    {
        /// <summary>
        /// 时间，如："20170313"
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 新增用户留存
        /// </summary>
        public GetWeAnalysisAppidWeeklyRetainInfonResultJson_visit_uv visit_uv_new { get; set; }
        /// <summary>
        /// 活跃用户留存
        /// </summary>
        public GetWeAnalysisAppidWeeklyRetainInfonResultJson_visit_uv visit_uv { get; set; }
    }

    /// <summary>
    /// 小程序“数据分析”接口 - 访问留存：周留存 返回结果 - visit_uv_new & visit_uv
    /// </summary>
    public class GetWeAnalysisAppidWeeklyRetainInfonResultJson_visit_uv
    {
        /// <summary>
        /// 标识，0开始，0表示当天，1表示1天后，依此类推，key取值分别是：0,1,2,3,4,5,6,7,14,30
        /// </summary>
        public int key { get; set; }
        /// <summary>
        /// key对应日期的新增用户数/活跃用户数（key=0时）或留存用户数（k>0时）
        /// </summary>
        public int value { get; set; }
    }

}
