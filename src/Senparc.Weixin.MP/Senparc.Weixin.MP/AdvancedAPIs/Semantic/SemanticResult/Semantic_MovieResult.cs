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
    
    文件名：Semantic_MovieResult.cs
    文件功能描述：语意理解接口上映电影服务（movie）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 上映电影服务（movie）
    /// </summary>
    public class Semantic_MovieResult : BaseSemanticResultJson
    {
        public Semantic_Movie semantic { get; set; }
    }

    public class Semantic_Movie : BaseSemanticIntent
    {
        public Semantic_Details_Movie details { get; set; }
    }

    public class Semantic_Details_Movie
    {
        /// <summary>
        /// 电影名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 主演
        /// </summary>
        public string actor { get; set; }
        /// <summary>
        /// 导演
        /// </summary>
        public string director { get; set; }
        /// <summary>
        /// 类型：动作片，剧情片，…
        /// </summary>
        public string tag { get; set; }
        /// <summary>
        /// 地区：美国，大陆，香港，…
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 电影院
        /// </summary>
        public string cinema { get; set; }
        /// <summary>
        /// 地点
        /// </summary>
        public Semantic_Location location { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public Semantic_DateTime datetime { get; set; }
        /// <summary>
        /// 优惠信息：0无（默认），1优惠券，2团购
        /// </summary>
        public int coupon { get; set; }
        /// <summary>
        /// 排序类型：0排序无要求（默认），1评价高优先级
        /// </summary>
        public int sort { get; set; }
    }
}
