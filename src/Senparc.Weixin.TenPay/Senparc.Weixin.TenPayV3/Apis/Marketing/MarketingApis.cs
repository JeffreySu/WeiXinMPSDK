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
  
    文件名：MarketingApis.cs
    文件功能描述：微信支付V3营销工具接口
    
    
    创建标识：Senparc - 20210821
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Marketing;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付V3营销工具接口
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_1.shtml 下的【营销工具】所有接口
    /// </summary>
    public class MarketingApis
    {

        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public MarketingApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 返回可用的微信支付地址（自动判断是否使用沙箱）
        /// </summary>
        /// <param name="urlFormat">如：<code>https://api.mch.weixin.qq.com/{0}pay/unifiedorder</code></param>
        /// <returns></returns>
        // TODO: 重复使用
        private static string ReurnPayApiUrl(string urlFormat)
        {
            return string.Format(urlFormat, Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "");
        }

        #region 代金券接口

        /// <summary>
        /// 创建代金券批次接口
        /// <para>调用此接口创建微信支付代金券批次，创建完成后将获得代金券批次id。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_1.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<CreateStockReturnJson> CreateStock(CreateStockRequsetData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/marketing/favor/coupon-stocks");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CreateStockReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 激活代金券批次接口
        /// <para>制券成功后，通过调用此接口激活批次，如果是预充值代金券，激活时会从商户账户余额中锁定本批次的营销资金</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_3.shtml </para>
        /// </summary>
        /// <param name="stock_id">批次号 微信为每个代金券批次分配的唯一id</param>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<StartStockReturnJson> StartStock(string stock_id, StartStockRequsetData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/stocks/{stock_id}/start");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<StartStockReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 发放代金券批次接口
        /// <para>商户平台/API完成制券后，可使用发放代金券接口发券。通过调用此接口可发放指定批次给指定用户，发券场景可以是小程序、H5、APP等</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_2.shtml </para>
        /// </summary>
        /// <param name="openid">用户openid</param>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<DistributeStockReturnJson> DistributeStock(string openid, DistributeStockRequsetData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/users/{openid}/coupons");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<DistributeStockReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 暂停发放代金券批次接口
        /// <para>通过此接口可暂停指定代金券批次。暂停后，该代金券批次暂停发放，用户无法通过任何渠道再领取该批次的券</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_13.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="stock_id">批次号 微信为每个代金券批次分配的唯一id 校验规则：必须为代金券（全场券或单品券）批次号，不支持立减与折扣。</param>
        /// <param name="stock_creator_mchid">批次创建方商户号 校验规则：接口传入的批次号需由stock_creator_mchid所创建</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<PauseStockReturnJson> PauseStock(string stock_id, string stock_creator_mchid, int timeOut = Config.TIME_OUT)
        {

            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/stocks/{stock_id}/pause");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<PauseStockReturnJson>(url, stock_creator_mchid, timeOut);
        }

        /// <summary>
        /// 重启发放代金券批次接口
        /// <para>通过此接口可暂停指定代金券批次。暂停后，该代金券批次暂停发放，用户无法通过任何渠道再领取该批次的券</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_14.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="stock_id">批次号 微信为每个代金券批次分配的唯一id 校验规则：必须为代金券（全场券或单品券）批次号，不支持立减与折扣。</param>
        /// <param name="stock_creator_mchid">批次创建方商户号 校验规则：接口传入的批次号需由stock_creator_mchid所创建</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<RestartStockReturnJson> RestartStock(string stock_id, string stock_creator_mchid, int timeOut = Config.TIME_OUT)
        {

            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/stocks/{stock_id}/restart");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<RestartStockReturnJson>(url, stock_creator_mchid, timeOut);
        }

        /// <summary>
        /// 条件查询代金券批次列表
        /// <para>通过此接口可查询多个批次的信息，包括批次的配置信息以及批次概况数据</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_4.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="offset">分页页码	 页码从0开始，默认第0页</param>
        /// <param name="limit">分页大小，最大10</param>
        /// <param name="stock_creator_mchid">批次创建方商户号 校验规则：接口传入的批次号需由stock_creator_mchid所创建</param>
        /// <param name="create_start_time">起始创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，可为null</param>
        /// <param name="create_end_time">终止创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，可为null</param>
        /// <param name="status">批次状态，枚举值： unactivated：未激活 audit：审核中 running：运行中 stoped：已停止 paused：暂停发放，可为null
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryStocksReturnJson> QueryStocks(uint offset, uint limit, string stock_creator_mchid, TenpayDateTime create_start_time = null, TenpayDateTime create_end_time = null, string status = null, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/stocks?offset={offset}&limit={limit}&stock_creator_mchid={stock_creator_mchid}");
            if (create_start_time is not null)
            {
                url += $"&create_start_time={create_start_time}";
            }
            if (create_end_time is not null)
            {
                url += $"&create_end_time={create_end_time}";
            }
            if (status is not null)
            {
                url += $"&status={status}";
            }

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryStocksReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 查询批次详情
        /// <para>通过此接口可查询批次信息，包括批次的配置信息以及批次概况数据</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_5.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="stock_id">批次号 微信为每个代金券批次分配的唯一id 校验规则：必须为代金券（全场券或单品券）批次号，不支持立减与折扣。</param>
        /// <param name="stock_creator_mchid">批次创建方商户号 校验规则：接口传入的批次号需由stock_creator_mchid所创建</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<StockReturnJson> QueryStock(string stock_id, string stock_creator_mchid, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/stocks/{stock_id}?stock_creator_mchid={stock_creator_mchid}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<StockReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 查询代金券可用商户
        /// <para>通过调用此接口可查询批次的可用商户号，判断券是否在某商户号可用，来决定是否展示</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_7.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="offset">分页页码</param>
        /// <param name="limit">分页大小</param>
        /// <param name="stock_creator_mchid">创建批次的商户号</param>
        /// <param name="stock_id">批次号</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryMerchantsReturnJson> QueryMerchantsStock(uint offset, uint limit, string stock_creator_mchid, string stock_id, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/stocks/{stock_id}/merchants?offset={offset}&limit={limit}&stock_creator_mchid={stock_creator_mchid}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryMerchantsReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 查询代金券可用单品
        /// <para>通过此接口可查询批次的可用商品编码，判断券是否可用于某些商品，来决定是否展示</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_8.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="offset">分页页码</param>
        /// <param name="limit">分页大小</param>
        /// <param name="stock_creator_mchid">创建批次的商户号</param>
        /// <param name="stock_id">批次号</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryItemsReturnJson> QueryItems(uint offset, uint limit, string stock_creator_mchid, string stock_id, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/stocks/{stock_id}/items?offset={offset}&limit={limit}&stock_creator_mchid={stock_creator_mchid}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryItemsReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 根据商户号查用户的券
        /// <para>可通过该接口查询用户在某商户号可用的全部券，可用于商户的小程序/H5中，用户"我的代金券"或"提交订单页"展示优惠信息。无法查询到微信支付立减金。本接口查不到用户的微信支付立减金（又称“全平台通用券”），即在所有商户都可以使用的券，例如：摇摇乐红包；当按可用商户号查询时，无法查询用户已经核销的券</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_9.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="openid">用户在商户appid 下的唯一标识</param>
        /// <param name="appid">微信为发券方商户分配的公众账号ID</param>
        /// <param name="stock_id">批次号，是否指定批次号查询，填写available_mchid，该字段不生效，可为null</param>
        /// <param name="status">代金券状态：枚举值: SENDED：可用 USED：已实扣 填写available_mchid，该字段不生效，可为null</param>
        /// <param name="creator_mchid">批次创建方商户号，creator_mchid sender_mchid available_mchid三选一</param>
        /// <param name="sender_mchid">批次发放商户号（该字段暂未开放），creator_mchid sender_mchid available_mchid三选一</param>
        /// <param name="available_mchid">可用商户号，creator_mchid sender_mchid available_mchid三选一</param>
        /// <param name="offset">分页页码</param>
        /// <param name="limit">分页大小</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryCouponsReturnJson> QueryCoupons(string openid, string appid, string stock_id, string status, string creator_mchid, string sender_mchid, string available_mchid, string stock_creator_mchid, uint offset = 0, uint limit = 20, int timeOut = Config.TIME_OUT)
        {

            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/users/{openid}/coupons?appid={appid}&offset={offset}&limit={limit}");

            if (status is not null)
            {
                url += $"&status={status}";
            }
            if (creator_mchid is not null)
            {
                url += $"&creator_mchid={creator_mchid}";
            }
            if (creator_mchid is not null)
            {
                url += $"&creator_mchid={creator_mchid}";
            }
            if (sender_mchid is not null)
            {
                url += $"&sender_mchid={sender_mchid}";
            }
            if (available_mchid is not null)
            {
                url += $"&available_mchid={available_mchid}";
            }

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryCouponsReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 下载批次核销明细
        /// 可获取到某批次的核销明细数据，包括订单号、单品信息、银行流水号等，用于对账/数据分析
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_10.shtml</para>
        /// </summary>
        /// <param name="stock_id">批次号</param>
        /// <param name="fileStream">fileStream</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<DownloadStockUseFlowReturnJson> DownloadStockUseFlow(string stock_id, Stream fileStream, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/stocks/{stock_id}/use-flow");

                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                var result = await tenPayApiRequest.RequestAsync<DownloadStockUseFlowReturnJson>(url, null, timeOut, ApiRequestMethod.GET);

                //下载交易账单
                if (result.VerifySignSuccess == true)
                {
                    var responseMessage = await tenPayApiRequest.GetHttpResponseMessageAsync(result.url, null, requestMethod: ApiRequestMethod.GET);
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
                return new DownloadStockUseFlowReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        /// <summary>
        /// 下载批次退款明细
        /// 可获取到某批次的退款明细数据，包括订单号、单品信息、银行流水号等，用于对账/数据分析
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_11.shtml</para>
        /// </summary>
        /// <param name="stock_id">批次号</param>
        /// <param name="fileStream">fileStream</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<DownloadStockRefundFlowReturnJson> DownloadStockRefundFlow(string stock_id, Stream fileStream, int timeOut = Config.TIME_OUT)
        {
            try
            {
                var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/stocks/{stock_id}/refund-flow");

                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                var result = await tenPayApiRequest.RequestAsync<DownloadStockRefundFlowReturnJson>(url, null, timeOut, ApiRequestMethod.GET);

                //下载交易账单
                if (result.VerifySignSuccess == true)
                {
                    var responseMessage = await tenPayApiRequest.GetHttpResponseMessageAsync(result.url, null, requestMethod: ApiRequestMethod.GET);
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
                return new DownloadStockRefundFlowReturnJson() { ResultCode = new TenPayApiResultCode() { ErrorMessage = ex.Message } };
            }
        }

        /// <summary>
        /// 设置消息通知地址
        /// <para>于设置接收营销事件通知的URL，可接收营销相关的事件通知，包括核销、发放、退款等</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_12.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<SetNotifyUrlReturnJson> SetNotifyUrl(SetNotifyUrlRequsetData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/marketing/favor/callbacks");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<SetNotifyUrlReturnJson>(url, data, timeOut);
        }

        #endregion
    }
}
