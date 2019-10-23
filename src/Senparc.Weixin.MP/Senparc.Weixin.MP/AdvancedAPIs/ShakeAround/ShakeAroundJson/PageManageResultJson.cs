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
    
    文件名：PageManageResultJson.cs
    文件功能描述：页面管理返回结果
    
    
    创建标识：Senparc - 20150512
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 新增与编辑页面返回结果基类
    /// </summary>
    public class BasePageResultJson : WxJsonResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public BasePage_Data data { get; set; }
    }

    public class BasePage_Data
    {
        /// <summary>
        /// 页面id
        /// </summary>
        public long page_id { get; set; }
    }

    /// <summary>
    /// 新增页面返回结果
    /// </summary>
    public class AddPageResultJson : BasePageResultJson
    {
        
    }

    /// <summary>
    /// 编辑页面返回结果
    /// </summary>
    public class UpdatePageResultJson : BasePageResultJson
    {

    }

    /// <summary>
    /// 查询页面列表返回结果
    /// </summary>
    public class SearchPagesResultJson : WxJsonResult
    {
        /// <summary>
        /// 查询页面列表返回数据
        /// </summary>
        public SearchPages_Data data { get; set; }
    }

    public class SearchPages_Data
    {
        /// <summary>
        /// 页面基本信息
        /// </summary>
        public List<SearchPages_Data_Page> pages { get; set; }
        /// <summary>
        /// 商户名下的页面总数
        /// </summary>
        public int total_count { get; set; }
    }

    public class SearchPages_Data_Page : WxJsonResult
    {
        /// <summary>
        /// 页面的备注信息
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 在摇一摇页面展示的副标题
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 在摇一摇页面展示的图片
        /// </summary>
        public string icon_url { get; set; }
        /// <summary>
        /// 摇周边页面唯一ID
        /// </summary>
        public long page_id { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string page_url { get; set; }
        /// <summary>
        /// 在摇一摇页面展示的主标题
        /// </summary>
        public string title { get; set; }
    }
}