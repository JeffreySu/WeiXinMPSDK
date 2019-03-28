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
    
    文件名：Semantic_TravelResult.cs
    文件功能描述：语意理解接口旅游服务（travel）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 旅游服务（travel）
    /// </summary>
    public class Semantic_TravelResult : BaseSemanticResultJson
    {
        public Semantic_Travel semantic { get; set; }
    }

    public class Semantic_Travel : BaseSemanticIntent
    {
        public Semantic_Details_Travel details { get; set; }
        /// <summary>
        /// SEARCH 普通查询
        /// PRICE 价格查询
        /// GUIDE 攻略查询
        /// </summary>
        public string intent { get; set; }
    }

    public class Semantic_Details_Travel
    {
        /// <summary>
        /// 旅游目的地
        /// </summary>
        public Semantic_Location location { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string spot { get; set; }
        /// <summary>
        /// 旅游日期
        /// </summary>
        public Semantic_DateTime datetime { get; set; }
        /// <summary>
        /// 旅游类型词
        /// </summary>
        public string tag { get; set; }
        /// <summary>
        /// 0默认，1自由行，2跟团游
        /// </summary>
        public int category { get; set; }
    }
}
