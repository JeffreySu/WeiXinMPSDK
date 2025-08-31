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
  
    文件名：BankComponentApis.cs
    文件功能描述：微信支付 V3 银行组件 API
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BankComponent
{
    /// <summary>
    /// 微信支付 V3 银行组件 API
    /// <para>银行组件相关的所有接口</para>
    /// </summary>
    public partial class BankComponentApis
    {
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public BankComponentApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        #region 银行查询API

        /// <summary>
        /// 获取对私银行卡号开户银行API
        /// <para>通过银行卡号查询开户银行信息</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_1_1.shtml</para>
        /// </summary>
        /// <param name="data">查询开户银行请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryBankReturnJson> QueryBankAsync(QueryBankRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/capital/capitallhh/banks/personal-banking");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryBankReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询支持个人业务的银行列表API
        /// <para>查询微信支付支持的银行列表</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_1_2.shtml</para>
        /// </summary>
        /// <param name="data">查询银行列表请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryBankListReturnJson> QueryBankListAsync(QueryBankListRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/capital/capitallhh/banks/personal-banking/banks");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryBankListReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 查询省份列表API
        /// <para>查询微信支付支持的省份列表</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_1_3.shtml</para>
        /// </summary>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryProvinceListReturnJson> QueryProvinceListAsync(int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/capital/capitallhh/areas/provinces");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryProvinceListReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 查询城市列表API
        /// <para>根据省份查询城市列表</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_1_4.shtml</para>
        /// </summary>
        /// <param name="data">查询城市列表请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryCityListReturnJson> QueryCityListAsync(QueryCityListRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/capital/capitallhh/areas/provinces/{data.province_code}/cities");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryCityListReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 查询支行列表API
        /// <para>根据银行和城市查询支行列表</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_1_5.shtml</para>
        /// </summary>
        /// <param name="data">查询支行列表请求数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryBranchListReturnJson> QueryBranchListAsync(QueryBranchListRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/capital/capitallhh/banks/{data.bank_alias_code}/branches");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryBranchListReturnJson>(url, data, timeOut);
        }

        #endregion
    }
}
