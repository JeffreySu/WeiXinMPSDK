#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：NovelReaderJson.cs
    文件功能描述：NovelReaderJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Novel
{
    /// <summary>单个章节的预览字数设置。</summary>
    public class NovelChapterPreviewSetting
    {
        /// <summary>章节索引，从 0 开始。</summary>
        public int chapter_index { get; set; }

        /// <summary>该章节允许预览的字数。</summary>
        public int words { get; set; }
    }

    /// <summary>小说预览设置。</summary>
    public class NovelPreviewSetting
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>未单独设置章节时使用的默认预览字数。</summary>
        public int default_words { get; set; }

        /// <summary>按章节覆盖的预览字数设置。</summary>
        public IList<NovelChapterPreviewSetting> chapter_setting { get; set; }
    }

    /// <summary>修改小说预览设置请求。</summary>
    /// <remarks>
    /// 官方参数表将 book_id、default_words、chapter_index 和 words 列为扁平字段，但官方请求示例使用
    /// setting.chapter_setting 嵌套结构；本模型遵循官方示例及获取接口返回结构。
    /// </remarks>
    public class NovelSetPreviewSettingRequest
    {
        /// <summary>完整预览设置。</summary>
        public NovelPreviewSetting setting { get; set; }
    }

    /// <summary>获取小说预览设置结果。</summary>
    public class NovelGetPreviewSettingJsonResult : WxJsonResult
    {
        /// <summary>当前预览设置。</summary>
        public NovelPreviewSetting setting { get; set; }
    }

    /// <summary>设置读后推荐小说请求。</summary>
    public class NovelSetRecommendedNovelsRequest
    {
        /// <summary>推荐类型：1 Android 付费小说，2 iOS 全文免费小说。</summary>
        public int recmd_type { get; set; }

        /// <summary>推荐小说的作品 ID 列表。</summary>
        public IList<string> book_id_list { get; set; }
    }
}
