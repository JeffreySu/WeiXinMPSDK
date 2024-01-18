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
  
    文件名：ComplaintApis.cs
    文件功能描述：微信支付V3消费者投诉2.0接口
    
    
    创建标识：Senparc - 20210926
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Apis.Complaint;
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
    /// 微信支付V3消费者投诉2.0接口
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_11.shtml 下的【消费者投诉2.0】所有接口
    /// </summary>
    public partial class ComplaintApis
    {

        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public ComplaintApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {

            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 查询投诉单列表接口
        /// <para>商户可通过调用此接口，查询指定时间段的所有用户投诉信息，以分页输出查询结果。对于服务商、渠道商，可通过调用此接口，查询指定子商户号对应子商户的投诉信息，若不指定则查询所有子商户投诉信息。</para>
        /// <para>注意：商户上送敏感信息时使用微信支付平台公钥加密，证书序列号包含在请求HTTP头部的Wechatpay-Serial，详见接口规则</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_11.shtml </para>
        /// </summary>
        /// <param name="begin_date">投诉发生的开始日期，格式为YYYY-MM-DD。注意，查询日期跨度不超过30天，当前查询为实时查询</param>
        /// <param name="end_date">投诉发生的结束日期，格式为YYYY-MM-DD。注意，查询日期跨度不超过30天，当前查询为实时查询</param>
        /// <param name="complainted_mchid">投诉单对应的被诉商户号, 可为null</param>
        /// <param name="limit">设置该次请求返回的最大投诉条数，范围【1,50】,商户自定义字段，不传默认为10。注：如遇到提示“当前查询结果数据量过大”，是回包触发微信支付下行数据包大小限制，请缩小入参limit并重试。</param>
        /// <param name="offset">该次请求的分页开始位置，从0开始计数，例如offset=10，表示从第11条记录开始返回，不传默认为0 。</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryComplaintsReturnJson> QueryComplaintsAsync(TenpayDateTime begin_date, TenpayDateTime end_date, string complainted_mchid, int limit = 10, int offset = 0, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-service/complaints-v2?limit={limit}&offset={offset}&begin_date={begin_date?.ToString()}&end_date={end_date?.ToString()}");
            url += complainted_mchid is not null ? $"&complainted_mchid={complainted_mchid}" : "";

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryComplaintsReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 查询投诉单详情接口
        /// <para>商户可通过调用此接口，查询指定投诉单的用户投诉详情，包含投诉内容、投诉关联订单、投诉人联系方式等信息，方便商户处理投诉。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_13.shtml </para>
        /// </summary>
        /// <param name="complaint_id">投诉单对应的投诉单号</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryComplaintReturnJson> QueryComplaintAsync(string complaint_id, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-service/complaints-v2/{complaint_id}");

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryComplaintReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 查询投诉协商历史接口
        /// <para>商户可通过调用此接口，查询指定投诉的用户商户协商历史，以分页输出查询结果，方便商户根据处理历史来制定后续处理方案。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_12.shtml </para>
        /// </summary>
        /// <param name="complaint_id">投诉单对应的投诉单号</param>
        /// <param name="limit">设置该次请求返回的最大协商历史条数，范围[1,300]，不传默认为100。 注：如遇到提示“当前查询结果数据量过大”，是回包触发微信支付下行数据包大小限制，请缩小入参limit并重试。</param>
        /// <param name="offset">该次请求的分页开始位置，从0开始计数，例如offset=10，表示从第11条记录开始返回，不传默认为0。</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryNegotiationHistorysReturnJson> QueryNegotiationHistorysAsync(string complaint_id, int limit = 10, int offset = 0, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-service/complaints-v2/{complaint_id}/negotiation-historys?limit={limit}&offset={offset}");

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryNegotiationHistorysReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 创建投诉通知回调地址接口
        /// <para>商户通过调用此接口创建投诉通知回调URL，当用户产生新投诉且投诉状态已变更时，微信支付会通过回 调URL通知商户。对于服务商、渠道商，会收到所有子商户的投诉信息推送。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_2.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CreateComplaintNotifyUrlReturnJson> CreateComplaintNotifyUrlAsync(CreateComplaintNotifyUrlRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/merchant-service/complaint-notifications");

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CreateComplaintNotifyUrlReturnJson>(url, data, timeOut);
        }


        /// <summary>
        /// 查询投诉通知回调地址接口
        /// <para>商户通过调用此接口查询投诉通知的回调URL。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_3.shtml </para>
        /// </summary>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryComplaintNotifyUrlReturnJson> QueryComplaintNotifyUrlAsync(int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/merchant-service/complaint-notifications");

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryComplaintNotifyUrlReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 更新投诉通知回调地址接口
        /// <para>商户通过调用此接口更新投诉通知的回调URL。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_4.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ModifyComplaintNotifyUrlReturnJson> ModifyComplaintNotifyUrlAsync(ModifyComplaintNotifyUrlRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/merchant-service/complaint-notifications");

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ModifyComplaintNotifyUrlReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 删除投诉通知回调地址接口
        /// <para>当商户不再需要推送通知时，可通过调用此接口删除投诉通知的回调URL，取消通知回调。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_5.shtml </para>
        /// </summary>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReturnJsonBase> DeleteComplaintNotifyUrlAsync(int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/merchant-service/complaint-notifications");
            //TODO: 此处新增DELETE方法 待测试是否有问题
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, null, timeOut, ApiRequestMethod.DELETE);
        }

        /// <summary>
        /// 提交回复接口
        /// <para>商户可通过调用此接口，提交回复内容。其中上传图片凭证需首先调用商户上传反馈图片接口，得到图片id，再将id填入请求。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_14.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReturnJsonBase> ResponseAsync(ResponseRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-service/complaints-v2/{data.complaint_id}/response");

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut);
        }

        /// <summary>
        /// 反馈处理完成接口
        /// <para>商户可通过调用此接口，反馈投诉单已处理完成。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_15.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<ReturnJsonBase> CompleteComplaintAsync(CompleteComplaintRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-service/complaints-v2/{data.complaint_id}/complete");

            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<ReturnJsonBase>(url, data, timeOut);
        }

        //TODO: 图片上传接口
    }
}
