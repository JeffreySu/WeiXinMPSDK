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
  
    文件名：FapiaoApis.cs
    文件功能描述：微信支付 V3 电子发票 API
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Fapiao
{
    /// <summary>
    /// 微信支付 V3 电子发票 API
    /// <para>电子发票相关的所有接口</para>
    /// </summary>
    public partial class FapiaoApis
    {
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public FapiaoApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        #region 公共API

        /// <summary>
        /// 检查子商户开票功能状态API
        /// <para>检查子商户是否开通电子发票功能</para>
        /// </summary>
        /// <param name="data">检查开票功能状态请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CheckFapiaoStatusReturnJson> CheckSubMerchantFapiaoStatusAsync(CheckFapiaoStatusRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/new-tax-control-fapiao/merchant/{data.sub_mchid}/check");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CheckFapiaoStatusReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 创建电子发票卡券模板API
        /// <para>创建电子发票卡券模板，用于生成电子发票</para>
        /// </summary>
        /// <param name="data">创建发票卡券模板请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CreateFapiaoCardTemplateReturnJson> CreateFapiaoCardTemplateAsync(CreateFapiaoCardTemplateRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/new-tax-control-fapiao/card-template");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CreateFapiaoCardTemplateReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询电子发票API
        /// <para>查询已开具的电子发票信息</para>
        /// </summary>
        /// <param name="data">查询电子发票请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryFapiaoReturnJson> QueryFapiaoAsync(QueryFapiaoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/new-tax-control-fapiao/fapiao-applications/{data.fapiao_apply_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryFapiaoReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 获取抬头填写链接API
        /// <para>获取用户填写发票抬头的链接</para>
        /// </summary>
        /// <param name="data">获取抬头填写链接请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<GetTitleUrlReturnJson> GetTitleUrlAsync(GetTitleUrlRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/new-tax-control-fapiao/user-title/title-url");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GetTitleUrlReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 获取用户填写的抬头API
        /// <para>获取用户已填写的发票抬头信息</para>
        /// </summary>
        /// <param name="data">获取用户抬头请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<GetUserTitleReturnJson> GetUserTitleAsync(GetUserTitleRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/new-tax-control-fapiao/user-title");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GetUserTitleReturnJson>(url, data, timeOut);
        }

        #endregion

        #region 区块链电子发票API

        /// <summary>
        /// 获取商户开票基础信息API
        /// <para>获取商户在微信支付开票系统中的基础信息</para>
        /// </summary>
        /// <param name="data">获取开票基础信息请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<GetMerchantInfoReturnJson> GetMerchantInfoAsync(GetMerchantInfoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/new-tax-control-fapiao/merchant/{data.sub_mchid}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GetMerchantInfoReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 开具电子发票API
        /// <para>向税务局请求开具电子发票</para>
        /// </summary>
        /// <param name="data">开具电子发票请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CreateFapiaoReturnJson> CreateFapiaoAsync(CreateFapiaoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/new-tax-control-fapiao/fapiao-applications");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CreateFapiaoReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 冲红电子发票API
        /// <para>冲红已开具的电子发票</para>
        /// </summary>
        /// <param name="data">冲红电子发票请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReverseFapiaoReturnJson> ReverseFapiaoAsync(ReverseFapiaoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/new-tax-control-fapiao/fapiao-applications/{data.fapiao_apply_id}/reverse");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ReverseFapiaoReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 获取发票下载信息API
        /// <para>获取发票文件的下载信息</para>
        /// </summary>
        /// <param name="data">获取发票下载信息请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<GetFapiaoFileReturnJson> GetFapiaoFileAsync(GetFapiaoFileRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/new-tax-control-fapiao/fapiao-applications/{data.fapiao_apply_id}/fapiao-files");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<GetFapiaoFileReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        #endregion
    }
}

