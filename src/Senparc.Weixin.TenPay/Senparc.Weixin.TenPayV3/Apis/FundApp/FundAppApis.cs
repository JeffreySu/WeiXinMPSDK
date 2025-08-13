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
  
    文件名：FundAppApis.cs
    文件功能描述：微信支付 V3 资金应用 - 商家转账 API
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    /// <summary>
    /// 微信支付 V3 资金应用 - 商家转账 API
    /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716434 下的【发起转账】所有接口</para>
    /// </summary>
    public partial class FundAppApis
    {
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public FundAppApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        #region 发起转账

        /// <summary>
        /// 发起转账API
        /// <para>商家转账用户确认模式下，用户申请收款时，商户可通过此接口申请创建转账单</para>
        /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716434</para>
        /// </summary>
        /// <param name="data">发起转账请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<TransferBillReturnJson> TransferBillAsync(TransferBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/fund-app/mch-transfer/transfer-bills");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<TransferBillReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 撤销转账API
        /// <para>商户通过转账接口发起付款后，在用户确认收款之前可以通过该接口撤销付款</para>
        /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716458</para>
        /// </summary>
        /// <param name="data">撤销转账请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CancelTransferReturnJson> CancelTransferAsync(CancelTransferRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/fund-app/mch-transfer/transfer-bills/out-bill-no/{data.out_bill_no}/cancel");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CancelTransferReturnJson>(url, null, timeOut, ApiRequestMethod.POST);
        }

        /// <summary>
        /// 商户单号查询转账单API
        /// <para>商户可通过商户单号查询转账单据详情</para>
        /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716437</para>
        /// </summary>
        /// <param name="data">查询转账单请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryTransferReturnJson> QueryTransferByOutBillNoAsync(QueryTransferByOutBillNoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/fund-app/mch-transfer/transfer-bills/out-bill-no/{data.out_bill_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryTransferReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 微信单号查询转账单API
        /// <para>商户可通过微信转账单号查询转账单据详情</para>
        /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716457</para>
        /// </summary>
        /// <param name="data">查询转账单请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryTransferReturnJson> QueryTransferByBillNoAsync(QueryTransferByBillNoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/fund-app/mch-transfer/transfer-bills/transfer-bill-no/{data.transfer_bill_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryTransferReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        #endregion

        #region 获取电子回单

        /// <summary>
        /// 商户单号申请电子回单API
        /// <para>商户可以指定商户转账单号通过该接口申请商家转账用户确认模式转账单据对应的电子回单</para>
        /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716452</para>
        /// </summary>
        /// <param name="data">申请电子回单请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ApplyElecsignReturnJson> ApplyElecsignByOutBillNoAsync(ApplyElecsignByOutBillNoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/fund-app/mch-transfer/elecsign/out-bill-no");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ApplyElecsignReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 商户单号查询电子回单API
        /// <para>商户可以指定商户转账单号通过该接口查询电子回单申请和处理进度</para>
        /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716436</para>
        /// </summary>
        /// <param name="data">查询电子回单请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryElecsignReturnJson> QueryElecsignByOutBillNoAsync(QueryElecsignByOutBillNoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/fund-app/mch-transfer/elecsign/out-bill-no/{data.out_bill_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryElecsignReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 微信单号申请电子回单API
        /// <para>商户可以指定微信转账单号通过该接口申请商家转账用户确认模式转账单据对应的电子回单</para>
        /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716456</para>
        /// </summary>
        /// <param name="data">申请电子回单请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ApplyElecsignReturnJson> ApplyElecsignByBillNoAsync(ApplyElecsignByBillNoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/fund-app/mch-transfer/elecsign/transfer-bill-no");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ApplyElecsignReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 微信单号查询电子回单API
        /// <para>商户可以指定微信转账单号通过该接口查询电子回单申请和处理进度</para>
        /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716455</para>
        /// </summary>
        /// <param name="data">查询电子回单请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryElecsignReturnJson> QueryElecsignByBillNoAsync(QueryElecsignByBillNoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/fund-app/mch-transfer/elecsign/transfer-bill-no/{data.transfer_bill_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryElecsignReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        #endregion
    }
}
