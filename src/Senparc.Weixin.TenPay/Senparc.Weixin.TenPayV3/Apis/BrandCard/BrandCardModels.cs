#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BrandCardModels.cs
    文件功能描述：微信支付 V3 服务商商家名片强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐商家名片配置与交易连接名片模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BrandCard
{
    /// <summary>
    /// 商家名片配置申请数据。
    /// </summary>
    public class BrandCardConfigRequestData
    {
        /// <summary>
        /// 服务商自定义的业务申请编号，仅支持数字、字母和下划线。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 商家进驻微信支付品牌商家后获得的品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 商家名片头部跳转的品牌小程序信息；不配置头部跳转时可不填。
        /// </summary>
        public BrandCardMiniProgramInfo brand_mini_program_info { get; set; }

        /// <summary>
        /// 商家名片中展示的品牌客服信息。
        /// </summary>
        public BrandCardCustomerServiceInfo brand_customer_service { get; set; }

        /// <summary>
        /// 品牌服务列表；配置时至少两项、最多十五项，最多三个分类且每类最多五项。
        /// </summary>
        public BrandCardServiceInfo[] service_list { get; set; }
    }

    /// <summary>
    /// 商家名片头部跳转的小程序信息。
    /// </summary>
    public class BrandCardMiniProgramInfo
    {
        /// <summary>
        /// 已认证的小程序 AppID。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 小程序默认跳转路径；不填时跳转小程序首页。
        /// </summary>
        public string default_jump_path { get; set; }

        /// <summary>
        /// 小程序跳转按钮文案；不填时默认显示“前往小程序”。
        /// </summary>
        public string button_text { get; set; }
    }

    /// <summary>
    /// 商家名片品牌客服信息。
    /// </summary>
    public class BrandCardCustomerServiceInfo
    {
        /// <summary>
        /// 客服类型：MINI_PROGRAM、WECOM、CUSTOMIZE_WEB、CUSTOMIZE_MP 或 SERVICE_PHONE。
        /// </summary>
        public string customer_service_type { get; set; }

        /// <summary>
        /// 品牌官方客服电话；客服类型为 SERVICE_PHONE 时必填。
        /// </summary>
        public string customer_service_phone { get; set; }

        /// <summary>
        /// 客服页面路径；WECOM、CUSTOMIZE_WEB 和 CUSTOMIZE_MP 类型时必填。
        /// </summary>
        public string customer_service_path { get; set; }

        /// <summary>
        /// 已认证的小程序 AppID；MINI_PROGRAM 和 CUSTOMIZE_MP 类型时必填。
        /// </summary>
        public string appid { get; set; }
    }

    /// <summary>
    /// 商家名片服务列表项。
    /// </summary>
    public class BrandCardServiceInfo
    {
        /// <summary>
        /// 服务分类名称，最多六个字符；整个列表最多三个分类。
        /// </summary>
        public string service_classify_name { get; set; }

        /// <summary>
        /// 服务名称，最多八个字符。
        /// </summary>
        public string service_name { get; set; }

        /// <summary>
        /// 服务跳转类型：JUMP_MINI_PROGRAM 或 JUMP_WEB_PAGE。
        /// </summary>
        public string service_jump_type { get; set; }

        /// <summary>
        /// 小程序页面路径或使用 HTTPS 协议的完整网页地址。
        /// </summary>
        public string service_jump_path { get; set; }

        /// <summary>
        /// 已认证的小程序 AppID；跳转类型为 JUMP_MINI_PROGRAM 时必填。
        /// </summary>
        public string appid { get; set; }
    }

    /// <summary>
    /// 商家名片配置发布申请数据。
    /// </summary>
    public class BrandCardConfigPublishRequestData
    {
        /// <summary>
        /// 业务申请编号，与微信支付申请单号二选一。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 微信支付申请单号，与业务申请编号二选一。
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 发布方式：IMMEDIATE_PUBLISH 或 SCHEDULED_PUBLISH。
        /// </summary>
        public string publish_type { get; set; }

        /// <summary>
        /// 定时发布时间，遵循 RFC 3339；定时发布时必填，需位于一小时后至九十天内。
        /// </summary>
        public string scheduled_publish_time { get; set; }
    }

    /// <summary>
    /// 商家名片配置申请的撤销、状态查询或预览链接查询条件。
    /// </summary>
    public class BrandCardConfigApplymentRequestData
    {
        /// <summary>
        /// 业务申请编号，与微信支付申请单号二选一。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 微信支付申请单号，与业务申请编号二选一。
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 添加交易连接名片规则的申请数据。
    /// </summary>
    public class BrandCardLinkRequestData
    {
        /// <summary>
        /// 品牌业务申请编号，仅支持数字、字母和下划线；同一品牌下应保持唯一。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 品牌 ID，暂不支持银行品牌。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 交易场景：MINI_PROGRAM、APP、PAYMENT_SCORE 或 PAYMENT_CODE。
        /// </summary>
        public string payment_scene { get; set; }

        /// <summary>
        /// 小程序或 APP 的 AppID；MINI_PROGRAM 和 APP 场景时必填。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 交易连接商户号；付款码支付或收银类平台小程序支付场景时必填。
        /// </summary>
        public string card_link_mchid { get; set; }

        /// <summary>
        /// 支付分服务 ID；PAYMENT_SCORE 场景时必填。
        /// </summary>
        public string service_id { get; set; }
    }

    /// <summary>
    /// 解除已生效交易连接名片规则的请求数据。
    /// </summary>
    public class BrandCardLinkUnbindRequestData
    {
        /// <summary>
        /// 品牌 ID，暂不支持银行品牌。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 交易场景：MINI_PROGRAM、APP、PAYMENT_SCORE 或 PAYMENT_CODE。
        /// </summary>
        public string payment_scene { get; set; }

        /// <summary>
        /// 小程序或 APP 的 AppID；MINI_PROGRAM 和 APP 场景时必填。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 交易连接商户号；付款码支付或收银类平台小程序支付场景时必填。
        /// </summary>
        public string card_link_mchid { get; set; }

        /// <summary>
        /// 支付分服务 ID；PAYMENT_SCORE 场景时必填。
        /// </summary>
        public string service_id { get; set; }
    }

    /// <summary>
    /// 撤销交易连接名片配置申请的请求数据。
    /// </summary>
    public class BrandCardLinkCancelRequestData
    {
        /// <summary>
        /// 添加规则时使用的品牌业务申请编号。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 已生效交易连接名片规则的查询条件。
    /// </summary>
    public class BrandCardActiveLinksQueryRequestData
    {
        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 可选交易场景；不填时查询所有场景。
        /// </summary>
        public string payment_scene { get; set; }

        /// <summary>
        /// 查询页码，从 1 开始；不填时返回第一页。
        /// </summary>
        public int? page_index { get; set; }

        /// <summary>
        /// 单页条数，不填时为 20，最大为 50。
        /// </summary>
        public int? page_size { get; set; }
    }

    /// <summary>
    /// 提交商家名片配置申请的返回结果。
    /// </summary>
    public class BrandCardConfigSubmitResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 业务申请编号。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 微信支付生成的申请单号。
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 商家名片预览二维码链接。
        /// </summary>
        public string card_preview_url { get; set; }

        /// <summary>
        /// 预览二维码链接过期时间，遵循 RFC 3339。
        /// </summary>
        public string url_expired_time { get; set; }
    }

    /// <summary>
    /// 发布商家名片配置的返回结果。
    /// </summary>
    public class BrandCardConfigPublishResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 业务申请编号。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 微信支付生成的申请单号。
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 发布方式：IMMEDIATE_PUBLISH 或 SCHEDULED_PUBLISH。
        /// </summary>
        public string publish_type { get; set; }

        /// <summary>
        /// 定时发布时间，遵循 RFC 3339；定时发布时返回。
        /// </summary>
        public string scheduled_publish_time { get; set; }
    }

    /// <summary>
    /// 商家名片配置申请状态结果。
    /// </summary>
    public class BrandCardConfigApplymentResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 业务申请编号。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 微信支付生成的申请单号。
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 申请状态：STATE_UNKNOWN、DRAFTING、AUDITING、AUDIT_REJECTED、PENDING_PUBLISH、PUBLISHED 或 CANCELED。
        /// </summary>
        public string applyment_state { get; set; }

        /// <summary>
        /// 定时发布时间，遵循 RFC 3339；发布时设置计划时间才返回。
        /// </summary>
        public string scheduled_publish_time { get; set; }

        /// <summary>
        /// 审核驳回原因；申请状态为 AUDIT_REJECTED 时返回。
        /// </summary>
        public string reject_reason { get; set; }

        /// <summary>
        /// 实际发布时间，遵循 RFC 3339；申请状态为 PUBLISHED 时返回。
        /// </summary>
        public string actual_publish_time { get; set; }
    }

    /// <summary>
    /// 商家名片预览二维码查询结果。
    /// </summary>
    public class BrandCardConfigPreviewResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 业务申请编号。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 微信支付生成的申请单号。
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 商家名片预览二维码链接。
        /// </summary>
        public string card_preview_url { get; set; }

        /// <summary>
        /// 预览二维码链接过期时间，遵循 RFC 3339。
        /// </summary>
        public string url_expired_time { get; set; }
    }

    /// <summary>
    /// 添加或解除交易连接名片规则的返回结果。
    /// </summary>
    public class BrandCardLinkResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 品牌业务申请编号；添加规则时返回。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 交易场景：MINI_PROGRAM、APP、PAYMENT_SCORE 或 PAYMENT_CODE。
        /// </summary>
        public string payment_scene { get; set; }

        /// <summary>
        /// 小程序或 APP 场景对应的 AppID。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 付款码或收银类平台小程序场景对应的商户号。
        /// </summary>
        public string card_link_mchid { get; set; }

        /// <summary>
        /// 支付分场景对应的服务 ID。
        /// </summary>
        public string service_id { get; set; }
    }

    /// <summary>
    /// 撤销交易连接名片配置申请的返回结果。
    /// </summary>
    public class BrandCardLinkCancelResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 品牌业务申请编号。
        /// </summary>
        public string business_code { get; set; }

        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 已生效交易连接名片规则列表查询结果。
    /// </summary>
    public class BrandCardActiveLinksResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 符合查询条件的已生效规则总数。
        /// </summary>
        public int total_num { get; set; }

        /// <summary>
        /// 已生效交易连接名片规则列表。
        /// </summary>
        public BrandCardActiveLinkInfo[] active_link_list { get; set; }

        /// <summary>
        /// 当前查询页码。
        /// </summary>
        public int? page_index { get; set; }

        /// <summary>
        /// 当前单页条数。
        /// </summary>
        public int? page_size { get; set; }
    }

    /// <summary>
    /// 单个已生效交易连接名片规则。
    /// </summary>
    public class BrandCardActiveLinkInfo
    {
        /// <summary>
        /// 交易场景：MINI_PROGRAM、APP、PAYMENT_SCORE 或 PAYMENT_CODE。
        /// </summary>
        public string payment_scene { get; set; }

        /// <summary>
        /// 小程序或 APP 场景对应的 AppID 列表。
        /// </summary>
        public string[] appid_list { get; set; }

        /// <summary>
        /// 付款码或收银类平台小程序场景对应的商户号。
        /// </summary>
        public string card_link_mchid { get; set; }

        /// <summary>
        /// 支付分场景对应的服务 ID。
        /// </summary>
        public string service_id { get; set; }
    }

    /// <summary>
    /// 交易连接名片添加申请状态查询结果。
    /// </summary>
    public class BrandCardLinkApplymentResultJson : BrandCardLinkResultJson
    {
        /// <summary>
        /// 配置状态：STATE_UNKNOWN、WAITING_AUDIT、AUDIT_REJECT、WAITING_CONFIRMATION、MERCHANT_ADMIN_REJECT、IN_EFFECT 或 CANCELED。
        /// </summary>
        public string configuration_state { get; set; }

        /// <summary>
        /// 平台审核或商户号超管驳回原因。
        /// </summary>
        public string reject_reason { get; set; }
    }
}
