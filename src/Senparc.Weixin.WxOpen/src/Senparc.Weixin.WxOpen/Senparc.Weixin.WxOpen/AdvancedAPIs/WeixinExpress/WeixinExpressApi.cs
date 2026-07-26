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

    文件名：WeixinExpressApi.cs
    文件功能描述：WeixinExpressApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress
{
    /// <summary>
    /// 微信物流服务查询组件和消息组件接口。
    /// </summary>
    /// <remarks>
    /// 本类覆盖官方两个组件中的 8 个目录项；“获取运力 ID 列表”在两处复用同一路径，因此共对应 7 个唯一接口。
    /// 所有接口均支持第三方平台代商家调用，可以传入 authorizer_access_token。
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static partial class WeixinExpressApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 同步方法

        /// <summary>
        /// 使用查询组件查询运单详情。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">运单查询 Token 及可选的用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运单状态、商品和运力信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-search/api_query_trace"/>。</remarks>
        public static WeixinExpressQueryTraceJsonResult QueryTrace(string accessTokenOrAppId, WeixinExpressQueryTraceRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressQueryTraceJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/query_trace", request, timeOut);
        }

        /// <summary>
        /// 获取微信物流服务支持的运力 ID 列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运力公司列表和数量。</returns>
        /// <remarks>
        /// 查询组件和消息组件共享本接口。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-msg/api_get_delivery_list"/>。
        /// </remarks>
        public static WeixinExpressDeliveryListJsonResult GetDeliveryList(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressDeliveryListJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/get_delivery_list", new { }, timeOut);
        }

        /// <summary>
        /// 使用查询组件向微信上传交易单对应的运单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">用户、运单、商品、交易和运力信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>后续查询使用的运单 Token。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-search/api_trace_waybill"/>。</remarks>
        public static WeixinExpressTraceWaybillJsonResult TraceWaybill(string accessTokenOrAppId, WeixinExpressTraceWaybillRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressTraceWaybillJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/trace_waybill", request, timeOut);
        }

        /// <summary>
        /// 更新查询组件运单的商品信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">运单 Token、用户及更新后的商品信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-search/api_update_waybill_goods"/>。</remarks>
        public static WxJsonResult UpdateWaybillGoods(string accessTokenOrAppId, WeixinExpressUpdateGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/update_waybill_goods", request, timeOut);
        }

        /// <summary>
        /// 更新消息组件运单的商品信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">运单 Token、用户及更新后的商品信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-msg/api_update_follow_waybill_goods"/>。</remarks>
        public static WxJsonResult UpdateFollowWaybillGoods(string accessTokenOrAppId, WeixinExpressUpdateGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/update_follow_waybill_goods", request, timeOut);
        }

        /// <summary>
        /// 使用消息组件查询运单详情。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">运单查询 Token 及可选的用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运单状态、商品和运力信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-msg/api_query_follow_trace"/>。</remarks>
        public static WeixinExpressQueryTraceJsonResult QueryFollowTrace(string accessTokenOrAppId, WeixinExpressQueryTraceRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressQueryTraceJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/query_follow_trace", request, timeOut);
        }

        /// <summary>
        /// 使用消息组件向微信上传交易单对应的运单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">用户、运单、商品、交易和运力信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>后续查询使用的运单 Token。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-msg/api_follow_waybill"/>。</remarks>
        public static WeixinExpressTraceWaybillJsonResult FollowWaybill(string accessTokenOrAppId, WeixinExpressTraceWaybillRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressTraceWaybillJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/follow_waybill", request, timeOut);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 异步使用查询组件查询运单详情。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">运单查询 Token 及可选的用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运单状态、商品和运力信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-search/api_query_trace"/>。</remarks>
        public static Task<WeixinExpressQueryTraceJsonResult> QueryTraceAsync(string accessTokenOrAppId, WeixinExpressQueryTraceRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressQueryTraceJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/query_trace", request, timeOut);
        }

        /// <summary>
        /// 异步获取微信物流服务支持的运力 ID 列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运力公司列表和数量。</returns>
        /// <remarks>
        /// 查询组件和消息组件共享本接口。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-msg/api_get_delivery_list"/>。
        /// </remarks>
        public static Task<WeixinExpressDeliveryListJsonResult> GetDeliveryListAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressDeliveryListJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/get_delivery_list", new { }, timeOut);
        }

        /// <summary>
        /// 异步使用查询组件向微信上传交易单对应的运单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">用户、运单、商品、交易和运力信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>后续查询使用的运单 Token。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-search/api_trace_waybill"/>。</remarks>
        public static Task<WeixinExpressTraceWaybillJsonResult> TraceWaybillAsync(string accessTokenOrAppId, WeixinExpressTraceWaybillRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressTraceWaybillJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/trace_waybill", request, timeOut);
        }

        /// <summary>
        /// 异步更新查询组件运单的商品信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">运单 Token、用户及更新后的商品信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-search/api_update_waybill_goods"/>。</remarks>
        public static Task<WxJsonResult> UpdateWaybillGoodsAsync(string accessTokenOrAppId, WeixinExpressUpdateGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/update_waybill_goods", request, timeOut);
        }

        /// <summary>
        /// 异步更新消息组件运单的商品信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">运单 Token、用户及更新后的商品信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-msg/api_update_follow_waybill_goods"/>。</remarks>
        public static Task<WxJsonResult> UpdateFollowWaybillGoodsAsync(string accessTokenOrAppId, WeixinExpressUpdateGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/update_follow_waybill_goods", request, timeOut);
        }

        /// <summary>
        /// 异步使用消息组件查询运单详情。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">运单查询 Token 及可选的用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运单状态、商品和运力信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-msg/api_query_follow_trace"/>。</remarks>
        public static Task<WeixinExpressQueryTraceJsonResult> QueryFollowTraceAsync(string accessTokenOrAppId, WeixinExpressQueryTraceRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressQueryTraceJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/query_follow_trace", request, timeOut);
        }

        /// <summary>
        /// 异步使用消息组件向微信上传交易单对应的运单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">用户、运单、商品、交易和运力信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>后续查询使用的运单 Token。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-msg/api_follow_waybill"/>。</remarks>
        public static Task<WeixinExpressTraceWaybillJsonResult> FollowWaybillAsync(string accessTokenOrAppId, WeixinExpressTraceWaybillRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressTraceWaybillJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/open_msg/follow_waybill", request, timeOut);
        }

        #endregion

        private static T Send<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token={0}";
                return CommonJsonSend.Send<T>(accessToken, url, request, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        private static Task<T> SendAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token={0}";
                return await CommonJsonSend.SendAsync<T>(accessToken, url, request, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId);
        }
    }
}
