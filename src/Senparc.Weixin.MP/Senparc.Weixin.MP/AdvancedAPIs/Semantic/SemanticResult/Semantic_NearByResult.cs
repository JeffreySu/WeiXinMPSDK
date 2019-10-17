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
    
    文件名：Semantic_NearByResult.cs
    文件功能描述：语意理解接口周边服务（nearby）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 周边服务（nearby）
    /// 注：service字段表示是否是服务类，比如：找家政、租房、招聘等即为服务类；找ATM、羽毛球馆等即为非服务类。
    /// </summary>
    public class Semantic_NearByResult : BaseSemanticResultJson
    {
        public Semantic_NearBy semantic { get; set; }
    }

    public class Semantic_NearBy : BaseSemanticIntent
    {
        public Semantic_Details_NearBy details { get; set; }
        /// <summary>
        /// SEARCH 普通查询
        /// </summary>
        public string intent { get; set; }
    }

    public class Semantic_Details_NearBy
    {
        /// <summary>
        /// 地点
        /// </summary>
        public Semantic_Location location { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 限定词
        /// </summary>
        public string limit { get; set; }
        /// <summary>
        /// 价格（单位元）
        /// </summary>
        public Semantic_Number price { get; set; }
        /// <summary>
        /// 距离（单位米）
        /// </summary>
        public Semantic_Number radius { get; set; }
        /// <summary>
        /// 是否是服务类：0不是（默认），1是
        /// </summary>
        public int service { get; set; }
        /// <summary>
        /// 优惠信息：0无（默认），1优惠券，2团购
        /// </summary>
        public int coupon { get; set; }
        /// <summary>
        /// 排序类型：0距离（默认），1点评高优先级，2服务质量高优先级，3环境高优先级，4价格高到低，5价格低到高
        /// </summary>
        public int sort { get; set; }
    }
}
