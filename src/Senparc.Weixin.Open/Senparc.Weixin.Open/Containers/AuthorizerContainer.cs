﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
Copyright(C) 2018 Senparc

    文件名：AuthorizerContainer.cs
    文件功能描述：通用接口JsApiTicket容器，用于OPEN第三方JSSDK自动管理JsApiTicket，如果过期会重新获取


    创建标识：Senparc - 20150211

    修改标识：renny - 20150921
    修改描述：整理接口

    修改标识：senparc - 20151004
    修改描述：文件名从JsApiTicketContainer.cs变为AuthorizerContainer.cs，用于集成所有授权方信息

    修改标识：Senparc - 20160206
    修改描述：将public object Lock更改为internal object Lock

    修改标识：Senparc - 20160318
    修改描述：v1.6.4 使用FlushCache.CreateInstance使注册过程立即生效

    修改标识：Senparc - 20160717
    修改描述：1.6.6 添加注册过程中的Name参数
    
    修改标识：Senparc - 20160803
    修改描述：v2.1.3 使用ApiUtility.GetExpireTime()方法处理过期
   
    修改标识：Senparc - 20160804
    修改描述：v2.1.5 增加异步方法

    修改标识：Senparc - 20160813
    修改描述：v2.2.1 添加TryReRegister()方法，处理分布式缓存重启（丢失）的情况

    修改标识：Senparc - 20160813
    修改描述：v2.2.2 完善getNewToken参数传递

    修改标识：Senparc - 20161027
    修改描述：v2.3.1 为GetAuthorizerInfoResult方法添加authorizerBag.AuthorizationInfo更新
    
    修改标识：Senparc - 20161203
    修改描述：v2.3.3 解决同步锁死锁的问题

    修改标识：Senparc - 20161203
    修改描述：v2.3.4 优化TryGetAuthorizerAccessToken方法，避免authorization_info.authorizer_access_token值为空

    修改标识：Senparc - 2071218
    修改描述：v2.8.3 修复 AuthorizerBag 使用外部缓存不会自动更新的问题

    修改标识：Senparc - 20180414
    修改描述：v2.9.2 修复 TryUpdateAuthorizationInfo 中缓存跟新的问题

    修改标识：Senparc - 20180614
    修改描述：CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存

    修改标识：Senparc - 20181226
    修改描述：v4.3.3 修改 DateTime 为 DateTimeOffset
    
    修改标识：Senparc - 20190422
    修改描述：v4.5.0 支持异步 Container

    修改标识：Senparc - 20190504
    修改描述：v4.5.1 完善 Container 注册委托的储存类型，解决多账户下的注册冲突问题

    修改标识：Senparc - 20190822
    修改描述：v4.5.9 完善同步方法的 AuthorizerContainer.Register() 对异步方法的调用，避免可能的线程锁死问题

    修改标识：Senparc - 20190826
    修改描述：v4.5.10 优化 Register() 方法

    修改标识：Senparc - 20191030
    修改描述：v4.7.102.1 修改 TryUpdateAuthorizationInfo() 相关方法，避免可能发生的 null 对象错误

    修改标识：ccccccmd - 20200609
    修改描述：v4.7.502.2 解决授权信息出现重复记录的问题

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Senparc.Weixin.Cache;
using Senparc.CO2NET.CacheUtility;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.Entities;
using Senparc.Weixin.Open.Exceptions;
using Senparc.Weixin.Utilities.WeixinUtility;

namespace Senparc.Weixin.Open.Containers
{
    /// <summary>
    /// 之前的JsApiTicketBag
    /// </summary>
    [Serializable]
    public class AuthorizerBag : BaseContainerBag
    {
        /// <summary>
        /// 授权方AppId，缓存中实际的Key
        /// </summary>
        public string AuthorizerAppId { get; set; }

        /// <summary>
        /// 第三方平台AppId
        /// </summary>
        public string ComponentAppId { get; set; }

        /// <summary>
        /// 授权信息
        /// </summary>
        [JsonIgnore]
        public GetAuthorizerInfoResult FullAuthorizerInfoResult
        {
            get
            {
                var result = new GetAuthorizerInfoResult()
                {
                    authorizer_info = AuthorizerInfo,
                    authorization_info = AuthorizationInfo
                };
                return result;
            }
        }


        public JsApiTicketResult JsApiTicketResult { get; set; } = new JsApiTicketResult();

        public DateTimeOffset JsApiTicketExpireTime { get; set; }

        /// <summary>
        /// 授权信息（请使用TryUpdateAuthorizationInfo()方法进行更新）
        /// </summary>
        public AuthorizationInfo AuthorizationInfo { get; set; } = new AuthorizationInfo();

        public DateTimeOffset AuthorizationInfoExpireTime { get; set; }

        /// <summary>
        /// 授权方资料信息
        /// </summary>
        public AuthorizerInfo AuthorizerInfo { get; set; } = new AuthorizerInfo();

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();

        public AuthorizerBag() { }
    }

    /// <summary>
    /// 授权方信息（用户的微信公众号）
    /// 包括通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    /// </summary>
    public class AuthorizerContainer : BaseContainer<AuthorizerBag>
    {
        const string LockResourceName = "Open.AuthorizerContainer";

        #region 同步方法

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="authorizerAppId"></param>
        /// <param name="componentAppId"></param>
        /// <param name="name">标记Authorizer名称（如微信公众号名称），帮助管理员识别</param>
        [Obsolete("请使用 RegisterAsync() 方法")]
        private static void Register(string componentAppId, string authorizerAppId, string name = null)
        {
            var task = RegisterAsync(componentAppId, authorizerAppId, name);
            Task.WaitAll(new[] { task }, 10000);
            //Task.Factory.StartNew(() =>
            //{
            //    RegisterAsync(componentAppId, authorizerAppId, name).ConfigureAwait(false);
            //}).ConfigureAwait(false);
        }

        /// <summary>
        /// 尝试注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <returns></returns>
        [Obsolete("请使用 TryRegisterAsync() 方法")]
        private static void TryRegister(string componentAppId, string authorizerAppid)
        {
            if (!CheckRegistered(authorizerAppid))
            {
                Register(componentAppId, authorizerAppid);
            }
        }

        #region 授权信息

        /// <summary>
        /// 获取或更新AuthorizationInfo。
        /// 如果读取refreshToken失败，则返回null。
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static AuthorizationInfo GetAuthorizationInfo(string componentAppId, string authorizerAppid,
            bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizerBag = TryGetItem(authorizerAppid);
            using (Cache.BeginCacheLock(LockResourceName + ".GetAuthorizationInfo", authorizerAppid))//同步锁
            {
                //更新Authorization
                if (getNewTicket || authorizerBag.AuthorizationInfoExpireTime <= SystemTime.Now)
                {
                    var componentVerifyTicket = ComponentContainer.TryGetComponentVerifyTicket(componentAppId);
                    var componentAccessToken = ComponentContainer.GetComponentAccessToken(componentAppId, componentVerifyTicket);

                    //获取新的AuthorizerAccessToken
                    var refreshToken = ComponentContainer.GetAuthorizerRefreshTokenFunc(componentAppId, authorizerAppid).GetAwaiter().GetResult();

                    if (refreshToken == null)
                    {
                        return null;
                    }

                    var refreshResult = RefreshAuthorizerToken(componentAccessToken, componentAppId, authorizerAppid,
                        refreshToken);

                    //更新数据
                    TryUpdateAuthorizationInfo(componentAppId, authorizerAppid,
                        refreshResult.authorizer_access_token, refreshResult.authorizer_refresh_token, refreshResult.expires_in);

                    authorizerBag = TryGetItem(authorizerAppid);//外部缓存需要重新获取新数据
                }
            }
            return authorizerBag.AuthorizationInfo;
        }


        /// <summary>
        /// 获取可用AuthorizerAccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetAuthorizerAccessToken(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizationInfo = GetAuthorizationInfo(componentAppId, authorizerAppid, getNewTicket);
            return authorizationInfo.authorizer_access_token;

            //v2.3.4 改用以上方法，避免authorization_info.authorizer_access_token值为空
            //return GetAuthorizerInfoResult(componentAppId, authorizerAppid, getNewTicket).authorization_info.authorizer_access_token;
        }

        /// <summary>
        /// 获取可用的GetAuthorizerInfoResult
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        ///// <exception cref="WeixinOpenException">此公众号没有高级权限</exception>
        public static GetAuthorizerInfoResult GetAuthorizerInfoResult(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizerBag = TryGetItem(authorizerAppid);
            using (Cache.BeginCacheLock(LockResourceName + ".GetAuthorizerInfoResult", authorizerAppid))//同步锁
            {
                //更新AuthorizerInfo
                if (getNewTicket || authorizerBag.AuthorizerInfo.user_name == null)
                {
                    var componentVerifyTicket = ComponentContainer.TryGetComponentVerifyTicket(componentAppId);
                    var componentAccessToken = ComponentContainer.GetComponentAccessToken(componentAppId, componentVerifyTicket);

                    //已过期，重新获取
                    var getAuthorizerInfoResult = ComponentApi.GetAuthorizerInfo(componentAccessToken, componentAppId, authorizerAppid);//TODO:如果是过期，可以通过刷新的方式重新获取

                    //AuthorizerInfo
                    authorizerBag.AuthorizerInfo = getAuthorizerInfoResult.authorizer_info;

                    //AuthorizationInfo
                    var getAuthorizationInfoResult = GetAuthorizationInfo(componentAppId, authorizerAppid, getNewTicket);
                    authorizerBag.AuthorizationInfo = getAuthorizationInfoResult;

                    Update(authorizerBag, null);//更新到缓存

                    //var componentBag = ComponentContainer.TryGetItem(componentAppId);
                    //if (string.IsNullOrEmpty(authorizerBag.AuthorizerInfoResult.authorization_info.authorizer_access_token))
                    //{
                    //    //账号没有此权限
                    //    throw new WeixinOpenException("此公众号没有高级权限", componentBag);
                    //}
                }
            }
            return authorizerBag.FullAuthorizerInfoResult;
        }

        /// <summary>
        /// 尝试更新AuthorizationInfo（如果没有AccessToken则不更新）
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="authorizationInfo"></param>
        public static void TryUpdateAuthorizationInfo(string componentAppId, string authorizerAppid, AuthorizationInfo authorizationInfo)
        {
            TryRegister(componentAppId, authorizerAppid);

            if (authorizationInfo.expires_in > 0 && authorizationInfo.authorizer_access_token != null)
            {
                var authorizerBag = TryGetItem(authorizerAppid);

                var refreshTokenChanged = authorizerBag == null
                                                      || authorizerBag.AuthorizationInfo.authorizer_access_token != authorizationInfo.authorizer_access_token
                                                      || authorizerBag.AuthorizationInfo.authorizer_refresh_token != authorizationInfo.authorizer_refresh_token;

                authorizerBag = authorizerBag ?? new AuthorizerBag();

                authorizerBag.AuthorizationInfo = authorizationInfo;
                authorizerBag.AuthorizationInfoExpireTime = ApiUtility.GetExpireTime(authorizationInfo.expires_in);

                Update(authorizerBag, null);//立即更新

                //通知变更
                if (refreshTokenChanged)
                {
                    ComponentContainer.AuthorizerTokenRefreshedFunc(componentAppId, authorizerAppid,
                        new RefreshAuthorizerTokenResult(authorizationInfo.authorizer_access_token,
                            authorizationInfo.authorizer_refresh_token, authorizationInfo.expires_in));
                }
            }
        }

        /// <summary>
        /// 尝试更新AuthorizationInfo（如果没有AccessToken则不更新）。
        /// 如果AuthorizerBag更新则返回最新的对象，否则返回null
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="authorizerAccessToken"></param>
        /// <param name="authorizerRefreshToken"></param>
        /// <param name="expiresIn"></param>
        public static void TryUpdateAuthorizationInfo(string componentAppId, string authorizerAppid, string authorizerAccessToken, string authorizerRefreshToken, int expiresIn)
        {
            TryRegister(componentAppId, authorizerAppid);

            if (expiresIn > 0 && authorizerAccessToken != null)
            {
                using (FlushCache.CreateInstance())
                {
                    var authorizerBag = TryGetItem(authorizerAppid);

                    var refreshTokenChanged = authorizerBag == null
                                              || authorizerBag.AuthorizationInfo.authorizer_access_token != authorizerAccessToken
                                              || authorizerBag.AuthorizationInfo.authorizer_refresh_token != authorizerRefreshToken;

                    authorizerBag = authorizerBag ?? new AuthorizerBag();

                    authorizerBag.AuthorizationInfo.authorizer_access_token = authorizerAccessToken;
                    authorizerBag.AuthorizationInfo.authorizer_refresh_token = authorizerRefreshToken;
                    authorizerBag.AuthorizationInfo.expires_in = expiresIn;
                    authorizerBag.AuthorizationInfoExpireTime = ApiUtility.GetExpireTime(expiresIn);

                    Update(authorizerBag, null);//立即更新

                    //通知变更
                    if (refreshTokenChanged)
                    {
                        ComponentContainer.AuthorizerTokenRefreshedFunc(componentAppId, authorizerAppid,
                            new RefreshAuthorizerTokenResult(authorizerAccessToken, authorizerRefreshToken, expiresIn));
                    }
                }
            }
        }

        /// <summary>
        /// 刷新AuthorizerToken
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static RefreshAuthorizerTokenResult RefreshAuthorizerToken(string componentAccessToken, string componentAppId, string authorizerAppid,
                      string refreshToken)
        {
            var refreshResult = ComponentApi.ApiAuthorizerToken(componentAccessToken, componentAppId, authorizerAppid,
                         refreshToken);
            //更新到存储
            ComponentContainer.AuthorizerTokenRefreshedFunc(componentAppId, authorizerAppid, refreshResult);
            return refreshResult;
        }

        #endregion

        #region JSTicket


        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetJsApiTicket(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            return GetJsApiTicket(componentAppId, authorizerAppid, getNewTicket);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetJsApiTicket(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            return GetJsApiTicketResult(componentAppId, authorizerAppid, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetJsApiTicketResult(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var accessTicketBag = TryGetItem(authorizerAppid);
            using (Cache.BeginCacheLock(LockResourceName + ".GetJsApiTicketResult", authorizerAppid))//同步锁
            {
                if (getNewTicket || accessTicketBag.JsApiTicketExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var authorizerAccessToken = TryGetAuthorizerAccessToken(componentAppId, authorizerAppid);

                    accessTicketBag.JsApiTicketResult = ComponentApi.GetJsApiTicket(authorizerAccessToken);

                    accessTicketBag.JsApiTicketExpireTime = ApiUtility.GetExpireTime(accessTicketBag.JsApiTicketResult.expires_in);

                    Update(accessTicketBag, null);//更新到缓存
                }
            }
            return accessTicketBag.JsApiTicketResult;
        }

        #endregion

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="authorizerAppId"></param>
        /// <param name="componentAppId"></param>
        /// <param name="name">标记Authorizer名称（如微信公众号名称），帮助管理员识别</param>
        private static async Task RegisterAsync(string componentAppId, string authorizerAppId, string name = null)
        {
            var componentBag = await ComponentContainer.TryGetItemAsync(componentAppId);
            if (componentBag == null)
            {
                throw new WeixinOpenException(string.Format("注册AuthorizerContainer之前，必须先注册对应的ComponentContainer！ComponentAppId：{0},AuthorizerAppId:{1}", componentAppId, authorizerAppId));
            }

            RegisterFuncCollection[authorizerAppId] = async () =>
             {
                 //using (FlushCache.CreateInstance())
                 //{
                 var bag = new AuthorizerBag()
                 {
                     Name = name,

                     AuthorizerAppId = authorizerAppId,
                     ComponentAppId = componentAppId,

                     AuthorizationInfo = new AuthorizationInfo(),
                     AuthorizationInfoExpireTime = DateTimeOffset.MinValue,

                     AuthorizerInfo = new AuthorizerInfo(),
                     //AuthorizerInfoExpireTime = DateTimeOffset.MinValue,

                     JsApiTicketResult = new JsApiTicketResult(),
                     JsApiTicketExpireTime = DateTimeOffset.MinValue,
                 };
                 await UpdateAsync(authorizerAppId, bag, null).ConfigureAwait(false);
                 return bag;
                 //}
             };
            await RegisterFuncCollection[authorizerAppId]().ConfigureAwait(false);

            //TODO：这里也可以考虑尝试进行授权（会影响速度）
        }

        /// <summary>
        /// 尝试注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <returns></returns>
        private static async Task TryRegisterAsync(string componentAppId, string authorizerAppid)
        {
            if (!await CheckRegisteredAsync(authorizerAppid).ConfigureAwait(false))
            {
                await RegisterAsync(componentAppId, authorizerAppid).ConfigureAwait(false);
            }
        }

        #region 授权信息

        /// <summary>
        /// 【异步方法】获取或更新AuthorizationInfo。
        /// 如果读取refreshToken失败，则返回null。
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static async Task<AuthorizationInfo> GetAuthorizationInfoAsync(string componentAppId, string authorizerAppid,
    bool getNewTicket = false)
        {
            await TryRegisterAsync(componentAppId, authorizerAppid).ConfigureAwait(false);

            var authorizerBag = await TryGetItemAsync(authorizerAppid).ConfigureAwait(false);
            using (await Cache.BeginCacheLockAsync(LockResourceName + ".GetAuthorizationInfo", authorizerAppid).ConfigureAwait(false))//同步锁
            {
                //更新Authorization
                if (getNewTicket || authorizerBag.AuthorizationInfoExpireTime <= SystemTime.Now)
                {
                    var componentVerifyTicket = await ComponentContainer.TryGetComponentVerifyTicketAsync(componentAppId).ConfigureAwait(false);
                    var componentAccessToken = await ComponentContainer.GetComponentAccessTokenAsync(componentAppId, componentVerifyTicket).ConfigureAwait(false);

                    //获取新的AuthorizerAccessToken
                    var refreshToken = await ComponentContainer.GetAuthorizerRefreshTokenFunc(componentAppId, authorizerAppid).ConfigureAwait(false);

                    if (refreshToken == null)
                    {
                        return null;
                    }

                    var refreshResult = await RefreshAuthorizerTokenAsync(componentAccessToken, componentAppId, authorizerAppid,
                        refreshToken).ConfigureAwait(false);

                    //更新数据
                    await TryUpdateAuthorizationInfoAsync(componentAppId, authorizerAppid,
                        refreshResult.authorizer_access_token, refreshResult.authorizer_refresh_token, refreshResult.expires_in);

                    authorizerBag = await TryGetItemAsync(authorizerAppid).ConfigureAwait(false);//外部缓存需要重新获取新数据
                }
            }
            return authorizerBag.AuthorizationInfo;
        }

        /// <summary>
        /// 【异步方法】获取可用AuthorizerAccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static async Task<string> TryGetAuthorizerAccessTokenAsync(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            await TryRegisterAsync(componentAppId, authorizerAppid);

            var authorizationInfo = await GetAuthorizationInfoAsync(componentAppId, authorizerAppid, getNewTicket).ConfigureAwait(false);
            return authorizationInfo.authorizer_access_token;

            //v2.3.4 改用以上方法，避免authorization_info.authorizer_access_token值为空
            //return GetAuthorizerInfoResult(componentAppId, authorizerAppid, getNewTicket).authorization_info.authorizer_access_token;

            //var result = await GetAuthorizerInfoResultAsync(componentAppId, authorizerAppid, getNewTicket);
            //return result.authorization_info.authorizer_access_token;
        }

        /// <summary>
        /// 【异步方法】获取可用的GetAuthorizerInfoResult
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        ///// <exception cref="WeixinOpenException">此公众号没有高级权限</exception>
        public static async Task<GetAuthorizerInfoResult> GetAuthorizerInfoResultAsync(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            await TryRegisterAsync(componentAppId, authorizerAppid).ConfigureAwait(false);

            var authorizerBag = await TryGetItemAsync(authorizerAppid).ConfigureAwait(false);
            using (await Cache.BeginCacheLockAsync(LockResourceName + ".GetAuthorizerInfoResult", authorizerAppid).ConfigureAwait(false))//同步锁
            {

                //更新AuthorizerInfokd
                if (getNewTicket || authorizerBag.AuthorizerInfo.user_name == null)
                {
                    var componentVerifyTicket = await ComponentContainer.TryGetComponentVerifyTicketAsync(componentAppId).ConfigureAwait(false);
                    var componentAccessToken = await ComponentContainer.GetComponentAccessTokenAsync(componentAppId, componentVerifyTicket).ConfigureAwait(false);

                    //已过期，重新获取
                    var getAuthorizerInfoResult = await ComponentApi.GetAuthorizerInfoAsync(componentAccessToken, componentAppId, authorizerAppid).ConfigureAwait(false);//TODO:如果是过期，可以通过刷新的方式重新获取

                    //AuthorizerInfo
                    authorizerBag.AuthorizerInfo = getAuthorizerInfoResult.authorizer_info;

                    await UpdateAsync(authorizerBag, null).ConfigureAwait(false);//更新到缓存

                    //var componentBag = ComponentContainer.TryGetItem(componentAppId);
                    //if (string.IsNullOrEmpty(authorizerBag.AuthorizerInfoResult.authorization_info.authorizer_access_token))
                    //{
                    //    //账号没有此权限
                    //    throw new WeixinOpenException("此公众号没有高级权限", componentBag);
                    //}
                }
            }
            return authorizerBag.FullAuthorizerInfoResult;
        }

        /// <summary>
        /// 【异步方法】尝试更新AuthorizationInfo（如果没有AccessToken则不更新）
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="authorizationInfo"></param>
        public static async Task TryUpdateAuthorizationInfoAsync(string componentAppId, string authorizerAppid, AuthorizationInfo authorizationInfo)
        {
            await TryRegisterAsync(componentAppId, authorizerAppid).ConfigureAwait(false);

            if (authorizationInfo.expires_in > 0 && authorizationInfo.authorizer_access_token != null)
            {
                var authorizerBag = await TryGetItemAsync(authorizerAppid).ConfigureAwait(false);

                var refreshTokenChanged = authorizerBag == null
                                           || authorizerBag.AuthorizationInfo.authorizer_access_token != authorizationInfo.authorizer_access_token
                                           || authorizerBag.AuthorizationInfo.authorizer_refresh_token != authorizationInfo.authorizer_refresh_token;

                authorizerBag = authorizerBag ?? new AuthorizerBag();

                authorizerBag.AuthorizationInfo = authorizationInfo;
                authorizerBag.AuthorizationInfoExpireTime = ApiUtility.GetExpireTime(authorizationInfo.expires_in);

                await UpdateAsync(authorizerBag, null).ConfigureAwait(false);//立即更新

                //通知变更
                if (refreshTokenChanged)
                {
                    ComponentContainer.AuthorizerTokenRefreshedFunc(componentAppId, authorizerAppid,
                        new RefreshAuthorizerTokenResult(authorizationInfo.authorizer_access_token,
                            authorizationInfo.authorizer_refresh_token, authorizationInfo.expires_in));
                }
            }
        }


        /// <summary>
        /// 【异步方法】尝试更新AuthorizationInfo（如果没有AccessToken则不更新）。
        /// 如果AuthorizerBag更新则返回最新的对象，否则返回null
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="authorizerAccessToken"></param>
        /// <param name="authorizerRefreshToken"></param>
        /// <param name="expiresIn"></param>
        public static async Task TryUpdateAuthorizationInfoAsync(string componentAppId, string authorizerAppid, string authorizerAccessToken, string authorizerRefreshToken, int expiresIn)
        {
            await TryRegisterAsync(componentAppId, authorizerAppid).ConfigureAwait(false);

            if (expiresIn > 0 && authorizerAccessToken != null)
            {
                using (FlushCache.CreateInstance())
                {
                    var authorizerBag = await TryGetItemAsync(authorizerAppid).ConfigureAwait(false);

                    var refreshTokenChanged = authorizerBag == null
                                              || authorizerBag.AuthorizationInfo.authorizer_access_token != authorizerAccessToken
                                              || authorizerBag.AuthorizationInfo.authorizer_refresh_token != authorizerRefreshToken;

                    authorizerBag = authorizerBag ?? new AuthorizerBag();

                    authorizerBag.AuthorizationInfo.authorizer_access_token = authorizerAccessToken;
                    authorizerBag.AuthorizationInfo.authorizer_refresh_token = authorizerRefreshToken;
                    authorizerBag.AuthorizationInfo.expires_in = expiresIn;
                    authorizerBag.AuthorizationInfoExpireTime = ApiUtility.GetExpireTime(expiresIn);

                    await UpdateAsync(authorizerBag, null).ConfigureAwait(false);//立即更新

                    //通知变更
                    if (refreshTokenChanged)
                    {
                        ComponentContainer.AuthorizerTokenRefreshedFunc(componentAppId, authorizerAppid,
                            new RefreshAuthorizerTokenResult(authorizerAccessToken, authorizerRefreshToken, expiresIn));
                    }
                }
            }
        }

        /// <summary>
        /// 【异步方法】刷新AuthorizerToken
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static async Task<RefreshAuthorizerTokenResult> RefreshAuthorizerTokenAsync(string componentAccessToken, string componentAppId, string authorizerAppid,
                      string refreshToken)
        {
            var refreshResult = await ComponentApi.ApiAuthorizerTokenAsync(componentAccessToken, componentAppId, authorizerAppid,
                         refreshToken).ConfigureAwait(false);
            //更新到存储
            ComponentContainer.AuthorizerTokenRefreshedFunc(componentAppId, authorizerAppid, refreshResult);
            return refreshResult;
        }

        #endregion

        #region JSTicket

        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static async Task<string> TryGetJsApiTicketAsync(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            await TryRegisterAsync(componentAppId, authorizerAppid).ConfigureAwait(false);

            return await GetJsApiTicketAsync(componentAppId, authorizerAppid, getNewTicket).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<string> GetJsApiTicketAsync(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            var result = await GetJsApiTicketResultAsync(componentAppId, authorizerAppid, getNewTicket).ConfigureAwait(false);
            return result.ticket;
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetJsApiTicketResultAsync(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            await TryRegisterAsync(componentAppId, authorizerAppid).ConfigureAwait(false);

            var accessTicketBag = await TryGetItemAsync(authorizerAppid).ConfigureAwait(false);
            using (await Cache.BeginCacheLockAsync(LockResourceName + ".GetJsApiTicketResult", authorizerAppid).ConfigureAwait(false))//同步锁
            {
                if (getNewTicket || accessTicketBag.JsApiTicketExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var authorizerAccessToken = await TryGetAuthorizerAccessTokenAsync(componentAppId, authorizerAppid).ConfigureAwait(false);

                    accessTicketBag.JsApiTicketResult = await ComponentApi.GetJsApiTicketAsync(authorizerAccessToken).ConfigureAwait(false);

                    accessTicketBag.JsApiTicketExpireTime = ApiUtility.GetExpireTime(accessTicketBag.JsApiTicketResult.expires_in);

                    await UpdateAsync(accessTicketBag, null).ConfigureAwait(false);//更新到缓存
                }
            }
            return accessTicketBag.JsApiTicketResult;
        }

        #endregion

        #endregion
    }
}
