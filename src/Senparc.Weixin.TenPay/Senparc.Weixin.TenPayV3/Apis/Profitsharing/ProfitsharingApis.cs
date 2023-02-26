#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2023 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2023 Senparc
  
    文件名：ProfitsharingApis.cs
    文件功能描述：微信支付V3分账接口
    
    
    创建标识：Senparc - 20210920

    修改标识：Senparc - 20220511
    修改描述：v0.6.2.1 修复 CreateProfitsharingAsync 接口路径问题

    
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Apis.Profitsharing;
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
    /// 微信支付V3分账接口
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_1.shtml 下的【分账】所有接口
    /// </summary>
    public class ProfitsharingApis
    {

        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public ProfitsharingApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
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

        #region 分账接口

        /// <summary>
        /// 请求分账接口
        /// <para>微信订单支付成功后，商户发起分账请求，将结算后的资金分到分账接收方</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_1.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CreateProfitsharingReturnJson> CreateProfitsharingAsync(CreateProfitsharingRequestData data, int timeOut = Config.TIME_OUT)
        {
            foreach(var each in data.receivers)
            {
                if (each.type == "MERCHANT_ID" && each.name == null)
                {
                    throw new TenpayApiRequestException($"当 {nameof(each.type)} 为 {each.type} 时，{nameof(each.name)} 必填！");
                }
            }
            // TODO: name 敏感信息加密
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/profitsharing/orders");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CreateProfitsharingReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询分账结果接口
        /// <para>发起分账请求后，可调用此接口查询分账结果。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_7_2.shtml </para>
        /// </summary>
        /// <param name="transaction_id">微信支付订单号</param>
        /// <param name="out_order_no">商户系统内部的分账单号，在商户系统内部唯一</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<QueryProfitsharingReturnJson> QueryProfitsharingAsync(string transaction_id, string out_order_no, int timeOut = Config.TIME_OUT)
        {

            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/profitsharing/orders/{out_order_no}?&transaction_id={transaction_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryProfitsharingReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 请求分账回退接口
        /// <para>如果订单已经分账，在退款时，可以先调此接口，将已分账的资金从分账接收方的账户回退给分账方，再发起退款。/para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_3.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<ReturnProfitsharingReturnJson> ReturnProfitsharingAsync(ReturnProfitsharingRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/profitsharing/return-orders");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ReturnProfitsharingReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询分账回退结果接口
        /// <para>商户需要核实回退结果，可调用此接口查询回退结果。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_4.shtml </para>
        /// </summary>
        /// <param name="out_return_no">调用回退接口提供的商户系统内部的回退单号</param>
        /// <param name="out_order_no">商户系统内部的分账单号，在商户系统内部唯一</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<QueryReturnProfitsharingReturnJson> QueryReturnProfitsharingAsync(string out_return_no, string out_order_no, int timeOut = Config.TIME_OUT)
        {

            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/profitsharing/return-orders/{out_return_no}?&out_order_no={out_return_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryReturnProfitsharingReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 解冻剩余资金接口
        /// <para>不需要进行分账的订单，可直接调用本接口将订单的金额全部解冻给特约商户</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_5.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<UnfreezeProfitsharingReturnJson> UnfreezeProfitsharingAsync(UnfreezeProfitsharingRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/profitsharing/orders/unfreeze");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<UnfreezeProfitsharingReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询剩余待分金额接口
        /// <para>可调用此接口查询订单剩余待分金额。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_6.shtml </para>
        /// </summary>
        /// <param name="transaction_id">微信支付订单号</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryProfitsharingAmountsReturnJson> QueryProfitsharingAmountsAsync(string transaction_id, int timeOut = Config.TIME_OUT)
        {

            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/profitsharing/transactions/{transaction_id}/amounts");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryProfitsharingAmountsReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 添加分账接收方接口
        /// <para>商户发起添加分账接收方请求，建立分账接收方列表。后续可通过发起分账请求，将分账方商户结算后的资金，分到该分账接收方</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_8.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<AddProfitsharingReceiverReturnJson> AddProfitsharingReceiverAsync(AddProfitsharingReceiverRequestData data, int timeOut = Config.TIME_OUT)
        {
            // TODO: name 敏感信息加密
            if (data.type == "MERCHANT_ID" && data.name == null)
            {
                throw new TenpayApiRequestException($"当 {nameof(data.type)} 为 {data.type} 时，{nameof(data.name)} 必填！");
            }
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/profitsharing/receivers/add");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AddProfitsharingReceiverReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 删除分账接收方接口
        /// <para>商户发起删除分账接收方请求。删除后，不支持将分账方商户结算后的资金，分到该分账接收方</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_9.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<DeleteProfitsharingReceiverReturnJson> DeleteProfitsharingAsync(DeleteProfitsharingReceiverRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/profitsharing/receivers/delete");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<DeleteProfitsharingReceiverReturnJson>(url, data, timeOut);
        }

        #endregion
    }
}
