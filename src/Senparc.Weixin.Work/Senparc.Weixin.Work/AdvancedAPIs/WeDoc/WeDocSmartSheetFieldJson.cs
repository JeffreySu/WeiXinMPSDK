/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocSmartSheetFieldJson.cs
    文件功能描述：企业微信智能表格字段强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智能表格字段增删改查及字段配置模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>
    /// 获取智能表格字段请求。
    /// </summary>
    public class WeDocSmartSheetGetFieldsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置视图 ID；指定后只返回该视图可见字段。
        /// </summary>
        public string view_id { get; set; }

        /// <summary>
        /// 获取或设置需要查询的字段 ID 列表。
        /// </summary>
        public IList<string> field_ids { get; set; }

        /// <summary>
        /// 获取或设置需要查询的字段标题列表。
        /// </summary>
        public IList<string> field_titles { get; set; }

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
    /// 文本字段配置。文本字段当前没有额外配置项。
    /// </summary>
    public class WeDocSmartSheetTextFieldProperty
    {
    }

    /// <summary>
    /// 数字字段配置。
    /// </summary>
    public class WeDocSmartSheetNumberFieldProperty
    {
        /// <summary>
        /// 获取或设置小数位数。
        /// </summary>
        public int? decimal_places { get; set; }

        /// <summary>
        /// 获取或设置是否使用千位分隔符。
        /// </summary>
        public bool? use_separate { get; set; }
    }

    /// <summary>
    /// 复选框字段配置。
    /// </summary>
    public class WeDocSmartSheetCheckboxFieldProperty
    {
        /// <summary>
        /// 获取或设置新增记录时是否默认勾选。
        /// </summary>
        public bool? @checked { get; set; }
    }

    /// <summary>
    /// 日期时间字段配置。
    /// </summary>
    public class WeDocSmartSheetDateTimeFieldProperty
    {
        /// <summary>
        /// 获取或设置日期显示格式。
        /// </summary>
        public string format { get; set; }

        /// <summary>
        /// 获取或设置新增记录时是否自动填入当前时间。
        /// </summary>
        public bool? auto_fill { get; set; }
    }

    /// <summary>
    /// 附件字段配置。
    /// </summary>
    public class WeDocSmartSheetAttachmentFieldProperty
    {
        /// <summary>
        /// 获取或设置附件显示模式。
        /// </summary>
        public string display_mode { get; set; }
    }

    /// <summary>
    /// 成员字段配置。
    /// </summary>
    public class WeDocSmartSheetUserFieldProperty
    {
        /// <summary>
        /// 获取或设置是否允许选择多个成员。
        /// </summary>
        public bool? is_multiple { get; set; }

        /// <summary>
        /// 获取或设置成员值变化时是否通知成员。
        /// </summary>
        public bool? is_notified { get; set; }
    }

    /// <summary>
    /// 超链接字段配置。
    /// </summary>
    public class WeDocSmartSheetUrlFieldProperty
    {
        /// <summary>
        /// 获取或设置超链接展示类型。
        /// </summary>
        public string type { get; set; }
    }

    /// <summary>
    /// 选择字段的候选项。
    /// </summary>
    public class WeDocSmartSheetFieldOption
    {
        /// <summary>
        /// 获取或设置选项 ID。
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 获取或设置选项文本。
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 获取或设置选项颜色样式编号。
        /// </summary>
        public int? style { get; set; }
    }

    /// <summary>
    /// 多选字段配置。
    /// </summary>
    public class WeDocSmartSheetSelectFieldProperty
    {
        /// <summary>
        /// 获取或设置填写记录时是否允许快速新增选项。
        /// </summary>
        public bool? is_quick_add { get; set; }

        /// <summary>
        /// 获取或设置候选项列表。
        /// </summary>
        public IList<WeDocSmartSheetFieldOption> options { get; set; }
    }

    /// <summary>
    /// 单选字段配置。
    /// </summary>
    public class WeDocSmartSheetSingleSelectFieldProperty : WeDocSmartSheetSelectFieldProperty
    {
    }

    /// <summary>
    /// 创建时间或最后编辑时间字段配置。
    /// </summary>
    public class WeDocSmartSheetSystemTimeFieldProperty
    {
        /// <summary>
        /// 获取或设置日期显示格式。
        /// </summary>
        public string format { get; set; }
    }

    /// <summary>
    /// 进度字段配置。
    /// </summary>
    public class WeDocSmartSheetProgressFieldProperty
    {
        /// <summary>
        /// 获取或设置小数位数。
        /// </summary>
        public int? decimal_places { get; set; }
    }

    /// <summary>
    /// 关联字段配置。
    /// </summary>
    public class WeDocSmartSheetReferenceFieldProperty
    {
        /// <summary>
        /// 获取或设置关联视图 ID。
        /// </summary>
        public string view_id { get; set; }

        /// <summary>
        /// 获取或设置关联工作表 ID。
        /// </summary>
        public string sub_id { get; set; }

        /// <summary>
        /// 获取或设置关联字段 ID。
        /// </summary>
        public string field_id { get; set; }

        /// <summary>
        /// 获取或设置是否允许关联多条记录。
        /// </summary>
        public bool? is_multiple { get; set; }
    }

    /// <summary>
    /// 地理位置字段配置。
    /// </summary>
    public class WeDocSmartSheetLocationFieldProperty
    {
        /// <summary>
        /// 获取或设置位置录入类型。
        /// </summary>
        public string input_type { get; set; }
    }

    /// <summary>
    /// 自动编号规则项。
    /// </summary>
    public class WeDocSmartSheetAutoNumberRule
    {
        /// <summary>
        /// 获取或设置规则类型。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 获取或设置规则值。
        /// </summary>
        public string value { get; set; }
    }

    /// <summary>
    /// 自动编号字段配置。
    /// </summary>
    public class WeDocSmartSheetAutoNumberFieldProperty
    {
        /// <summary>
        /// 获取或设置编号录入类型。
        /// </summary>
        public string input_type { get; set; }

        /// <summary>
        /// 获取或设置自定义编号规则列表。
        /// </summary>
        public IList<WeDocSmartSheetAutoNumberRule> rules { get; set; }

        /// <summary>
        /// 获取或设置是否重新格式化已有记录的编号。
        /// </summary>
        public bool? reformat_existing_record { get; set; }
    }

    /// <summary>
    /// 货币字段配置。
    /// </summary>
    public class WeDocSmartSheetCurrencyFieldProperty
    {
        /// <summary>
        /// 获取或设置货币类型。
        /// </summary>
        public string currency_type { get; set; }

        /// <summary>
        /// 获取或设置小数位数。
        /// </summary>
        public int? decimal_places { get; set; }

        /// <summary>
        /// 获取或设置是否使用千位分隔符。
        /// </summary>
        public bool? use_separate { get; set; }
    }

    /// <summary>
    /// 企业微信群字段配置。
    /// </summary>
    public class WeDocSmartSheetGroupChatFieldProperty
    {
        /// <summary>
        /// 获取或设置是否允许选择多个群聊。
        /// </summary>
        public bool? allow_multiple { get; set; }
    }

    /// <summary>
    /// 百分数字段配置。
    /// </summary>
    public class WeDocSmartSheetPercentageFieldProperty
    {
        /// <summary>
        /// 获取或设置小数位数。
        /// </summary>
        public int? decimal_places { get; set; }

        /// <summary>
        /// 获取或设置是否使用千位分隔符。
        /// </summary>
        public bool? use_separate { get; set; }
    }

    /// <summary>
    /// 条码字段配置。
    /// </summary>
    public class WeDocSmartSheetBarcodeFieldProperty
    {
        /// <summary>
        /// 获取或设置是否仅允许通过手机扫码录入。
        /// </summary>
        public bool? mobile_scan_only { get; set; }
    }

    /// <summary>
    /// 智能表格字段信息。
    /// </summary>
    public class WeDocSmartSheetField
    {
        /// <summary>
        /// 获取或设置字段 ID；新增字段时无需填写。
        /// </summary>
        public string field_id { get; set; }

        /// <summary>
        /// 获取或设置字段标题。
        /// </summary>
        public string field_title { get; set; }

        /// <summary>
        /// 获取或设置字段类型，例如 <c>FIELD_TYPE_TEXT</c>。
        /// </summary>
        public string field_type { get; set; }

        /// <summary>
        /// 获取或设置文本字段配置。
        /// </summary>
        public WeDocSmartSheetTextFieldProperty property_text { get; set; }

        /// <summary>
        /// 获取或设置数字字段配置。
        /// </summary>
        public WeDocSmartSheetNumberFieldProperty property_number { get; set; }

        /// <summary>
        /// 获取或设置复选框字段配置。
        /// </summary>
        public WeDocSmartSheetCheckboxFieldProperty property_checkbox { get; set; }

        /// <summary>
        /// 获取或设置日期时间字段配置。
        /// </summary>
        public WeDocSmartSheetDateTimeFieldProperty property_date_time { get; set; }

        /// <summary>
        /// 获取或设置附件字段配置。
        /// </summary>
        public WeDocSmartSheetAttachmentFieldProperty property_attachment { get; set; }

        /// <summary>
        /// 获取或设置成员字段配置。
        /// </summary>
        public WeDocSmartSheetUserFieldProperty property_user { get; set; }

        /// <summary>
        /// 获取或设置超链接字段配置。
        /// </summary>
        public WeDocSmartSheetUrlFieldProperty property_url { get; set; }

        /// <summary>
        /// 获取或设置多选字段配置。
        /// </summary>
        public WeDocSmartSheetSelectFieldProperty property_select { get; set; }

        /// <summary>
        /// 获取或设置创建时间字段配置。
        /// </summary>
        public WeDocSmartSheetSystemTimeFieldProperty property_created_time { get; set; }

        /// <summary>
        /// 获取或设置最后编辑时间字段配置。
        /// </summary>
        public WeDocSmartSheetSystemTimeFieldProperty property_modified_time { get; set; }

        /// <summary>
        /// 获取或设置进度字段配置。
        /// </summary>
        public WeDocSmartSheetProgressFieldProperty property_progress { get; set; }

        /// <summary>
        /// 获取或设置单选字段配置。
        /// </summary>
        public WeDocSmartSheetSingleSelectFieldProperty property_single_select { get; set; }

        /// <summary>
        /// 获取或设置关联字段配置。
        /// </summary>
        public WeDocSmartSheetReferenceFieldProperty property_reference { get; set; }

        /// <summary>
        /// 获取或设置地理位置字段配置。
        /// </summary>
        public WeDocSmartSheetLocationFieldProperty property_location { get; set; }

        /// <summary>
        /// 获取或设置自动编号字段配置。
        /// </summary>
        public WeDocSmartSheetAutoNumberFieldProperty property_auto_number { get; set; }

        /// <summary>
        /// 获取或设置货币字段配置。
        /// </summary>
        public WeDocSmartSheetCurrencyFieldProperty property_currency { get; set; }

        /// <summary>
        /// 获取或设置企业微信群字段配置。
        /// </summary>
        public WeDocSmartSheetGroupChatFieldProperty property_ww_group { get; set; }

        /// <summary>
        /// 获取或设置百分数字段配置。
        /// </summary>
        public WeDocSmartSheetPercentageFieldProperty property_percentage { get; set; }

        /// <summary>
        /// 获取或设置条码字段配置。
        /// </summary>
        public WeDocSmartSheetBarcodeFieldProperty property_barcode { get; set; }
    }

    /// <summary>
    /// 获取智能表格字段结果。
    /// </summary>
    public class WeDocSmartSheetGetFieldsResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置字段列表。
        /// </summary>
        public IList<WeDocSmartSheetField> fields { get; set; }

        /// <summary>
        /// 获取或设置符合条件的字段总数。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 获取或设置下一页起始偏移量。
        /// </summary>
        public int? next { get; set; }

        /// <summary>
        /// 获取或设置是否还有更多字段。
        /// </summary>
        public bool has_more { get; set; }
    }

    /// <summary>
    /// 批量新增智能表格字段请求。
    /// </summary>
    public class WeDocSmartSheetAddFieldsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置待新增字段列表。
        /// </summary>
        public IList<WeDocSmartSheetField> fields { get; set; }
    }

    /// <summary>
    /// 批量新增智能表格字段结果。
    /// </summary>
    public class WeDocSmartSheetAddFieldsResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置新增后的字段列表。
        /// </summary>
        public IList<WeDocSmartSheetField> fields { get; set; }
    }

    /// <summary>
    /// 批量更新智能表格字段请求。
    /// </summary>
    public class WeDocSmartSheetUpdateFieldsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置待更新字段列表；每项必须包含字段 ID。
        /// </summary>
        public IList<WeDocSmartSheetField> fields { get; set; }
    }

    /// <summary>
    /// 批量删除智能表格字段请求。
    /// </summary>
    public class WeDocSmartSheetDeleteFieldsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置待删除的字段 ID 列表。
        /// </summary>
        public IList<string> field_ids { get; set; }
    }
}
