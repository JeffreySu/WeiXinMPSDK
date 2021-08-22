#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2021 Senparc
  
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
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Apis.BasePay.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Senparc.NeuChar.Helpers;
using Senparc.CO2NET.Extensions;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Entities;
using Senparc.Weixin.TenPayV3.Helpers;

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
        private static string ReurnPayApiUrl(string urlFormat)
        {
            return string.Format(urlFormat, Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "");
        }

        #region 平台证书

        /// <summary>
        /// 获取平台证书
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<CertificatesResultJson> CertificatesAsync(int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/certificates");
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
        public async Task<PublicKeyCollection> GetPublicKeysAsync(int timeOut = Config.TIME_OUT)
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
                var publicKey = ApiSecurityHelper.AesGcmDecryptCiphertext(_tenpayV3Setting.TenPayV3_APIv3Key, cert.encrypt_certificate.nonce,
                                    cert.encrypt_certificate.associated_data, cert.encrypt_certificate.ciphertext);
                keys[cert.serial_no] = publicKey.Replace("-----BEGIN CERTIFICATE-----", "").Replace("-----END CERTIFICATE-----", "").Replace("\r", "").Replace("\n", "");
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
        public async Task<JsApiReturnJson> JsApiAsync(JsApiRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/pay/transactions/jsapi");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<JsApiReturnJson>(url, data, timeOut);
        }

        // TODO: 待测试
        /// <summary>
        /// JSAPI合单支付下单接口
        /// <para>在微信支付服务后台生成JSAPI合单预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<JsApiReturnJson> JsApiCombineAsync(JsApiCombineRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/pay/combine-transactions/jsapi");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<JsApiReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new JsApiReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// APP支付下单接口
        /// <para>在微信支付服务后台生成APP支付预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<AppReturnJson> AppAsync(AppRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/pay/transactions/app");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<AppReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new AppReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
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
        public async Task<AppReturnJson> AppCombineAsync(AppCombineRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/pay/combine-transactions/app");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<AppReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new AppReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// H5支付下单接口
        /// <para>在微信支付服务后台生成H5支付预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<H5ReturnJson> H5Async(H5RequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/pay/transactions/h5");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<H5ReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new H5ReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// H5合单支付下单接口
        /// <para>在微信支付服务后台生成H5合单支付预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<H5ReturnJson> H5CombineAsync(H5CombineRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/pay/combine-transactions/h5");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<H5ReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new H5ReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
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
        public async Task<NativeReturnJson> NativeAsync(NativeRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/pay/transactions/native");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<NativeReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new NativeReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
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
        public async Task<NativeReturnJson> NativeCombineAsync(NativeCombineRequestData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/pay/combine-transactions/native");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<NativeReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new NativeReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
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
        public async Task<OrderReturnJson> OrderQueryByTransactionIdAsync(string transaction_id, string mchid, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/v3/{{0}}pay/transactions/id/{transaction_id}?mchid={mchid}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<OrderReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new OrderReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 商户订单号查询
        /// </summary>
        /// <param name="out_trade_no"> 微信支付系统生成的订单号 示例值：1217752501201407033233368018</param>
        /// <param name="mchid">直连商户的商户号，由微信支付生成并下发。 示例值：1230000109</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<OrderReturnJson> OrderQueryByOutTradeNoAsync(string out_trade_no, string mchid, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/v3/{{0}}pay/transactions/id/{out_trade_no}?mchid={mchid}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<OrderReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new OrderReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
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

                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/v3/{{0}}combine-transactions/out-trade-no/{combine_out_trade_no}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<CombineOrderReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new CombineOrderReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
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
        public async Task<ReturnJsonBase> CloseOrderAsync(string out_trade_no, string mchid, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/v3/{{0}}pay/transactions/out-trade-no/{out_trade_no}/close");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, mchid, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new ReturnJsonBase() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
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
                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/v3/{{0}}combine-transactions/out-trade-no/{combine_out_trade_no}/close");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new ReturnJsonBase() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }
        #endregion

        #region 退款相关接口
        // TODO: 待测试
        /// <summary>
        /// 申请退款接口
        /// </summary>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<RefundReturnJson> RefundAsync(RefundRequsetData data, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/v3/{{0}}refund/domestic/refunds");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<RefundReturnJson>(url, data, timeOut);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new RefundReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        // TODO: 待测试
        /// <summary>
        /// 查询单笔退款接口
        /// </summary>
        /// <param name="out_refund_no">商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。示例值：1217752501201407033233368018</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<RefundReturnJson> RefundQueryAsync(string out_refund_no, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/v3/{{0}}refund/domestic/refunds");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<RefundReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new RefundReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }
        #endregion

        #region 交易账单接口
        // TODO: 待测试
        /// <summary>
        /// 申请交易账单接口
        /// 获得微信支付按天提供的交易账单文件
        /// </summary>
        /// <param name="bill_date">账单日期 格式YYYY-MM-DD 仅支持三个月内的账单下载申请</param>
        /// <param name="bill_type">填则默认是ALL 枚举值：ALL：返回当日所有订单信息（不含充值退款订单）SUCCESS：返回当日成功支付的订单（不含充值退款订单）REFUND：返回当日退款订单（不含充值退款订单</param>
        /// <param name="tar_type"> 不填则默认是数据流 枚举值：GZIP：返回格式为.gzip的压缩包账单</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<BillReturnJson> TradeBillQueryAsync(string bill_date, string bill_type = "ALL", string tar_type = null, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/v3/{{0}}bill/tradebill?bill_date={bill_date}&bill_type={bill_type}");
                if (tar_type != null)
                {
                    url += $"&tar_type={tar_type}";
                }
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<BillReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new BillReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        /// <summary>
        /// 申请资金账单接口
        /// 获得微信支付按天提供的微信支付账户资金流水账单文件
        /// </summary>
        /// <param name="signature">请求签名</param>
        /// <param name="bill_date">账单日期 格式YYYY-MM-DD 仅支持三个月内的账单下载申请</param>
        /// <param name="account_type">不填则默认是BASIC 枚举值：BASIC：基本账户 OPERATION：运营账户 FEES：手续费账户</param>
        /// <param name="tar_type"> 不填则默认是数据流 枚举值：GZIP：返回格式为.gzip的压缩包账单</param>
        /// <returns></returns>
        public async Task<BillReturnJson> FundflowBillQueryAsync(string bill_date, string account_type = "BASIC", string tar_type = null, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/v3/{{0}}bill/fundflowbill?bill_date={bill_date}&account_type={account_type}");
                if (tar_type != null)
                {
                    url += $"&tar_type={tar_type}";
                }
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<BillReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                return new BillReturnJson() { ResultCode = new HttpHandlers.TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }
        #endregion
    }
}
