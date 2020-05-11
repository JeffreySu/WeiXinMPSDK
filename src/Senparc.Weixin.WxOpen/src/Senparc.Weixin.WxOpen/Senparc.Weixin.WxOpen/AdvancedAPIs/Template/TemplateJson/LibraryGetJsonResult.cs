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
    
    文件名：LibraryListJsonResult.cs
    文件功能描述：“获取模板库某个模板标题下关键词库”接口：LibraryGet 结果
    
    
    创建标识：Senparc - 20170827

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Template
{
    /// <summary>
    /// “获取模板库某个模板标题下关键词库”接口：LibraryGet 结果
    /// </summary>
    public class LibraryGetJsonResult : WxJsonResult
    {
        public string id { get; set; }
        public string title { get; set; }
        public List<LibraryGetJsonResult_KeywordList> keyword_list { get; set; }
    }

    public class LibraryGetJsonResult_KeywordList
    {
        /// <summary>
        /// 关键词id，添加模板时需要
        /// </summary>
        public int keyword_id { get; set; }
        /// <summary>
        /// 关键词内容
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 关键词内容对应的示例
        /// </summary>
        public string example { get; set; }
    }
}
