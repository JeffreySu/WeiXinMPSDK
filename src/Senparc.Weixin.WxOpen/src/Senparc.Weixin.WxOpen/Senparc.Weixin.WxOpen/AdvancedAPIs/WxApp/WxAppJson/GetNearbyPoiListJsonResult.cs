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
    
    文件名：GetNearbyPoiListJsonResult.cs
    文件功能描述：GetNearbyPoiList 接口结果
    
    
    创建标识：Senparc - 20180106
    
    修改标识：Senparc - 20190615
    修改描述：修复附近的小程序添加地点

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 添加地点返回结果
    /// </summary>
    public class GetNearbyPoiListJsonResult : WxJsonResult
    {
        public Data data { get; set; }
    }

    [Serializable]
    public class Data
    {
        /// <summary>
        /// 剩余可添加地点个数
        /// </summary>
        public int left_apply_num { get; set; }

        /// <summary>
        /// 最大可添加地点个数
        /// </summary>
        public int max_apply_num { get; set; }

        /// <summary>
        /// 地址列表的 JSON 格式字符串
        /// </summary>
        public PoiList data { get; set; }
    }

    [Serializable]
    public class PoiList
    {
        public List<PoiInfo> poi_list { get; set; }
    }

    /// <summary>
    /// 地点详情
    /// </summary>
    [Serializable]
    public class PoiInfo
    {
        /// <summary>
        /// 附近地点 ID
        /// </summary>
        public string poi_id { get; set; }

        /// <summary>
        /// 资质证件地址
        /// </summary>
        public string qualification_address { get; set; }

        /// <summary>
        /// 资质证件证件号
        /// </summary>
        public string qualification_num { get; set; }

        /// <summary>
        /// 地点审核状态
        /// 3	审核中
        /// 4	审核失败
        /// 5	审核通过
        /// </summary>
        public int audit_status { get; set; }

        /// <summary>
        /// 地点展示在附近状态
        /// 0	未展示
        /// 1	展示中
        /// </summary>
        public int display_status { get; set; }

        /// <summary>
        /// 审核失败原因，audit_status=4 时返回
        /// </summary>
        public string refuse_reason { get; set; }
    }
}
