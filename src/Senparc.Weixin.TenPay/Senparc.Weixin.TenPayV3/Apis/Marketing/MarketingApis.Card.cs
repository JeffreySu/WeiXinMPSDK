using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.TenPayV3.Apis.Marketing;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付V3营销工具接口
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_1.shtml 下的【营销工具】所有接口 &gt; 【消费卡接口】
    /// </summary>
    public partial class MarketingApis
    {
        #region 消费卡接口

        /// <summary>
        /// 发放消费卡接口
        /// <para>商户通过调用本接口向用户发放消费卡，用户领到卡的同时会领取到一批代金券，消费卡会自动放入卡包中。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_6_1.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<SendCardReturnJson> SendCardAsync(SendCardRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/busifavor/coupons/{data.card_id}/send");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<SendCardReturnJson>(url, data, timeOut);
        }

        #endregion
    }
}
