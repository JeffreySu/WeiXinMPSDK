#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BrandStoreModels.cs
    文件功能描述：微信支付品牌门店请求与返回模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐品牌门店请求、详情、列表、状态及收款商户模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BrandStore
{
    /// <summary>
    /// 创建品牌门店的请求数据。
    /// </summary>
    public class BrandStoreCreateRequestData
    {
        /// <summary>
        /// 门店基础信息；门店内部编号和分店名称均为免审核字段。
        /// </summary>
        public BrandStoreBasics store_basics { get; set; }

        /// <summary>
        /// 门店地址信息，创建门店时必填。
        /// </summary>
        public BrandStoreAddress store_address { get; set; }

        /// <summary>
        /// 门店经营信息。
        /// </summary>
        public BrandStoreBusiness store_business { get; set; }
    }

    /// <summary>
    /// 更新品牌门店的请求数据。
    /// </summary>
    public class BrandStoreUpdateRequestData
    {
        /// <summary>
        /// 需要更新的门店基础信息。
        /// </summary>
        public BrandStoreBasics store_basics { get; set; }

        /// <summary>
        /// 需要更新的门店地址信息；提交该对象时按官方要求填写完整地址字段。
        /// </summary>
        public BrandStoreAddress store_address { get; set; }

        /// <summary>
        /// 需要更新的门店经营信息。
        /// </summary>
        public BrandStoreBusiness store_business { get; set; }
    }

    /// <summary>
    /// 品牌门店基础信息。
    /// </summary>
    public class BrandStoreBasics
    {
        /// <summary>
        /// 品牌方内部使用的门店唯一编号，最长 32 个字符。
        /// </summary>
        public string store_reference_id { get; set; }

        /// <summary>
        /// 分店名称，不包含品牌名称和地区信息，最长 50 个字符。
        /// </summary>
        public string branch_name { get; set; }
    }

    /// <summary>
    /// 品牌门店地址信息。
    /// </summary>
    public class BrandStoreAddress
    {
        /// <summary>
        /// 国家标准行政区划代码，最长 20 个字符。
        /// </summary>
        public string address_code { get; set; }

        /// <summary>
        /// 门店详细地址，最长 200 个字符。
        /// </summary>
        public string address_detail { get; set; }

        /// <summary>
        /// 地址补充信息，例如商场楼层或铺位号，最长 50 个字符。
        /// </summary>
        public string address_complements { get; set; }

        /// <summary>
        /// 门店位置经度，字符串格式，最长 32 个字符。
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 门店位置纬度，字符串格式，最长 32 个字符。
        /// </summary>
        public string latitude { get; set; }
    }

    /// <summary>
    /// 品牌门店经营信息。
    /// </summary>
    public class BrandStoreBusiness
    {
        /// <summary>
        /// 门店服务电话；最多填写两个号码，多个号码使用竖线分隔，最长 32 个字符。
        /// </summary>
        public string service_phone { get; set; }

        /// <summary>
        /// 营业时间描述，最多包含七个营业时段，最长 256 个字符。
        /// </summary>
        public string business_hours { get; set; }
    }

    /// <summary>
    /// 品牌门店列表的分页和状态筛选条件。
    /// </summary>
    public class BrandStoreListQueryRequestData
    {
        /// <summary>
        /// 门店状态筛选条件：OPEN、CREATING 或 CLOSED；为空时查询全部状态。
        /// </summary>
        public string store_state { get; set; }

        /// <summary>
        /// 分页偏移量，默认从 0 开始。
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 单页条数，默认 20，取值范围为 1 至 200。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 绑定品牌门店收款商户号的请求数据。
    /// </summary>
    public class BrandStoreBindRecipientRequestData
    {
        /// <summary>
        /// 门店收款商户号，仅允许填写品牌已关联的商户号，最长 16 个字符。
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 门店收款主体名称，最长 256 个字符。
        /// </summary>
        public string company_name { get; set; }
    }

    /// <summary>
    /// 解绑品牌门店收款商户号的请求数据。
    /// </summary>
    public class BrandStoreUnbindRecipientRequestData
    {
        /// <summary>
        /// 需要解绑且当前状态为已绑定的门店收款商户号，最长 16 个字符。
        /// </summary>
        public string mchid { get; set; }
    }

    /// <summary>
    /// 单个品牌门店详情的返回结果。
    /// </summary>
    public class BrandStoreResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付为品牌门店分配的唯一 ID。
        /// </summary>
        public string store_id { get; set; }

        /// <summary>
        /// 门店状态：OPEN、CREATING 或 CLOSED。
        /// </summary>
        public string store_state { get; set; }

        /// <summary>
        /// 审核状态：SUCCESS、PROCESSING 或 REJECTED。
        /// </summary>
        public string audit_state { get; set; }

        /// <summary>
        /// 审核驳回原因；审核未驳回时可能为空。
        /// </summary>
        public string review_reject_reason { get; set; }

        /// <summary>
        /// 门店基础信息。
        /// </summary>
        public BrandStoreBasics store_basics { get; set; }

        /// <summary>
        /// 门店地址信息。
        /// </summary>
        public BrandStoreAddress store_address { get; set; }

        /// <summary>
        /// 门店经营信息。
        /// </summary>
        public BrandStoreBusiness store_business { get; set; }

        /// <summary>
        /// 门店收款商户号及其绑定状态列表。
        /// </summary>
        public BrandStoreRecipient[] store_recipient { get; set; }
    }

    /// <summary>
    /// 品牌门店列表的返回结果。
    /// </summary>
    public class BrandStoreListResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 当前分页返回的品牌门店列表。
        /// </summary>
        public BrandStoreResultJson[] data { get; set; }

        /// <summary>
        /// 当前分页偏移量。
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 当前单页条数。
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 符合查询条件的品牌门店总数。
        /// </summary>
        public long total_count { get; set; }
    }

    /// <summary>
    /// 品牌门店营业状态变更的返回结果。
    /// </summary>
    public class BrandStoreStateResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付为品牌门店分配的唯一 ID。
        /// </summary>
        public string store_id { get; set; }

        /// <summary>
        /// 变更后的门店状态：OPEN、CREATING 或 CLOSED。
        /// </summary>
        public string store_state { get; set; }
    }

    /// <summary>
    /// 品牌门店收款商户信息。
    /// </summary>
    public class BrandStoreRecipient
    {
        /// <summary>
        /// 门店收款商户号。
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 门店收款主体名称。
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// 收款绑定状态：CONFIRMED、ADMIN_REJECTED、CONFIRMING 或 TIMEOUT_REJECTED。
        /// </summary>
        public string recipient_state { get; set; }
    }

    /// <summary>
    /// 绑定品牌门店收款商户号的返回结果。
    /// </summary>
    public class BrandStoreBindRecipientResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付为品牌门店分配的唯一 ID。
        /// </summary>
        public string store_id { get; set; }

        /// <summary>
        /// 门店收款商户号。
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 门店收款主体名称。
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// 收款绑定状态：CONFIRMED、ADMIN_REJECTED、CONFIRMING 或 TIMEOUT_REJECTED。
        /// </summary>
        public string recipient_state { get; set; }
    }

    /// <summary>
    /// 解绑品牌门店收款商户号的返回结果。
    /// </summary>
    public class BrandStoreUnbindRecipientResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 解绑失败原因；解绑成功时可能为空。
        /// </summary>
        public string failed_reason { get; set; }
    }
}
