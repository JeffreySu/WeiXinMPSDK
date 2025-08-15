#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：MerchantRiskApis.cs
    文件功能描述：微信支付 V3 商户违规通知回调 API
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.MerchantRisk
{
    /// <summary>
    /// 微信支付 V3 商户违规通知回调 API
    /// <para>商户违规通知回调管理相关的所有接口</para>
    /// </summary>
    public partial class MerchantRiskApis
    {
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public MerchantRiskApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        #region 商户违规通知回调API

        /// <summary>
        /// 创建商户违规通知回调地址API
        /// <para>创建商户违规通知回调地址，用于接收微信支付推送的商户违规通知</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_1.shtml</para>
        /// </summary>
        /// <param name="data">创建回调地址请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CreateMerchantRiskNotifyUrlReturnJson> CreateMerchantRiskNotifyUrlAsync(CreateMerchantRiskNotifyUrlRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-risk-manage/violation-notifications");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CreateMerchantRiskNotifyUrlReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询商户违规通知回调地址API
        /// <para>查询商户设置的违规通知回调地址</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_2.shtml</para>
        /// </summary>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryMerchantRiskNotifyUrlReturnJson> QueryMerchantRiskNotifyUrlAsync(int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-risk-manage/violation-notifications");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryMerchantRiskNotifyUrlReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 修改商户违规通知回调地址API
        /// <para>修改商户违规通知回调地址</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_3.shtml</para>
        /// </summary>
        /// <param name="data">修改回调地址请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<UpdateMerchantRiskNotifyUrlReturnJson> UpdateMerchantRiskNotifyUrlAsync(UpdateMerchantRiskNotifyUrlRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-risk-manage/violation-notifications");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<UpdateMerchantRiskNotifyUrlReturnJson>(url, data, timeOut, ApiRequestMethod.PUT);
        }

        /// <summary>
        /// 删除商户违规通知回调地址API
        /// <para>删除商户违规通知回调地址</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_4.shtml</para>
        /// </summary>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<DeleteMerchantRiskNotifyUrlReturnJson> DeleteMerchantRiskNotifyUrlAsync(int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-risk-manage/violation-notifications");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<DeleteMerchantRiskNotifyUrlReturnJson>(url, null, timeOut, ApiRequestMethod.DELETE);
        }

        #endregion
    }
}


