/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocSmartDocumentJson.cs
    文件功能描述：企业微信智能文档内容管理强类型模型


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智能文档页面、内容块、数据表与发布模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>智能文档通用请求。</summary>
    public class WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置智能文档 DocID。</summary>
        public string docid { get; set; }
    }

    /// <summary>智能文档页面信息。</summary>
    public class WeDocSmartDocumentPageInfo
    {
        /// <summary>获取或设置页面 ID；添加页面时由系统生成。</summary>
        public string page_id { get; set; }

        /// <summary>获取或设置页面标题。</summary>
        public string title { get; set; }

        /// <summary>获取或设置父页面 ID；空字符串表示根页面。</summary>
        public string parent_id { get; set; }

        /// <summary>获取或设置排序参照页面 ID。</summary>
        public string after_id { get; set; }

        /// <summary>获取或设置页面布局：1 默认，2 纸张，3 全宽。</summary>
        public uint? layout_mode { get; set; }
    }

    /// <summary>添加智能文档页面请求。</summary>
    public class WeDocSmartDocumentAddPageRequest : WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置待添加的页面信息。</summary>
        public WeDocSmartDocumentPageInfo info { get; set; }
    }

    /// <summary>更新智能文档页面请求。</summary>
    public class WeDocSmartDocumentUpdatePageRequest : WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置待更新的页面信息；page_id 必填。</summary>
        public WeDocSmartDocumentPageInfo info { get; set; }
    }

    /// <summary>智能文档页面操作结果。</summary>
    public class WeDocSmartDocumentPageResult : WorkJsonResult
    {
        /// <summary>获取或设置添加或更新后的页面信息。</summary>
        public WeDocSmartDocumentPageInfo info { get; set; }
    }

    /// <summary>删除智能文档页面请求。</summary>
    public class WeDocSmartDocumentDeletePageRequest : WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置需要删除的页面 ID。</summary>
        public string page_id { get; set; }
    }

    /// <summary>智能文档页面层级结果。</summary>
    public class WeDocSmartDocumentPageHierarchyResult : WorkJsonResult
    {
        /// <summary>获取或设置页面及父页面关系列表。</summary>
        public IList<WeDocSmartDocumentPageInfo> pages { get; set; }
    }

    /// <summary>智能文档文本块属性。</summary>
    public class WeDocSmartDocumentTextBlockProperties
    {
        /// <summary>获取或设置块级对齐方式。</summary>
        public uint? align { get; set; }

        /// <summary>获取或设置文本对齐方式。</summary>
        public uint? text_align { get; set; }

        /// <summary>获取或设置文本块背景色枚举字符串。</summary>
        public string block_color { get; set; }
    }

    /// <summary>智能文档图片块属性。</summary>
    public class WeDocSmartDocumentImageBlockProperties
    {
        /// <summary>获取或设置图片地址。</summary>
        public string url { get; set; }

        /// <summary>获取或设置缩放后宽度。</summary>
        public double? width { get; set; }

        /// <summary>获取或设置缩放后高度。</summary>
        public double? height { get; set; }

        /// <summary>获取或设置是否显示图片描边。</summary>
        public bool? stroke { get; set; }
    }

    /// <summary>智能文档文件块属性。</summary>
    public class WeDocSmartDocumentFileBlockProperties
    {
        /// <summary>获取或设置微盘文件 ID。</summary>
        public string file_id { get; set; }
    }

    /// <summary>智能文档链接块属性。</summary>
    public class WeDocSmartDocumentLinkBlockProperties
    {
        /// <summary>获取或设置链接地址。</summary>
        public string link_url { get; set; }

        /// <summary>获取或设置链接名称。</summary>
        public string link_name { get; set; }

        /// <summary>获取或设置链接描述。</summary>
        public string link_description { get; set; }
    }

    /// <summary>智能文档表格单元格位置。</summary>
    public class WeDocSmartDocumentTableCellPosition
    {
        /// <summary>获取或设置行坐标。</summary>
        public uint row { get; set; }

        /// <summary>获取或设置列坐标。</summary>
        public uint column { get; set; }
    }

    /// <summary>智能文档表格单元格合并范围。</summary>
    public class WeDocSmartDocumentTableCellMerge
    {
        /// <summary>获取或设置合并起始单元格。</summary>
        public WeDocSmartDocumentTableCellPosition start_cell { get; set; }

        /// <summary>获取或设置合并结束单元格。</summary>
        public WeDocSmartDocumentTableCellPosition end_cell { get; set; }
    }

    /// <summary>智能文档表格块属性。</summary>
    public class WeDocSmartDocumentTableBlockProperties
    {
        /// <summary>获取或设置单元格合并范围列表。</summary>
        public IList<WeDocSmartDocumentTableCellMerge> cell_positions { get; set; }

        /// <summary>获取或设置是否启用行标题。</summary>
        public bool? enable_row_header { get; set; }

        /// <summary>获取或设置是否启用列标题。</summary>
        public bool? enable_column_header { get; set; }
    }

    /// <summary>智能文档数据表视图块属性。</summary>
    public class WeDocSmartDocumentViewBlockProperties
    {
        /// <summary>获取或设置数据表文档 ID。</summary>
        public string doc_id { get; set; }

        /// <summary>获取或设置数据表 ID。</summary>
        public string table_id { get; set; }

        /// <summary>获取或设置视图 ID。</summary>
        public string view_id { get; set; }

        /// <summary>获取或设置数据表内容块 ID。</summary>
        public string table_block_id { get; set; }

        /// <summary>获取或设置是否显示数据表标题。</summary>
        public bool? enable_table_title { get; set; }

        /// <summary>获取或设置是否显示添加记录按钮。</summary>
        public bool? enable_add_row { get; set; }
    }

    /// <summary>智能文档分栏块属性。</summary>
    public class WeDocSmartDocumentColumnListProperties
    {
        /// <summary>获取或设置分栏数量；有效范围为 2 至 4。</summary>
        public uint? column_num { get; set; }
    }

    /// <summary>智能文档背景块属性。</summary>
    public class WeDocSmartDocumentHighlightBlockProperties
    {
        /// <summary>获取或设置背景图地址。</summary>
        public string background_image_url { get; set; }

        /// <summary>获取或设置背景图是否扩展至整个块区域。</summary>
        public bool? extend_background { get; set; }

        /// <summary>获取或设置块背景色枚举字符串。</summary>
        public string block_color { get; set; }

        /// <summary>获取或设置边框颜色枚举字符串。</summary>
        public string border_color { get; set; }
    }

    /// <summary>智能文档代码块属性。</summary>
    public class WeDocSmartDocumentCodeBlockProperties
    {
        /// <summary>获取或设置官方 CodeLanguage 枚举字符串，例如 CODE_LANGUAGE_CSHARP。</summary>
        public string code_language { get; set; }

        /// <summary>获取或设置代码是否自动换行。</summary>
        public bool? code_wrap { get; set; }
    }

    /// <summary>智能文档待办块属性。</summary>
    public class WeDocSmartDocumentTodoBlockProperties
    {
        /// <summary>获取或设置待办是否已完成。</summary>
        public bool? @checked { get; set; }
    }

    /// <summary>智能文档内容块属性集合；按块类型使用对应属性。</summary>
    public class WeDocSmartDocumentBlockProperties
    {
        /// <summary>获取或设置文本块属性。</summary>
        public WeDocSmartDocumentTextBlockProperties text_props { get; set; }

        /// <summary>获取或设置图片块属性。</summary>
        public WeDocSmartDocumentImageBlockProperties image_props { get; set; }

        /// <summary>获取或设置文件块属性。</summary>
        public WeDocSmartDocumentFileBlockProperties file_props { get; set; }

        /// <summary>获取或设置链接块属性。</summary>
        public WeDocSmartDocumentLinkBlockProperties link_props { get; set; }

        /// <summary>获取或设置普通表格块属性。</summary>
        public WeDocSmartDocumentTableBlockProperties table_props { get; set; }

        /// <summary>获取或设置数据表视图块属性。</summary>
        public WeDocSmartDocumentViewBlockProperties view_props { get; set; }

        /// <summary>获取或设置分栏块属性。</summary>
        public WeDocSmartDocumentColumnListProperties column_props { get; set; }

        /// <summary>获取或设置背景块属性。</summary>
        public WeDocSmartDocumentHighlightBlockProperties highlight_props { get; set; }

        /// <summary>获取或设置代码块属性。</summary>
        public WeDocSmartDocumentCodeBlockProperties code_props { get; set; }

        /// <summary>获取或设置待办块属性。</summary>
        public WeDocSmartDocumentTodoBlockProperties todo_props { get; set; }
    }

    /// <summary>智能文档内容块信息。</summary>
    public class WeDocSmartDocumentBlockInfo
    {
        /// <summary>获取或设置内容块唯一 ID；添加时由系统生成。</summary>
        public string id { get; set; }

        /// <summary>获取或设置官方 BlockType 字符串。</summary>
        public string type { get; set; }

        /// <summary>获取或设置内容块文本。</summary>
        public string title { get; set; }

        /// <summary>获取或设置部分官方示例使用的内容字段。</summary>
        public string content { get; set; }

        /// <summary>获取或设置父内容块 ID。</summary>
        public string parent_id { get; set; }

        /// <summary>获取或设置排序参照内容块 ID。</summary>
        public string after_id { get; set; }

        /// <summary>获取或设置子内容块 ID 列表。</summary>
        public IList<string> children { get; set; }

        /// <summary>获取或设置与块类型对应的强类型属性。</summary>
        public WeDocSmartDocumentBlockProperties props { get; set; }
    }

    /// <summary>智能文档页面内容块通用请求。</summary>
    public class WeDocSmartDocumentPageBlocksRequest : WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置页面 ID。</summary>
        public string page_id { get; set; }
    }

    /// <summary>添加智能文档内容块请求。</summary>
    public class WeDocSmartDocumentAddBlocksRequest : WeDocSmartDocumentPageBlocksRequest
    {
        /// <summary>获取或设置待添加的内容块列表。</summary>
        public IList<WeDocSmartDocumentBlockInfo> blocks { get; set; }
    }

    /// <summary>更新智能文档内容块请求。</summary>
    public class WeDocSmartDocumentUpdateBlocksRequest : WeDocSmartDocumentPageBlocksRequest
    {
        /// <summary>获取或设置待更新的内容块列表；每项 id 必填。</summary>
        public IList<WeDocSmartDocumentBlockInfo> blocks { get; set; }
    }

    /// <summary>删除智能文档内容块请求。</summary>
    public class WeDocSmartDocumentDeleteBlocksRequest : WeDocSmartDocumentPageBlocksRequest
    {
        /// <summary>获取或设置待删除的内容块 ID 列表。</summary>
        public IList<string> ids { get; set; }
    }

    /// <summary>智能文档内容块操作结果。</summary>
    public class WeDocSmartDocumentBlocksResult : WorkJsonResult
    {
        /// <summary>获取或设置添加或更新后的内容块列表。</summary>
        public IList<WeDocSmartDocumentBlockInfo> blocks { get; set; }
    }

    /// <summary>获取智能文档内容块请求。</summary>
    public class WeDocSmartDocumentGetBlocksRequest : WeDocSmartDocumentPageBlocksRequest
    {
        /// <summary>获取或设置需要获取的内容块 ID 列表。</summary>
        public IList<string> ids { get; set; }

        /// <summary>获取或设置分批起始位置，最小为 0。</summary>
        public int? start { get; set; }

        /// <summary>获取或设置分批数量，最大及默认值为 200。</summary>
        public int? limit { get; set; }
    }

    /// <summary>智能文档内容块列表结果。</summary>
    public class WeDocSmartDocumentBlockListResult : WeDocSmartDocumentBlocksResult
    {
        /// <summary>获取或设置是否还有更多数据；官方协议定义为字符串 true 或 false。</summary>
        public string has_more { get; set; }

        /// <summary>获取或设置下一批起始位置。</summary>
        public int next_start { get; set; }
    }

    /// <summary>提交智能文档导出任务请求。</summary>
    public class WeDocSmartDocumentExportTaskRequest : WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置内容格式；当前仅支持 1（Markdown）。</summary>
        public uint content_type { get; set; }

        /// <summary>获取或设置官方请求示例中可选的智能文档地址。</summary>
        public string url { get; set; }

        /// <summary>获取或设置仅导出的页面 ID；未填写时导出整个文档。</summary>
        public string page_id { get; set; }
    }

    /// <summary>智能文档导出任务创建结果。</summary>
    public class WeDocSmartDocumentExportTaskResult : WorkJsonResult
    {
        /// <summary>获取或设置异步导出任务 ID。</summary>
        public string task_id { get; set; }
    }

    /// <summary>查询智能文档导出结果请求。</summary>
    public class WeDocSmartDocumentExportResultRequest
    {
        /// <summary>获取或设置异步导出任务 ID。</summary>
        public string task_id { get; set; }
    }

    /// <summary>智能文档导出结果。</summary>
    public class WeDocSmartDocumentExportResult : WorkJsonResult
    {
        /// <summary>获取或设置任务是否已经完成。</summary>
        public bool task_done { get; set; }

        /// <summary>获取或设置任务完成后返回的 Markdown 内容。</summary>
        public string content { get; set; }
    }

    /// <summary>智能文档数据源结果。</summary>
    public class WeDocSmartDocumentDataSourceResult : WorkJsonResult
    {
        /// <summary>获取或设置智能文档绑定或新建的智能表格 DocID。</summary>
        public string ss_docid { get; set; }
    }

    /// <summary>智能文档数据表信息。</summary>
    public class WeDocSmartDocumentDataTableInfo
    {
        /// <summary>获取或设置数据表内容块 ID。</summary>
        public string block_id { get; set; }

        /// <summary>获取或设置数据表标题。</summary>
        public string title { get; set; }

        /// <summary>获取或设置数据表子表 ID。</summary>
        public string sheet_id { get; set; }

        /// <summary>获取或设置数据源智能表格 DocID。</summary>
        public string ss_docid { get; set; }

        /// <summary>获取或设置排序参照数据表内容块 ID。</summary>
        public string after_id { get; set; }
    }

    /// <summary>添加智能文档数据表请求。</summary>
    public class WeDocSmartDocumentAddDataTableRequest : WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置待添加的数据表标题和排序信息。</summary>
        public WeDocSmartDocumentDataTableInfo info { get; set; }
    }

    /// <summary>更新智能文档数据表请求。</summary>
    public class WeDocSmartDocumentUpdateDataTableRequest : WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置待更新的数据表信息；block_id 必填。</summary>
        public WeDocSmartDocumentDataTableInfo info { get; set; }
    }

    /// <summary>删除智能文档数据表请求。</summary>
    public class WeDocSmartDocumentDeleteDataTableRequest : WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置需要删除的数据表内容块 ID。</summary>
        public string block_id { get; set; }
    }

    /// <summary>智能文档数据表操作结果。</summary>
    public class WeDocSmartDocumentDataTableResult : WorkJsonResult
    {
        /// <summary>获取或设置添加或更新后的数据表信息。</summary>
        public WeDocSmartDocumentDataTableInfo info { get; set; }
    }

    /// <summary>智能文档发布指定可见成员。</summary>
    public class WeDocSmartDocumentPublishAuth
    {
        /// <summary>获取或设置成员类型：1 企业成员，2 部门。</summary>
        public uint type { get; set; }

        /// <summary>获取或设置企业成员 UserId；type 为 1 时必填。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置部门 ID；type 为 2 时必填。</summary>
        public ulong? departmentid { get; set; }
    }

    /// <summary>发布智能文档请求。</summary>
    public class WeDocSmartDocumentPublishRequest : WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置发布范围：1 企业内，3 企业内外，4 指定成员。</summary>
        public uint? publish_range { get; set; }

        /// <summary>获取或设置指定可见成员；publish_range 为 4 时必填。</summary>
        public IList<WeDocSmartDocumentPublishAuth> auth_list { get; set; }
    }

    /// <summary>智能文档发布结果。</summary>
    public class WeDocSmartDocumentPublishResult : WorkJsonResult
    {
        /// <summary>获取或设置发布分享码。</summary>
        public string share_code { get; set; }

        /// <summary>获取或设置官方响应示例中的发布页地址。</summary>
        public string publish_url { get; set; }

        /// <summary>获取或设置发布版本号。</summary>
        public ulong version { get; set; }

        /// <summary>获取或设置发布时间戳，单位为秒。</summary>
        public ulong publish_time { get; set; }

        /// <summary>获取或设置发布页标题。</summary>
        public string publish_doc_title { get; set; }
    }

    /// <summary>修改智能文档发布范围请求。</summary>
    public class WeDocSmartDocumentPublishSettingRequest : WeDocSmartDocumentRequest
    {
        /// <summary>获取或设置发布范围：1 企业内，3 企业内外，4 指定成员。</summary>
        public uint publish_range { get; set; }

        /// <summary>获取或设置指定可见成员；publish_range 为 4 时必填。</summary>
        public IList<WeDocSmartDocumentPublishAuth> auth_list { get; set; }
    }
}
