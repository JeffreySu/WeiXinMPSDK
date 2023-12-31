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

    文件名：OpenApi.cs
    文件功能描述：OpenApi 接口


    创建标识：Senparc - 20220731


----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.OpenAPIs.OpenApiJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.OpenAPIs
{
    /// <summary>
    /// OpenApi 接口
    /// </summary>
    public static class OpenApi
    {
        #region 同步方法

        /// <summary>
        /// 查询 openAPI 调用quota
        /// <para>1、如果查询的 api 属于公众号的接口，则需要用公众号的access_token；如果查询的 api 属于小程序的接口，则需要用小程序的access_token；如果查询的接口属于第三方平台的接口，则需要用第三方平台的component_access_token；否则会出现76022报错。</para>
        /// <para>2、如果是第三方服务商代公众号或者小程序查询公众号或者小程序的api，则需要用authorizer_access_token</para>
        /// <para>3、每个接口都有调用次数限制，请开发者合理调用接口</para>
        /// <para>4、”/xxx/sns/xxx“这类接口不支持使用该接口，会出现76022报错。</para>
        /// https://developers.weixin.qq.com/doc/offiaccount/openApi/get_api_quota.html
        /// </summary>
        /// <param name="accessTokenOrAppId">依据需要查询的接口属于的账号类型不同而使用不同的token</param>
        /// <param name="cgiPath">api的请求地址，例如"/cgi-bin/message/custom/send";不要前缀“https://api.weixin.qq.com” ，也不要漏了"/",否则都会76003的报错</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QuotaGetJsonResult QuotaGet(string accessTokenOrAppId, string cgiPath, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/openapi/quota/get?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    cgi_path = cgiPath
                };

                return CommonJsonSend.Send<QuotaGetJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询 rid 信息
        /// https://developers.weixin.qq.com/doc/offiaccount/openApi/get_rid_info.html
        /// <para>1、由于查询 rid 信息属于开发者私密行为，因此仅支持同账号的查询。举个例子，rid=1111，是小程序账号 A 调用某接口出现的报错，那么则需要使用小程序账号 A 的access_token调用当前接口查询rid=1111的详情信息，如果使用小程序账号 B 的身份查询，则出现报错，错误码为xxx。公众号、第三方平台账号的接口同理。</para>
        /// <para>2、如果是第三方服务商代公众号或者小程序查询公众号或者小程序的 api 返回的rid，则使用同一账号的authorizer_access_token调用即可</para>
        /// <para>3、rid的有效期只有7天，即只可查询最近7天的rid，查询超过7天的 rid 会出现报错，错误码为76001</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">据需要查询的接口属于的账号类型不同而使用不同的token</param>
        /// <param name="rid">调用接口报错返回的rid</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static RidGetJsonResult RidGet(string accessTokenOrAppId, string rid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/openapi/rid/get?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    rid
                };

                return CommonJsonSend.Send<RidGetJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }


        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】查询 openAPI 调用quota
        /// <para>1、如果查询的 api 属于公众号的接口，则需要用公众号的access_token；如果查询的 api 属于小程序的接口，则需要用小程序的access_token；如果查询的接口属于第三方平台的接口，则需要用第三方平台的component_access_token；否则会出现76022报错。</para>
        /// <para>2、如果是第三方服务商代公众号或者小程序查询公众号或者小程序的api，则需要用authorizer_access_token</para>
        /// <para>3、每个接口都有调用次数限制，请开发者合理调用接口</para>
        /// <para>4、”/xxx/sns/xxx“这类接口不支持使用该接口，会出现76022报错。</para>
        /// https://developers.weixin.qq.com/doc/offiaccount/openApi/get_api_quota.html
        /// </summary>
        /// <param name="accessTokenOrAppId">依据需要查询的接口属于的账号类型不同而使用不同的token</param>
        /// <param name="cgiPath">api的请求地址，例如"/cgi-bin/message/custom/send";不要前缀“https://api.weixin.qq.com” ，也不要漏了"/",否则都会76003的报错</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>     
        public static async Task<QuotaGetJsonResult> QuotaGetAsync(string accessTokenOrAppId, string cgiPath, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/openapi/quota/get?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    cgi_path = cgiPath
                };

                return await CommonJsonSend.SendAsync<QuotaGetJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询 rid 信息
        /// https://developers.weixin.qq.com/doc/offiaccount/openApi/get_rid_info.html
        /// <para>1、由于查询 rid 信息属于开发者私密行为，因此仅支持同账号的查询。举个例子，rid=1111，是小程序账号 A 调用某接口出现的报错，那么则需要使用小程序账号 A 的access_token调用当前接口查询rid=1111的详情信息，如果使用小程序账号 B 的身份查询，则出现报错，错误码为xxx。公众号、第三方平台账号的接口同理。</para>
        /// <para>2、如果是第三方服务商代公众号或者小程序查询公众号或者小程序的 api 返回的rid，则使用同一账号的authorizer_access_token调用即可</para>
        /// <para>3、rid的有效期只有7天，即只可查询最近7天的rid，查询超过7天的 rid 会出现报错，错误码为76001</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">据需要查询的接口属于的账号类型不同而使用不同的token</param>
        /// <param name="rid">调用接口报错返回的rid</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<RidGetJsonResult> RidGetAsync(string accessTokenOrAppId, string rid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/openapi/rid/get?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    rid
                };

                return await CommonJsonSend.SendAsync<RidGetJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        #endregion
    }
}
