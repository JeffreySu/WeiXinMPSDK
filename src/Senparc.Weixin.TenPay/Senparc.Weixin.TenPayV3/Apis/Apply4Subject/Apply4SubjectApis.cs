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
  
    文件名：Apply4SubjectApis.cs
    文件功能描述：微信支付 V3 商户开户意愿确认 API
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Subject
{
    /// <summary>
    /// 微信支付 V3 商户开户意愿确认 API
    /// <para>商户开户意愿确认相关的所有接口</para>
    /// </summary>
    public partial class Apply4SubjectApis
    {
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public Apply4SubjectApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        #region 商户开户意愿确认API

        /// <summary>
        /// 提交申请单API
        /// <para>商户提交开户意愿确认申请</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter11_2_1.shtml</para>
        /// </summary>
        /// <param name="data">申请单数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<Apply4SubjectApplymentReturnJson> Apply4SubjectApplymentAsync(Apply4SubjectApplymentRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/apply4subject/applyment");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<Apply4SubjectApplymentReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 撤销申请单API
        /// <para>撤销已提交的开户意愿确认申请</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter11_2_2.shtml</para>
        /// </summary>
        /// <param name="data">撤销申请单数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CancelApply4SubjectApplymentReturnJson> CancelApply4SubjectApplymentAsync(CancelApply4SubjectApplymentRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/apply4subject/applyment/{data.applyment_id}/cancel");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CancelApply4SubjectApplymentReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 通过申请单号查询申请状态API
        /// <para>通过申请单号查询开户意愿确认申请状态</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter11_2_3.shtml</para>
        /// </summary>
        /// <param name="data">查询申请状态数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryApply4SubjectApplymentReturnJson> QueryApply4SubjectApplymentByIdAsync(QueryApply4SubjectApplymentByIdRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/apply4subject/applyment/applyment_id/{data.applyment_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryApply4SubjectApplymentReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 通过业务申请编号查询申请状态API
        /// <para>通过业务申请编号查询开户意愿确认申请状态</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter11_2_4.shtml</para>
        /// </summary>
        /// <param name="data">查询申请状态数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryApply4SubjectApplymentReturnJson> QueryApply4SubjectApplymentByOutRequestNoAsync(QueryApply4SubjectApplymentByOutRequestNoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/apply4subject/applyment/out_request_no/{data.out_request_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryApply4SubjectApplymentReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        #endregion
    }
}



