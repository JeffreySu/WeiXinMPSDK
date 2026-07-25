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

    文件名：NovelBookJson.cs
    文件功能描述：NovelBookJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Novel
{
    /// <summary>创建小说作品请求。</summary>
    public class NovelCreateBookRequest
    {
        /// <summary>作品名，长度 1 至 30 个字。</summary>
        public string title { get; set; }

        /// <summary>作品简介，长度 1 至 500 个字。</summary>
        public string intro { get; set; }

        /// <summary>通过新增临时素材接口获得的封面图 media_id。</summary>
        public string cover_media_id { get; set; }

        /// <summary>作者名，长度 1 至 100 个字。</summary>
        public string author { get; set; }

        /// <summary>一级作品类型 ID。</summary>
        public int first_category_id { get; set; }

        /// <summary>二级作品类型 ID。</summary>
        public int second_category_id { get; set; }

        /// <summary>三级作品类型 ID。</summary>
        public int third_category_id { get; set; }

        /// <summary>完结状态：1 连载中，2 已完结。</summary>
        public int complete_status { get; set; }

        /// <summary>可选提供方作品主键，可用于去重，最长 255 字节。</summary>
        public string original_id { get; set; }

        /// <summary>可选章节排序方式：0 追加，1 按 seq 递增。</summary>
        public int? chapter_order_method { get; set; }

        /// <summary>可选自定义信息，最长 128 字节。</summary>
        public string custom_info { get; set; }

        /// <summary>可选题材关键词，最多 3 个，每个长度 1 至 4 个字。</summary>
        public IList<string> keyword_list { get; set; }

        /// <summary>可选精彩片段，需为本书内容，长度 400 至 1000 个字。</summary>
        public string awesome_paragraph { get; set; }
    }

    /// <summary>编辑小说作品请求。</summary>
    public class NovelUpdateBookRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>可选作品名，长度 1 至 30 个字。</summary>
        public string title { get; set; }

        /// <summary>可选作品简介，长度 1 至 500 个字。</summary>
        public string intro { get; set; }

        /// <summary>可选封面图 media_id。</summary>
        public string cover_media_id { get; set; }

        /// <summary>可选作者名，长度 1 至 100 个字。</summary>
        public string author { get; set; }

        /// <summary>可选一级作品类型 ID；修改类型时三个类型 ID 应同时提交。</summary>
        public int? first_category_id { get; set; }

        /// <summary>可选二级作品类型 ID。</summary>
        public int? second_category_id { get; set; }

        /// <summary>可选三级作品类型 ID。</summary>
        public int? third_category_id { get; set; }

        /// <summary>可选完结状态：1 连载中，2 已完结。</summary>
        public int? complete_status { get; set; }

        /// <summary>可选章节 ID 列表，顺序即预期章节顺序。</summary>
        public IList<string> chapter_id_list { get; set; }

        /// <summary>是否需要分卷；不设置表示不修改分卷，false 表示清除分卷。</summary>
        public bool? need_volume { get; set; }

        /// <summary>可选分卷信息；need_volume 为 true 时使用。</summary>
        public IList<NovelVolumeInfo> volume_list { get; set; }

        /// <summary>可选章节排序方式：0 追加，1 按 seq 递增。</summary>
        public int? chapter_order_method { get; set; }

        /// <summary>可选自定义信息，最长 128 字节。</summary>
        public string custom_info { get; set; }

        /// <summary>是否更新题材关键词。</summary>
        public bool? update_keyword { get; set; }

        /// <summary>可选题材关键词，最多 3 个。</summary>
        public IList<string> keyword_list { get; set; }

        /// <summary>可选精彩片段，需为本书内容。</summary>
        public string awesome_paragraph { get; set; }
    }

    /// <summary>小说分卷信息。</summary>
    public class NovelVolumeInfo
    {
        /// <summary>分卷名，长度 1 至 100 个字。</summary>
        public string volume_title { get; set; }

        /// <summary>分卷起始章节下标，包含该章节。</summary>
        public int start_index { get; set; }

        /// <summary>分卷截止章节下标，包含该章节。</summary>
        public int end_index { get; set; }
    }

    /// <summary>仅包含作品 ID 的请求。</summary>
    public class NovelBookIdRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }
    }

    /// <summary>分页查询作品列表请求。</summary>
    public class NovelListBooksRequest
    {
        /// <summary>单页数量，范围 1 至 100，默认 100。</summary>
        public int? limit { get; set; }

        /// <summary>起始偏移量；与 last_id 二选一，优先使用 last_id。</summary>
        public int? offset { get; set; }

        /// <summary>分页 ID；首次填 0，后续使用上次返回值。</summary>
        public long? last_id { get; set; }

        /// <summary>true 获取编辑版信息，false 获取发布版信息。</summary>
        public bool? need_edited_data { get; set; }
    }

    /// <summary>查询作品详情请求。</summary>
    public class NovelGetBookRequest
    {
        /// <summary>可选作品 ID；与 original_id 二选一并优先使用。</summary>
        public string book_id { get; set; }

        /// <summary>true 获取编辑版信息，false 获取发布版信息。</summary>
        public bool? need_edited_data { get; set; }

        /// <summary>可选提供方作品主键，与 book_id 二选一。</summary>
        public string original_id { get; set; }
    }

    /// <summary>小说作品审核信息。</summary>
    public class NovelAuditInfo
    {
        /// <summary>审核状态：0 未提审，1 审核中，2 审核不通过，3 审核通过。</summary>
        public int audit_status { get; set; }

        /// <summary>提审 Unix 时间戳，单位秒。</summary>
        public long create_time { get; set; }

        /// <summary>审核 Unix 时间戳，单位秒。</summary>
        public long audit_time { get; set; }

        /// <summary>审核原因。</summary>
        public string reason { get; set; }

        /// <summary>审核不通过时的修改建议。</summary>
        public string suggestion { get; set; }
    }

    /// <summary>小说作品信息。</summary>
    public class NovelBookInfo
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>作品名。官方列表返回表误标为 number，示例和详情均为 string。</summary>
        public string title { get; set; }

        /// <summary>作品简介。</summary>
        public string intro { get; set; }

        /// <summary>封面图 URL。</summary>
        public string cover_url { get; set; }

        /// <summary>作者名。</summary>
        public string author { get; set; }

        /// <summary>一级作品类型 ID。</summary>
        public int first_category_id { get; set; }

        /// <summary>一级作品类型名。</summary>
        public string first_category_name { get; set; }

        /// <summary>二级作品类型 ID。</summary>
        public int second_category_id { get; set; }

        /// <summary>二级作品类型名。</summary>
        public string second_category_name { get; set; }

        /// <summary>三级作品类型 ID。</summary>
        public int third_category_id { get; set; }

        /// <summary>三级作品类型名。</summary>
        public string third_category_name { get; set; }

        /// <summary>完结状态：1 连载中，2 已完结。</summary>
        public int complete_status { get; set; }

        /// <summary>上传场景：1 本地上传，2 API 上传。</summary>
        public int upload_scene { get; set; }

        /// <summary>章节数量。</summary>
        public int chapter_cnt { get; set; }

        /// <summary>分卷数量。</summary>
        public int volume_cnt { get; set; }

        /// <summary>分卷信息。</summary>
        public IList<NovelVolumeInfo> volume_list { get; set; }

        /// <summary>作品总字数。</summary>
        public long total_word_cnt { get; set; }

        /// <summary>审核信息，未发起审核时不返回。</summary>
        public NovelAuditInfo audit_info { get; set; }

        /// <summary>创建 Unix 时间戳，单位秒。</summary>
        public long create_time { get; set; }

        /// <summary>提供方作品主键。</summary>
        public string original_id { get; set; }

        /// <summary>章节排序方式：0 追加，1 按 seq 递增。</summary>
        public int chapter_order_method { get; set; }

        /// <summary>自定义信息。</summary>
        public string custom_info { get; set; }

        /// <summary>管控状态：0 正常，1 下架。</summary>
        public int ban_status { get; set; }
    }

    /// <summary>创建作品结果。</summary>
    public class NovelBookIdJsonResult : WxJsonResult
    {
        /// <summary>作品 ID，最长 64 字节。</summary>
        public string book_id { get; set; }
    }

    /// <summary>作品列表结果。</summary>
    public class NovelListBooksJsonResult : WxJsonResult
    {
        /// <summary>作品信息列表。</summary>
        public IList<NovelBookInfo> book_list { get; set; }

        /// <summary>作品总数。</summary>
        public int total_cnt { get; set; }

        /// <summary>下一次分页使用的 ID；请求使用 last_id 且结果不为空时返回。</summary>
        public long? last_id { get; set; }
    }

    /// <summary>作品详情结果。</summary>
    public class NovelGetBookJsonResult : WxJsonResult
    {
        /// <summary>作品信息。官方返回表误标为 objarray，示例及协议实际为单个对象。</summary>
        public NovelBookInfo book { get; set; }
    }

    /// <summary>待创建章节内容。</summary>
    public class NovelChapterInput
    {
        /// <summary>章节标题，长度 1 至 80 个字。</summary>
        public string chapter_title { get; set; }

        /// <summary>章节正文，长度 1 至 20000 个字。</summary>
        public string content { get; set; }

        /// <summary>可选提供方章节主键，可用于去重。</summary>
        public string original_id { get; set; }

        /// <summary>可选章节相对顺序，可非连续递增。</summary>
        public long? seq { get; set; }

        /// <summary>可选自定义信息，最长 128 字节。</summary>
        public string custom_info { get; set; }
    }

    /// <summary>创建单个章节请求。</summary>
    public class NovelCreateChapterRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>章节内容。</summary>
        public NovelChapterInput chapter { get; set; }
    }

    /// <summary>批量创建章节请求。</summary>
    public class NovelBatchCreateChaptersRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>章节内容列表，单次最多 10 章。</summary>
        public IList<NovelChapterInput> chapter_list { get; set; }
    }

    /// <summary>创建单个章节结果。</summary>
    public class NovelChapterIdJsonResult : WxJsonResult
    {
        /// <summary>章节 ID，最长 64 字节。</summary>
        public string chapter_id { get; set; }
    }

    /// <summary>批量创建章节结果。</summary>
    public class NovelBatchCreateChaptersJsonResult : WxJsonResult
    {
        /// <summary>章节 ID 列表；官方返回示例使用此字段名。</summary>
        public IList<string> chapter_id_list { get; set; }

        /// <summary>章节 ID 列表兼容字段；官方返回参数表写为 chapter_id。</summary>
        public IList<string> chapter_id { get; set; }

        /// <summary>发生冲突的提供方章节主键列表。</summary>
        public IList<string> conflict_original_id_list { get; set; }
    }

    /// <summary>删除章节请求。</summary>
    public class NovelDeleteChapterRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>章节 ID。</summary>
        public string chapter_id { get; set; }
    }

    /// <summary>替换章节请求。</summary>
    public class NovelReplaceChapterRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>被替换的章节 ID。</summary>
        public string chapter_id { get; set; }

        /// <summary>新章节标题，长度 1 至 80 个字。</summary>
        public string new_chapter_title { get; set; }

        /// <summary>新章节正文，长度 1 至 20000 个字。</summary>
        public string new_content { get; set; }
    }

    /// <summary>替换章节结果。</summary>
    public class NovelReplaceChapterJsonResult : WxJsonResult
    {
        /// <summary>替换后生成的新章节 ID。</summary>
        public string new_chapter_id { get; set; }
    }

    /// <summary>分页查询章节列表请求。</summary>
    public class NovelListChaptersRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>true 获取编辑版信息，false 获取发布版信息。</summary>
        public bool? need_edited_data { get; set; }

        /// <summary>单页数量，默认 10，最大 100。</summary>
        public int? limit { get; set; }

        /// <summary>起始偏移量，默认 0。</summary>
        public int? offset { get; set; }

        /// <summary>可选分卷下标；设置后仅返回指定分卷章节。</summary>
        public int? volume_index { get; set; }
    }

    /// <summary>查询章节详情请求。</summary>
    public class NovelGetChapterRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>章节 ID。</summary>
        public string chapter_id { get; set; }

        /// <summary>true 获取编辑版信息，false 获取发布版信息。</summary>
        public bool? need_edited_data { get; set; }
    }

    /// <summary>小说章节信息。</summary>
    public class NovelChapterInfo
    {
        /// <summary>作品 ID；官方返回表未列出，但详情示例返回。</summary>
        public string book_id { get; set; }

        /// <summary>章节 ID。</summary>
        public string chapter_id { get; set; }

        /// <summary>章节标题。</summary>
        public string chapter_title { get; set; }

        /// <summary>章节正文；官方返回表未列出，但详情示例返回。</summary>
        public string content { get; set; }

        /// <summary>章节字数。</summary>
        public int word_cnt { get; set; }

        /// <summary>创建 Unix 时间戳，单位秒。</summary>
        public long create_time { get; set; }

        /// <summary>审核信息。</summary>
        public NovelAuditInfo audit_info { get; set; }

        /// <summary>所属分卷下标，-1 表示不属于任何分卷。</summary>
        public int volume_index { get; set; }

        /// <summary>提供方章节主键。</summary>
        public string original_id { get; set; }

        /// <summary>章节相对顺序。</summary>
        public long seq { get; set; }

        /// <summary>自定义信息。</summary>
        public string custom_info { get; set; }

        /// <summary>管控状态：0 正常，1 下架。</summary>
        public int ban_status { get; set; }
    }

    /// <summary>章节列表结果。</summary>
    public class NovelListChaptersJsonResult : WxJsonResult
    {
        /// <summary>章节信息列表。</summary>
        public IList<NovelChapterInfo> chapter_list { get; set; }

        /// <summary>章节总数。</summary>
        public int total_cnt { get; set; }
    }

    /// <summary>章节详情结果。</summary>
    public class NovelGetChapterJsonResult : WxJsonResult
    {
        /// <summary>章节信息。</summary>
        public NovelChapterInfo chapter { get; set; }
    }

    /// <summary>调整章节顺序请求。</summary>
    public class NovelReorderChapterRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>待移动章节 ID。</summary>
        public string chapter_id { get; set; }

        /// <summary>目标章节 ID。</summary>
        public string target_chapter_id { get; set; }

        /// <summary>操作类型：1 交换，2 插入目标章节之前，3 插入目标章节之后。</summary>
        public int operation { get; set; }
    }

    /// <summary>章节相对顺序项。</summary>
    public class NovelChapterSequence
    {
        /// <summary>章节 ID。</summary>
        public string chapter_id { get; set; }

        /// <summary>章节相对顺序，可非连续递增。</summary>
        public long seq { get; set; }
    }

    /// <summary>批量调整章节相对顺序请求。</summary>
    public class NovelUpdateChapterSequenceRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>章节及其新 seq 列表。</summary>
        public IList<NovelChapterSequence> chapter_seq_list { get; set; }
    }
}
