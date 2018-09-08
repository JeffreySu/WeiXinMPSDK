#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：UserAPI.cs
    文件功能描述：用户接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：jsionr - 20150322
    修改描述：添加修改关注者备注信息接口
    
    修改标识：Senparc - 20150325
    修改描述：修改关注者备注信息开放代理请求超时时间

    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

----------------------------------------------------------------*/

/*
    接口详见：http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E7%94%A8%E6%88%B7%E5%9F%BA%E6%9C%AC%E4%BF%A1%E6%81%AF
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.HttpUtility;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public static class UserApi
    {
        #region 同步方法

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static UserInfoJson Info(string accessTokenOrAppId, string openId, Language lang = Language.zh_CN)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}",
                    accessToken.AsUrlData(), openId.AsUrlData(), lang.ToString("g").AsUrlData());
                return HttpUtility.Get.GetJson<UserInfoJson>(url);

                //错误时微信会返回错误码等信息，JSON数据包示例如下（该示例为AppID无效错误）:
                //{"errcode":40013,"errmsg":"invalid appid"}

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取关注者OpenId信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public static OpenIdResultJson Get(string accessTokenOrAppId, string nextOpenId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/user/get?access_token={0}", accessToken.AsUrlData());
                if (!string.IsNullOrEmpty(nextOpenId))
                {
                    url += "&next_openid=" + nextOpenId;
                }
                return HttpUtility.Get.GetJson<OpenIdResultJson>(url);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改关注者备注信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="remark">新的备注名，长度必须小于30字符</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult UpdateRemark(string accessTokenOrAppId, string openId, string remark, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/user/info/updateremark?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid = openId,
                    remark = remark
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 批量获取用户基本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="userList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static BatchGetUserInfoJsonResult BatchGetUserInfo(string accessTokenOrAppId, List<BatchGetUserInfoData> userList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/user/info/batchget?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    user_list = userList,
                };
                return CommonJsonSend.Send<BatchGetUserInfoJsonResult>(accessToken, url, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取黑名单
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginOpenId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static OpenIdResultJson GetBlackList(string accessTokenOrAppId, string beginOpenId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi<OpenIdResultJson>(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/tags/members/getblacklist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    begin_openid = beginOpenId
                };
                return CommonJsonSend.Send<OpenIdResultJson>(accessToken, url, data, timeOut: timeOut);
            }, accessTokenOrAppId, true);
        }

        /// <summary>
        /// 取消拉黑用户
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openidList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult BatchUnBlackList(string accessTokenOrAppId, List<string> openidList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi<OpenIdResultJson>(accessToken =>
            {
                string urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/tags/members/batchunblacklist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid_list = openidList
                };
                return CommonJsonSend.Send<OpenIdResultJson>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId, true);
        }

        /// <summary>
        /// 拉黑用户
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openidList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult BatchBlackList(string accessTokenOrAppId, List<string> openidList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi<OpenIdResultJson>(accessToken =>
            {
                string urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/tags/members/batchblacklist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid_list = openidList
                };
                return CommonJsonSend.Send<OpenIdResultJson>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId, true);
        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】获取用户信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static async Task<UserInfoJson> InfoAsync(string accessTokenOrAppId, string openId, Language lang = Language.zh_CN)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = string.Format(Config.ApiMpHost + "/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}",
                   accessToken.AsUrlData(), openId.AsUrlData(), lang.ToString("g").AsUrlData());
               return await HttpUtility.Get.GetJsonAsync<UserInfoJson>(url);

                //错误时微信会返回错误码等信息，JSON数据包示例如下（该示例为AppID无效错误）:
                //{"errcode":40013,"errmsg":"invalid appid"}

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取关注者OpenId信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public static async Task<OpenIdResultJson> GetAsync(string accessTokenOrAppId, string nextOpenId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = string.Format(Config.ApiMpHost + "/cgi-bin/user/get?access_token={0}", accessToken.AsUrlData());
               if (!string.IsNullOrEmpty(nextOpenId))
               {
                   url += "&next_openid=" + nextOpenId;
               }
               return await HttpUtility.Get.GetJsonAsync<OpenIdResultJson>(url);

           }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】修改关注者备注信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="remark">新的备注名，长度必须小于30字符</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UpdateRemarkAsync(string accessTokenOrAppId, string openId, string remark, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = string.Format(Config.ApiMpHost + "/cgi-bin/user/info/updateremark?access_token={0}", accessToken.AsUrlData());
               var data = new
               {
                   openid = openId,
                   remark = remark
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, timeOut: timeOut);

           }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】批量获取用户基本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="userList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<BatchGetUserInfoJsonResult> BatchGetUserInfoAsync(string accessTokenOrAppId, List<BatchGetUserInfoData> userList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/user/info/batchget?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    user_list = userList,
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<BatchGetUserInfoJsonResult>(accessToken, url, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取黑名单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginOpenId">当 begin_openid 为空时，默认从开头拉取。</param>
        /// <param name="timeOut"></param>
        /// <returns>每次调用最多可拉取 10000 个OpenID</returns>
        public static async Task<OpenIdResultJson> GetBlackListAsync(string accessTokenOrAppId, string beginOpenId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync<OpenIdResultJson>(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/tags/members/getblacklist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    begin_openid = beginOpenId
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<OpenIdResultJson>(accessToken, url, data, timeOut: timeOut);
            }, accessTokenOrAppId, true);
        }

        /// <summary>
        /// 取消拉黑用户
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openidList">需要移除黑名单的用户的openid，一次移除最多允许20个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> BatchUnBlackListAsync(string accessTokenOrAppId, List<string> openidList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync<OpenIdResultJson>(async accessToken =>
            {
                string urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/tags/members/batchunblacklist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid_list = openidList
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<OpenIdResultJson>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId, true);
        }

        /// <summary>
        /// 拉黑用户
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openidList">需要拉入黑名单的用户的openid，一次拉黑最多允许20个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> BatchBlackListAsync(string accessTokenOrAppId, List<string> openidList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync<OpenIdResultJson>(async accessToken =>
            {
                string urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/tags/members/batchblacklist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid_list = openidList
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<OpenIdResultJson>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId, true);
        }
        #endregion
#endif
    }
}
