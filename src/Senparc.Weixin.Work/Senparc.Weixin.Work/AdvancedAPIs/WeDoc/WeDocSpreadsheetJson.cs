/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocSpreadsheetJson.cs
    文件功能描述：企业微信电子表格强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐电子表格批量更新、属性和单元格模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>电子表格批量更新请求。</summary>
    public class WeDocSpreadsheetBatchUpdateRequest : WeDocIdRequest
    {
        /// <summary>按顺序执行的更新操作，单次最多 5 个。</summary>
        public IList<WeDocSpreadsheetUpdateRequest> requests { get; set; }
    }

    /// <summary>单个电子表格更新操作；四种操作只填写一种。</summary>
    public class WeDocSpreadsheetUpdateRequest
    {
        /// <summary>新增工作表请求。</summary>
        public WeDocSpreadsheetAddSheetRequest add_sheet_request { get; set; }

        /// <summary>删除工作表请求。</summary>
        public WeDocSpreadsheetDeleteSheetRequest delete_sheet_request { get; set; }

        /// <summary>更新单元格范围请求。</summary>
        public WeDocSpreadsheetUpdateRangeRequest update_range_request { get; set; }

        /// <summary>删除连续行或列请求。</summary>
        public WeDocSpreadsheetDeleteDimensionRequest delete_dimension_request { get; set; }
    }

    /// <summary>新增电子工作表请求。</summary>
    public class WeDocSpreadsheetAddSheetRequest
    {
        /// <summary>工作表名称。</summary>
        public string title { get; set; }

        /// <summary>初始行数。</summary>
        public int row_count { get; set; }

        /// <summary>初始列数。</summary>
        public int column_count { get; set; }
    }

    /// <summary>删除电子工作表请求。</summary>
    public class WeDocSpreadsheetDeleteSheetRequest
    {
        /// <summary>工作表唯一标识。</summary>
        public string sheet_id { get; set; }
    }

    /// <summary>更新电子表格单元格范围请求。</summary>
    public class WeDocSpreadsheetUpdateRangeRequest
    {
        /// <summary>工作表唯一标识。</summary>
        public string sheet_id { get; set; }

        /// <summary>待写入的单元格数据和格式。</summary>
        public WeDocSpreadsheetGridData grid_data { get; set; }
    }

    /// <summary>删除电子表格连续行或列请求。</summary>
    public class WeDocSpreadsheetDeleteDimensionRequest
    {
        /// <summary>工作表唯一标识。</summary>
        public string sheet_id { get; set; }

        /// <summary>维度类型：ROW 或 COLUMN。</summary>
        public string dimension { get; set; }

        /// <summary>起始序号，从 1 开始且包含该位置。</summary>
        public int start_index { get; set; }

        /// <summary>结束序号，从 1 开始且不包含该位置。</summary>
        public int end_index { get; set; }
    }

    /// <summary>电子表格批量更新结果。</summary>
    public class WeDocSpreadsheetBatchUpdateResult : WorkJsonResult
    {
        /// <summary>新增工作表结果。</summary>
        public WeDocSpreadsheetAddSheetResult add_sheet_response { get; set; }

        /// <summary>删除工作表结果。</summary>
        public WeDocSpreadsheetDeleteSheetResult delete_sheet_response { get; set; }

        /// <summary>更新单元格范围结果。</summary>
        public WeDocSpreadsheetUpdateRangeResult update_range_response { get; set; }

        /// <summary>删除连续行或列结果。</summary>
        public WeDocSpreadsheetDeleteDimensionResult delete_dimension_response { get; set; }
    }

    /// <summary>新增工作表结果。</summary>
    public class WeDocSpreadsheetAddSheetResult
    {
        /// <summary>新增工作表属性。</summary>
        public WeDocSpreadsheetProperty properties { get; set; }
    }

    /// <summary>删除工作表结果。</summary>
    public class WeDocSpreadsheetDeleteSheetResult
    {
        /// <summary>被删除的工作表 ID。</summary>
        public string sheet_id { get; set; }
    }

    /// <summary>更新单元格范围结果。</summary>
    public class WeDocSpreadsheetUpdateRangeResult
    {
        /// <summary>成功更新的单元格数量。</summary>
        public int updated_cells { get; set; }
    }

    /// <summary>删除连续行或列结果。</summary>
    public class WeDocSpreadsheetDeleteDimensionResult
    {
        /// <summary>被删除的行数或列数。</summary>
        public int deleted { get; set; }
    }

    /// <summary>电子表格工作表属性结果。</summary>
    public class WeDocSpreadsheetPropertiesResult : WorkJsonResult
    {
        /// <summary>工作表属性列表。</summary>
        public IList<WeDocSpreadsheetProperty> properties { get; set; }
    }

    /// <summary>电子工作表属性。</summary>
    public class WeDocSpreadsheetProperty
    {
        /// <summary>工作表唯一标识。</summary>
        public string sheet_id { get; set; }

        /// <summary>工作表名称。</summary>
        public string title { get; set; }

        /// <summary>总行数。</summary>
        public int row_count { get; set; }

        /// <summary>总列数。</summary>
        public int column_count { get; set; }
    }

    /// <summary>读取电子表格范围请求。</summary>
    public class WeDocSpreadsheetRangeRequest : WeDocIdRequest
    {
        /// <summary>工作表唯一标识。</summary>
        public string sheet_id { get; set; }

        /// <summary>A1 表示法的读取范围，例如 A1:B2。</summary>
        public string range { get; set; }
    }

    /// <summary>电子表格范围数据结果。</summary>
    public class WeDocSpreadsheetDataResult : WorkJsonResult
    {
        /// <summary>范围内的单元格数据。</summary>
        public WeDocSpreadsheetGridData grid_data { get; set; }
    }

    /// <summary>电子表格网格数据。</summary>
    public class WeDocSpreadsheetGridData
    {
        /// <summary>起始行编号，从 0 开始。</summary>
        public int start_row { get; set; }

        /// <summary>起始列编号，从 0 开始。</summary>
        public int start_column { get; set; }

        /// <summary>逐行单元格数据。</summary>
        public IList<WeDocSpreadsheetRowData> rows { get; set; }
    }

    /// <summary>电子表格行数据。</summary>
    public class WeDocSpreadsheetRowData
    {
        /// <summary>该行的单元格列表。</summary>
        public IList<WeDocSpreadsheetCellData> values { get; set; }
    }

    /// <summary>电子表格单元格数据。</summary>
    public class WeDocSpreadsheetCellData
    {
        /// <summary>单元格值。</summary>
        public WeDocSpreadsheetCellValue cell_value { get; set; }

        /// <summary>单元格格式。</summary>
        public WeDocSpreadsheetCellFormat cell_format { get; set; }
    }

    /// <summary>电子表格单元格值；文本和链接只填写一种。</summary>
    public class WeDocSpreadsheetCellValue
    {
        /// <summary>文本值。</summary>
        public string text { get; set; }

        /// <summary>链接值。</summary>
        public WeDocSpreadsheetLink link { get; set; }
    }

    /// <summary>电子表格链接值。</summary>
    public class WeDocSpreadsheetLink
    {
        /// <summary>链接地址。</summary>
        public string url { get; set; }

        /// <summary>链接标题。</summary>
        public string text { get; set; }
    }

    /// <summary>电子表格单元格格式。</summary>
    public class WeDocSpreadsheetCellFormat
    {
        /// <summary>文字格式。</summary>
        public WeDocSpreadsheetTextFormat text_format { get; set; }
    }

    /// <summary>电子表格文字格式。</summary>
    public class WeDocSpreadsheetTextFormat
    {
        /// <summary>字体名称。</summary>
        public string font { get; set; }

        /// <summary>字号，最大 72。</summary>
        public int? font_size { get; set; }

        /// <summary>是否加粗。</summary>
        public bool? bold { get; set; }

        /// <summary>是否斜体。</summary>
        public bool? italic { get; set; }

        /// <summary>是否使用删除线。</summary>
        public bool? strikethrough { get; set; }

        /// <summary>是否使用下划线。</summary>
        public bool? underline { get; set; }

        /// <summary>文字颜色。</summary>
        public WeDocSpreadsheetColor color { get; set; }
    }

    /// <summary>电子表格 RGBA 颜色。</summary>
    public class WeDocSpreadsheetColor
    {
        /// <summary>红色通道，范围 0 至 255。</summary>
        public int red { get; set; }

        /// <summary>绿色通道，范围 0 至 255。</summary>
        public int green { get; set; }

        /// <summary>蓝色通道，范围 0 至 255。</summary>
        public int blue { get; set; }

        /// <summary>透明度通道，范围 0 至 255。</summary>
        public int alpha { get; set; }
    }
}
