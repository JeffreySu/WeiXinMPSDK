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
  
    文件名：MarketingApis.Paygift.cs
    文件功能描述：微信支付V3营销工具接口
    
    
    创建标识：Senparc - 20210821
    
----------------------------------------------------------------*/

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
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_1.shtml 下的【营销工具】所有接口 &gt; 【支付有礼接口】
    /// </summary>
    public partial class MarketingApis
    {
        #region 支付有礼接口

        /// <summary>
        /// 创建全场满额送活动接口
        /// <para>商户可以创建满额送活动，用户支付后送全场券，提升交易额。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_2.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<CreateUniqueThresholdActivityReturnJson> CreateUniqueThresholdActivityAsync(CreateUniqueThresholdActivityRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/marketing/paygiftactivity/unique-threshold-activity");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CreateUniqueThresholdActivityReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询活动详情接口
        /// <para>商户创建活动后，可以通过该接口查询支付有礼的活动详情，用于管理活动。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_4.shtml </para>
        /// </summary>
        /// <param name="activity_id">活动id</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<QueryPaygiftActivityReturnJson> QueryPaygiftActivityAsync(string activity_id, int timeOut = Config.TIME_OUT)
        {

            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/marketing/paygiftactivity/activities/{activity_id}");

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryPaygiftActivityReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 查询活动发券商户号接口
        /// <para>商户创建活动后，可以通过该接口查询支付有礼的发券商户号，用于管理活动。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_5.shtml </para>
        /// </summary>
        /// <param name="activity_id">活动id</param>
        /// <param name="limit">分页大小，最大50。不传默认为20。 默认值：10</param>
        /// <param name="offset">分页页码，页码从0开始。 默认值：0</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<QueryPaygiftActivityMerchantsReturnJson> QueryPaygiftActivityMerchantsAsync(string activity_id, ulong limit = 10, ulong offset = 0, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/marketing/paygiftactivity/activities/{activity_id}/merchants?offset={offset}&limit={limit}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryPaygiftActivityMerchantsReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 查询活动指定商品列表接口
        /// <para>商户创建活动后，可以通过该接口查询支付有礼的活动指定商品，用于管理活动。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_6.shtml </para>
        /// </summary>
        /// <param name="activity_id">活动id</param>
        /// <param name="limit">分页大小，最大50。不传默认为20。 默认值：10</param>
        /// <param name="offset">分页页码，页码从0开始。 默认值：0</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<QueryPaygiftActivityGoodsReturnJson> QueryPaygiftActivityGoodsAsync(string activity_id, ulong limit = 10, ulong offset = 0, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/marketing/paygiftactivity/activities/{activity_id}/goods?offset={offset}&limit={limit}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryPaygiftActivityGoodsReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 终止支付有礼活动接口
        /// <para>商户可以创建满额送活动，用户支付后送全场券，提升交易额。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_7.shtml </para>
        /// </summary>
        /// <param name="activity_id">支付有礼活动id</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<TerminatePaygiftActivityReturnJson> TerminatePaygiftActivityAsync(string activity_id, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/marketing/paygiftactivity/activities/{activity_id}/terminate");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            // TODO: 此处应该注意检查post方法body为null时候有问题 文档确实body没有传任何数据
            return await tenPayApiRequest.RequestAsync<TerminatePaygiftActivityReturnJson>(url, null, timeOut);
        }

        /// <summary>
        /// 删除活动发券商户号接口
        /// <para>商户创建活动后，可以通过该接口增加支付有礼的发券商户号，用于管理活动。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_8.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<AddPaygiftActivityMerchantsReturnJson> AddPaygiftActivityMerchantsAsync(AddPaygiftActivityMerchantsRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/marketing/paygiftactivity/activities/{data.activity_id}/merchants/add");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AddPaygiftActivityMerchantsReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 获取支付有礼活动列表接口
        /// <para>商户根据一定过滤条件，查询已创建的支付有礼活动。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_9.shtml </para>
        /// </summary>
        /// <param name="activity_name">活动名称，支持模糊搜索</param>
        /// <param name="activity_status">活动状态，枚举值：ACT_STATUS_UNKNOWN：状态未知 CREATE_ACT_STATUS：已创建 ONGOING_ACT_STATUS：运行中 TERMINATE_ACT_STATUS：已终止 STOP_ACT_STATUS：已暂停 OVER_TIME_ACT_STATUS：已过期 CREATE_ACT_FAILED：创建活动失败</param>
        /// <param name="award_type"> 奖品类型，暂时只支持商家券 枚举值: BUSIFAVOR：商家券</param>
        /// <param name="limit">分页大小，最大50。不传默认为20。 默认值：10</param>
        /// <param name="offset">分页页码，页码从0开始。 默认值：0</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<QueryPaygiftActivitiesReturnJson> QueryPaygiftActivitiesAsync(string activity_name, string activity_status, string award_type, int limit = 10, int offset = 0, int timeOut = Config.TIME_OUT)
        {

            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/marketing/paygiftactivity/activities?offset={offset}&limit={limit}");

            url += activity_name is not null ? $"&activity_name={activity_name}" : "";
            url += activity_status is not null ? $"&activity_status={activity_status}" : "";
            url += award_type is not null ? $"&award_type={award_type}" : "";

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryPaygiftActivitiesReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 新增活动发券商户号接口
        /// <para>商户创建活动后，可以通过该接口删除支付有礼的发券商户号，用于管理活动。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_10.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<DeletePaygiftActivitiyMerchantsReturnJson> DeletePaygiftActivitiyMerchantsAsync(DeletePaygiftActivitiyMerchantsRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/marketing/paygiftactivity/activities/{data.activity_id}/merchants/delete");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<DeletePaygiftActivitiyMerchantsReturnJson>(url, data, timeOut);
        }

        #endregion
    }
}
