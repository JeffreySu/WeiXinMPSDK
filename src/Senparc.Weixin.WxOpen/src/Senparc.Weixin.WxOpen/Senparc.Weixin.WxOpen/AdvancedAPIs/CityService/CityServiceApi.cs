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

    文件名：CityServiceApi.cs
    文件功能描述：CityServiceApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.CityService
{
    /// <summary>微信城市服务、就医助手和长辈就医服务端接口。</summary>
    /// <remarks>
    /// 本组接口混合使用小程序和公众号 AccessToken，部分接口不支持第三方平台代调用。
    /// 因此所有方法均接收调用方已经取得的 AccessToken，不根据 AppId 自动换取令牌；具体账号类型和代调用限制见各方法注释。
    /// </remarks>
    public static class CityServiceApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        /// <summary>获取城市服务限定页面链接。</summary>
        /// <param name="accessToken">提供城市服务的公众号或小程序 AccessToken；代调用时可传 authorizer_access_token。</param>
        /// <param name="request">页面类型、来源渠道和页面业务参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>城市服务页面路径、业务类型及跳转参数。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 22 或 105。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/basic/api_cityserviceservicehomepath"/>。</remarks>
        public static CityServiceGetServicePathJsonResult GetServicePath(string accessToken, CityServiceGetServicePathRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<CityServiceGetServicePathJsonResult>(accessToken, "/cityservice/getservicepath", request, timeOut);
        }

        /// <summary>异步获取城市服务限定页面链接。</summary>
        /// <inheritdoc cref="GetServicePath"/>
        public static Task<CityServiceGetServicePathJsonResult> GetServicePathAsync(string accessToken, CityServiceGetServicePathRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<CityServiceGetServicePathJsonResult>(accessToken, "/cityservice/getservicepath", request, timeOut);
        }

        /// <summary>通过城市服务消息通路发送消息。</summary>
        /// <typeparam name="TData">城市服务分配模板对应的数据结构。</typeparam>
        /// <param name="accessToken">公众号 AccessToken；通过小程序提供服务时必须使用关联公众号的 AccessToken，代调用时可传其 authorizer_access_token。</param>
        /// <param name="request">用户、模板、订单、跳转地址及动态模板数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>消息发送结果及可选的结果页 URL。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 22 或 105。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/basic/api_cityservice_sendmsgdata"/>。</remarks>
        public static CityServiceSendMessageDataJsonResult SendMessageData<TData>(string accessToken, CityServiceSendMessageDataRequest<TData> request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<CityServiceSendMessageDataJsonResult>(accessToken, "/cityservice/sendmsgdata", request, timeOut);
        }

        /// <summary>异步通过城市服务消息通路发送消息。</summary>
        /// <inheritdoc cref="SendMessageData{TData}"/>
        public static Task<CityServiceSendMessageDataJsonResult> SendMessageDataAsync<TData>(string accessToken, CityServiceSendMessageDataRequest<TData> request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<CityServiceSendMessageDataJsonResult>(accessToken, "/cityservice/sendmsgdata", request, timeOut);
        }

        /// <summary>校验城市服务实名信息。</summary>
        /// <param name="accessToken">申请实名校验权限的小程序 AccessToken。</param>
        /// <param name="request">用户 OpenId、姓名、证件和实名校验 Code。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>OpenId 和姓名证件信息的校验结果。</returns>
        /// <remarks>本接口不支持第三方平台代调用。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/basic/api_checkrealnameinfo"/>。</remarks>
        public static CityServiceCheckRealNameJsonResult CheckRealName(string accessToken, CityServiceCheckRealNameRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<CityServiceCheckRealNameJsonResult>(accessToken, "/intp/realname/checkrealnameinfo", request, timeOut);
        }

        /// <summary>异步校验城市服务实名信息。</summary>
        /// <inheritdoc cref="CheckRealName"/>
        public static Task<CityServiceCheckRealNameJsonResult> CheckRealNameAsync(string accessToken, CityServiceCheckRealNameRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<CityServiceCheckRealNameJsonResult>(accessToken, "/intp/realname/checkrealnameinfo", request, timeOut);
        }

        /// <summary>获取交通出行仿原生业务页面参数。</summary>
        /// <param name="accessToken">已开通交通出行能力的小程序 AccessToken。</param>
        /// <param name="request">需要打开的仿原生页面类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>仿原生业务类型、调用参数和到期时间。</returns>
        /// <remarks>本接口不支持第三方平台代调用。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/basic/api_transportcode_getbusinessview"/>。</remarks>
        public static CityServiceBusinessViewJsonResult GetBusinessView(string accessToken, CityServiceBusinessViewRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<CityServiceBusinessViewJsonResult>(accessToken, "/intp/transportcode/getbusinessview", request, timeOut);
        }

        /// <summary>异步获取交通出行仿原生业务页面参数。</summary>
        /// <inheritdoc cref="GetBusinessView"/>
        public static Task<CityServiceBusinessViewJsonResult> GetBusinessViewAsync(string accessToken, CityServiceBusinessViewRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<CityServiceBusinessViewJsonResult>(accessToken, "/intp/transportcode/getbusinessview", request, timeOut);
        }

        /// <summary>推送微信就医助手消息。</summary>
        /// <typeparam name="TBusinessInfo">当前消息状态对应的业务信息类型。</typeparam>
        /// <param name="accessToken">已开通就医助手的同主体公众号 AccessToken；代调用时可传 authorizer_access_token。</param>
        /// <param name="request">用户、订单、消息状态及对应业务信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 22、105、113 或 134。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/medicalassistant/api_cityservice_sendchannelmsg"/>。</remarks>
        public static WxJsonResult SendMedicalMessage<TBusinessInfo>(string accessToken, CityServiceMedicalMessageRequest<TBusinessInfo> request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessToken, "/cityservice/sendchannelmsg", request, timeOut);
        }

        /// <summary>异步推送微信就医助手消息。</summary>
        /// <inheritdoc cref="SendMedicalMessage{TBusinessInfo}"/>
        public static Task<WxJsonResult> SendMedicalMessageAsync<TBusinessInfo>(string accessToken, CityServiceMedicalMessageRequest<TBusinessInfo> request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessToken, "/cityservice/sendchannelmsg", request, timeOut);
        }

        /// <summary>查询长辈就医授权的实名信息。</summary>
        /// <param name="accessToken">已开通长辈就医能力的公众号 AccessToken；代调用时可传 authorizer_access_token。</param>
        /// <param name="request">业务方 AppId、用户 OpenId 和实名授权 Code。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>加密实名信息、算法和密钥版本。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 134。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/elderMedical/api_cityservice_getmedrealname"/>。</remarks>
        public static CityServiceGetMedicalRealNameJsonResult GetMedicalRealName(string accessToken, CityServiceGetMedicalRealNameRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<CityServiceGetMedicalRealNameJsonResult>(accessToken, "/cityservice/getmedrealname", request, timeOut);
        }

        /// <summary>异步查询长辈就医授权的实名信息。</summary>
        /// <inheritdoc cref="GetMedicalRealName"/>
        public static Task<CityServiceGetMedicalRealNameJsonResult> GetMedicalRealNameAsync(string accessToken, CityServiceGetMedicalRealNameRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<CityServiceGetMedicalRealNameJsonResult>(accessToken, "/cityservice/getmedrealname", request, timeOut);
        }

        /// <summary>查询用户是否开通长辈就医消息服务。</summary>
        /// <param name="accessToken">公众号 AccessToken；代调用时可传 authorizer_access_token。</param>
        /// <param name="request">业务 ID 和用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>用户是否已经开通订阅。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 22、105、113 或 134。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/elderMedical/api_cityservice_getmsgrelation"/>。</remarks>
        public static CityServiceGetMessageRelationJsonResult GetMessageRelation(string accessToken, CityServiceGetMessageRelationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<CityServiceGetMessageRelationJsonResult>(accessToken, "/cityservice/getmsgrelation", request, timeOut);
        }

        /// <summary>异步查询用户是否开通长辈就医消息服务。</summary>
        /// <inheritdoc cref="GetMessageRelation"/>
        public static Task<CityServiceGetMessageRelationJsonResult> GetMessageRelationAsync(string accessToken, CityServiceGetMessageRelationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<CityServiceGetMessageRelationJsonResult>(accessToken, "/cityservice/getmsgrelation", request, timeOut);
        }

        /// <summary>查询医院最近五条公告。</summary>
        /// <param name="accessToken">已开通长辈就医能力的公众号 AccessToken；代调用时可传 authorizer_access_token。</param>
        /// <param name="request">业务方 AppId 和公告类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>医院公告及草稿预览用户列表。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 134。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/elderMedical/api_intp_eldermed_gethospnoticelist"/>。</remarks>
        public static CityServiceGetHospitalNoticeListJsonResult GetHospitalNoticeList(string accessToken, CityServiceGetHospitalNoticeListRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<CityServiceGetHospitalNoticeListJsonResult>(accessToken, "/intp/eldermedical/gethospnoticelist", request, timeOut);
        }

        /// <summary>异步查询医院最近五条公告。</summary>
        /// <inheritdoc cref="GetHospitalNoticeList"/>
        public static Task<CityServiceGetHospitalNoticeListJsonResult> GetHospitalNoticeListAsync(string accessToken, CityServiceGetHospitalNoticeListRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<CityServiceGetHospitalNoticeListJsonResult>(accessToken, "/intp/eldermedical/gethospnoticelist", request, timeOut);
        }

        /// <summary>添加或移除医院公告草稿预览权限。</summary>
        /// <param name="accessToken">已开通长辈就医能力的公众号 AccessToken；代调用时可传 authorizer_access_token。</param>
        /// <param name="request">公告、预览用户和添加或删除操作。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>被操作的公告 ID。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 134。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/elderMedical/api_previewhopsnotice"/>。</remarks>
        public static CityServiceNoticeIdJsonResult SetHospitalNoticePreview(string accessToken, CityServicePreviewHospitalNoticeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<CityServiceNoticeIdJsonResult>(accessToken, "/intp/eldermedical/previewhopsnotice", request, timeOut);
        }

        /// <summary>异步添加或移除医院公告草稿预览权限。</summary>
        /// <inheritdoc cref="SetHospitalNoticePreview"/>
        public static Task<CityServiceNoticeIdJsonResult> SetHospitalNoticePreviewAsync(string accessToken, CityServicePreviewHospitalNoticeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<CityServiceNoticeIdJsonResult>(accessToken, "/intp/eldermedical/previewhopsnotice", request, timeOut);
        }

        /// <summary>正式发布医院公告。</summary>
        /// <param name="accessToken">已开通长辈就医能力的公众号 AccessToken；代调用时可传 authorizer_access_token。</param>
        /// <param name="request">业务方 AppId、公告类型和公告 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已发布的公告 ID。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 134。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/elderMedical/api_intp_eldermed_publichopsnotice"/>。</remarks>
        public static CityServiceNoticeIdJsonResult PublishHospitalNotice(string accessToken, CityServicePublishHospitalNoticeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<CityServiceNoticeIdJsonResult>(accessToken, "/intp/eldermedical/publichopsnotice", request, timeOut);
        }

        /// <summary>异步正式发布医院公告。</summary>
        /// <inheritdoc cref="PublishHospitalNotice"/>
        public static Task<CityServiceNoticeIdJsonResult> PublishHospitalNoticeAsync(string accessToken, CityServicePublishHospitalNoticeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<CityServiceNoticeIdJsonResult>(accessToken, "/intp/eldermedical/publichopsnotice", request, timeOut);
        }

        /// <summary>新增或覆盖医院公告草稿。</summary>
        /// <param name="accessToken">已开通长辈就医能力的公众号 AccessToken；代调用时可传 authorizer_access_token。</param>
        /// <param name="request">业务方 AppId、公告类型、内容及可选公告 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新增或覆盖的公告 ID。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 134。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/cityservice/elderMedical/api_sethopsnotice"/>。</remarks>
        public static CityServiceNoticeIdJsonResult SetHospitalNotice(string accessToken, CityServiceSetHospitalNoticeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<CityServiceNoticeIdJsonResult>(accessToken, "/intp/eldermedical/sethopsnotice", request, timeOut);
        }

        /// <summary>异步新增或覆盖医院公告草稿。</summary>
        /// <inheritdoc cref="SetHospitalNotice"/>
        public static Task<CityServiceNoticeIdJsonResult> SetHospitalNoticeAsync(string accessToken, CityServiceSetHospitalNoticeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<CityServiceNoticeIdJsonResult>(accessToken, "/intp/eldermedical/sethopsnotice", request, timeOut);
        }

        private static T SendPost<T>(string accessToken, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            var url = Config.ApiMpHost + path + "?access_token=" + accessToken.AsUrlData();
            return CommonJsonSend.Send<T>(null, url, request ?? new { }, CommonJsonSendType.POST,
                timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        private static Task<T> SendPostAsync<T>(string accessToken, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            var url = Config.ApiMpHost + path + "?access_token=" + accessToken.AsUrlData();
            return CommonJsonSend.SendAsync<T>(null, url, request ?? new { }, CommonJsonSendType.POST,
                timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }
    }
}
