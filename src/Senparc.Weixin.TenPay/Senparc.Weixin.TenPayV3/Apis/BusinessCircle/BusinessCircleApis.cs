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
  
    文件名：BusinessCircleApis.cs
    文件功能描述：微信支付V3智慧商圈接口
    
    
    创建标识：Senparc - 20210926
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.BusinessCircle;
using Senparc.Weixin.TenPayV3.Apis.Entities;
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
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/businesscircle/user-authorizations/{openid}?appid={appid}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryUserAuthorizationReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

    }
}
