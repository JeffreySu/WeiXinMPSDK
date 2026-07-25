#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChainBrandProfitsharingModels.cs
    文件功能描述：微信支付连锁品牌分账请求与返回模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐现行连锁品牌分账请求、回退、接收方和账单模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Attributes;

namespace Senparc.Weixin.TenPayV3.Apis.ChainBrandProfitsharing
{
    /// <summary>
    /// 请求连锁品牌分账。
    /// </summary>
    public class ChainBrandProfitsharingCreateOrderRequestData
    {
        /// <summary>品牌主商户号。</summary>
        public string brand_mchid { get; set; }

        /// <summary>出资的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>服务商 AppID，接收方包含 PERSONAL_OPENID 时填写。</summary>
        public string appid { get; set; }

        /// <summary>子商户 AppID，接收方包含 PERSONAL_SUB_OPENID 时填写。</summary>
        public string sub_appid { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户分账单号。</summary>
        public string out_order_no { get; set; }

        /// <summary>分账接收方，最多 50 个。</summary>
        public ChainBrandProfitsharingReceiverRequestData[] receivers
        { get; set; }

        /// <summary>本次分账后是否完结分账。</summary>
        public bool finish { get; set; }
    }

    /// <summary>
    /// 查询连锁品牌分账结果。
    /// </summary>
    public class ChainBrandProfitsharingOrderQueryRequestData
    {
        /// <summary>出资的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户分账单号。</summary>
        public string out_order_no { get; set; }
    }

    /// <summary>
    /// 请求连锁品牌分账回退。
    /// </summary>
    public class ChainBrandProfitsharingReturnOrderRequestData
    {
        /// <summary>出资的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>微信分账单号，与 out_order_no 二选一。</summary>
        public string order_id { get; set; }

        /// <summary>商户分账单号，与 order_id 二选一。</summary>
        public string out_order_no { get; set; }

        /// <summary>商户分账回退单号。</summary>
        public string out_return_no { get; set; }

        /// <summary>只能是原分账接收方的回退商户号。</summary>
        public string return_mchid { get; set; }

        /// <summary>回退金额，单位为分。</summary>
        public long amount { get; set; }

        /// <summary>回退描述。</summary>
        public string description { get; set; }
    }

    /// <summary>
    /// 查询连锁品牌分账回退结果。
    /// </summary>
    public class ChainBrandProfitsharingReturnOrderQueryRequestData
    {
        /// <summary>出资的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>商户分账回退单号。</summary>
        public string out_return_no { get; set; }

        /// <summary>微信分账单号，与 out_order_no 二选一。</summary>
        public string order_id { get; set; }

        /// <summary>商户分账单号，与 order_id 二选一。</summary>
        public string out_order_no { get; set; }
    }

    /// <summary>
    /// 完结连锁品牌分账。
    /// </summary>
    public class ChainBrandProfitsharingFinishOrderRequestData
    {
        /// <summary>出资的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户分账单号。</summary>
        public string out_order_no { get; set; }

        /// <summary>完结分账描述。</summary>
        public string description { get; set; }
    }

    /// <summary>
    /// 申请分账账单。
    /// </summary>
    public class ChainBrandProfitsharingBillRequestData
    {
        /// <summary>指定子商户号；不填时返回服务商维度账单。</summary>
        public string sub_mchid { get; set; }

        /// <summary>账单日期，格式为 yyyy-MM-dd。</summary>
        public string bill_date { get; set; }

        /// <summary>压缩类型；可选值 GZIP。</summary>
        public string tar_type { get; set; }
    }

    /// <summary>
    /// 分账接收方请求数据。
    /// </summary>
    public class ChainBrandProfitsharingReceiverRequestData
    {
        /// <summary>接收方类型。</summary>
        public string type { get; set; }

        /// <summary>接收方账号。</summary>
        public string account { get; set; }

        /// <summary>分账金额，单位为分。</summary>
        public long amount { get; set; }

        /// <summary>分账描述。</summary>
        public string description { get; set; }

        /// <summary>接收方姓名；调用接口时自动加密。</summary>
        [FieldEncrypt]
        public string name { get; set; }
    }

    /// <summary>
    /// 连锁品牌分账单返回数据。
    /// </summary>
    public class ChainBrandProfitsharingOrderResultJson : ReturnJsonBase
    {
        /// <summary>品牌主商户号。</summary>
        public string brand_mchid { get; set; }

        /// <summary>出资的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户分账单号。</summary>
        public string out_order_no { get; set; }

        /// <summary>微信分账单号。</summary>
        public string order_id { get; set; }

        /// <summary>分账单状态：PROCESSING 或 FINISHED。</summary>
        public string status { get; set; }

        /// <summary>分账接收方执行结果。</summary>
        public ChainBrandProfitsharingReceiverResult[] receivers
        { get; set; }

        /// <summary>完结分账金额，单位为分。</summary>
        public long? finish_amount { get; set; }

        /// <summary>完结分账描述。</summary>
        public string finish_description { get; set; }
    }

    /// <summary>
    /// 分账接收方执行结果。
    /// </summary>
    public class ChainBrandProfitsharingReceiverResult
    {
        /// <summary>接收方类型。</summary>
        public string type { get; set; }

        /// <summary>接收方账号。</summary>
        public string account { get; set; }

        /// <summary>分账金额，单位为分。</summary>
        public long amount { get; set; }

        /// <summary>分账描述。</summary>
        public string description { get; set; }

        /// <summary>接收方分账结果。</summary>
        public string result { get; set; }

        /// <summary>分账完成时间。</summary>
        public string finish_time { get; set; }

        /// <summary>分账失败原因。</summary>
        public string fail_reason { get; set; }

        /// <summary>微信分账明细单号。</summary>
        public string detail_id { get; set; }
    }

    /// <summary>
    /// 连锁品牌分账回退结果。
    /// </summary>
    public class ChainBrandProfitsharingReturnOrderResultJson : ReturnJsonBase
    {
        /// <summary>出资的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>微信分账单号。</summary>
        public string order_id { get; set; }

        /// <summary>商户分账单号。</summary>
        public string out_order_no { get; set; }

        /// <summary>商户分账回退单号。</summary>
        public string out_return_no { get; set; }

        /// <summary>回退商户号。</summary>
        public string return_mchid { get; set; }

        /// <summary>回退金额，单位为分。</summary>
        public long amount { get; set; }

        /// <summary>微信分账回退单号。</summary>
        public string return_no { get; set; }

        /// <summary>回退结果：PROCESSING、SUCCESS 或 FAIL。</summary>
        public string result { get; set; }

        /// <summary>回退失败原因。</summary>
        public string fail_reason { get; set; }

        /// <summary>回退完成时间。</summary>
        public string finish_time { get; set; }
    }

    /// <summary>
    /// 完结连锁品牌分账结果。
    /// </summary>
    public class ChainBrandProfitsharingFinishOrderResultJson : ReturnJsonBase
    {
        /// <summary>出资的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户分账单号。</summary>
        public string out_order_no { get; set; }

        /// <summary>微信分账单号。</summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 连锁品牌订单剩余待分金额。
    /// </summary>
    public class ChainBrandProfitsharingAmountsResultJson : ReturnJsonBase
    {
        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>订单剩余待分金额，单位为分。</summary>
        public long unsplit_amount { get; set; }
    }

    /// <summary>
    /// 连锁品牌最大分账比例配置。
    /// </summary>
    public class ChainBrandProfitsharingBrandConfigResultJson : ReturnJsonBase
    {
        /// <summary>品牌主商户号。</summary>
        public string brand_mchid { get; set; }

        /// <summary>最大分账比例，单位为万分比，例如 2000 表示 20%。</summary>
        public long max_ratio { get; set; }
    }

    /// <summary>
    /// 添加连锁品牌分账接收方。
    /// </summary>
    public class ChainBrandProfitsharingAddReceiverRequestData
    {
        /// <summary>品牌主商户号。</summary>
        public string brand_mchid { get; set; }

        /// <summary>服务商 AppID。</summary>
        public string appid { get; set; }

        /// <summary>子商户 AppID。</summary>
        public string sub_appid { get; set; }

        /// <summary>接收方类型。</summary>
        public string type { get; set; }

        /// <summary>接收方账号。</summary>
        public string account { get; set; }

        /// <summary>接收方姓名；调用接口时自动加密。</summary>
        [FieldEncrypt]
        public string name { get; set; }

        /// <summary>与品牌方的关系类型。</summary>
        public string relation_type { get; set; }
    }

    /// <summary>
    /// 删除连锁品牌分账接收方。
    /// </summary>
    public class ChainBrandProfitsharingDeleteReceiverRequestData
    {
        /// <summary>品牌主商户号。</summary>
        public string brand_mchid { get; set; }

        /// <summary>服务商 AppID。</summary>
        public string appid { get; set; }

        /// <summary>子商户 AppID。</summary>
        public string sub_appid { get; set; }

        /// <summary>接收方类型。</summary>
        public string type { get; set; }

        /// <summary>接收方账号。</summary>
        public string account { get; set; }
    }

    /// <summary>
    /// 添加或删除分账接收方的返回数据。
    /// </summary>
    public class ChainBrandProfitsharingReceiverResultJson : ReturnJsonBase
    {
        /// <summary>品牌主商户号。</summary>
        public string brand_mchid { get; set; }

        /// <summary>接收方类型。</summary>
        public string type { get; set; }

        /// <summary>接收方账号。</summary>
        public string account { get; set; }
    }

    /// <summary>
    /// 连锁品牌分账账单元数据。
    /// </summary>
    public class ChainBrandProfitsharingBillResultJson : ReturnJsonBase
    {
        /// <summary>摘要算法，当前为 SHA1。</summary>
        public string hash_type { get; set; }

        /// <summary>账单文件摘要。</summary>
        public string hash_value { get; set; }

        /// <summary>短期有效的账单下载地址。</summary>
        public string download_url { get; set; }
    }

}
