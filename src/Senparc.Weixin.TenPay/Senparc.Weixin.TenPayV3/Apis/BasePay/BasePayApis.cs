#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
  
    文件名：BasePayApis.cs
    文件功能描述：新微信支付V3基础接口
    
    
    创建标识：Senparc - 20210804

    修改标识：Senparc - 20210811
    修改描述：完成JsApi支付签名方法

    修改标识：Senparc - 20210819
    修改描述：修改重构基础api支付接口

    修改标识：Senparc - 20210820
    修改描述：重命名部分异步方法

    修改标识：Senparc - 20210822
    修改描述：修改BasePayApis 此类型不再为静态类 使用ISenparcWeixinSettingForTenpayV3初始化实例

    修改标识：Senparc - 20210825
    修改描述：统一各支付方式请求类

    修改标识：Senparc - 20211026
    修改描述：v0.3.500.5 修复 RefundQueryAsync() 方法接口参数传递问题

    修改标识：Senparc - 20211026
    修改描述：v0.6.1 修复 CloseOrderAsync() 参数问题

    修改标识：Senparc - 20230112
    修改描述：v0.6.8.8 修复 RefundQueryAsync() URL 问题

----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
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

        //private readonly IServiceProvider _serviceProvider;

        //public BasePayApis(IServiceProvider serviceProvider)
        //{
        //    this._serviceProvider = serviceProvider;
        //}

        /// <summary>
        /// 返回可用的微信支付地址（自动判断是否使用沙箱）
        /// </summary>
        /// <param name="urlFormat">如：<code>https://api.mch.weixin.qq.com/{0}pay/unifiedorder</code></param>
        /// <returns></returns>
        internal static string GetPayApiUrl(string urlFormat, string sp_mchid = "")
        {
            //注意：目前微信支付 V3 还没有支持沙箱，此处只是预留
            var sendbox = Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "";
            var partner = string.IsNullOrWhiteSpace(sp_mchid) ? "" : "partner/";
            return string.Format(urlFormat, sendbox, partner);
        }

        #region 平台证书

        /// <summary>
        /// 获取平台证书
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CertificatesResultJson> CertificatesAsync(int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/certificates");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            //var responseMessge = await tenPayApiRequest.GetHttpResponseMessageAsync(url, null, timeOut);
            //return await responseMessge.Content.ReadAsStringAsync();
            return await tenPayApiRequest.RequestAsync<CertificatesResultJson>(url, null, timeOut, ApiRequestMethod.GET, checkSign: false);
        }

        /// <summary>
        /// 获取平台证书的公钥（会从远程请求，不会缓存）
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<PublicKeyCollection> GetPublicKeysAsync(/*int timeOut = Config.TIME_OUT*/)
        {
            var certificates = await CertificatesAsync();
            if (!certificates.ResultCode.Success)
            {
                throw new TenpayApiRequestException("获取证书公钥失败：" + certificates.ResultCode.ErrorMessage);
            }

            if (certificates.data?.Length == 0)
            {
                throw new TenpayApiRequestException("Certificates 获取结果为空");
            }

            PublicKeyCollection keys = new();
            //var tenpayV3Setting = Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;//TODO:改成从构造函数配置

            foreach (var cert in certificates.data)
            {
                var publicKey = SecurityHelper.AesGcmDecryptCiphertext(_tenpayV3Setting.TenPayV3_APIv3Key, cert.encrypt_certificate.nonce,
                                    cert.encrypt_certificate.associated_data, cert.encrypt_certificate.ciphertext);
                keys[cert.serial_no] = SecurityHelper.GetUnwrapCertKey(publicKey);
            }
            return keys;
        }

        #endregion

        #region 下单接口
        /// <summary>
        /// JSAPI下单接口
        /// <para>在微信支付服务后台生成JSAPI预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<JsApiReturnJson> JsApiAsync(TransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/pay/{1}transactions/jsapi", data?.sp_mchid);
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<JsApiReturnJson>(url, data, timeOut);
        }

        // TODO: 待测试
        /// <summary>
        /// JSAPI合单支付下单接口
        /// <para>在微信支付服务后台生成JSAPI合单预支付交易单，返回预支付交易会话标识</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter5_1_3.shtml</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<JsApiReturnJson> JsApiCombineAsync(CombineTransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                if (data.sub_orders.Count() is not >= 2 or not <= 10)
                {
                    throw new TenpayApiRequestException("sub_orders 参数必须在 2 到 10 之间！");
                }

                var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/combine-transactions/jsapi");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<JsApiReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new JsApiReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        /// <summary>
        /// APP支付下单接口
        /// <para>在微信支付服务后台生成APP支付预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<AppReturnJson> AppAsync(TransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/pay/{1}transactions/app", data?.sp_mchid);
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<AppReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new AppReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// APP合单支付下单接口
        /// <para>在微信支付服务后台生成APP合单支付预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<AppReturnJson> AppCombineAsync(CombineTransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/combine-transactions/app");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<AppReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new AppReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        /// <summary>
        /// H5支付下单接口
        /// <para>在微信支付服务后台生成H5支付预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据 注意：H5下单scene_info参数必填</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<H5ReturnJson> H5Async(TransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/pay/{1}transactions/h5", data?.sp_mchid);
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<H5ReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new H5ReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// H5合单支付下单接口
        /// <para>在微信支付服务后台生成H5合单支付预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<H5ReturnJson> H5CombineAsync(CombineTransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/combine-transactions/h5");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<H5ReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new H5ReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// Native支付下单接口
        /// <para>在微信支付服务后台生成Native支付预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<NativeReturnJson> NativeAsync(TransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/pay/{1}transactions/native", data?.sp_mchid);
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<NativeReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new NativeReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// Native合单支付下单接口
        /// <para>在微信支付服务后台生成Native合单支付预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<NativeReturnJson> NativeCombineAsync(CombineTransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/combine-transactions/native");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<NativeReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new NativeReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }
        #endregion

        #region 订单操作接口

        // TODO: 待测试
        /// <summary>
        /// 微信支付订单号查询
        /// </summary>
        /// <param name="transaction_id"> 微信支付系统生成的订单号 示例值：1217752501201407033233368018</param>
        /// <param name="mchid">直连商户的商户号，由微信支付生成并下发。 示例值：1230000109</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        [Obsolete("请使用新方法 OrderQueryByTransactionIdAsync(QueryRequestData data, int timeOut = Config.TIME_OUT)")]
        public async Task<OrderReturnJson> OrderQueryByTransactionIdAsync(string transaction_id, string mchid, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/pay/transactions/id/{transaction_id}?mchid={mchid}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<OrderReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new OrderReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 商户订单号查询
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_1_2.shtml</para>
        /// </summary>
        /// <param name="out_trade_no"> 微信支付系统生成的订单号 示例值：1217752501201407033233368018</param>
        /// <param name="mchid">直连商户的商户号，由微信支付生成并下发。 示例值：1230000109</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        [Obsolete("请使用新方法 OrderQueryByOutTradeNoAsync(QueryRequestData data, int timeOut = Config.TIME_OUT)")]
        public async Task<OrderReturnJson> OrderQueryByOutTradeNoAsync(string out_trade_no, string mchid, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/pay/transactions/out-trade-no/{out_trade_no}?mchid={mchid}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<OrderReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new OrderReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 微信支付订单号查询
        /// </summary>
        /// <param name="data">查询请求参数</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<OrderReturnJson> OrderQueryByTransactionIdAsync(QueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/pay/{{1}}transactions/id/{data.order_no}{UrlQueryHelper.ToParams(data)}", data?.sp_mchid);
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<OrderReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new OrderReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 商户订单号查询
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter4_1_2.shtml</para>
        /// </summary>
        /// <param name="data">查询请求参数</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<OrderReturnJson> OrderQueryByOutTradeNoAsync(QueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/pay/{{1}}transactions/out-trade-no/{data.order_no}{UrlQueryHelper.ToParams(data)}", data?.sp_mchid);
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<OrderReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new OrderReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 合单查询订单接口
        /// </summary>
        /// <param name="combine_out_trade_no">合单支付总订单号 示例值：P20150806125346</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CombineOrderReturnJson> CombineOrderQueryAsync(string combine_out_trade_no, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/combine-transactions/out-trade-no/{combine_out_trade_no}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<CombineOrderReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new CombineOrderReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 关闭订单接口
        /// </summary>
        /// <param name="out_trade_no">商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一 示例值：1217752501201407033233368018</param>
        /// <param name="mchid">直连商户的商户号，由微信支付生成并下发。 示例值：1230000109</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        [Obsolete("请使用新方法 CloseOrderAsync(CloseRequestData data, int timeOut = Config.TIME_OUT)")]
        public async Task<ReturnJsonBase> CloseOrderAsync(string out_trade_no, string mchid, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/pay/transactions/out-trade-no/{out_trade_no}/close");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                var data = new
                {
                    mchid = mchid
                };
                return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new ReturnJsonBase() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 关闭订单接口
        /// </summary>
        /// <param name="data">关闭订单请求参数</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReturnJsonBase> CloseOrderAsync(CloseRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/pay/{{1}}transactions/out-trade-no/{data.out_trade_no}/close", data?.sp_mchid);
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new ReturnJsonBase() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 合单关闭订单接口
        /// </summary>
        /// <param name="combine_out_trade_no">合单支付总订单号 示例值：P20150806125346</param>
        /// <param name="data">合单关闭订单请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReturnJsonBase> CloseCombineOrderAsync(string combine_out_trade_no, CloseCombineOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/combine-transactions/out-trade-no/{combine_out_trade_no}/close");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new ReturnJsonBase() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }
        #endregion

        #region 退款操作接口

        // TODO: 待测试
        /// <summary>
        /// 申请退款接口
        /// </summary>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        [Obsolete("请使用新方法 RefundAsync(RefundRequestData data, int timeOut = Config.TIME_OUT)")]
        public async Task<RefundReturnJson> RefundAsync(RefundRequsetData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/refund/domestic/refunds");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<RefundReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new RefundReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 查询单笔退款接口
        /// </summary>
        /// <param name="out_refund_no">商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。示例值：1217752501201407033233368018</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        [Obsolete("请使用新方法 RefundQueryAsync(RefundQueryRequestData data, int timeOut = Config.TIME_OUT)")]
        public async Task<RefundReturnJson> RefundQueryAsync(string out_refund_no, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/refund/domestic/refunds/{out_refund_no}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<RefundReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new RefundReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 申请退款接口
        /// </summary>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<RefundReturnJson> RefundAsync(RefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/refund/domestic/refunds");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<RefundReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new RefundReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 查询单笔退款接口
        /// </summary>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<RefundReturnJson> RefundQueryAsync(RefundQueryRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/refund/domestic/refunds/{data.out_refund_no}{UrlQueryHelper.ToParams(data)}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<RefundReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new RefundReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }
        #endregion

        #region 交易账单接口
        /// <summary>
        /// 申请交易账单接口
        /// 获得微信支付按天提供的交易账单文件
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_6.shtml</para>
        /// </summary>
        /// <param name="bill_date">账单日期 格式YYYY-MM-DD 仅支持三个月内的账单下载申请</param>
        /// <param name="bill_type">填则默认是ALL 枚举值：ALL：返回当日所有订单信息（不含充值退款订单）SUCCESS：返回当日成功支付的订单（不含充值退款订单）REFUND：返回当日退款订单（不含充值退款订单</param>
        /// <param name="tar_type">不填则默认是数据流 枚举值：GZIP：返回格式为.gzip的压缩包账单</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        [Obsolete("请使用新方法 TradeBillQueryAsync(TradeBillQueryRequestData data, Stream fileStream, int timeOut = Config.TIME_OUT)")]
        public async Task<BillReturnJson> TradeBillQueryAsync(string bill_date, Stream fileStream, string bill_type = "ALL", string tar_type = null, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/bill/tradebill?bill_date={bill_date}&bill_type={bill_type}");
                if (tar_type != null)
                {
                    url += $"&tar_type={tar_type}";
                }
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                var result = await tenPayApiRequest.RequestAsync<BillReturnJson>(url, null, timeOut, ApiRequestMethod.GET);

                //下载交易账单
                if (result.VerifySignSuccess == true)
                {
                    var responseMessage = await tenPayApiRequest.GetHttpResponseMessageAsync(result.download_url, null, requestMethod: ApiRequestMethod.GET);
                    fileStream.Seek(0, SeekOrigin.Begin);
                    await responseMessage.Content.CopyToAsync(fileStream);
                    fileStream.Seek(0, SeekOrigin.Begin);

                    //校验文件Hash
                    var fileHash = FileHelper.GetFileHash(fileStream, result.hash_type, false);
                    Console.WriteLine("fileHash: " + fileHash);
                    var fileVerify = fileHash.Equals(result.hash_value, StringComparison.OrdinalIgnoreCase);
                    if (!fileVerify)
                    {
                        result.VerifySignSuccess = false;
                        result.ResultCode.Additional += "请求成功，但文件校验错误。请查看日志！";
                        SenparcTrace.BaseExceptionLog(new TenpayApiRequestException($"TradeBillQueryAsync 下载文件成功，但校验失败，正确值：{result.hash_value}，实际值：{fileHash}（忽略大小写）"));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new BillReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        /// <summary>
        /// 申请资金账单接口
        /// 获得微信支付按天提供的微信支付账户资金流水账单文件
        /// </summary>
        /// <param name="bill_date">账单日期 格式YYYY-MM-DD 仅支持三个月内的账单下载申请</param>
        /// <param name="account_type">不填则默认是BASIC 枚举值：BASIC：基本账户 OPERATION：运营账户 FEES：手续费账户</param>
        /// <param name="tar_type"> 不填则默认是数据流 枚举值：GZIP：返回格式为.gzip的压缩包账单</param>
        /// <returns></returns>
        [Obsolete("请使用新方法 FundflowBillQueryAsync(FundflowBillQueryRequestData data, Stream fileStream, int timeOut = Config.TIME_OUT)")]
        public async Task<BillReturnJson> FundflowBillQueryAsync(string bill_date, Stream fileStream, string account_type = "BASIC", string tar_type = null, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/bill/fundflowbill?bill_date={bill_date}&account_type={account_type}");
                if (tar_type != null)
                {
                    url += $"&tar_type={tar_type}";
                }
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                var result = await tenPayApiRequest.RequestAsync<BillReturnJson>(url, null, timeOut, ApiRequestMethod.GET);

                //下载资金账单
                if (result.VerifySignSuccess == true)
                {
                    var responseMessage = await tenPayApiRequest.GetHttpResponseMessageAsync(result.download_url, null, requestMethod: ApiRequestMethod.GET);
                    fileStream.Seek(0, SeekOrigin.Begin);
                    await responseMessage.Content.CopyToAsync(fileStream);
                    fileStream.Seek(0, SeekOrigin.Begin);

                    //校验文件Hash
                    var fileHash = FileHelper.GetFileHash(fileStream, result.hash_type);
                    var fileVerify = fileHash.Equals(result.hash_value, StringComparison.OrdinalIgnoreCase);
                    if (!fileVerify)
                    {
                        result.VerifySignSuccess = false;
                        result.ResultCode.Additional += "请求成功，但文件校验错误。请查看日志！";
                        SenparcTrace.BaseExceptionLog(new TenpayApiRequestException($"TradeBillQueryAsync 下载文件成功，但校验失败，正确值：{result.hash_value}，实际值：{fileHash}（忽略大小写）"));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new BillReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        /// <summary>
        /// 申请交易账单接口
        /// 获得微信支付按天提供的交易账单文件
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_6.shtml</para>
        /// </summary>
        /// <param name="data">请求参数</param>
        /// <param name="fileStream">返回的文件流</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<BillReturnJson> TradeBillQueryAsync(TradeBillQueryRequestData data, Stream fileStream, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/bill/tradebill{UrlQueryHelper.ToParams(data)}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                var result = await tenPayApiRequest.RequestAsync<BillReturnJson>(url, null, timeOut, ApiRequestMethod.GET);

                //下载交易账单
                if (result.VerifySignSuccess == true)
                {
                    var responseMessage = await tenPayApiRequest.GetHttpResponseMessageAsync(result.download_url, null, requestMethod: ApiRequestMethod.GET);
                    fileStream.Seek(0, SeekOrigin.Begin);
                    await responseMessage.Content.CopyToAsync(fileStream);
                    fileStream.Seek(0, SeekOrigin.Begin);

                    //校验文件Hash
                    var fileHash = FileHelper.GetFileHash(fileStream, result.hash_type, false);
                    Console.WriteLine("fileHash: " + fileHash);
                    var fileVerify = fileHash.Equals(result.hash_value, StringComparison.OrdinalIgnoreCase);
                    if (!fileVerify)
                    {
                        result.VerifySignSuccess = false;
                        result.ResultCode.Additional += "请求成功，但文件校验错误。请查看日志！";
                        SenparcTrace.BaseExceptionLog(new TenpayApiRequestException($"TradeBillQueryAsync 下载文件成功，但校验失败，正确值：{result.hash_value}，实际值：{fileHash}（忽略大小写）"));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new BillReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        /// <summary>
        /// 申请资金账单接口
        /// 获得微信支付按天提供的微信支付账户资金流水账单文件
        /// </summary>
        /// <param name="data">请求参数</param>
        /// <param name="fileStream">返回的文件流</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<BillReturnJson> FundflowBillQueryAsync(FundflowBillQueryRequestData data, Stream fileStream, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/bill/fundflowbill{UrlQueryHelper.ToParams(data)}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                var result = await tenPayApiRequest.RequestAsync<BillReturnJson>(url, null, timeOut, ApiRequestMethod.GET);

                //下载资金账单
                if (result.VerifySignSuccess == true)
                {
                    var responseMessage = await tenPayApiRequest.GetHttpResponseMessageAsync(result.download_url, null, requestMethod: ApiRequestMethod.GET);
                    fileStream.Seek(0, SeekOrigin.Begin);
                    await responseMessage.Content.CopyToAsync(fileStream);
                    fileStream.Seek(0, SeekOrigin.Begin);

                    //校验文件Hash
                    var fileHash = FileHelper.GetFileHash(fileStream, result.hash_type);
                    var fileVerify = fileHash.Equals(result.hash_value, StringComparison.OrdinalIgnoreCase);
                    if (!fileVerify)
                    {
                        result.VerifySignSuccess = false;
                        result.ResultCode.Additional += "请求成功，但文件校验错误。请查看日志！";
                        SenparcTrace.BaseExceptionLog(new TenpayApiRequestException($"TradeBillQueryAsync 下载文件成功，但校验失败，正确值：{result.hash_value}，实际值：{fileHash}（忽略大小写）"));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new BillReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        /// <summary>
        /// 服务商：申请单个子商户资金账单
        /// 获得微信支付按天提供的微信支付账户资金流水账单文件
        /// </summary>
        /// <param name="data">请求参数</param>
        /// <param name="fileStreams">返回的文件流</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<SubmerchantBillReturnJson> SubmerchantFundflowBillQueryAsync(FundflowBillQueryRequestData data, List<Stream> fileStreams, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/bill/sub-merchant-fundflowbill{UrlQueryHelper.ToParams(data)}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                var result = await tenPayApiRequest.RequestAsync<SubmerchantBillReturnJson>(url, null, timeOut, ApiRequestMethod.GET);

                //下载资金账单
                if (result.VerifySignSuccess == true)
                {
                    foreach (var item in result.download_bill_list ?? new List<SubmerchantBillDownloadItem>())
                    {
                        var fileStream = new MemoryStream();
                        var responseMessage = await tenPayApiRequest.GetHttpResponseMessageAsync(item.download_url, null, requestMethod: ApiRequestMethod.GET);
                        fileStream.Seek(0, SeekOrigin.Begin);
                        await responseMessage.Content.CopyToAsync(fileStream);
                        fileStream.Seek(0, SeekOrigin.Begin);

                        //校验文件Hash
                        var fileHash = FileHelper.GetFileHash(fileStream, item.hash_type);
                        var fileVerify = fileHash.Equals(item.hash_value, StringComparison.OrdinalIgnoreCase);
                        if (!fileVerify)
                        {
                            result.VerifySignSuccess = false;
                            result.ResultCode.Additional += "请求成功，但文件校验错误。请查看日志！";
                            SenparcTrace.BaseExceptionLog(new TenpayApiRequestException($"TradeBillQueryAsync 下载文件成功，但校验失败，正确值：{item.hash_value}，实际值：{fileHash}（忽略大小写）"));
                        }

                        fileStreams.Add(fileStream);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new SubmerchantBillReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }
        #endregion
    }
}
