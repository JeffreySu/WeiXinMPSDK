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
    
    文件名：Semantic_TrainResult.cs
    文件功能描述：语意理解接口火车服务（train）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 火车服务（train）
    /// </summary>
    public class Semantic_TrainResult : BaseSemanticResultJson
    {
        public Semantic_Train semantic { get; set; }
    }

    public class Semantic_Train : BaseSemanticIntent
    {
        public Semantic_Details_Train details { get; set; }
    }

    public class Semantic_Details_Train
    {
        /// <summary>
        /// 出发日期
        /// </summary>
        public Semantic_DateTime start_date { get; set; }
        /// <summary>
        /// 返回日期
        /// </summary>
        public Semantic_DateTime end_date { get; set; }
        /// <summary>
        /// 起点
        /// </summary>
        public Semantic_Location start_loc { get; set; }
        /// <summary>
        /// 终点
        /// </summary>
        public Semantic_Location end_loc { get; set; }
        /// <summary>
        /// 车次代码，比如：T43等
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 座位级别：YZ（硬座），RZ（软座），YW（硬卧），RW（软卧），YD（一等座），ED（二等座），TD（特等座）
        /// </summary>
        public string seat { get; set; }
        /// <summary>
        /// 车次类型：G（高铁），D（动车），T（特快），K（快速），Z（直达），L（临时客车），P（普通）
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 类型：DC（单程），WF（往返）
        /// </summary>
        public string type { get; set; }
    }
}