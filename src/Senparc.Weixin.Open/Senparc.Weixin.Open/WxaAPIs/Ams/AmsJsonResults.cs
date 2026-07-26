#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：AmsJsonResults.cs
    文件功能描述：AmsJsonResults 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.Ams
{
    /// <summary>
    /// 流量主代运营接口通用返回结果。
    /// </summary>
    /// <remarks>
    /// 流量主接口同时存在顶层 <c>ret/err_msg</c> 和嵌套 <c>base_resp</c> 两种官方返回结构，
    /// 此类型兼容两种结构并统一提供 <see cref="BaseJsonResult.ErrorCodeValue"/> 和 <see cref="BaseJsonResult.errmsg"/>。
    /// </remarks>
    public class AmsJsonResult : BaseJsonResult
    {
        /// <summary>
        /// 顶层业务错误码，0 表示成功。
        /// </summary>
        public int ret { get; set; }

        /// <summary>
        /// 顶层业务错误信息。
        /// </summary>
        public string err_msg { get; set; }

        /// <summary>
        /// 部分接口返回的嵌套基础响应。
        /// </summary>
        public AmsBaseResponse base_resp { get; set; }

        /// <summary>
        /// 获取当前响应的业务错误码，优先使用 <see cref="base_resp"/> 中的错误码。
        /// </summary>
        public override int ErrorCodeValue => base_resp?.ret ?? ret;

        /// <summary>
        /// 获取或设置当前响应的业务错误信息。
        /// </summary>
        public override string errmsg
        {
            get => base_resp?.err_msg ?? err_msg;
            set => err_msg = value;
        }
    }

    /// <summary>
    /// 流量主代运营接口嵌套基础响应。
    /// </summary>
    public class AmsBaseResponse
    {
        /// <summary>
        /// 业务错误码，0 表示成功。
        /// </summary>
        public int ret { get; set; }

        /// <summary>
        /// 业务错误信息。
        /// </summary>
        public string err_msg { get; set; }
    }

    /// <summary>
    /// 设置或查询分账比例的请求参数。
    /// </summary>
    public class AmsShareRatioRequest
    {
        /// <summary>
        /// 服务商分账比例，例如 40 表示服务商获得广告收益的 40%。
        /// </summary>
        public decimal share_ratio { get; set; }

        /// <summary>
        /// 设置自定义比例时对应的小程序 AppID；设置默认比例时不传。
        /// </summary>
        public string appid { get; set; }
    }

    /// <summary>
    /// 按小程序查询分账比例的请求参数。
    /// </summary>
    public class AmsAppIdRequest
    {
        /// <summary>
        /// 待查询的小程序或第三方平台 AppID。
        /// </summary>
        public string appid { get; set; }
    }

    /// <summary>
    /// 分账比例查询结果。
    /// </summary>
    public class AmsShareRatioJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 当前生效或已配置的服务商分账比例。
        /// </summary>
        public decimal share_ratio { get; set; }
    }

    /// <summary>
    /// 检测小程序能否开通流量主的结果。
    /// </summary>
    public class AmsPublisherStatusJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 能否开通流量主：0 表示不能，1 表示可以。
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 创建广告单元的请求参数。
    /// </summary>
    public class AmsCreateAdUnitRequest
    {
        /// <summary>
        /// 广告单元名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 广告单元类型，例如 <c>SLOT_ID_WEAPP_BANNER</c>。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 原生模板广告的固定模板类型；与 <see cref="tmpl_id"/> 二选一。
        /// </summary>
        public int? tmpl_type { get; set; }

        /// <summary>
        /// 服务商自定义原生模板 ID；与 <see cref="tmpl_type"/> 二选一。
        /// </summary>
        /// <remarks>官方参数表标为 number，但 HTTPS 示例和查询接口均返回字符串 ID，因此按字符串保留。</remarks>
        public string tmpl_id { get; set; }

        /// <summary>
        /// 激励视频解锁激励时长，当前支持 15 或 30 秒。
        /// </summary>
        public int? unlock_reward_duration { get; set; }
    }

    /// <summary>
    /// 更新广告单元的请求参数。
    /// </summary>
    public class AmsUpdateAdUnitRequest
    {
        /// <summary>
        /// 广告单元名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 广告单元 ID。
        /// </summary>
        public string ad_unit_id { get; set; }

        /// <summary>
        /// 广告单元开关状态：<c>AD_UNIT_STATUS_ON</c> 或 <c>AD_UNIT_STATUS_OFF</c>。
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 原生模板广告的固定模板类型；与 <see cref="tmpl_id"/> 二选一。
        /// </summary>
        public int? tmpl_type { get; set; }

        /// <summary>
        /// 服务商自定义原生模板 ID；与 <see cref="tmpl_type"/> 二选一。
        /// </summary>
        public string tmpl_id { get; set; }

        /// <summary>
        /// 激励视频解锁激励时长，当前支持 15 或 30 秒。
        /// </summary>
        public int? unlock_reward_duration { get; set; }
    }

    /// <summary>
    /// 广告单元 ID 请求参数。
    /// </summary>
    public class AmsAdUnitIdRequest
    {
        /// <summary>
        /// 广告单元 ID。
        /// </summary>
        public string ad_unit_id { get; set; }
    }

    /// <summary>
    /// 创建广告单元的结果。
    /// </summary>
    public class AmsCreateAdUnitJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 新创建的广告单元 ID。
        /// </summary>
        public string ad_unit_id { get; set; }
    }

    /// <summary>
    /// 获取原生模板广告模板类型的结果。
    /// </summary>
    public class AmsTemplateTypeJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 原生模板类型。微信当前以字符串返回该字段。
        /// </summary>
        public string tmpl_type { get; set; }
    }

    /// <summary>
    /// 查询服务商原生模板及绑定关系的请求参数。
    /// </summary>
    public class AmsAgencyTemplateListRequest
    {
        /// <summary>
        /// 页码，从 1 开始。
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 每页返回的数据条数。
        /// </summary>
        public int page_size { get; set; }

        /// <summary>
        /// 广告位类型名称，例如 <c>SLOT_ID_WEAPP_TEMPLATE</c>。
        /// </summary>
        public string ad_slot { get; set; }

        /// <summary>
        /// 指定的服务商自定义模板 ID；不指定时返回模板列表。
        /// </summary>
        public string tmpl_id { get; set; }

        /// <summary>
        /// 是否返回模板绑定的小程序广告单元，1 表示返回。
        /// </summary>
        public int? is_return_tmpl_bind_list { get; set; }
    }

    /// <summary>
    /// 服务商原生模板列表查询结果。
    /// </summary>
    public class AmsAgencyTemplateListJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 服务商原生模板总数。
        /// </summary>
        public int total_num { get; set; }

        /// <summary>
        /// 原生模板及绑定信息列表。
        /// </summary>
        public List<AmsAgencyTemplateItem> ad_unit_list { get; set; }
    }

    /// <summary>
    /// 服务商原生模板及其绑定信息。
    /// </summary>
    public class AmsAgencyTemplateItem
    {
        /// <summary>
        /// 服务商原生模板 ID。
        /// </summary>
        public string tmpl_id { get; set; }

        /// <summary>
        /// 原生模板名称。
        /// </summary>
        public string ad_unit_name { get; set; }

        /// <summary>
        /// 广告位类型名称。官方返回示例使用此字段。
        /// </summary>
        public string ad_slot { get; set; }

        /// <summary>
        /// 广告位类型名称。官方参数表使用此别名。
        /// </summary>
        public string slot_id { get; set; }

        /// <summary>
        /// 绑定到该模板的小程序广告单元数量。
        /// </summary>
        public int tmpl_bind_total_num { get; set; }

        /// <summary>
        /// 绑定到该模板的小程序广告单元列表。
        /// </summary>
        public List<AmsAdUnitItem> tmpl_bind_list { get; set; }
    }

    /// <summary>
    /// 设置封面广告位开关状态的请求参数。
    /// </summary>
    public class AmsCoverStatusRequest
    {
        /// <summary>
        /// 封面广告位状态：1 表示开启，4 表示关闭。
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 设置封面广告位场景值的请求参数。
    /// </summary>
    public class AmsCoverSceneRequest
    {
        /// <summary>
        /// 以英文逗号分隔的封面广告场景值。
        /// </summary>
        public string cover_scene_list { get; set; }
    }

    /// <summary>
    /// 封面广告位状态查询结果。
    /// </summary>
    public class AmsCoverStatusJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 下次允许开启封面广告位的 Unix 时间戳。
        /// </summary>
        public long next_open_time { get; set; }

        /// <summary>
        /// 封面广告位开关状态。
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 封面广告位场景设置查询结果。
    /// </summary>
    public class AmsCoverSceneJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 以英文逗号分隔的封面广告场景值。
        /// </summary>
        public string scene_list { get; set; }
    }

    /// <summary>
    /// 查询广告位或广告单元的请求参数。
    /// </summary>
    public class AmsAdUnitListRequest
    {
        /// <summary>
        /// 页码，从 1 开始。
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 每页返回的数据条数。
        /// </summary>
        public int page_size { get; set; }

        /// <summary>
        /// 广告位类型名称；不传时不按类型筛选。
        /// </summary>
        public string ad_slot { get; set; }

        /// <summary>
        /// 广告单元 ID；不传时不按广告单元筛选。
        /// </summary>
        public string ad_unit_id { get; set; }
    }

    /// <summary>
    /// 广告位或广告单元查询结果。
    /// </summary>
    public class AmsAdUnitListJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 广告单元列表。
        /// </summary>
        public List<AmsAdUnitItem> ad_unit { get; set; }

        /// <summary>
        /// 符合条件的广告单元总数。
        /// </summary>
        public int total_num { get; set; }
    }

    /// <summary>
    /// 广告单元信息。
    /// </summary>
    public class AmsAdUnitItem
    {
        /// <summary>
        /// 广告位类型名称。
        /// </summary>
        public string ad_slot { get; set; }

        /// <summary>
        /// 广告单元 ID。
        /// </summary>
        public string ad_unit_id { get; set; }

        /// <summary>
        /// 广告单元名称。
        /// </summary>
        public string ad_unit_name { get; set; }

        /// <summary>
        /// 广告单元尺寸列表。官方参数表标为 object，但 HTTPS 示例返回数组。
        /// </summary>
        public List<AmsAdUnitSize> ad_unit_size { get; set; }

        /// <summary>
        /// 广告单元开关状态：1 表示开启，2 表示关闭。
        /// </summary>
        public int ad_unit_status { get; set; }

        /// <summary>
        /// 广告单元类型。
        /// </summary>
        public string ad_unit_type { get; set; }

        /// <summary>
        /// 广告单元所属小程序 AppID。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 广告展示最大时长（秒）。
        /// </summary>
        public int? video_duration_max { get; set; }

        /// <summary>
        /// 广告展示最小时长（秒）。
        /// </summary>
        public int? video_duration_min { get; set; }
    }

    /// <summary>
    /// 广告单元尺寸。
    /// </summary>
    public class AmsAdUnitSize
    {
        /// <summary>
        /// 广告单元宽度。
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// 广告单元高度。
        /// </summary>
        public int height { get; set; }
    }

    /// <summary>
    /// 获取广告单元代码的结果。
    /// </summary>
    public class AmsAdUnitCodeJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 广告单元对应的小程序组件代码。
        /// </summary>
        public string code { get; set; }
    }

    /// <summary>
    /// 设置屏蔽广告主的请求参数。
    /// </summary>
    public class AmsSetBlackListRequest
    {
        /// <summary>
        /// 操作类型：1 表示设置屏蔽，2 表示删除屏蔽。
        /// </summary>
        public int op { get; set; }

        /// <summary>
        /// 屏蔽项数组序列化后的 JSON 字符串，不能直接传 JSON 数组。
        /// </summary>
        public string list { get; set; }
    }

    /// <summary>
    /// 广告主屏蔽操作项。
    /// </summary>
    public class AmsBlackListOperationItem
    {
        /// <summary>
        /// 屏蔽类型：1 公众号、2 iOS 应用、3 Android 应用、4 小程序或小游戏。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 对应类型的广告主标识。
        /// </summary>
        public string id { get; set; }
    }

    /// <summary>
    /// 已屏蔽广告主查询结果。
    /// </summary>
    public class AmsBlackListJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 已屏蔽的公众号广告主。
        /// </summary>
        public List<AmsBlackListItem> blacklist_biz { get; set; }

        /// <summary>
        /// 已屏蔽的小程序或小游戏广告主。
        /// </summary>
        public List<AmsBlackListItem> blacklist_weapp { get; set; }

        /// <summary>
        /// 已屏蔽的 iOS 应用广告主。
        /// </summary>
        public List<AmsBlackListItem> blacklist_ios { get; set; }

        /// <summary>
        /// 已屏蔽的 Android 应用广告主。
        /// </summary>
        public List<AmsBlackListItem> blacklist_android { get; set; }
    }

    /// <summary>
    /// 已屏蔽广告主信息。
    /// </summary>
    public class AmsBlackListItem
    {
        /// <summary>
        /// 广告主标识。
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 广告主名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 广告主头像或标识图片地址。官方 HTTPS 示例使用此字段。
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 广告主头像或标识图片地址。官方参数表使用此别名。
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 设置行业屏蔽信息的请求参数。
    /// </summary>
    public class AmsCategoryBlackListRequest
    {
        /// <summary>
        /// 以竖线分隔的行业枚举值，例如 <c>CHESS|INSURANCE</c>。
        /// </summary>
        public string ams_category { get; set; }
    }

    /// <summary>
    /// 行业屏蔽信息查询结果。
    /// </summary>
    public class AmsCategoryBlackListJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 以竖线分隔的已屏蔽行业枚举值。
        /// </summary>
        public string ams_category { get; set; }
    }

    /// <summary>
    /// 广告汇总数据查询参数。
    /// </summary>
    public class AmsAdDataRequest
    {
        /// <summary>
        /// 页码，从 1 开始。
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 每页返回的数据条数。
        /// </summary>
        public int page_size { get; set; }

        /// <summary>
        /// 查询开始日期，格式为 yyyy-MM-dd。
        /// </summary>
        public string start_date { get; set; }

        /// <summary>
        /// 查询结束日期，格式为 yyyy-MM-dd。
        /// </summary>
        public string end_date { get; set; }

        /// <summary>
        /// 广告位类型名称；不传时查询全部广告位类型。
        /// </summary>
        public string ad_slot { get; set; }
    }

    /// <summary>
    /// 小程序广告单元细分数据查询参数。
    /// </summary>
    public class AmsAdDetailDataRequest : AmsAdDataRequest
    {
        /// <summary>
        /// 广告单元 ID；不传时不按广告单元筛选。
        /// </summary>
        public string ad_unit_id { get; set; }
    }

    /// <summary>
    /// 小程序广告汇总数据查询结果。
    /// </summary>
    public class AmsAdPositionGeneralJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 按日期和广告位拆分的数据列表。
        /// </summary>
        public List<AmsAdStatItem> list { get; set; }

        /// <summary>
        /// 查询区间汇总数据。官方参数表标为数组，HTTPS 示例返回单个对象。
        /// </summary>
        public AmsAdStatSummary summary { get; set; }

        /// <summary>
        /// 数据总条数。
        /// </summary>
        public int total_num { get; set; }
    }

    /// <summary>
    /// 小程序广告单元细分数据查询结果。
    /// </summary>
    public class AmsAdPositionDetailJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 广告单元细分数据列表。
        /// </summary>
        public List<AmsAdUnitStatItem> list { get; set; }

        /// <summary>
        /// 数据总条数。
        /// </summary>
        public int total_num { get; set; }
    }

    /// <summary>
    /// 服务商广告汇总数据查询结果。
    /// </summary>
    public class AmsAgencyAdsStatJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 按小程序、日期和广告位拆分的数据列表。
        /// </summary>
        public List<AmsAdStatItem> list { get; set; }

        /// <summary>
        /// 查询区间汇总数据。
        /// </summary>
        public AmsAdStatSummary summary { get; set; }

        /// <summary>
        /// 数据总条数。
        /// </summary>
        public int total_num { get; set; }
    }

    /// <summary>
    /// 服务商广告单元明细数据查询结果。
    /// </summary>
    public class AmsAgencyAdsDetailJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 服务商所代运营小程序的广告单元明细列表。
        /// </summary>
        public List<AmsAdUnitStatItem> list { get; set; }

        /// <summary>
        /// 数据总条数。
        /// </summary>
        public int total_num { get; set; }
    }

    /// <summary>
    /// 广告数据统计项。
    /// </summary>
    public class AmsAdStatItem
    {
        /// <summary>
        /// 广告位类型数字 ID。
        /// </summary>
        public long slot_id { get; set; }

        /// <summary>
        /// 广告位类型名称。
        /// </summary>
        public string ad_slot { get; set; }

        /// <summary>
        /// 数据日期。
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 广告拉取成功数。
        /// </summary>
        public long req_succ_count { get; set; }

        /// <summary>
        /// 广告曝光量。
        /// </summary>
        public long exposure_count { get; set; }

        /// <summary>
        /// 广告曝光率。
        /// </summary>
        public decimal exposure_rate { get; set; }

        /// <summary>
        /// 广告点击量。
        /// </summary>
        public long click_count { get; set; }

        /// <summary>
        /// 广告点击率。
        /// </summary>
        public decimal click_rate { get; set; }

        /// <summary>
        /// 广告总收入，单位为分。
        /// </summary>
        public long income { get; set; }

        /// <summary>
        /// 小程序分账后收入，单位为分。
        /// </summary>
        public long publisher_income { get; set; }

        /// <summary>
        /// 服务商分账后收入，单位为分。
        /// </summary>
        public long agency_income { get; set; }

        /// <summary>
        /// 广告千次曝光收益，单位为分。
        /// </summary>
        public decimal ecpm { get; set; }

        /// <summary>
        /// 数据所属小程序 AppID。
        /// </summary>
        public string publisher_appid { get; set; }
    }

    /// <summary>
    /// 广告统计汇总数据。
    /// </summary>
    public class AmsAdStatSummary : AmsAdStatItem
    {
        /// <summary>
        /// 广告曝光用户数。
        /// </summary>
        public long exposure_uv { get; set; }

        /// <summary>
        /// 小程序打开用户数。
        /// </summary>
        public long open_uv { get; set; }
    }

    /// <summary>
    /// 广告单元统计数据。
    /// </summary>
    public class AmsAdUnitStatItem
    {
        /// <summary>
        /// 广告单元 ID。
        /// </summary>
        public string ad_unit_id { get; set; }

        /// <summary>
        /// 广告单元名称。
        /// </summary>
        public string ad_unit_name { get; set; }

        /// <summary>
        /// 广告单元所属小程序 AppID。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 广告单元统计明细。
        /// </summary>
        public AmsAdStatItem stat_item { get; set; }
    }

    /// <summary>
    /// 结算收入数据查询参数。
    /// </summary>
    public class AmsSettlementRequest
    {
        /// <summary>
        /// 页码，从 1 开始。
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 每页返回的数据条数。
        /// </summary>
        public int page_size { get; set; }

        /// <summary>
        /// 查询开始日期，格式为 yyyy-MM-dd。
        /// </summary>
        public string start_date { get; set; }

        /// <summary>
        /// 查询结束日期，格式为 yyyy-MM-dd。
        /// </summary>
        public string end_date { get; set; }
    }

    /// <summary>
    /// 小程序或服务商结算收入查询结果。
    /// </summary>
    public class AmsSettlementJsonResult : AmsJsonResult
    {
        /// <summary>
        /// 结算主体名称。
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 累计收入，单位为分。
        /// </summary>
        public long revenue_all { get; set; }

        /// <summary>
        /// 累计扣除金额，单位为分。
        /// </summary>
        public long penalty_all { get; set; }

        /// <summary>
        /// 累计已结算金额，单位为分。
        /// </summary>
        public long settled_revenue_all { get; set; }

        /// <summary>
        /// 结算区间列表。
        /// </summary>
        public List<AmsSettlementItem> settlement_list { get; set; }

        /// <summary>
        /// 结算区间总条数。
        /// </summary>
        public int total_num { get; set; }
    }

    /// <summary>
    /// 单个结算区间信息。
    /// </summary>
    public class AmsSettlementItem
    {
        /// <summary>
        /// 数据更新时间。
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 结算日期区间说明。
        /// </summary>
        public string zone { get; set; }

        /// <summary>
        /// 收入月份，格式为 yyyyMM。
        /// </summary>
        public string month { get; set; }

        /// <summary>
        /// 半月序号：1 表示上半月，2 表示下半月。
        /// </summary>
        public int order { get; set; }

        /// <summary>
        /// 结算状态：1 结算中，2/3 已结算，4 付款中，5 已付款。
        /// </summary>
        public int sett_status { get; set; }

        /// <summary>
        /// 区间内结算收入，单位为分。
        /// </summary>
        public long settled_revenue { get; set; }

        /// <summary>
        /// 结算单编号。
        /// </summary>
        public string sett_no { get; set; }

        /// <summary>
        /// 申请补发结算单次数。
        /// </summary>
        public string mail_send_cnt { get; set; }

        /// <summary>
        /// 各广告位的结算收入列表。
        /// </summary>
        public List<AmsSlotRevenueItem> slot_revenue { get; set; }
    }

    /// <summary>
    /// 广告位结算收入信息。
    /// </summary>
    public class AmsSlotRevenueItem
    {
        /// <summary>
        /// 产生收入的广告位类型名称。
        /// </summary>
        public string slot_id { get; set; }

        /// <summary>
        /// 该广告位的结算金额，单位为分。
        /// </summary>
        public long slot_settled_revenue { get; set; }
    }
}
