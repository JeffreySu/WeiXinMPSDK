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
    
    文件名：GetWeAnalysisAppidVisitPageResultJson.cs
    文件功能描述：小程序“数据分析”接口 - 访问页面 返回结果
    
    
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
    /// 小程序“数据分析”接口 - 访问页面 返回结果
    /// </summary>
    public class GetWeAnalysisAppidVisitPageResultJson : WxJsonResult
    {
        /// <summary>
        /// 时间，如："20170313"
        /// </summary>
        public string ref_date { get; set; }
        public List<GetWeAnalysisAppidVisitPageResultJson_list> list { get; set; }
    }

    /// <summary>
    /// 小程序“数据分析”接口 - 访问页面 返回结果 - list
    /// </summary>
    public class GetWeAnalysisAppidVisitPageResultJson_list
    {
        /// <summary>
        /// 页面路径
        /// </summary>
        public string page_path { get; set; }
        /// <summary>
        /// 访问次数
        /// </summary>
        public int page_visit_pv { get; set; }
        /// <summary>
        /// 访问人数
        /// </summary>
        public string page_visit_uv { get; set; }
        /// <summary>
        /// 次均停留时长
        /// </summary>
        public double page_staytime_pv { get; set; }
        /// <summary>
        /// 进入页次数
        /// </summary>
        public int entrypage_pv { get; set; }
        /// <summary>
        /// 退出页次数
        /// </summary>
        public int exitpage_pv { get; set; }
        /// <summary>
        /// 转发次数
        /// </summary>
        public int page_share_pv { get; set; }
        /// <summary>
        /// 转发人数
        /// </summary>
        public int page_share_uv { get; set; }
    }

}
