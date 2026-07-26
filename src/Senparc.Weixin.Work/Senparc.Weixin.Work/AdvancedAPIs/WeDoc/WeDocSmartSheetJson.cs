/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocSmartSheetJson.cs
    文件功能描述：企业微信智能表格权限和工作表强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 按官方协议拆分智能表格权限和工作表请求响应模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Text.Json;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>
    /// 智能表格文档级请求基类。
    /// </summary>
    public abstract class WeDocSmartSheetDocumentRequest
    {
        /// <summary>
        /// 获取或设置智能表格文档 DocID。
        /// </summary>
        public string docid { get; set; }
    }

    /// <summary>
    /// 智能表格工作表级请求基类。
    /// </summary>
    public abstract class WeDocSmartSheetSheetRequest : WeDocSmartSheetDocumentRequest
    {
        /// <summary>
        /// 获取或设置工作表 ID。
        /// </summary>
        public string sheet_id { get; set; }
    }

    /// <summary>
    /// 获取智能表格内容权限请求。
    /// </summary>
    public class WeDocSmartSheetAuthRequest : WeDocSmartSheetDocumentRequest
    {
        /// <summary>
        /// 获取或设置工作表 ID；不指定时由当前文档权限范围决定返回内容。
        /// </summary>
        public string sheet_id { get; set; }
    }

    /// <summary>
    /// 修改智能表格内容权限请求。
    /// </summary>
    public class WeDocSmartSheetModifyAuthRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置权限载荷。权限结构会随字段权限、记录权限及官方后续扩展变化，
        /// 因而保留企业微信原始 JSON 结构，避免丢失未知权限项。
        /// </summary>
        public JsonElement auth_info { get; set; }
    }

    /// <summary>
    /// 获取智能表格内容权限结果。
    /// </summary>
    public class WeDocSmartSheetAuthResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置智能表格文档 DocID。
        /// </summary>
        public string docid { get; set; }

        /// <summary>
        /// 获取或设置工作表 ID。
        /// </summary>
        public string sheet_id { get; set; }

        /// <summary>
        /// 获取或设置通用权限载荷。
        /// </summary>
        public JsonElement? auth_info { get; set; }

        /// <summary>
        /// 获取或设置部分协议版本返回的字段权限载荷。
        /// </summary>
        public JsonElement? field_auth { get; set; }

        /// <summary>
        /// 获取或设置部分协议版本返回的记录权限载荷。
        /// </summary>
        public JsonElement? record_auth { get; set; }
    }

    /// <summary>
    /// 获取智能表格工作表请求。
    /// </summary>
    public class WeDocSmartSheetGetSheetsRequest : WeDocSmartSheetDocumentRequest
    {
        /// <summary>
        /// 获取或设置需要查询的工作表 ID；留空时获取工作表列表。
        /// </summary>
        public string sheet_id { get; set; }
    }

    /// <summary>
    /// 智能表格工作表信息。
    /// </summary>
    public class WeDocSmartSheetProperties
    {
        /// <summary>
        /// 获取或设置工作表 ID。
        /// </summary>
        public string sheet_id { get; set; }

        /// <summary>
        /// 获取或设置工作表标题。
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 获取或设置工作表在文档中的索引。
        /// </summary>
        public int? index { get; set; }

        /// <summary>
        /// 获取或设置工作表类型，例如 <c>smartsheet</c>。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 获取或设置当前调用方是否可见该工作表。
        /// </summary>
        public bool? is_visible { get; set; }
    }

    /// <summary>
    /// 获取智能表格工作表结果。
    /// </summary>
    public class WeDocSmartSheetGetSheetsResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置工作表列表。
        /// </summary>
        public IList<WeDocSmartSheetProperties> sheet_list { get; set; }
    }

    /// <summary>
    /// 新增智能表格工作表时使用的属性。
    /// </summary>
    public class WeDocSmartSheetAddSheetProperties
    {
        /// <summary>
        /// 获取或设置工作表标题。
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 获取或设置新工作表插入后的索引。
        /// </summary>
        public int index { get; set; }
    }

    /// <summary>
    /// 新增智能表格工作表请求。
    /// </summary>
    public class WeDocSmartSheetAddSheetRequest : WeDocSmartSheetDocumentRequest
    {
        /// <summary>
        /// 获取或设置待新增工作表的属性。
        /// </summary>
        public WeDocSmartSheetAddSheetProperties properties { get; set; }
    }

    /// <summary>
    /// 新增智能表格工作表结果。
    /// </summary>
    public class WeDocSmartSheetAddSheetResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置已新增工作表的属性。
        /// </summary>
        public WeDocSmartSheetProperties properties { get; set; }
    }

    /// <summary>
    /// 更新智能表格工作表时使用的属性。
    /// </summary>
    public class WeDocSmartSheetUpdateSheetProperties
    {
        /// <summary>
        /// 获取或设置工作表 ID。
        /// </summary>
        public string sheet_id { get; set; }

        /// <summary>
        /// 获取或设置新的工作表标题。
        /// </summary>
        public string title { get; set; }
    }

    /// <summary>
    /// 更新智能表格工作表请求。
    /// </summary>
    public class WeDocSmartSheetUpdateSheetRequest : WeDocSmartSheetDocumentRequest
    {
        /// <summary>
        /// 获取或设置待更新工作表的属性。
        /// </summary>
        public WeDocSmartSheetUpdateSheetProperties properties { get; set; }
    }

    /// <summary>
    /// 删除智能表格工作表请求。
    /// </summary>
    public class WeDocSmartSheetDeleteSheetRequest : WeDocSmartSheetSheetRequest
    {
    }
}
