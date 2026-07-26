/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocSmartSheetRecordJson.cs
    文件功能描述：企业微信智能表格记录强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智能表格记录增删改查强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>
    /// 获取智能表格记录时使用的排序项。
    /// </summary>
    public class WeDocSmartSheetRecordSortItem
    {
        /// <summary>
        /// 获取或设置排序字段标题。
        /// </summary>
        public string field_title { get; set; }

        /// <summary>
        /// 获取或设置是否按降序排列。
        /// </summary>
        public bool desc { get; set; }
    }

    /// <summary>
    /// 获取智能表格记录请求。
    /// </summary>
    public class WeDocSmartSheetGetRecordsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置视图 ID。
        /// </summary>
        public string view_id { get; set; }

        /// <summary>
        /// 获取或设置需要查询的记录 ID 列表。
        /// </summary>
        public IList<string> record_ids { get; set; }

        /// <summary>
        /// 获取或设置需要返回的字段 ID 列表。
        /// </summary>
        public IList<string> field_ids { get; set; }

        /// <summary>
        /// 获取或设置需要返回的字段标题列表。
        /// </summary>
        public IList<string> field_titles { get; set; }

        /// <summary>
        /// 获取或设置单元格字典的键类型，例如 <c>CELL_VALUE_KEY_TYPE_FIELD_TITLE</c>。
        /// </summary>
        public string key_type { get; set; }

        /// <summary>
        /// 获取或设置排序列表。
        /// </summary>
        public IList<WeDocSmartSheetRecordSortItem> sort { get; set; }

        /// <summary>
        /// 获取或设置筛选配置。
        /// </summary>
        public WeDocSmartSheetFilterSpec filter_spec { get; set; }

        /// <summary>
        /// 获取或设置分页起始偏移量。
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 获取或设置单页返回数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 新增智能表格记录时使用的记录数据。
    /// </summary>
    public class WeDocSmartSheetRecordInput
    {
        /// <summary>
        /// 获取或设置单元格值，键由请求的 <c>key_type</c> 决定；
        /// 不同字段类型的值可能是标量或复合数组，因此保留对应 JSON 值结构。
        /// </summary>
        public IDictionary<string, JsonElement> values { get; set; }
    }

    /// <summary>
    /// 更新智能表格记录时使用的记录数据。
    /// </summary>
    public class WeDocSmartSheetRecordUpdate
    {
        /// <summary>
        /// 获取或设置记录 ID。
        /// </summary>
        public string record_id { get; set; }

        /// <summary>
        /// 获取或设置需要更新的单元格值，键由请求的 <c>key_type</c> 决定。
        /// </summary>
        public IDictionary<string, JsonElement> values { get; set; }
    }

    /// <summary>
    /// 智能表格记录信息。
    /// </summary>
    public class WeDocSmartSheetRecord
    {
        /// <summary>
        /// 获取或设置记录 ID。
        /// </summary>
        public string record_id { get; set; }

        /// <summary>
        /// 获取或设置记录创建者名称。
        /// </summary>
        public string creator_name { get; set; }

        /// <summary>
        /// 获取或设置记录最后编辑者名称。
        /// </summary>
        public string updater_name { get; set; }

        /// <summary>
        /// 获取或设置单元格值。不同字段类型的值结构不同，因此逐项保留原始 JSON 值。
        /// </summary>
        public IDictionary<string, JsonElement> values { get; set; }

        /// <summary>
        /// 获取或设置记录创建时间的毫秒级时间戳。
        /// </summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long create_time { get; set; }

        /// <summary>
        /// 获取或设置记录更新时间的毫秒级时间戳。
        /// </summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long update_time { get; set; }
    }

    /// <summary>
    /// 获取智能表格记录结果。
    /// </summary>
    public class WeDocSmartSheetGetRecordsResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置记录列表。
        /// </summary>
        public IList<WeDocSmartSheetRecord> records { get; set; }

        /// <summary>
        /// 获取或设置符合条件的记录总数。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 获取或设置下一页起始偏移量。
        /// </summary>
        public int? next { get; set; }

        /// <summary>
        /// 获取或设置是否还有更多记录。
        /// </summary>
        public bool has_more { get; set; }
    }

    /// <summary>
    /// 批量新增智能表格记录请求。
    /// </summary>
    public class WeDocSmartSheetAddRecordsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置单元格字典的键类型。
        /// </summary>
        public string key_type { get; set; }

        /// <summary>
        /// 获取或设置待新增记录列表。
        /// </summary>
        public IList<WeDocSmartSheetRecordInput> records { get; set; }
    }

    /// <summary>
    /// 批量新增智能表格记录结果项。
    /// </summary>
    public class WeDocSmartSheetAddedRecord
    {
        /// <summary>
        /// 获取或设置新增后的记录 ID。
        /// </summary>
        public string record_id { get; set; }
    }

    /// <summary>
    /// 批量新增智能表格记录结果。
    /// </summary>
    public class WeDocSmartSheetAddRecordsResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置新增后的记录列表。
        /// </summary>
        public IList<WeDocSmartSheetAddedRecord> records { get; set; }
    }

    /// <summary>
    /// 批量更新智能表格记录请求。
    /// </summary>
    public class WeDocSmartSheetUpdateRecordsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置单元格字典的键类型。
        /// </summary>
        public string key_type { get; set; }

        /// <summary>
        /// 获取或设置待更新记录列表。
        /// </summary>
        public IList<WeDocSmartSheetRecordUpdate> records { get; set; }
    }

    /// <summary>
    /// 批量删除智能表格记录请求。
    /// </summary>
    public class WeDocSmartSheetDeleteRecordsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置待删除的记录 ID 列表。
        /// </summary>
        public IList<string> record_ids { get; set; }
    }
}
