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
  
    文件名：Apply4SubApis.cs
    文件功能描述：微信支付 V3 服务商进件 API
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Sub
{
    /// <summary>
    /// 微信支付 V3 服务商进件 API
    /// <para>服务商模式下特约商户进件相关的所有接口</para>
    /// </summary>
    public partial class Apply4SubApis
    {
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public Apply4SubApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        #region 特约商户进件API

        /// <summary>
        /// 提交申请单API
        /// <para>服务商为特约商户提交进件申请</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_1.shtml</para>
        /// </summary>
        /// <param name="data">申请单数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<Apply4SubApplymentReturnJson> Apply4SubApplymentAsync(Apply4SubApplymentRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/apply4sub/applyment/");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<Apply4SubApplymentReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 通过申请单号查询申请状态API
        /// <para>通过申请单号查询特约商户申请状态</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_2.shtml</para>
        /// </summary>
        /// <param name="data">查询申请状态数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryApply4SubApplymentReturnJson> QueryApply4SubApplymentByIdAsync(QueryApply4SubApplymentByIdRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/apply4sub/applyment/applyment_id/{data.applyment_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryApply4SubApplymentReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 通过业务申请编号查询申请状态API
        /// <para>通过业务申请编号查询特约商户申请状态</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_3.shtml</para>
        /// </summary>
        /// <param name="data">查询申请状态数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryApply4SubApplymentReturnJson> QueryApply4SubApplymentByOutRequestNoAsync(QueryApply4SubApplymentByOutRequestNoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/apply4sub/applyment/out_request_no/{data.out_request_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryApply4SubApplymentReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 修改结算账号API
        /// <para>修改特约商户的结算账号信息</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_4.shtml</para>
        /// </summary>
        /// <param name="data">修改结算账号数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ModifyApply4SubSettlementReturnJson> ModifyApply4SubSettlementAsync(ModifyApply4SubSettlementRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/apply4sub/sub_merchants/{data.sub_mchid}/modify-settlement");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ModifyApply4SubSettlementReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询结算账号API
        /// <para>查询特约商户的结算账号信息</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_5.shtml</para>
        /// </summary>
        /// <param name="data">查询结算账号数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryApply4SubSettlementReturnJson> QueryApply4SubSettlementAsync(QueryApply4SubSettlementRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/apply4sub/sub_merchants/{data.sub_mchid}/settlement");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryApply4SubSettlementReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        #endregion
    }
}



