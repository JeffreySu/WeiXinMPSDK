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
  
    文件名：MarketingApis.Partnerships.cs
    文件功能描述：微信支付V3营销工具接口
    
    
    创建标识：Senparc - 20210821
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.TenPayV3.Apis.Marketing;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付V3营销工具接口
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_1.shtml 下的【营销工具】所有接口 &gt; 【代金券接口】
    /// </summary>
    public partial class MarketingApis
    {
        #region 委托营销接口

        /// <summary>
        /// 建立合作关系接口
        /// <para>该接口主要为商户提供营销资源的授权能力，可授权给其他商户或小程序，方便商户间的互利合作。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_5_1.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<BuildPartnershipsReturnJson> BuildPartnershipsAsync(BuildPartnershipsRequestData data, int timeOut = Config.TIME_OUT)
        {
            if (data.partner.type == "APPID" && (data.partner.appid is null || data.partner.merchant_id is not null))
            {
                throw new TenpayApiRequestException($"当 {nameof(data.partner.type)} 为 {data.partner.type} 时，{nameof(data.partner.appid)} 必填！，且{nameof(data.partner.merchant_id)}为null！");
            }
            if (data.partner.type == "MERCHANT" && (data.partner.appid is not null || data.partner.merchant_id is null))
            {
                throw new TenpayApiRequestException($"当 {nameof(data.partner.type)} 为 {data.partner.type} 时，{nameof(data.partner.merchant_id)} 必填！，且{nameof(data.partner.merchant_id)}为null！");
            }

            var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/marketing/partnerships/build");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<BuildPartnershipsReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 终止合作关系接口
        /// <para>该接口主要为商户提供营销资源的终止授权能力，便于商户管理运营现存的合作关系。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_5_2.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<TerminatePartnershipsReturnJson> TerminatePartnershipsAsync(TerminatePartnershipsRequestData data, int timeOut = Config.TIME_OUT)
        {

            var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/marketing/partnerships/terminate");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<TerminatePartnershipsReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询合作关系接口
        /// <para>该接口主要为商户提供合作关系列表的查询能力。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_5_3.shtml </para>
        /// </summary>
        /// <param name="data">查询合作关系需要的Data数据</param>
        /// <param name="limit">分页大小<para>query分页大小，最大50。不传默认为20。</para><para>示例值：5</para></param>
        /// <param name="offset">分页页码<para>query分页页码，页码从0开始。</para><para>示例值：10</para></param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryPartnershipsReturnJson> QueryPartnershipsAsync(QueryPartnershipsRequestData data, ulong limit = 20, ulong offset = 0, int timeOut = Config.TIME_OUT)
        {
            // TODO: 此处序列化Json需测试时候可以换行问题
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/marketing/partnerships?authorized_data={data.authorized_data.ToJson()}&partner={data.partner.ToJson()}&offset={offset}&limit={limit}".UrlEncode());
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryPartnershipsReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        #endregion
    }
}
