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
  
    文件名：BusinessCircleApis.cs
    文件功能描述：微信支付V3智慧商圈接口
    
    
    创建标识：Senparc - 20210926

    修改标识：Senparc - 20260728
    修改描述：v2.6.0 新增积分提交状态查询和停车状态同步接口
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.BusinessCircle;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付V3智慧商圈接口
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_1.shtml 下的【智慧商圈】所有接口
    /// </summary>
    public partial class BusinessCircleApis
    {

        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public BusinessCircleApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {

            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 商圈积分同步接口
        /// <para>通过此API，商圈商户/服务商可针对微信支付前序推送给商圈系统的顾客商圈内交易通知，告知微信支付系统该笔交易的积分情况。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_6_2.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReturnJsonBase> NotifyBusinessCirclePointsAsync(NotifyBusinessCirclePointsRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/businesscircle/points/notify");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut);
        }

        /// <summary>
        /// 商圈积分授权查询接口
        /// <para>通过积分授权查询API，商圈商户可自行查询用户积分功能开通情况</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_6_4.shtml">更多详细请参考微信支付官方文档</see></para>
        /// </summary>
        /// <param name="appid">顾客授权积分时使用的小程序的appid</param>
        /// <param name="openid">顾客授权时使用的小程序上的openid</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryUserAuthorizationReturnJson> QueryUserAuthorizationAsync(string appid, string openid, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/businesscircle/user-authorizations/{Uri.EscapeDataString(openid ?? string.Empty)}?appid={Uri.EscapeDataString(appid ?? string.Empty)}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryUserAuthorizationReturnJson>(url, null, timeOut, ApiRequestMethod.GET).ConfigureAwait(false);
        }

        /// <summary>
        /// 查询顾客积分提交状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4012534994</para>
        /// </summary>
        /// <param name="openid">顾客在小程序 AppID 下的 OpenID。</param>
        /// <param name="brandid">微信支付分配的品牌 ID。</param>
        /// <param name="appid">顾客授权时使用的小程序 AppID。</param>
        /// <param name="subMchid">服务商模式下的子商户号。</param>
        /// <param name="timeOut">超时时间，单位为毫秒。</param>
        public async Task<QueryPointsCommitStatusReturnJson>
            QueryPointsCommitStatusAsync(string openid, long brandid,
                string appid, string subMchid = null,
                int timeOut = Config.TIME_OUT)
        {
            var query = new List<string>();
            if (!string.IsNullOrWhiteSpace(subMchid))
            {
                query.Add("sub_mchid=" + Uri.EscapeDataString(subMchid));
            }
            query.Add("brandid=" + brandid);
            query.Add("appid=" + Uri.EscapeDataString(appid ?? string.Empty));

            var path = $"v3/businesscircle/users/{Uri.EscapeDataString(openid ?? string.Empty)}/points/commit_status?{string.Join("&", query)}";
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestAsync<QueryPointsCommitStatusReturnJson>(
                url, null, timeOut, ApiRequestMethod.GET).ConfigureAwait(false);
        }

        /// <summary>
        /// 同步智慧商圈停车入场或离场状态。
        /// <para>成功时微信支付返回 HTTP 204，SDK 返回空的 ReturnJsonBase 实例。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4012535502</para>
        /// </summary>
        /// <param name="data">品牌、顾客、车辆及停车状态信息。</param>
        /// <param name="timeOut">超时时间，单位为毫秒。</param>
        public async Task<ReturnJsonBase> SyncParkingStateAsync(
            BusinessCircleParkingRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/businesscircle/parkings");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestAsync<ReturnJsonBase>(url, data, timeOut)
                .ConfigureAwait(false);
        }

    }
}
