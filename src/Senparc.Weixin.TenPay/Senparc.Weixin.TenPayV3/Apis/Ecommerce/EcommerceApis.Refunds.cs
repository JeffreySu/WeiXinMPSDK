#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：EcommerceApis.Refunds.cs
    文件功能描述：微信支付 V3 电商收付通交易退款接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐电商退款、垫付回补、异常退款和通知模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using System;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 微信支付 V3 电商收付通交易退款接口。
    /// </summary>
    public partial class EcommerceApis
    {
        /// <summary>
        /// 为平台二级商户申请交易退款。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476892</para>
        /// </summary>
        /// <param name="data">原订单、退款单号、退款金额、出资账户及通知地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>退款受理结果、退款金额及优惠退款明细。</returns>
        public Task<EcommerceRefundResultJson> ApplyEcommerceRefundAsync(
            EcommerceRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/ecommerce/refunds/apply";
            return PostEcommerceRefundAsync<EcommerceRefundResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 按微信支付退款单号查询单笔电商退款。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476908</para>
        /// </summary>
        /// <param name="refundId">微信支付生成的退款单号。</param>
        /// <param name="subMchId">退款对应的二级商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>退款状态、入账账户、出资明细和优惠退款信息。</returns>
        public Task<EcommerceRefundResultJson>
            QueryEcommerceRefundByRefundIdAsync(string refundId,
                string subMchId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/refunds/id/{EscapeEcommerceRefundValue(refundId)}";
            path = AddEcommerceRefundSubMerchant(path, subMchId);
            return GetEcommerceRefundAsync<EcommerceRefundResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 按商户退款单号查询单笔电商退款。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476911</para>
        /// </summary>
        /// <param name="outRefundNo">商户自定义的退款单号。</param>
        /// <param name="subMchId">退款对应的二级商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>退款状态、入账账户、出资明细和优惠退款信息。</returns>
        public Task<EcommerceRefundResultJson>
            QueryEcommerceRefundByOutRefundNoAsync(string outRefundNo,
                string subMchId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/refunds/out-refund-no/{EscapeEcommerceRefundValue(outRefundNo)}";
            path = AddEcommerceRefundSubMerchant(path, subMchId);
            return GetEcommerceRefundAsync<EcommerceRefundResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 查询电商平台垫付退款的回补结果。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476916</para>
        /// </summary>
        /// <param name="refundId">微信支付生成的退款单号。</param>
        /// <param name="subMchId">退款对应的二级商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>垫付回补金额、出入账账户、处理结果和完成时间。</returns>
        public Task<EcommerceRefundAdvanceReturnResultJson>
            QueryEcommerceRefundAdvanceReturnAsync(string refundId,
                string subMchId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/refunds/{EscapeEcommerceRefundValue(refundId)}/return-advance";
            path = AddEcommerceRefundSubMerchant(path, subMchId);
            return GetEcommerceRefundAsync<EcommerceRefundAdvanceReturnResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 将二级商户退款资金回补给垫付的电商平台账户。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476927</para>
        /// </summary>
        /// <param name="refundId">微信支付生成的退款单号。</param>
        /// <param name="data">退款对应的二级商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>垫付回补金额、出入账账户、处理结果和完成时间。</returns>
        public Task<EcommerceRefundAdvanceReturnResultJson>
            ReturnEcommerceRefundAdvanceAsync(string refundId,
                EcommerceRefundAdvanceReturnRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/refunds/{EscapeEcommerceRefundValue(refundId)}/return-advance";
            return PostEcommerceRefundAsync<EcommerceRefundAdvanceReturnResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 按平台收付通路径发起异常退款。
        /// <para>该入口与 BasePay 的国内退款路径不同，原有 BasePay 方法保持不变。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015181616</para>
        /// </summary>
        /// <param name="refundId">微信支付生成的退款单号。</param>
        /// <param name="data">二级商户号、商户退款单号、异常退款类型及加密后的银行卡信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>异常退款处理后的退款状态和金额明细。</returns>
        public Task<EcommerceRefundResultJson>
            ApplyEcommerceAbnormalRefundAsync(string refundId,
                AbnormalRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/refunds/{EscapeEcommerceRefundValue(refundId)}/apply-abnormal-refund";
            return PostEcommerceRefundAsync<EcommerceRefundResultJson>(
                path, data, timeOut);
        }

        private Task<T> PostEcommerceRefundAsync<T>(string path, object data,
            int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetEcommerceRefundUrl(path), data,
                timeOut);
        }

        private Task<T> GetEcommerceRefundAsync<T>(string path, int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetEcommerceRefundUrl(path), null,
                timeOut, ApiRequestMethod.GET);
        }

        private static string GetEcommerceRefundUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string EscapeEcommerceRefundValue(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }

        private static string AddEcommerceRefundSubMerchant(string path,
            string subMchId)
        {
            return $"{path}?sub_mchid={EscapeEcommerceRefundValue(subMchId)}";
        }
    }
}
