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


    }
}
