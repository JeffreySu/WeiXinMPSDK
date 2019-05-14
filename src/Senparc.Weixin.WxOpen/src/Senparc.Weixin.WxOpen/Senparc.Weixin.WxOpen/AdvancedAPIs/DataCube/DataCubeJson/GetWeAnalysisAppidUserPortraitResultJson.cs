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
    
    文件名：GetWeAnalysisAppidUserPortraitResultJson.cs
    文件功能描述：小程序“数据分析”接口 - 用户画像 返回结果
    
    
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
    /// 小程序“数据分析”接口 - 用户画像 返回结果
    /// </summary>
    public class GetWeAnalysisAppidUserPortraitResultJson : WxJsonResult
    {
        /// <summary>
        /// 时间范围,如： "20170611-20170617"
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 新用户
        /// </summary>
        public List<GetWeAnalysisAppidUserPortraitResultJson_visit_uv> visit_uv_new { get; set; }
        /// <summary>
        /// 活跃用户
        /// </summary>
        public List<GetWeAnalysisAppidUserPortraitResultJson_visit_uv> visit_uv { get; set; }
    }

    /// <summary>
    /// 小程序“数据分析”接口 - 用户画像 返回结果 - visit_uv_new and visit_uv
    /// </summary>
    public class GetWeAnalysisAppidUserPortraitResultJson_visit_uv
    {
        /// <summary>
        /// 省份，如北京、广东等
        /// </summary>
        public GetWeAnalysisAppidUserPortraitResultJson_visit_uv_item province { get; set; }
        /// <summary>
        /// 城市，如北京、广州等
        /// </summary>
        public GetWeAnalysisAppidUserPortraitResultJson_visit_uv_item city { get; set; }
        /// <summary>
        /// 性别，包括男、女、未知
        /// </summary>
        public GetWeAnalysisAppidUserPortraitResultJson_visit_uv_item genders { get; set; }
        /// <summary>
        /// 终端类型，包括iPhone, android,其他
        /// </summary>
        public GetWeAnalysisAppidUserPortraitResultJson_visit_uv_item platforms { get; set; }
        /// <summary>
        /// 机型，如苹果iPhone6, OPPO R9等
        /// </summary>
        public GetWeAnalysisAppidUserPortraitResultJson_visit_uv_item devices { get; set; }
        /// <summary>
        /// 年龄，包括17岁以下、18-24岁等区间
        /// </summary>
        public GetWeAnalysisAppidUserPortraitResultJson_visit_uv_item ages { get; set; }
    }

    /// <summary>
    /// 小程序“数据分析”接口 - 用户画像 返回结果 - visit_uv_new and visit_uv 每一项属性
    /// </summary>
    public class GetWeAnalysisAppidUserPortraitResultJson_visit_uv_item
    {
        /// <summary>
        /// 属性值id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 属性值名称，与id一一对应。如属性为province时，返回的属性值名称包括“广东”等
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 属性值对应的指标值，如指标为visit_uv,属性为province,属性值为"广东省”，value对应广东地区的活跃用户数
        /// </summary>
        public int value { get; set; }
    }
}
