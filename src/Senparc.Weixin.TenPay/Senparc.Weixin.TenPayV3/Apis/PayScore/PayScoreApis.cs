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
  
    文件名：PayScoreApis.cs
    文件功能描述：微信支付V3经营能力接口
    
    
    创建标识：Senparc - 20210926
    
    修改标识：Senparc - 20220202
    修改描述：v0.6.8.14 修复 PayScoreApis.QueryServiceOrderAsync() 重复代码

    修改标识：Senparc - 20220202
    修改描述：v0.6.8.15 修复 PayScoreApis.QueryServiceOrderAsync() 参数判断逻辑

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Apis.PayScore;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
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
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}payscore/serviceorder/direct-complete");
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
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/payscore/permissions");
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
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/payscore/permissions/authorization-code/{authorization_code}&service_id={service_id}");
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
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/payscore/permissions/authorization-code/{data.authorization_code}/terminate");
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
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/payscore/permissions/openid/{openid}?appid={appid}&service_id={service_id}");
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
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/payscore/permissions/openid/{data.openid}/terminate");
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
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/payscore/serviceorder");
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

            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/payscore/serviceorder?service_id={service_id}&appid={appid}");


            url += out_order_no is not null ? $"&out_order_no={out_order_no}" : "";
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
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/payscore/serviceorder/{data.out_order_no}/cancel");
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
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/payscore/serviceorder/{data.out_order_no}/modify");
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
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/payscore/serviceorder/{data.out_order_no}/complete");
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
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/payscore/serviceorder/{data.out_order_no}/pay");
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
            if (data.type == "Order_Paid" && data.detail is null)
            {
                throw new TenpayApiRequestException($"{nameof(data.type)}为'Order_Paid'与{nameof(data.detail)}");
            }

            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/payscore/serviceorder/{data.out_order_no}/sync");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<SyncPayServiceOrderReturnJson>(url, data, timeOut);
        }


        #endregion

        #region 支付即服务

        /// <summary>
        /// 服务人员注册接口
        /// <para>用于商户开发者为商户注册服务人员使用。</para>
        /// <para>注意：调用接口前商家需完成支付即服务产品的开通和设置。若服务商为特约商户调用接口，需在特约商户开通并完成产品设置后，与特约商户建立产品授权关系。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_1.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<RegisterGuideReturnJson> RegisterGuideAsync(RegisterGuideRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/smartguide/guides");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<RegisterGuideReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 服务人员分配接口
        /// <para>用于商户开发者在顾客下单后为顾客分配服务人员使用。</para>
        /// <para>注意：调用服务人员分配接口需在完成支付之前，若分配服务人员晚于支付完成，则将无法在支付凭证上出现服务人员名片入口。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_2.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReturnJsonBase> AssignGuideAsync(AssignGuideRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/smartguide/guides/{data.guide_id}/assign");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut);
        }

        /// <summary>
        /// 服务人员查询接口
        /// <para>用于商户开发者查询已注册的服务人员ID等信息。</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_3.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="store_id">门店在微信支付商户平台的唯一标识</param>
        /// <param name="userid">员工在商户企业微信通讯录使用的唯一标识，企业微信商家可传入该字段查询单个服务人员信息；不传则查询整个门店下的服务人员信息</param>
        /// <param name="mobile">服务人员通过小程序注册时填写的手机号码，企业微信/个人微信商家可传入该字段查询单个服务人员信息；不传则查询整个门店下的服务人员信息。</param>
        /// <param name="work_id">服务人员通过小程序注册时填写的工号，个人微信商家可传入该字段查询单个服务人员信息；不传则查询整个门店下的服务人员信息</param>
        /// <param name="limit">家自定义字段，该次请求可返回的最大资源条数，不大于10，默认值为10</param>
        /// <param name="offset">商家自定义字段，该次请求资源的起始位置，默认值为0</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        [Obsolete("请使用 QueryGuideAsync 接口")]
        public async Task<QueryGuideReturnJson> QueryServiceOrderAsync(string store_id, string userid, string service_id, string mobile, string work_id, int limit = 0, int offset = 0, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/smartguide/guides?store_id={store_id}");
            url += userid is not null ? $"&userid={userid}" : "";
            url += mobile is not null ? $"&mobile={mobile}" : "";//TODO: 敏感信息加密处理
            url += work_id is not null ? $"&work_id={work_id}" : "";

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryGuideReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 服务人员查询接口
        /// <para>用于商户开发者查询已注册的服务人员ID等信息。</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_3.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryGuideReturnJson> QueryGuideAsync(QueryGuideRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/smartguide/guides?store_id={UrlQueryHelper.ToParams(data)}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryGuideReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 服务人员信息更新接口
        /// <para>用于商户开发者为商户更新门店服务人员的姓名、头像等信息</para>
        /// <para>注意：个人微信商家、企业微信商家均支持服务人员信息更新</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_4.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReturnJsonBase> ModifyGuideAsync(ModifyGuideRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/smartguide/guides/{data.guide_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut, ApiRequestMethod.PATCH);
        }


        #endregion

        #region 点金计划
        /// <summary>
        /// 点金计划管理API
        /// <para>服务商为特约商户开通或关闭点金计划。</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<GoldplanChangeGoldplanStatusReturnJson> GoldplanChangeGoldplanStatusAsync(GoldplanChangeGoldplanStatusRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/goldplan/merchants/changegoldplanstatus");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GoldplanChangeGoldplanStatusReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 商家小票管理API
        /// <para>服务商使用此接口为特约商户开通或关闭商家小票功能。</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<GoldplanChangeCustomPageStatusReturnJson> GoldplanChangeCustomPageStatusAsync(GoldplanChangeCustomPageStatusRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/goldplan/merchants/changecustompagestatus");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GoldplanChangeCustomPageStatusReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 同业过滤标签管理API
        /// <para>服务商使用此接口为特约商户配置同业过滤标签，防止特约商户支付后出现同行业的广告内容。</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<GoldplanSetAdvertisingIndustryFilterReturnJson> GoldplanSetAdvertisingIndustryFilterAsync(GoldplanSetAdvertisingIndustryFilterRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/goldplan/merchants/set-advertising-industry-filter");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GoldplanSetAdvertisingIndustryFilterReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 开通广告展示API
        /// <para>此接口为特约商户的点金计划页面开通广告展示功能，可同时配置同业过滤标签，防止特约商户支付后出现同行业的广告内容。</para>
        /// <para>最多可传入3个同业过滤标签值</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<GoldplanOpenAdvertisingShowReturnJson> GoldplanOpenAdvertisingShowAsync(GoldplanOpenAdvertisingShowRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/goldplan/merchants/open-advertising-show");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GoldplanOpenAdvertisingShowReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 关闭广告展示API
        /// <para>使用此接口为特约商户的点金计划页面关闭广告展示功能</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<GoldplanCloseAdvertisingShowReturnJson> GoldplanCloseAdvertisingShowAsync(GoldplanCloseAdvertisingShowRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/goldplan/merchants/close-advertising-show");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GoldplanCloseAdvertisingShowReturnJson>(url, data, timeOut);
        }
        #endregion
    }
}
