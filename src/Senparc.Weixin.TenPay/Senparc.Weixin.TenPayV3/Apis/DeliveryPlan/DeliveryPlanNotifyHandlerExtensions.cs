#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：DeliveryPlanNotifyHandlerExtensions.cs
    文件功能描述：摇一摇有优惠投放计划状态通知的强类型解密入口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 新增投放计划状态变更通知解密扩展

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.DeliveryPlan;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 投放计划状态变更通知的强类型验签、解密入口。
    /// </summary>
    public static class DeliveryPlanNotifyHandlerExtensions
    {
        /// <summary>
        /// 异步验签并解密摇一摇有优惠投放计划状态变更通知。
        /// </summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式解密。</param>
        /// <returns>验签并解密后的投放计划状态变更数据。</returns>
        public static Task<DeliveryPlanNotifyJson> DecryptDeliveryPlanNotifyAsync(
            this TenPayNotifyHandler handler, bool isPublicKey = false) =>
            handler.DecryptGetObjectAsync<DeliveryPlanNotifyJson>(isPublicKey);
    }
}
