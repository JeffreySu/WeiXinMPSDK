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
  
    文件名：PayScoreApis.cs
    文件功能描述：微信支付V3经营能力接口
    
    
    创建标识：Senparc - 20210926
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Apis.PayScore;
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
    /// 微信支付V3营经营能力接口
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_1.shtml 下的【经营能力】所有接口
    /// </summary>
    public partial class PayScoreApis
    {

        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public PayScoreApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
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

        #region 微信支付分（免确认模式）

        /// <summary>
        /// 创单结单合并接口
        /// <para>该接口适用于无需微信支付分做订单风控判断的业务场景，在服务完成后，通过该接口对用户进行免密代扣。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_1.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CreateDirectCompleteServiceOrderReturnJson> CreateDirectCompleteServiceOrderAsync(CreateDirectCompleteServiceOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}payscore/serviceorder/direct-complete");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CreateDirectCompleteServiceOrderReturnJson>(url, data, timeOut);
        }

        #endregion

        #region 微信支付分（免确认预授权模式）

        /// <summary>
        /// 商户预授权接口
        /// <para>商户预授权。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_2.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<GivePermissionReturnJson> GivePermissionAsync(GivePermissionRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/payscore/permissions");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GivePermissionReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询用户授权记录（授权协议号）接口
        /// <para>通过authorization_code，商户查询与用户授权关系</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_3.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="service_id">服务ID 该服务ID有本接口对应产品的权限。 </param>
        /// <param name="authorization_code">商户系统内部授权协议号，要求此参数只能由数字、大小写字母_-*组成，且在同一个商户号下唯一。</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryPermissionByAuthorizationCodeReturnJson> QueryPermissionByAuthorizationCodeAsync(string service_id, string authorization_code, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/payscore/permissions/authorization-code/{authorization_code}&service_id={service_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryPermissionByAuthorizationCodeReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 解除用户授权关系（授权协议号）接口
        /// <para>通过authorization_code，商户解除用户授权关系</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_4.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReturnJsonBase> TerminatePermissionByAuthorizationCodeAsync(TerminatePermissionByAuthorizationCodeRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/payscore/permissions/authorization-code/{data.authorization_code}/terminate");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut);
        }

        /// <summary>
        /// 查询用户授权记录（openid）接口
        /// <para>通过openid查询用户授权信息</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_5.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="service_id">服务ID 该服务ID有本接口对应产品的权限。 </param>
        /// <param name="appid">微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系，此参数需在本系统先进行配置。 </param>
        /// <param name="openid">微信用户在商户对应appid下的唯一标识</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryPermissionByOpenidReturnJson> QueryPermissionByOpenidAsync(string service_id, string appid, string openid, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/payscore/permissions/openid/{openid}?appid={appid}&service_id={service_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryPermissionByOpenidReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 解除用户授权关系（openid）接口
        /// <para>通过openid， 商户解除用户授权关系</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_6.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReturnJsonBase> TerminatePermissionByOpenidAsync(TerminatePermissionByOpenidRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/payscore/permissions/openid/{data.openid}/terminate");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut);
        }


        #endregion

        #region 微信支付分（公共API）

        /// <summary>
        /// 创建支付分订单接口
        /// <para>用户申请使用服务时，商户可通过此接口申请创建微信支付分订单。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_14.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CreateServiceOrderReturnJson> CreateServiceOrderAsync(CreateServiceOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/payscore/serviceorder");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CreateServiceOrderReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询支付分订单接口
        /// <para>用于查询单笔微信支付分订单详细信息。</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_15.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="out_order_no">商户系统内部服务订单号（不是交易单号），与创建订单时一致，商户单号与回跳查询id必填其中一个.不允许都填写或都不填写。</param>
        /// <param name="query_id">微信侧回跳到商户前端时用于查单的单据查询id。详见章节“小程序跳转接口，回跳商户接口”，商户单号与回跳查询id必填其中一个.不允许都填写或都不填写。</param>
        /// <param name="service_id">服务ID,该服务ID有本接口对应产品的权限</param>
        /// <param name="appid"> 微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系，此参数需在本系统先进行配置。</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryServiceOrderReturnJson> QueryServiceOrderAsync(string out_order_no, string query_id, string service_id, string appid, int timeOut = Config.TIME_OUT)
        {
            if ((out_order_no is null && query_id is null) || (out_order_no is not null && query_id is not null))
            {
                throw new TenpayApiRequestException($"{nameof(out_order_no)}与{query_id}必填其中一个.不允许都填写或都不填写");
            }

            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/payscore/serviceorder?service_id={service_id}&appid={appid}");
            url += query_id is not null ? $"&query_id={query_id}" : "";
            url += query_id is not null ? $"&query_id={query_id}" : "";

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryServiceOrderReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 取消支付分订单接口
        /// <para>微信支付分订单创建之后，由于某些原因导致订单不能正常支付时，可使用此接口取消订单。</para>
        /// <para>订单为以下状态时可以取消订单：CREATED（已创单）、DOING（进行中）（包括商户完结支付分订单后，且支付分订单收款状态为待支付USER_PAYING）。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_16.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CancelServiceOrderReturnJson> CancelServiceOrderAsync(CancelServiceOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/payscore/serviceorder/{data.out_order_no}/cancel");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CancelServiceOrderReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 修改订单金额接口
        /// <para>完结订单总金额与实际金额不符时，可通过该接口修改订单金额。</para>
        /// <para>充电宝场景，由于机器计费问题导致商户完结订单时扣除用户99元，用户客诉成功后，商户需要按照实际的消费金额（如10元）扣费，当服务订单支付状态处于“待支付”时，商户可使用此能力修改订单金额。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_17.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ModifyServiceOrderReturnJson> ModifyServiceOrderAsync(ModifyServiceOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/payscore/serviceorder/{data.out_order_no}/modify");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ModifyServiceOrderReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 完结支付分订单接口
        /// <para>完结微信支付分订单。用户使用服务完成后，商户可通过此接口完结订单。</para>
        /// <para>特别说明：完结接口调用成功后，微信支付将自动发起免密代扣。 若扣款失败，微信支付将自动再次发起免密代扣（按照一定频次），直到扣成功为止。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_18.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CompleteServiceOrderReturnJson> CompleteServiceOrderAsync(CompleteServiceOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/payscore/serviceorder/{data.out_order_no}/complete");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CompleteServiceOrderReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 商户发起催收扣款接口
        /// <para>当微信支付分订单支付状态处于“待支付”时，商户可使用该接口向用户发起收款。</para>
        /// <para>特别说明：此能力不影响微信支付分代商户向用户发起收款的策略。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_19.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<PayServiceOrderReturnJson> PayServiceOrderAsync(PayServiceOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            //TODO: 方法命名是否合理?
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/payscore/serviceorder/{data.out_order_no}/pay");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<PayServiceOrderReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 同步服务订单信息接口
        /// <para>由于收款商户进行的某些“线下操作”会导致微信支付侧的订单状态与实际情况不符。例如，用户通过线下付款的方式已经完成支付，而微信支付侧并未支付成功，此时可能导致用户重复支付。因此商户需要通过订单同步接口将订单状态同步给微信支付，修改订单在微信支付系统中的状态。</para>
        /// <para>特别说明：待支付（USER_PAYING）状态下，当用户正在尝试通过收银台主动支付订单金额时，同步服务订单信息API无法调用成功，可等待3min后重试</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_20.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<SyncPayServiceOrderReturnJson> SyncPayServiceOrderAsync(SyncPayServiceOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            //TODO: 方法命名是否合理?
            var url = ReurnPayApiUrl($"https://api.mch.weixin.qq.com/{{0}}v3/payscore/serviceorder/{data.out_order_no}/sync");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<SyncPayServiceOrderReturnJson>(url, data, timeOut);
        }


        #endregion


    }
}
