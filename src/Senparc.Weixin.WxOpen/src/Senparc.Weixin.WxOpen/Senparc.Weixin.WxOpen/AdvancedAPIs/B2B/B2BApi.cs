#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License
is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：B2BApi.cs
    文件功能描述：B2BApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.B2B
{
    /// <summary>
    /// 小程序 B2B 门店助手接口。
    /// </summary>
    /// <remarks>本类中的 34 项接口均支持第三方平台使用 <c>authorizer_access_token</c> 代小程序调用。</remarks>
    public static partial class B2BApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 门店授权

        /// <summary>
        /// 申请开通 B2B 门店助手。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">业务覆盖范围、服务类型和联系人信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/store_assistant/api_retailbusinessapply"/>。</remarks>
        public static WxJsonResult ApplyRetailBusiness(string accessTokenOrAppId, B2BRetailBusinessApplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/business/retailbusinessapply", request, timeOut);
        }

        /// <summary>异步申请开通 B2B 门店助手。</summary>
        /// <inheritdoc cref="ApplyRetailBusiness"/>
        public static Task<WxJsonResult> ApplyRetailBusinessAsync(string accessTokenOrAppId, B2BRetailBusinessApplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/business/retailbusinessapply", request, timeOut);
        }

        /// <summary>
        /// 批量预录入门店信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店信息列表，单次最多 100 家。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功数、失败数和失败记录。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/store_assistant/api_batchcreateretail"/>。</remarks>
        public static B2BBatchCreateRetailJsonResult BatchCreateRetail(string accessTokenOrAppId, B2BBatchCreateRetailRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BBatchCreateRetailJsonResult>(accessTokenOrAppId, "/wxa/business/batchcreateretail", request, timeOut);
        }

        /// <summary>异步批量预录入门店信息。</summary>
        /// <inheritdoc cref="BatchCreateRetail"/>
        public static Task<B2BBatchCreateRetailJsonResult> BatchCreateRetailAsync(string accessTokenOrAppId, B2BBatchCreateRetailRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BBatchCreateRetailJsonResult>(accessTokenOrAppId, "/wxa/business/batchcreateretail", request, timeOut);
        }

        /// <summary>
        /// 按 OpenId 或手机号查询门店信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">OpenId 或手机号查询条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>匹配的门店、管理员和员工信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/store_assistant/api_getretailinfo"/>。</remarks>
        public static B2BGetRetailInfoJsonResult GetRetailInfo(string accessTokenOrAppId, B2BGetRetailInfoRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BGetRetailInfoJsonResult>(accessTokenOrAppId, "/wxa/business/getretailinfo", request, timeOut);
        }

        /// <summary>异步按 OpenId 或手机号查询门店信息。</summary>
        /// <inheritdoc cref="GetRetailInfo"/>
        public static Task<B2BGetRetailInfoJsonResult> GetRetailInfoAsync(string accessTokenOrAppId, B2BGetRetailInfoRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BGetRetailInfoJsonResult>(accessTokenOrAppId, "/wxa/business/getretailinfo", request, timeOut);
        }

        /// <summary>
        /// 分页查询全量已授权门店 OpenId。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">分页数量和分页上下文。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>OpenId 列表和下一页上下文。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/store_assistant/api_getretailopenidlist"/>。</remarks>
        public static B2BGetRetailOpenIdListJsonResult GetRetailOpenIdList(string accessTokenOrAppId, B2BGetRetailOpenIdListRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BGetRetailOpenIdListJsonResult>(accessTokenOrAppId, "/wxa/business/getretailopenidlist", request, timeOut);
        }

        /// <summary>异步分页查询全量已授权门店 OpenId。</summary>
        /// <inheritdoc cref="GetRetailOpenIdList"/>
        public static Task<B2BGetRetailOpenIdListJsonResult> GetRetailOpenIdListAsync(string accessTokenOrAppId, B2BGetRetailOpenIdListRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BGetRetailOpenIdListJsonResult>(accessTokenOrAppId, "/wxa/business/getretailopenidlist", request, timeOut);
        }

        #endregion

        #region 门店消息

        /// <summary>
        /// 向门店负责人批量发送 B2B 模板消息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">模板类型、接收人和 JSON 格式消息内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>消息下发结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/notify/api_retailnotifybusiness"/>。</remarks>
        public static WxJsonResult SendRetailNotification(string accessTokenOrAppId, B2BSendRetailNotificationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/business/retailnotifybusiness", request, timeOut);
        }

        /// <summary>异步向门店负责人批量发送 B2B 模板消息。</summary>
        /// <inheritdoc cref="SendRetailNotification"/>
        public static Task<WxJsonResult> SendRetailNotificationAsync(string accessTokenOrAppId, B2BSendRetailNotificationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/business/retailnotifybusiness", request, timeOut);
        }

        /// <summary>
        /// 分页查询 B2B 门店消息效果数据。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">分页位置和日期范围。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>消息发送人数、进入人数等效果数据。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/notify/api_getretailmessagelist"/>。</remarks>
        public static B2BGetRetailMessageListJsonResult GetRetailMessageList(string accessTokenOrAppId, B2BGetRetailMessageListRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BGetRetailMessageListJsonResult>(accessTokenOrAppId, "/wxa/business/getretailmessagelist", request, timeOut);
        }

        /// <summary>异步分页查询 B2B 门店消息效果数据。</summary>
        /// <inheritdoc cref="GetRetailMessageList"/>
        public static Task<B2BGetRetailMessageListJsonResult> GetRetailMessageListAsync(string accessTokenOrAppId, B2BGetRetailMessageListRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BGetRetailMessageListJsonResult>(accessTokenOrAppId, "/wxa/business/getretailmessagelist", request, timeOut);
        }

        #endregion

        private static T Send<T>(string accessTokenOrAppId, string path, object request, int timeOut, string paySig = null)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken, BuildUrl(path, paySig), request,
                    CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting),
                accessTokenOrAppId);
        }

        private static Task<T> SendAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut, string paySig = null)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<T>(accessToken, BuildUrl(path, paySig), request,
                    CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false),
                accessTokenOrAppId);
        }

        private static string BuildUrl(string path, string paySig)
        {
            var url = Config.ApiMpHost + path + "?access_token={0}";
            if (!string.IsNullOrEmpty(paySig))
            {
                url += "&pay_sig=" + paySig.AsUrlData();
            }

            return url;
        }
    }
}
