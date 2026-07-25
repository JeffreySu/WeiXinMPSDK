/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：HumanResourcesJson.cs
    文件功能描述：企业微信人事助手花名册强类型模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐人事助手花名册强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.HumanResources
{
    /// <summary>花名册选项配置。</summary>
    public class StaffFieldOption
    {
        /// <summary>选项枚举值。</summary>
        public long id { get; set; }

        /// <summary>选项文本。</summary>
        public string value { get; set; }
    }

    /// <summary>花名册字段配置。</summary>
    public class StaffFieldDefinition
    {
        /// <summary>字段 ID；对应官方返回示例使用的名称。</summary>
        public long fieldid { get; set; }

        /// <summary>字段 ID；兼容官方参数表使用的名称。</summary>
        public long field_id { get; set; }

        /// <summary>字段名称。</summary>
        public string field_name { get; set; }

        /// <summary>字段类型：1 文本、2 选项、3 时间、4 图片、5 单文件、6 多文件。</summary>
        public int field_type { get; set; }

        /// <summary>字段值类型：1 字符串、2 uint64、3 uint32、4 int64、5 电话、6 文件。</summary>
        public int value_type { get; set; }

        /// <summary>字段是否必填。</summary>
        public bool is_must { get; set; }

        /// <summary>选项字段的枚举值与文本。</summary>
        public IList<StaffFieldOption> option_list { get; set; }
    }

    /// <summary>花名册字段组配置。</summary>
    public class StaffFieldGroup
    {
        /// <summary>字段组 ID。</summary>
        public long group_id { get; set; }

        /// <summary>字段组名称。</summary>
        public string group_name { get; set; }

        /// <summary>字段组内的字段配置。</summary>
        public IList<StaffFieldDefinition> field_list { get; set; }
    }

    /// <summary>获取员工字段配置结果。</summary>
    public class GetStaffFieldsResult : WorkJsonResult
    {
        /// <summary>字段组配置列表。</summary>
        public IList<StaffFieldGroup> group_list { get; set; }
    }

    /// <summary>需要查询的员工字段。</summary>
    public class StaffFieldSelector
    {
        /// <summary>字段 ID。</summary>
        public long fieldid { get; set; }

        /// <summary>可重复字段组中的下标；非重复字段填写 0。</summary>
        public int? sub_idx { get; set; }
    }

    /// <summary>获取员工花名册信息请求。</summary>
    public class GetStaffInfoRequest
    {
        /// <summary>员工 UserId；员工须在应用可见范围内。</summary>
        public string userid { get; set; }

        /// <summary>是否获取全部字段；不填写时默认为否。</summary>
        public bool? get_all { get; set; }

        /// <summary>指定字段列表；get_all 不为 true 时不可为空。</summary>
        public IList<StaffFieldSelector> fieldids { get; set; }
    }

    /// <summary>花名册电话号码值。</summary>
    public class StaffMobileValue
    {
        /// <summary>电话号码区号。</summary>
        public string value_country_code { get; set; }

        /// <summary>兼容官方更新示例中使用的区号字段名。</summary>
        public string value_mobile_country_code { get; set; }

        /// <summary>电话号码。</summary>
        public string value_mobile { get; set; }
    }

    /// <summary>花名册文件值。</summary>
    public class StaffFileValue
    {
        /// <summary>临时素材 MediaId 列表。</summary>
        public IList<string> media_id { get; set; }
    }

    /// <summary>员工花名册字段值。</summary>
    public class StaffFieldValue
    {
        /// <summary>字段 ID。</summary>
        public long fieldid { get; set; }

        /// <summary>可重复字段组中的下标。</summary>
        public int sub_idx { get; set; }

        /// <summary>查询结果：1 成功、2 失败、3 未找到、5 不支持获取。</summary>
        public int result { get; set; }

        /// <summary>字段值类型。</summary>
        public int value_type { get; set; }

        /// <summary>字符串值。</summary>
        public string value_string { get; set; }

        /// <summary>64 位非负整数值。</summary>
        public ulong? value_uint64 { get; set; }

        /// <summary>32 位非负整数值。</summary>
        public uint? value_uint32 { get; set; }

        /// <summary>64 位整数值。</summary>
        public long? value_int64 { get; set; }

        /// <summary>电话号码值。</summary>
        public StaffMobileValue value_mobile { get; set; }

        /// <summary>文件值。</summary>
        public StaffFileValue value_file { get; set; }
    }

    /// <summary>获取员工花名册信息结果。</summary>
    public class GetStaffInfoResult : WorkJsonResult
    {
        /// <summary>查询到的字段值列表。</summary>
        public IList<StaffFieldValue> field_info { get; set; }
    }

    /// <summary>需要更新或插入的员工字段值。</summary>
    public class StaffFieldValueInput
    {
        /// <summary>字段 ID。</summary>
        public long fieldid { get; set; }

        /// <summary>可重复字段组中的下标；非重复字段填写 0。</summary>
        public int? sub_idx { get; set; }

        /// <summary>字符串值。</summary>
        public string value_string { get; set; }

        /// <summary>64 位非负整数值。</summary>
        public ulong? value_uint64 { get; set; }

        /// <summary>32 位非负整数值。</summary>
        public uint? value_uint32 { get; set; }

        /// <summary>64 位整数值。</summary>
        public long? value_int64 { get; set; }

        /// <summary>电话号码值。</summary>
        public StaffMobileValue value_mobile { get; set; }
    }

    /// <summary>需要删除的可重复字段组。</summary>
    public class StaffGroupRemoveItem
    {
        /// <summary>字段组类型：1 教育、2 工作、3 家庭、4 紧急联系人、5 合同。</summary>
        public int group_type { get; set; }

        /// <summary>需要删除的字段组下标。</summary>
        public int sub_idx { get; set; }
    }

    /// <summary>需要插入的可重复字段组。</summary>
    public class StaffGroupInsertItem
    {
        /// <summary>字段组类型：1 教育、2 工作、3 家庭、4 紧急联系人、5 合同。</summary>
        public int group_type { get; set; }

        /// <summary>需要插入的一组字段值。</summary>
        public IList<StaffFieldValueInput> item { get; set; }
    }

    /// <summary>更新员工花名册信息请求。</summary>
    public class UpdateStaffInfoRequest
    {
        /// <summary>员工 UserId；员工须在应用可见范围内。</summary>
        public string userid { get; set; }

        /// <summary>需要更新、增加或清空的单个字段。</summary>
        public IList<StaffFieldValueInput> update_items { get; set; }

        /// <summary>需要整组删除的可重复字段组。</summary>
        public IList<StaffGroupRemoveItem> remove_items { get; set; }

        /// <summary>需要增加的可重复字段组。</summary>
        public IList<StaffGroupInsertItem> insert_items { get; set; }
    }

    /// <summary>单个字段更新结果。</summary>
    public class StaffFieldUpdateResult
    {
        /// <summary>字段 ID。</summary>
        public long fieldid { get; set; }

        /// <summary>字段下标。</summary>
        public int sub_idx { get; set; }

        /// <summary>结果：1 成功、2 失败、3 未找到、4 必填为空、5 不支持更新。</summary>
        public int result { get; set; }
    }

    /// <summary>重复字段组删除结果。</summary>
    public class StaffGroupRemoveResult
    {
        /// <summary>字段组类型。</summary>
        public int group_type { get; set; }

        /// <summary>字段组下标。</summary>
        public int sub_idx { get; set; }

        /// <summary>操作结果。</summary>
        public int result { get; set; }
    }

    /// <summary>重复字段组插入结果。</summary>
    public class StaffGroupInsertResult
    {
        /// <summary>字段组类型。</summary>
        public int group_type { get; set; }

        /// <summary>输入列表中的下标。</summary>
        public int idx { get; set; }

        /// <summary>操作结果。</summary>
        public int result { get; set; }
    }

    /// <summary>更新员工花名册信息结果。</summary>
    public class UpdateStaffInfoResult : WorkJsonResult
    {
        /// <summary>字段更新结果。</summary>
        public IList<StaffFieldUpdateResult> update_results { get; set; }

        /// <summary>字段组删除结果。</summary>
        public IList<StaffGroupRemoveResult> remove_results { get; set; }

        /// <summary>字段组插入结果；对应官方参数表使用的名称。</summary>
        public IList<StaffGroupInsertResult> insert_results { get; set; }

        /// <summary>字段组插入结果；兼容官方返回示例使用的名称。</summary>
        public IList<StaffGroupInsertResult> insert_result { get; set; }
    }
}
