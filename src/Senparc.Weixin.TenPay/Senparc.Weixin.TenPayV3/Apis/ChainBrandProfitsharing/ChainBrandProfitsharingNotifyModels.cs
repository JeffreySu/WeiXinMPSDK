#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChainBrandProfitsharingNotifyModels.cs
    文件功能描述：微信支付连锁品牌分账通知模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 新增连锁品牌分账动账通知模型及事件常量

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.ChainBrandProfitsharing
{
    /// <summary>
    /// 连锁品牌分账通知事件常量。
    /// </summary>
    public static class ChainBrandProfitsharingNotifyEventTypes
    {
        /// <summary>分账或分账回退成功。</summary>
        public const string TransactionSuccess = "TRANSACTION.SUCCESS";

        /// <summary>通知资源的 original_type。</summary>
        public const string OriginalType = "profitsharing";
    }

    /// <summary>
    /// 连锁品牌分账或分账回退成功通知的解密资源。
    /// </summary>
    public class ChainBrandProfitsharingNotifyJson : ReturnJsonBase
    {
        /// <summary>服务商商户号。</summary>
        public string sp_mchid { get; set; }

        /// <summary>出资的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>微信分账或回退单号。</summary>
        public string order_id { get; set; }

        /// <summary>商户分账或回退单号。</summary>
        public string out_order_no { get; set; }

        /// <summary>发生动账的分账接收方。</summary>
        public ChainBrandProfitsharingNotifyReceiver receiver { get; set; }

        /// <summary>动账成功时间。</summary>
        public string success_time { get; set; }
    }

    /// <summary>
    /// 连锁品牌分账通知中的接收方。
    /// </summary>
    public class ChainBrandProfitsharingNotifyReceiver
    {
        /// <summary>接收方类型。</summary>
        public string type { get; set; }

        /// <summary>接收方账号。</summary>
        public string account { get; set; }

        /// <summary>动账金额，单位为分。</summary>
        public long amount { get; set; }

        /// <summary>分账或回退描述。</summary>
        public string description { get; set; }
    }
}
