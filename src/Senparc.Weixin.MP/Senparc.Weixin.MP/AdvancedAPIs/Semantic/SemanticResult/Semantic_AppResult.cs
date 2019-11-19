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
    
    文件名：Semantic_AppResult.cs
    文件功能描述：语意理解接口应用服务（app）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 应用服务（app）
    /// </summary>
    public class Semantic_AppResult : BaseSemanticResultJson
    {
        public Semantic_App semantic { get; set; }
    }

    public class Semantic_App : BaseSemanticIntent
    {
        public Semantic_Details_App details { get; set; }
    }

    public class Semantic_Details_App
    {
        /// <summary>
        /// app名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// app类别
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 排序方式：0（按质量从高到低），1（按时间从新到旧）
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// 查看的类型：install（已安装），buy（已购买），update（可更新），latest（最近运行的），home（主页）
        /// </summary>
        public string type { get; set; }
    }
}