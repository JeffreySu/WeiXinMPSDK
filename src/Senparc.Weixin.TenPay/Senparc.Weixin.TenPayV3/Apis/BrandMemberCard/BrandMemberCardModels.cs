#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BrandMemberCardModels.cs
    文件功能描述：微信支付商家名片会员卡请求与返回模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v2.5.1 补齐会员卡模板创建、查询、修改和作废模型；补齐用户会员卡管理模型；补齐会员预授权、导入确认、动态、积分和图片上传模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BrandMemberCard
{
    /// <summary>
    /// 创建商家名片会员卡模板的请求数据。
    /// </summary>
    public class BrandMemberCardCreateRequestData
    {
        /// <summary>
        /// 商家请求单号，最长 128 个字符，仅支持数字、字母、连接线和下划线，且应保持唯一。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 商家 AppID，可以是服务号、订阅号、公众号或小程序 AppID，且须与品牌存在 B-A 关系。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 会员卡类型：PURCHASE、NORMAL 或 BALANCE；当前官方仅支持 NORMAL。
        /// </summary>
        public string card_type { get; set; }

        /// <summary>
        /// 卡面名称，最长 10 个中文字符。
        /// </summary>
        public string card_title { get; set; }

        /// <summary>
        /// 卡面背景颜色，使用七位十六进制 RGB 编码，例如 #FFFF00。
        /// </summary>
        public string card_color { get; set; }

        /// <summary>
        /// 通过品牌会员图片上传接口获得的卡面图片 URL，最长 256 个字符。
        /// </summary>
        public string card_picture_url { get; set; }

        /// <summary>
        /// 会员卡 Code 分配方式：SYSTEM_ALLOCATE 或 MERCHANT_ALLOCATE。
        /// </summary>
        public string code_mode { get; set; }

        /// <summary>
        /// 会员码展示类型：NONE_CODE、BAR_CODE、QR_CODE、BAR_CODE_AND_QR_CODE 或 JUMP_MINI_PROGRAM。
        /// </summary>
        public string code_type { get; set; }

        /// <summary>
        /// 会员码跳转小程序信息；code_type 为 JUMP_MINI_PROGRAM 时必填。
        /// </summary>
        public BrandMemberCardJumpInformation code_jump_information { get; set; }

        /// <summary>
        /// 会员权益说明，最长 32 个字符。
        /// </summary>
        public string benefits { get; set; }

        /// <summary>
        /// 接收开卡结果通知的 HTTPS 地址，最长 256 个字符。
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 是否将会员卡置顶展示，默认 false。
        /// </summary>
        public bool? need_pinned { get; set; }

        /// <summary>
        /// 是否在会员卡面展示会员等级，默认 false。
        /// </summary>
        public bool? need_display_level { get; set; }

        /// <summary>
        /// 新用户开卡后的初始会员等级；展示会员等级时必填，最长 10 个字符。
        /// </summary>
        public string init_level { get; set; }

        /// <summary>
        /// 展示在会员卡详情中的服务电话，最长 32 个字符。
        /// </summary>
        public string service_phone { get; set; }

        /// <summary>
        /// 用户与商家之间的纯文本法务协议，最长 20480 个字符。
        /// </summary>
        public string legal_agreement { get; set; }

        /// <summary>
        /// 会员卡有效期配置。
        /// </summary>
        public BrandMemberCardValidDateInformation valid_date_information { get; set; }

        /// <summary>
        /// 用户点击会员卡卡面后进入的会员中心小程序信息。
        /// </summary>
        public BrandMemberCardJumpInformation member_information { get; set; }

        /// <summary>
        /// 会员积分入口小程序信息；为空时不启用积分入口。
        /// </summary>
        public BrandMemberCardJumpInformation points_information { get; set; }

        /// <summary>
        /// 会员储值入口小程序信息；为空时不启用储值入口。
        /// </summary>
        public BrandMemberCardJumpInformation balance_information { get; set; }

        /// <summary>
        /// 付费会员价格和购买入口；为空时不启用付费会员入口。
        /// </summary>
        public BrandMemberCardPurchaseInformation purchase_information { get; set; }

        /// <summary>
        /// 用户开通会员卡时需要填写的通用和自定义信息。
        /// </summary>
        public BrandMemberCardUserInformation user_information { get; set; }
    }

    /// <summary>
    /// 修改商家名片会员卡模板的请求数据。
    /// </summary>
    public class BrandMemberCardUpdateRequestData
    {
        /// <summary>
        /// 更新后的卡面名称，最长 10 个中文字符。
        /// </summary>
        public string card_title { get; set; }

        /// <summary>
        /// 更新后的七位十六进制 RGB 卡面背景颜色。
        /// </summary>
        public string card_color { get; set; }

        /// <summary>
        /// 更新后的卡面图片 URL。
        /// </summary>
        public string card_picture_url { get; set; }

        /// <summary>
        /// 更新后的会员码跳转小程序信息。
        /// </summary>
        public BrandMemberCardJumpInformation code_jump_information { get; set; }

        /// <summary>
        /// 更新后的会员权益说明。
        /// </summary>
        public string benefits { get; set; }

        /// <summary>
        /// 更新后的开卡结果通知地址。
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 是否将会员卡置顶展示。
        /// </summary>
        public bool? need_pinned { get; set; }

        /// <summary>
        /// 是否在会员卡面展示会员等级。
        /// </summary>
        public bool? need_display_level { get; set; }

        /// <summary>
        /// 更新后的服务电话。
        /// </summary>
        public string service_phone { get; set; }

        /// <summary>
        /// 更新后的会员卡有效期配置。
        /// </summary>
        public BrandMemberCardValidDateInformation valid_date_information { get; set; }

        /// <summary>
        /// 更新后的会员中心小程序信息。
        /// </summary>
        public BrandMemberCardJumpInformation member_information { get; set; }

        /// <summary>
        /// 更新后的会员积分入口小程序信息。
        /// </summary>
        public BrandMemberCardJumpInformation points_information { get; set; }

        /// <summary>
        /// 更新后的会员储值入口小程序信息。
        /// </summary>
        public BrandMemberCardJumpInformation balance_information { get; set; }

        /// <summary>
        /// 更新后的付费会员价格和购买入口。
        /// </summary>
        public BrandMemberCardPurchaseInformation purchase_information { get; set; }

        /// <summary>
        /// 更新后的用户开卡必填信息。
        /// </summary>
        public BrandMemberCardUserInformation user_information { get; set; }
    }

    /// <summary>
    /// 查询会员卡模板列表的筛选和分页条件。
    /// </summary>
    public class BrandMemberCardListQueryRequestData
    {
        /// <summary>
        /// 会员卡模板状态：CARD_EFFECTIVE 或 CARD_INVALID；为空时查询全部状态。
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 分页偏移量，从 0 开始。
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 单页条数，取值范围为 1 至 20。
        /// </summary>
        public int limit { get; set; }
    }

    /// <summary>
    /// 会员卡相关的小程序跳转信息。
    /// </summary>
    public class BrandMemberCardJumpInformation
    {
        /// <summary>
        /// 跳转目标小程序 AppID，最长 32 个字符。
        /// </summary>
        public string jump_appid { get; set; }

        /// <summary>
        /// 跳转目标小程序页面路径，最长 128 个字符。
        /// </summary>
        public string jump_path { get; set; }
    }

    /// <summary>
    /// 会员卡有效期配置。
    /// </summary>
    public class BrandMemberCardValidDateInformation
    {
        /// <summary>
        /// 有效期类型：FIX_TIME_RANGE、FIX_TERM 或 PERMANENT。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 固定时间范围的开始时间，采用 RFC 3339 格式。
        /// </summary>
        public string available_begin_time { get; set; }

        /// <summary>
        /// 固定时间范围的结束时间，采用 RFC 3339 格式。
        /// </summary>
        public string available_end_time { get; set; }

        /// <summary>
        /// 领取后有效的自然日数，仅用于 FIX_TERM，最长不超过 10958 天。
        /// </summary>
        public int? available_day_after_receive { get; set; }
    }

    /// <summary>
    /// 付费会员价格和购买入口配置。
    /// </summary>
    public class BrandMemberCardPurchaseInformation
    {
        /// <summary>
        /// 付费会员价格，单位为分。
        /// </summary>
        public long? price { get; set; }

        /// <summary>
        /// 购买付费会员时跳转的小程序 AppID。
        /// </summary>
        public string jump_appid { get; set; }

        /// <summary>
        /// 购买付费会员时跳转的小程序页面路径。
        /// </summary>
        public string jump_path { get; set; }
    }

    /// <summary>
    /// 用户开通会员卡时需要填写的信息配置。
    /// </summary>
    public class BrandMemberCardUserInformation
    {
        /// <summary>
        /// 平台通用开卡信息字段列表，例如 USER_FORM_FLAG_NAME 或 USER_FORM_FLAG_BIRTHDAY。
        /// </summary>
        public string[] common_field_list { get; set; }

        /// <summary>
        /// 商家自定义开卡信息字段列表，当前最多支持一项。
        /// </summary>
        public BrandMemberCardCustomField[] custom_field_list { get; set; }
    }

    /// <summary>
    /// 商家自定义的用户开卡信息字段。
    /// </summary>
    public class BrandMemberCardCustomField
    {
        /// <summary>
        /// 字段类型：CHECK_BOX 或 RADIO。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 自定义字段名称，最长 32 个字符。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 可选字段值列表，最多 10 项，每项最长 8 个字符。
        /// </summary>
        public string[] values { get; set; }
    }

    /// <summary>
    /// 单个商家名片会员卡模板的返回结果。
    /// </summary>
    public class BrandMemberCardResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 商家创建会员卡模板时提交的唯一请求单号。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 微信支付生成的会员卡模板 ID。
        /// </summary>
        public string card_id { get; set; }

        /// <summary>
        /// 会员卡归属的品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 商家 AppID。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 会员卡类型：PURCHASE、NORMAL 或 BALANCE。
        /// </summary>
        public string card_type { get; set; }

        /// <summary>
        /// 卡面名称。
        /// </summary>
        public string card_title { get; set; }

        /// <summary>
        /// 卡面背景颜色。
        /// </summary>
        public string card_color { get; set; }

        /// <summary>
        /// 卡面图片 URL。
        /// </summary>
        public string card_picture_url { get; set; }

        /// <summary>
        /// 会员卡 Code 分配方式：SYSTEM_ALLOCATE 或 MERCHANT_ALLOCATE。
        /// </summary>
        public string code_mode { get; set; }

        /// <summary>
        /// 会员码展示类型。
        /// </summary>
        public string code_type { get; set; }

        /// <summary>
        /// 会员码跳转小程序信息。
        /// </summary>
        public BrandMemberCardJumpInformation code_jump_information { get; set; }

        /// <summary>
        /// 会员权益说明。
        /// </summary>
        public string benefits { get; set; }

        /// <summary>
        /// 开卡结果通知地址。
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 是否置顶展示会员卡。
        /// </summary>
        public bool? need_pinned { get; set; }

        /// <summary>
        /// 是否展示会员等级。
        /// </summary>
        public bool? need_display_level { get; set; }

        /// <summary>
        /// 新用户初始会员等级。
        /// </summary>
        public string init_level { get; set; }

        /// <summary>
        /// 服务电话。
        /// </summary>
        public string service_phone { get; set; }

        /// <summary>
        /// 商家法务协议纯文本。
        /// </summary>
        public string legal_agreement { get; set; }

        /// <summary>
        /// 会员卡有效期配置。
        /// </summary>
        public BrandMemberCardValidDateInformation valid_date_information { get; set; }

        /// <summary>
        /// 会员中心小程序信息。
        /// </summary>
        public BrandMemberCardJumpInformation member_information { get; set; }

        /// <summary>
        /// 会员积分入口小程序信息。
        /// </summary>
        public BrandMemberCardJumpInformation points_information { get; set; }

        /// <summary>
        /// 会员储值入口小程序信息。
        /// </summary>
        public BrandMemberCardJumpInformation balance_information { get; set; }

        /// <summary>
        /// 付费会员价格和购买入口。
        /// </summary>
        public BrandMemberCardPurchaseInformation purchase_information { get; set; }

        /// <summary>
        /// 用户开通会员卡时的必填信息配置。
        /// </summary>
        public BrandMemberCardUserInformation user_information { get; set; }

        /// <summary>
        /// 会员卡模板状态：CARD_EFFECTIVE 或 CARD_INVALID。
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 会员卡模板创建时间，采用 RFC 3339 格式。
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 会员卡模板最后更新时间，采用 RFC 3339 格式。
        /// </summary>
        public string modify_time { get; set; }
    }

    /// <summary>
    /// 商家名片会员卡模板列表的返回结果。
    /// </summary>
    public class BrandMemberCardListResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 符合查询条件的会员卡模板列表。
        /// </summary>
        public BrandMemberCardResultJson[] data { get; set; }

        /// <summary>
        /// 符合查询条件的会员卡模板总数。
        /// </summary>
        public long total_count { get; set; }

        /// <summary>
        /// 当前分页偏移量。
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 当前单页条数。
        /// </summary>
        public int limit { get; set; }
    }

    /// <summary>
    /// 查询单张用户会员卡的请求数据。
    /// </summary>
    public class BrandMemberCardUserCardQueryRequestData
    {
        /// <summary>
        /// 会员卡模板 ID。
        /// </summary>
        public string card_id { get; set; }

        /// <summary>
        /// 用户在会员卡模板 AppId 下的 OpenId。
        /// </summary>
        public string openid { get; set; }
    }

    /// <summary>
    /// 查询品牌下用户会员卡列表的请求数据。
    /// </summary>
    public class BrandMemberCardUserCardListQueryRequestData
    {
        /// <summary>
        /// 用户在会员卡模板 AppId 下的 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户会员卡状态：UNACTIVATED、EFFECTIVE、EXPIRED 或 INVALID。
        /// </summary>
        public string user_card_state { get; set; }

        /// <summary>
        /// 分页偏移量，从 0 开始。
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 单页条数，取值范围为 1 至 20。
        /// </summary>
        public int limit { get; set; }
    }

    /// <summary>
    /// 修改用户会员卡的请求数据。
    /// </summary>
    public class BrandMemberCardUserCardUpdateRequestData
    {
        /// <summary>
        /// 会员卡模板 ID。
        /// </summary>
        public string card_id { get; set; }

        /// <summary>
        /// 用户在会员卡模板 AppId 下的 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 七位十六进制 RGB 卡面背景颜色。
        /// </summary>
        public string card_color { get; set; }

        /// <summary>
        /// 通过品牌会员图片上传接口获得的卡面图片 URL。
        /// </summary>
        public string card_picture_url { get; set; }

        /// <summary>
        /// 使用微信支付公钥加密的会员手机号。
        /// </summary>
        public string phone_number { get; set; }

        /// <summary>
        /// 用户会员等级。
        /// </summary>
        public string level { get; set; }

        /// <summary>
        /// 用户会员卡有效期。
        /// </summary>
        public BrandMemberCardValidDateInformation valid_date_information { get; set; }

        /// <summary>
        /// 用户开卡时填写的通用和自定义信息。
        /// </summary>
        public BrandMemberCardUserProfileInformation user_information { get; set; }

        /// <summary>
        /// 商家自定义数据包，最长 256 个字符。
        /// </summary>
        public string attach { get; set; }
    }

    /// <summary>
    /// 作废用户会员卡的请求数据。
    /// </summary>
    public class BrandMemberCardUserCardInvalidateRequestData
    {
        /// <summary>
        /// 会员卡模板 ID。
        /// </summary>
        public string card_id { get; set; }

        /// <summary>
        /// 用户在会员卡模板 AppId 下的 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 作废原因，最长 32 个字符。
        /// </summary>
        public string invalid_reason { get; set; }
    }

    /// <summary>
    /// 用户开卡时填写的个人信息。
    /// </summary>
    public class BrandMemberCardUserProfileInformation
    {
        /// <summary>
        /// 平台通用开卡字段及加密字段值。
        /// </summary>
        public BrandMemberCardCommonFieldValue[] common_field_list { get; set; }

        /// <summary>
        /// 商家自定义开卡字段及加密选择值。
        /// </summary>
        public BrandMemberCardCustomFieldValue[] custom_field_list { get; set; }
    }

    /// <summary>
    /// 平台通用开卡字段值。
    /// </summary>
    public class BrandMemberCardCommonFieldValue
    {
        /// <summary>
        /// 通用字段名称，例如 USER_FORM_FLAG_NAME。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 使用微信支付公钥加密的字段值。
        /// </summary>
        public string value { get; set; }
    }

    /// <summary>
    /// 商家自定义开卡字段值。
    /// </summary>
    public class BrandMemberCardCustomFieldValue
    {
        /// <summary>
        /// 自定义字段名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 使用微信支付公钥加密的用户选择值。
        /// </summary>
        public string[] user_chosen_values { get; set; }
    }

    /// <summary>
    /// 用户会员卡详情。
    /// </summary>
    public class BrandMemberCardUserCardResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 会员卡 Code。
        /// </summary>
        public string user_card_code { get; set; }

        /// <summary>
        /// 会员卡模板 ID。
        /// </summary>
        public string card_id { get; set; }

        /// <summary>
        /// 用户在会员卡模板 AppId 下的 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 卡面背景颜色。
        /// </summary>
        public string card_color { get; set; }

        /// <summary>
        /// 卡面图片 URL。
        /// </summary>
        public string card_picture_url { get; set; }

        /// <summary>
        /// 会员卡所属品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 会员卡类型：PURCHASE、NORMAL 或 BALANCE。
        /// </summary>
        public string card_type { get; set; }

        /// <summary>
        /// 使用品牌 API 证书私钥解密的会员手机号密文。
        /// </summary>
        public string phone_number { get; set; }

        /// <summary>
        /// 用户会员等级。
        /// </summary>
        public string level { get; set; }

        /// <summary>
        /// 用户会员卡有效期。
        /// </summary>
        public BrandMemberCardValidDateInformation valid_date_information { get; set; }

        /// <summary>
        /// 用户领取会员卡的 RFC 3339 时间。
        /// </summary>
        public string pickup_time { get; set; }

        /// <summary>
        /// 用户开卡时填写的个人信息。
        /// </summary>
        public BrandMemberCardUserProfileInformation user_information { get; set; }

        /// <summary>
        /// 商家自定义数据包。
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 用户会员卡状态：UNACTIVATED、EFFECTIVE、EXPIRED 或 INVALID。
        /// </summary>
        public string user_card_state { get; set; }

        /// <summary>
        /// 作废原因。
        /// </summary>
        public string invalid_reason { get; set; }

        /// <summary>
        /// 作废时间，采用 RFC 3339 格式。
        /// </summary>
        public string invalid_time { get; set; }

        /// <summary>
        /// 创建时间，采用 RFC 3339 格式。
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 最后更新时间，采用 RFC 3339 格式。
        /// </summary>
        public string modify_time { get; set; }
    }

    /// <summary>
    /// 用户会员卡分页列表。
    /// </summary>
    public class BrandMemberCardUserCardListResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 符合条件的用户会员卡。
        /// </summary>
        public BrandMemberCardUserCardResultJson[] data { get; set; }

        /// <summary>
        /// 符合条件的总数量。
        /// </summary>
        public long total_count { get; set; }

        /// <summary>
        /// 当前分页偏移量。
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 当前单页条数。
        /// </summary>
        public int limit { get; set; }
    }

    /// <summary>
    /// 品牌会员入会组件预授权请求数据。
    /// </summary>
    public class BrandMemberCardPreAuthTokenRequestData
    {
        /// <summary>会员卡模板 ID。</summary>
        public string card_id { get; set; }

        /// <summary>用户在会员卡模板 AppId 下的 OpenId。</summary>
        public string openid { get; set; }
    }

    /// <summary>
    /// 品牌会员入会组件预授权结果。
    /// </summary>
    public class BrandMemberCardPreAuthTokenResultJson : ReturnJsonBase
    {
        /// <summary>用于拉起入会组件的预授权 Token。</summary>
        public string token { get; set; }

        /// <summary>Token 的 RFC 3339 过期时间。</summary>
        public string expire_time { get; set; }
    }

    /// <summary>
    /// 根据 OpenId 导入用户会员卡的请求数据。
    /// </summary>
    public class BrandMemberCardUserCardImportRequestData
    {
        /// <summary>会员卡模板 ID。</summary>
        public string card_id { get; set; }

        /// <summary>用户在会员卡模板 AppId 下的 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>商家分配或存量会员卡 Code。</summary>
        public string user_card_code { get; set; }

        /// <summary>使用微信支付公钥加密的手机号。</summary>
        public string phone_number { get; set; }

        /// <summary>卡面背景颜色。</summary>
        public string card_color { get; set; }

        /// <summary>卡面图片 URL。</summary>
        public string card_picture_url { get; set; }

        /// <summary>用户会员等级。</summary>
        public string level { get; set; }

        /// <summary>用户开卡时填写的加密个人信息。</summary>
        public BrandMemberCardUserProfileInformation user_information { get; set; }

        /// <summary>会员卡有效期，类型须与模板一致。</summary>
        public BrandMemberCardValidDateInformation valid_date_information { get; set; }

        /// <summary>领取时间；相对有效期类型时填写。</summary>
        public string pickup_time { get; set; }
    }

    /// <summary>
    /// 同步会员开通结果的请求数据。
    /// </summary>
    public class BrandMemberCardUserCardConfirmRequestData :
        BrandMemberCardUserCardUpdateRequestData
    {
        /// <summary>
        /// 开卡状态：CREATE_CARD_SUCCESS、CREATE_CARD_FAIL 或 CREATE_CARD_ALREADY_EXISTS。
        /// </summary>
        public string user_card_confirm_state { get; set; }
    }

    /// <summary>
    /// 创建用户会员动态的请求数据。
    /// </summary>
    public class BrandMemberCardUserFeedRequestData
    {
        /// <summary>会员卡模板 ID。</summary>
        public string card_id { get; set; }

        /// <summary>会员卡 Code。</summary>
        public string user_card_code { get; set; }

        /// <summary>用户在会员卡模板 AppId 下的 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>商家唯一请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>与支付下单 Cell 结构一致的会员动态内容。</summary>
        public string cell { get; set; }
    }

    /// <summary>
    /// 已创建的用户会员动态。
    /// </summary>
    public class BrandMemberCardUserFeedResultJson : ReturnJsonBase
    {
        /// <summary>品牌 ID。</summary>
        public string brand_id { get; set; }

        /// <summary>会员卡模板 ID。</summary>
        public string card_id { get; set; }

        /// <summary>会员卡 Code。</summary>
        public string user_card_code { get; set; }

        /// <summary>商家唯一请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>会员动态 Cell 内容。</summary>
        public string cell { get; set; }
    }

    /// <summary>
    /// 同步用户积分余额的请求数据。
    /// </summary>
    public class BrandMemberCardPointBalanceRequestData
    {
        /// <summary>商家唯一请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>会员卡模板 ID。</summary>
        public string card_id { get; set; }

        /// <summary>用户在会员卡模板 AppId 下的 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>会员卡 Code。</summary>
        public string user_card_code { get; set; }

        /// <summary>积分余额。</summary>
        public long point_balance { get; set; }
    }

    /// <summary>
    /// 用户积分余额同步结果。
    /// </summary>
    public class BrandMemberCardPointBalanceResultJson : ReturnJsonBase
    {
        /// <summary>商家唯一请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>品牌 ID。</summary>
        public string brand_id { get; set; }

        /// <summary>会员卡模板 ID。</summary>
        public string card_id { get; set; }

        /// <summary>用户在会员卡模板 AppId 下的 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>会员卡 Code。</summary>
        public string user_card_code { get; set; }

        /// <summary>积分余额。</summary>
        public long point_balance { get; set; }
    }

    /// <summary>
    /// 同步积分兑券结果的请求数据。
    /// </summary>
    public class BrandMemberCardPointExchangeRequestData
    {
        /// <summary>积分兑券记录 ID。</summary>
        public string record_id { get; set; }

        /// <summary>积分兑券模板 ID。</summary>
        public string exchange_coupon_template_id { get; set; }

        /// <summary>会员卡模板 ID。</summary>
        public string card_id { get; set; }

        /// <summary>用户在会员卡模板 AppId 下的 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>会员卡 Code。</summary>
        public string user_card_code { get; set; }

        /// <summary>POINT_EXCHANGE_COUPON_ALLOW 或 POINT_EXCHANGE_COUPON_REJECT。</summary>
        public string result { get; set; }

        /// <summary>拒绝兑券原因。</summary>
        public string reject_reason { get; set; }
    }

    /// <summary>
    /// 积分兑券同步结果。
    /// </summary>
    public class BrandMemberCardPointExchangeResultJson : ReturnJsonBase
    {
        /// <summary>积分兑券记录 ID。</summary>
        public string record_id { get; set; }

        /// <summary>积分兑券模板 ID。</summary>
        public string exchange_coupon_template_id { get; set; }

        /// <summary>品牌 ID。</summary>
        public string brand_id { get; set; }

        /// <summary>会员卡模板 ID。</summary>
        public string card_id { get; set; }

        /// <summary>用户在会员卡模板 AppId 下的 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>会员卡 Code。</summary>
        public string user_card_code { get; set; }

        /// <summary>POINT_EXCHANGE_COUPON_SUCCESS 或 POINT_EXCHANGE_COUPON_FAIL。</summary>
        public string state { get; set; }

        /// <summary>商品券 ID。</summary>
        public string product_coupon_id { get; set; }

        /// <summary>商品券批次类型。</summary>
        public string product_coupon_stock_type { get; set; }

        /// <summary>商品券批次 ID。</summary>
        public string stock_id { get; set; }

        /// <summary>兑券成功后的券 Code。</summary>
        public string coupon_code { get; set; }

        /// <summary>拒绝兑券原因。</summary>
        public string reject_reason { get; set; }
    }

    /// <summary>
    /// 商家名片会员图片上传结果。
    /// </summary>
    public class BrandMemberCardImageUploadResultJson : ReturnJsonBase
    {
        /// <summary>永久有效的媒体文件 URL。</summary>
        public string media_url { get; set; }
    }
}
