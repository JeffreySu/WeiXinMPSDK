#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：EcommerceApis.cs
    文件功能描述：微信支付 V3 电商收付通 API
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 微信支付 V3 电商收付通 API
    /// <para>电商收付通相关的所有接口</para>
    /// </summary>
    public partial class EcommerceApis
    {
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public EcommerceApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        #region 二级商户管理API

        /// <summary>
        /// 二级商户进件API
        /// <para>电商平台为二级商户发起进件申请</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_1_1.shtml</para>
        /// </summary>
        /// <param name="data">二级商户进件请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<SubMerchantApplymentReturnJson> SubMerchantApplymentAsync(SubMerchantApplymentRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/applyments/");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<SubMerchantApplymentReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询二级商户进件申请状态API
        /// <para>通过申请单号查询二级商户进件申请状态</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_1_2.shtml</para>
        /// </summary>
        /// <param name="data">查询申请状态请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QuerySubMerchantApplymentReturnJson> QuerySubMerchantApplymentAsync(QuerySubMerchantApplymentRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/applyments/{data.applyment_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QuerySubMerchantApplymentReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 通过业务申请编号查询申请状态API
        /// <para>通过业务申请编号查询二级商户进件申请状态</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_1_3.shtml</para>
        /// </summary>
        /// <param name="data">通过业务申请编号查询请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QuerySubMerchantApplymentReturnJson> QuerySubMerchantApplymentByOutRequestNoAsync(QuerySubMerchantApplymentByOutRequestNoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/applyments/out-request-no/{data.out_request_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QuerySubMerchantApplymentReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        #endregion

        #region 合单支付API

        /// <summary>
        /// 合单下单API
        /// <para>电商收付通合单支付，一次可以提交多个子商户的支付请求</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_3_1.shtml</para>
        /// </summary>
        /// <param name="data">合单下单请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CombineTransactionsReturnJson> CombineTransactionsAsync(CombineTransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/combine/transactions/jsapi");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineTransactionsReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 合单查询订单API
        /// <para>查询合单支付订单状态</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_3_2.shtml</para>
        /// </summary>
        /// <param name="data">合单查询订单请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryCombineTransactionsReturnJson> QueryCombineTransactionsAsync(QueryCombineTransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/combine/transactions/out-trade-no/{data.combine_out_trade_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryCombineTransactionsReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 合单关闭订单API
        /// <para>关闭合单支付订单</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_3_3.shtml</para>
        /// </summary>
        /// <param name="data">合单关闭订单请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CloseCombineTransactionsReturnJson> CloseCombineTransactionsAsync(CloseCombineTransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/combine/transactions/out-trade-no/{data.combine_out_trade_no}/close");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CloseCombineTransactionsReturnJson>(url, data, timeOut);
        }

        #endregion

        #region 分账API

        /// <summary>
        /// 请求分账API
        /// <para>电商收付通分账，将款项分账给指定的接收方</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_4_1.shtml</para>
        /// </summary>
        /// <param name="data">请求分账请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<EcommerceProfitsharingReturnJson> EcommerceProfitsharingAsync(EcommerceProfitsharingRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/profitsharing/orders");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<EcommerceProfitsharingReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询分账结果API
        /// <para>查询电商收付通分账结果</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_4_2.shtml</para>
        /// </summary>
        /// <param name="data">查询分账结果请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryEcommerceProfitsharingReturnJson> QueryEcommerceProfitsharingAsync(QueryEcommerceProfitsharingRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/profitsharing/orders");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryEcommerceProfitsharingReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 请求分账回退API
        /// <para>将已分账的款项回退给电商平台</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_4_3.shtml</para>
        /// </summary>
        /// <param name="data">分账回退请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<EcommerceProfitsharingReturnOrderReturnJson> EcommerceProfitsharingReturnOrderAsync(EcommerceProfitsharingReturnOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/profitsharing/return-orders");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<EcommerceProfitsharingReturnOrderReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询分账回退结果API
        /// <para>查询分账回退结果</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_4_4.shtml</para>
        /// </summary>
        /// <param name="data">查询分账回退结果请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryEcommerceProfitsharingReturnOrderReturnJson> QueryEcommerceProfitsharingReturnOrderAsync(QueryEcommerceProfitsharingReturnOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/profitsharing/return-orders");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryEcommerceProfitsharingReturnOrderReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 完结分账API
        /// <para>完结分账，不再进行后续分账操作</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_4_5.shtml</para>
        /// </summary>
        /// <param name="data">完结分账请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<EcommerceProfitsharingFinishReturnJson> EcommerceProfitsharingFinishAsync(EcommerceProfitsharingFinishRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/profitsharing/finish-order");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<EcommerceProfitsharingFinishReturnJson>(url, data, timeOut);
        }

        #endregion

        #region 补差API

        /// <summary>
        /// 请求补差API
        /// <para>电商收付通补差，用于平台垫付资金的补差</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_5_1.shtml</para>
        /// </summary>
        /// <param name="data">请求补差请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<EcommerceSubsidiesReturnJson> EcommerceSubsidiesAsync(EcommerceSubsidiesRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/subsidies/create");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<EcommerceSubsidiesReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 请求补差回退API
        /// <para>将补差款项回退</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_5_2.shtml</para>
        /// </summary>
        /// <param name="data">补差回退请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<EcommerceSubsidiesReturnReturnJson> EcommerceSubsidiesReturnAsync(EcommerceSubsidiesReturnRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/subsidies/return");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<EcommerceSubsidiesReturnReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 取消补差API
        /// <para>取消补差</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter7_5_3.shtml</para>
        /// </summary>
        /// <param name="data">取消补差请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<EcommerceSubsidiesCancelReturnJson> EcommerceSubsidiesCancelAsync(EcommerceSubsidiesCancelRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/ecommerce/subsidies/cancel");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<EcommerceSubsidiesCancelReturnJson>(url, data, timeOut);
        }

        #endregion
    }
}
