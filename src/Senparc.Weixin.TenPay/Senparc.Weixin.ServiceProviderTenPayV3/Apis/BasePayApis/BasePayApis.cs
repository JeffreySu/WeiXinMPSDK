
#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2022 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2022 Senparc

    文件名：BasePayApis.cs
    文件功能描述：微信支付V3服务商平台接口

    创建标识：Senparc - 20220804

----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
// TODO: 引入Entities
// using Senparc.Weixin.TenPayV3.Apis.BasePay;
// using Senparc.Weixin.TenPayV3.Apis.Entities;
// using Senparc.Weixin.TenPayV3.Entities;
// using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.ServiceProviderTenPayV3.Apis
{
    public class BasePayApis
    {

        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public BasePayApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;

        }

        /// <summary>
        /// 返回可用的微信支付地址（自动判断是否使用沙箱）
        /// </summary>
        /// <param name="urlFormat">如：<code>https://api.mch.weixin.qq.com/pay/unifiedorder</code></param>
        /// <returns></returns>
        internal static string GetPayApiUrl(string urlFormat)
        {
            //注意：目前微信支付 V3 还没有支持沙箱，此处只是预留
            return string.Format(urlFormat, Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "");
        }


        #region JSAPI支付



        /// <summary>
        /// JSAPI下单
        /// </summary>
        /// <param name="data">JSAPI下单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<JsApiAsyncResultJson> JsApiAsyncAsync(JsApiAsyncRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = JsApiAsyncApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/jsapi");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<JsApiAsyncResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询订单:1、微信支付订单号查询
        /// </summary>
        /// <param name="data">查询订单:1、微信支付订单号查询需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<OrderQueryResultJson> JsApiOrderQueryWXAsync(OrderQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = OrderQueryApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/id/{data.transaction_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<OrderQueryResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }


        /// <summary>
        /// 查询订单:2、商户订单号查询
        /// </summary>
        /// <param name="data">查询订单:2、商户订单号查询需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<OrderQueryResultJson> JsApiOrderQueryAsync(OrderQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = OrderQueryApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/out-trade-no/{data.out_trade_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<OrderQueryResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="data">关闭订单需要 POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<ResultJsonBase> JsApiCloseOrderAsync(CloseOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CloseOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/out-trade-no/{data.out_trade_no}/close");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }





        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="data">申请退款需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<RefundResultJson> JsApiRefundAsync(RefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = RefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<RefundResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询单笔退款
        /// </summary>
        /// <param name="data">查询单笔退款需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<GetRefundResultJson> JsApiGetRefundAsync(GetRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = GetRefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds/{data.out_refund_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GetRefundResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }




        /// <summary>
        /// 申请交易账单
        /// </summary>
        /// <param name="data">申请交易账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<GetTradeBillResultJson> JsApiGetTradeBillAsync(GetTradeBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = GetTradeBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/tradebill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GetTradeBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请资金账单
        /// </summary>
        /// <param name="data">申请资金账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<FundFlowBillResultJson> JsApiFundFlowBillAsync(FundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = FundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<FundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请单个子商户资金账单 
        /// </summary>
        /// <param name="data">申请单个子商户资金账单 需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<SubMerchantFundFlowBillResultJson> JsApiSubMerchantFundFlowBillAsync(SubMerchantFundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = SubMerchantFundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/sub-merchant-fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<SubMerchantFundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        #endregion


        #region APP支付



        /// <summary>
        /// APP下单
        /// </summary>
        /// <param name="data">APP下单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppResultJson> JsApiAppAsync(AppRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/app");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询订单:1、微信支付订单号查询
        /// </summary>
        /// <param name="data">查询订单:1、微信支付订单号查询需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppOrderQueryResultJson> AppOrderQueryWXAsync(AppOrderQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppOrderQueryApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/id/{data.transaction_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppOrderQueryResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }


        /// <summary>
        /// 查询订单:2、商户订单号查询
        /// </summary>
        /// <param name="data">查询订单:2、商户订单号查询需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppOrderQueryResultJson> AppOrderQueryAsync(AppOrderQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppOrderQueryApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/out-trade-no/{data.out_trade_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppOrderQueryResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="data">关闭订单需要 POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<ResultJsonBase> AppCloseOrderAsync(AppCloseOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppCloseOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/out-trade-no/{data.out_trade_no}/close");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }





        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="data">申请退款需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppRefundResultJson> AppRefundAsync(AppRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppRefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppRefundResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询单笔退款
        /// </summary>
        /// <param name="data">查询单笔退款需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppGetRefundResultJson> AppGetRefundAsync(AppGetRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppGetRefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds/{data.out_refund_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppGetRefundResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }




        /// <summary>
        /// 申请交易账单
        /// </summary>
        /// <param name="data">申请交易账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppGetTradeBillResultJson> AppGetTradeBillAsync(AppGetTradeBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppGetTradeBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/tradebill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppGetTradeBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请资金账单
        /// </summary>
        /// <param name="data">申请资金账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppFundFlowBillResultJson> AppFundFlowBillAsync(AppFundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppFundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppFundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请单个子商户资金账单 
        /// </summary>
        /// <param name="data">申请单个子商户资金账单 需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppSubMerchantFundFlowBillResultJson> AppSubMerchantFundFlowBillAsync(AppSubMerchantFundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppSubMerchantFundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/sub-merchant-fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppSubMerchantFundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        #endregion


        #region H5支付



        /// <summary>
        /// H5下单
        /// </summary>
        /// <param name="data">H5下单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<H5OrderResultJson> H5OrderAsync(H5OrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = H5OrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/h5");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<H5OrderResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询订单:1、微信支付订单号查询
        /// </summary>
        /// <param name="data">查询订单:1、微信支付订单号查询需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<H5OrderQueryResultJson> H5OrderQueryWXAsync(H5OrderQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = H5OrderQueryApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/id/{data.transaction_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<H5OrderQueryResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }


        /// <summary>
        /// 查询订单:2、商户订单号查询
        /// </summary>
        /// <param name="data">查询订单:2、商户订单号查询需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<H5OrderQueryResultJson> H5OrderQueryAsync(H5OrderQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = H5OrderQueryApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/out-trade-no/{data.out_trade_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<H5OrderQueryResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="data">关闭订单需要 POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<ResultJsonBase> H5CloseOrderAsync(H5CloseOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = H5CloseOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/out-trade-no/{data.out_trade_no}/close");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }





        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="data">申请退款需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<H5RefundResultJson> H5RefundAsync(H5RefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = H5RefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<H5RefundResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询单笔退款
        /// </summary>
        /// <param name="data">查询单笔退款需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<H5GetRefundResultJson> H5GetRefundAsync(H5GetRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = H5GetRefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds/{data.out_refund_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<H5GetRefundResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }




        /// <summary>
        /// 申请交易账单
        /// </summary>
        /// <param name="data">申请交易账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<H5GetTradeBillResultJson> H5GetTradeBillAsync(H5GetTradeBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = H5GetTradeBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/tradebill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<H5GetTradeBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请资金账单
        /// </summary>
        /// <param name="data">申请资金账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<H5FundFlowBillResultJson> H5FundFlowBillAsync(H5FundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = H5FundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<H5FundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请单个子商户资金账单 
        /// </summary>
        /// <param name="data">申请单个子商户资金账单 需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<H5ResultJson> H5Async(H5RequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = H5Apis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/sub-merchant-fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<H5ResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        #endregion


        #region Native支付



        /// <summary>
        /// Native下单
        /// </summary>
        /// <param name="data">Native下单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<NativeOrderResultJson> NativeOrderAsync(NativeOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = NativeOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/native");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<NativeOrderResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询订单:1、微信支付订单号查询
        /// </summary>
        /// <param name="data">查询订单:1、微信支付订单号查询需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<NativeOrderQueryResultJson> NativeOrderQueryAsync(NativeOrderQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = NativeOrderQueryApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/id/{data.transaction_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<NativeOrderQueryResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }


        /// <summary>
        /// 查询订单:2、商户订单号查询
        /// </summary>
        /// <param name="data">查询订单:2、商户订单号查询需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<NativeOrderQueryResultJson> NativeOrderQueryAsync(NativeOrderQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = NativeOrderQueryApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/out-trade-no/{data.out_trade_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<NativeOrderQueryResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="data">关闭订单需要 POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<ResultJsonBase> NativeCloseOrderAsync(NativeCloseOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = NativeCloseOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/out-trade-no/{data.out_trade_no}/close");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }





        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="data">申请退款需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<NativeRefundResultJson> NativeRefundAsync(NativeRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = NativeRefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<NativeRefundResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询单笔退款
        /// </summary>
        /// <param name="data">查询单笔退款需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<NativeGetRefundResultJson> NativeGetRefundAsync(NativeGetRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = NativeGetRefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds/{data.out_refund_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<NativeGetRefundResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }




        /// <summary>
        /// 申请交易账单
        /// </summary>
        /// <param name="data">申请交易账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<NativeGetTradeBillResultJson> NativeGetTradeBillAsync(NativeGetTradeBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = NativeGetTradeBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/tradebill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<NativeGetTradeBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请资金账单
        /// </summary>
        /// <param name="data">申请资金账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<NativeFundFlowBillResultJson> NativeFundFlowBillAsync(NativeFundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = NativeFundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<NativeFundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请单个子商户资金账单 
        /// </summary>
        /// <param name="data">申请单个子商户资金账单 需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<NativeSubMerchantFundFlowBillResultJson> NativeSubMerchantFundFlowBillAsync(NativeSubMerchantFundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = NativeSubMerchantFundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/sub-merchant-fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<NativeSubMerchantFundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        #endregion


        #region 小程序支付



        /// <summary>
        /// JSAPI下单
        /// </summary>
        /// <param name="data">JSAPI下单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppletsOrderResultJson> AppletsOrderAsync(AppletsOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppletsOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/jsapi");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppletsOrderResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询订单:1、微信支付订单号查询
        /// </summary>
        /// <param name="data">查询订单:1、微信支付订单号查询需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppletsOrderQueryResultJson> AppletsOrderQueryAsync(AppletsOrderQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppletsOrderQueryApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/id/{data.transaction_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppletsOrderQueryResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }


        /// <summary>
        /// 查询订单:2、商户订单号查询
        /// </summary>
        /// <param name="data">查询订单:2、商户订单号查询需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppletsOrderQueryResultJson> AppletsOrderQueryAsync(AppletsOrderQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppletsOrderQueryApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/out-trade-no/{data.out_trade_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppletsOrderQueryResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="data">关闭订单需要 POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<ResultJsonBase> AppletsCloseOrderAsync(AppletsCloseOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppletsCloseOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/pay/partner/transactions/out-trade-no/{data.out_trade_no}/close");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }





        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="data">申请退款需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppletsRefundResultJson> AppletsRefundAsync(AppletsRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppletsRefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppletsRefundResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询单笔退款
        /// </summary>
        /// <param name="data">查询单笔退款需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppletsGetRefundResultJson> AppletsGetRefundAsync(AppletsGetRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppletsGetRefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds/{data.out_refund_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppletsGetRefundResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }




        /// <summary>
        /// 申请交易账单
        /// </summary>
        /// <param name="data">申请交易账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppletsGetTradeBillResultJson> AppletsGetTradeBillAsync(AppletsGetTradeBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppletsGetTradeBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/tradebill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppletsGetTradeBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请资金账单
        /// </summary>
        /// <param name="data">申请资金账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppletsFundFlowBillResultJson> AppletsFundFlowBillAsync(AppletsFundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppletsFundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppletsFundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请单个子商户资金账单 
        /// </summary>
        /// <param name="data">申请单个子商户资金账单 需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<AppletsSubMerchantFundFlowBillResultJson> AppletsSubMerchantFundFlowBillAsync(AppletsSubMerchantFundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = AppletsSubMerchantFundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/sub-merchant-fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AppletsSubMerchantFundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        #endregion


        #region 合单支付



        /// <summary>
        /// 合单APP下单
        /// </summary>
        /// <param name="data">合单APP下单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineAppOrderResultJson> CombineAppOrderAsync(CombineAppOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineAppOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/combine-transactions/app");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineAppOrderResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 合单H5下单
        /// </summary>
        /// <param name="data">合单H5下单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineH5OrderResultJson> CombineH5OrderAsync(CombineH5OrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineH5OrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/combine-transactions/h5");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineH5OrderResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 合单JSAPI下单
        /// </summary>
        /// <param name="data">合单JSAPI下单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineJsapiOrderResultJson> CombineJsapiOrderAsync(CombineJsapiOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineJsapiOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/combine-transactions/jsapi");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineJsapiOrderResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 合单小程序下单
        /// </summary>
        /// <param name="data">合单小程序下单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineAppletsOrderResultJson> CombineAppletsOrderAsync(CombineAppletsOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineAppletsOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/combine-transactions/jsapi");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineAppletsOrderResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 合单Native下单
        /// </summary>
        /// <param name="data">合单Native下单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineMativeOrderResultJson> CombineMativeOrderAsync(CombineMativeOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineMativeOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/combine-transactions/native");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineMativeOrderResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }








        /// <summary>
        /// 合单查询订单
        /// </summary>
        /// <param name="data">合单查询订单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineGetOrderResultJson> CombineGetOrderAsync(CombineGetOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineGetOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/combine-transactions/out-trade-no/{data.combine_out_trade_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineGetOrderResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 合单关闭订单
        /// </summary>
        /// <param name="data">合单关闭订单需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<ResultJsonBase> CombineCloseOrderAsync(CombineCloseOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineCloseOrderApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/combine-transactions/out-trade-no/{data.combine_out_trade_no}/close");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }




        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="data">申请退款需要POST的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineRefundResultJson> CombineRefundAsync(CombineRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineRefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineRefundResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
        }



        /// <summary>
        /// 查询单笔退款
        /// </summary>
        /// <param name="data">查询单笔退款需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineGetRefundResultJson> CombineGetRefundAsync(CombineGetRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineGetRefundApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/refund/domestic/refunds/{data.out_refund_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineGetRefundResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }




        /// <summary>
        /// 申请交易账单
        /// </summary>
        /// <param name="data">申请交易账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineGetTradeBillResultJson> CombineGetTradeBillAsync(CombineGetTradeBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineGetTradeBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/tradebill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineGetTradeBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请资金账单
        /// </summary>
        /// <param name="data">申请资金账单需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineFundFlowBillResultJson> CombineFundFlowBillAsync(CombineFundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineFundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineFundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        /// <summary>
        /// 申请单个子商户资金账单 
        /// </summary>
        /// <param name="data">申请单个子商户资金账单 需要GET的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CombineSubMerchantFundFlowBillResultJson> CombineSubMerchantFundFlowBillAsync(CombineSubMerchantFundFlowBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = CombineSubMerchantFundFlowBillApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/bill/sub-merchant-fundflowbill");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CombineSubMerchantFundFlowBillResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
        }



        #endregion

    }
}
