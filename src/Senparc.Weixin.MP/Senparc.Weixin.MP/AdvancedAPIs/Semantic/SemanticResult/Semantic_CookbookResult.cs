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
    
    文件名：Semantic_CookbookResult.cs
    文件功能描述：语意理解接口菜谱服务（cookbook）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 菜谱服务（cookbook）
    /// </summary>
    public class Semantic_CookbookResult : BaseSemanticResultJson
    {
        public Semantic_Cookbook semantic { get; set; }
    }

    public class Semantic_Cookbook : BaseSemanticIntent
    {
        public Semantic_Details_Cookbook details { get; set; }
    }

    public class Semantic_Details_Cookbook
    {
        /// <summary>
        /// 菜名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 菜系
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 食材
        /// </summary>
        public string ingredient { get; set; }

    }
}
