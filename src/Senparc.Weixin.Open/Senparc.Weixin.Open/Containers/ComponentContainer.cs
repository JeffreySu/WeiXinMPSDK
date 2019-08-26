#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc

    文件名：ComponentContainer.cs
    文件功能描述：通用接口ComponentAccessToken容器，用于自动管理ComponentAccessToken，如果过期会重新获取


    创建标识：Senparc - 20150430

    修改标识：Senparc - 20151004
    修改描述：v1.4.1 改名为ComponentContainer.cs，合并多个ComponentApp相关容器

    修改标识：Senparc - 20151005
    修改描述：v1.4.3 添加ComponentVerifyTicketExpireTime及自动更新机制

    修改标识：Senparc - 20160206
    修改描述：将public object Lock更改为internal object Lock

    修改标识：Senparc - 20160318
    修改描述：13.6.10 使用FlushCache.CreateInstance使注册过程立即生效

    修改标识：Senparc - 20160318
    修改描述：v1.6.4 使用FlushCache.CreateInstance使注册过程立即生效

    修改标识：Senparc - 20160717
    修改描述：1.6.6 添加注册过程中的Name参数
    
    修改标识：Senparc - 20160803
    修改描述：v2.1.3 使用ApiUtility.GetExpireTime()方法处理过期
 
    修改标识：Senparc - 20160804
    修改描述：v2.1.4 增加了TryGetComponentAccessTokenAsync，GetComponentAccessTokenAsync，
              GetComponentAccessTokenResultAsync，TryGetPreAuthCodeAsync，GetPreAuthCodeAsync，GetPreAuthCodeResultAsync，GetQueryAuthResultAsync的异步方法

    修改标识：Senparc - 20160808
    修改描述：v2.2.0 删除 ItemCollection 属性，直接使用ContainerBag加入到缓存

    修改标识：Senparc - 20160813
    修改描述：v2.2.1 添加TryReRegister()方法，处理分布式缓存重启（丢失）的情况

    修改标识：Senparc - 20160813
    修改描述：v2.2.2 完善getNewToken参数传递

    修改标识：Senparc - 20161203
    修改描述：v2.3.3 解决同步锁死锁的问题

    修改标识：Senparc - 20170318
    修改描述：v2.3.8 将ComponentContainer.GetComponentVerifyTicketFunc和GetAuthorizerRefreshTokenFunc改为属性

    修改标识：Senparc - 20180614
    修改描述：支持 CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存

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

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Senparc.Weixin.Cache;
using Senparc.CO2NET.CacheUtility;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.Open.CommonAPIs;
using Senparc.Weixin.Open.Entities;
using Senparc.Weixin.Open.Exceptions;
using Senparc.Weixin.Utilities.WeixinUtility;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.Open.Containers
{
    /// <summary>
    /// 第三方APP信息包
    /// </summary>
    [Serializable]
    public class ComponentBag : BaseContainerBag
    {
        /// <summary>
        /// 第三方平台AppId
        /// </summary>
        public string ComponentAppId { get; set; }
        //        {
        //            get { return _componentAppId; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _componentAppId, value, "ComponentAppId"); }
        //#else
        //            set { this.SetContainerProperty(ref _componentAppId, value); }
        //#endif
        //        }

        /// <summary>
        /// 第三方平台AppSecret
        /// </summary>
        public string ComponentAppSecret { get; set; }
        //        {
        //            get { return _componentAppSecret; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _componentAppSecret, value, "ComponentAppSecret"); }
        //#else
        //            set { this.SetContainerProperty(ref _componentAppSecret, value); }
        //#endif
        //        }

        /// <summary>
        /// 第三方平台ComponentVerifyTicket（每隔10分钟微信会主动推送到服务器，IP必须在白名单内）
        /// </summary>
        public string ComponentVerifyTicket { get; set; }
        //        {
        //            get { return _componentVerifyTicket; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _componentVerifyTicket, value, "ComponentVerifyTicket"); }
        //#else
        //            set { this.SetContainerProperty(ref _componentVerifyTicket, value); }
        //#endif
        //        }

        /// <summary>
        /// 第三方平台ComponentVerifyTicket过期时间（实际上过期之后仍然可以使用一段时间）
        /// </summary>
        public DateTimeOffset ComponentVerifyTicketExpireTime { get; set; }
        //        {
        //            get { return _componentVerifyTicketExpireTime; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _componentVerifyTicketExpireTime, value, "ComponentVerifyTicketExpireTime"); }
        //#else
        //            set { this.SetContainerProperty(ref _componentVerifyTicketExpireTime, value); }
        //#endif

        //        }

        /// <summary>
        /// ComponentAccessTokenResult
        /// </summary>
        public ComponentAccessTokenResult ComponentAccessTokenResult { get; set; }
        //        {
        //            get { return _componentAccessTokenResult; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _componentAccessTokenResult, value, "ComponentAccessTokenResult"); }
        //#else
        //            set { this.SetContainerProperty(ref _componentAccessTokenResult, value); }
        //#endif
        //        }

        /// <summary>
        /// ComponentAccessToken过期时间
        /// </summary>
        public DateTimeOffset ComponentAccessTokenExpireTime { get; set; }
        //        {
        //            get { return _componentAccessTokenExpireTime; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _componentAccessTokenExpireTime, value, "ComponentAccessTokenExpireTime"); }
        //#else
        //            set { this.SetContainerProperty(ref _componentAccessTokenExpireTime, value); }
        //#endif
        //        }


        /// <summary>
        /// PreAuthCodeResult 预授权码结果
        /// </summary>
        public PreAuthCodeResult PreAuthCodeResult { get; set; }
        //        {
        //            get { return _preAuthCodeResult; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _preAuthCodeResult, value, "PreAuthCodeResult"); }
        //#else
        //            set { this.SetContainerProperty(ref _preAuthCodeResult, value); }
        //#endif
        //        }

        /// <summary>
        /// 预授权码过期时间
        /// </summary>
        public DateTimeOffset PreAuthCodeExpireTime { get; set; }
        //        {
        //            get { return _preAuthCodeExpireTime; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _preAuthCodeExpireTime, value, "PreAuthCodeExpireTime"); }
        //#else
        //            set { this.SetContainerProperty(ref _preAuthCodeExpireTime, value); }
        //#endif
        //        }

        /// <summary>
        /// AuthorizerAccessToken
        /// </summary>
        public string AuthorizerAccessToken { get; set; }
        //        {
        //            get { return _authorizerAccessToken; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _authorizerAccessToken, value, "AuthorizerAccessToken"); }
        //#else
        //            set { this.SetContainerProperty(ref _authorizerAccessToken, value); }
        //#endif
        //        }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();

        //private string _componentAppId;
        //private string _componentAppSecret;
        //private string _componentVerifyTicket;
        //private DateTimeOffset _componentVerifyTicketExpireTime;
        //private ComponentAccessTokenResult _componentAccessTokenResult;
        //private DateTimeOffset _componentAccessTokenExpireTime;
        //private PreAuthCodeResult _preAuthCodeResult;
        //private DateTimeOffset _preAuthCodeExpireTime;
        //private string _authorizerAccessToken;

        /// <summary>
        /// ComponentBag
        /// </summary>
        public ComponentBag()
        {
            ComponentAccessTokenResult = new ComponentAccessTokenResult();
            ComponentAccessTokenExpireTime = DateTimeOffset.MinValue;

            PreAuthCodeResult = new PreAuthCodeResult();
            PreAuthCodeExpireTime = DateTimeOffset.MinValue;
        }
    }

    /// <summary>
    /// 通用接口ComponentAccessToken容器，用于自动管理ComponentAccessToken，如果过期会重新获取
    /// </summary>
    public class ComponentContainer : BaseContainer<ComponentBag>
    {
        private const string UN_REGISTER_ALERT = "此appId尚未注册，ComponentContainer.Register完成注册（全局执行一次即可）！";
        /// <summary>
        /// ComponentVerifyTicket服务器推送更新时间（分钟）
        /// </summary>
        private const int COMPONENT_VERIFY_TICKET_UPDATE_MINUTES = 10;

        const string LockResourceName = "Open.ComponentContainer";


        /// <summary>
        /// 获取ComponentVerifyTicket的方法
        /// </summary>
        public static Func<string, Task<string>> GetComponentVerifyTicketFunc { get; set; }

        /// <summary>
        /// 从数据库中获取已存的AuthorizerAccessToken的方法
        /// </summary>
        public static Func<string, string, Task<string>> GetAuthorizerRefreshTokenFunc { get; set; }

        /// <summary>
        /// AuthorizerAccessToken更新后的回调
        /// </summary>
        public static Action<string, string, RefreshAuthorizerTokenResult> AuthorizerTokenRefreshedFunc = null;

        #region 同步方法



        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getComponentVerifyTicketFunc">获取ComponentVerifyTicket的方法</param>
        /// <param name="getAuthorizerRefreshTokenFunc">从数据库中获取已存的AuthorizerAccessToken的方法</param>
        /// <param name="authorizerTokenRefreshedFunc">AuthorizerAccessToken更新后的回调</param>
        /// <param name="name">标记名称（如开放平台名称），帮助管理员识别</param>
        [Obsolete("请使用 RegisterAsync() 方法")]
        public static void Register(string componentAppId, string componentAppSecret,
            Func<string, Task<string>> getComponentVerifyTicketFunc,
            Func<string, string, Task<string>> getAuthorizerRefreshTokenFunc,
            Action<string, string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc,
            string name = null)
        {
            var task = RegisterAsync(componentAppId, componentAppSecret, getComponentVerifyTicketFunc, getAuthorizerRefreshTokenFunc, authorizerTokenRefreshedFunc, name);
            Task.WaitAll(new[] { task }, 10000);
            //Task.Factory.StartNew(() =>
            //{
            //    RegisterAsync(componentAppId, componentAppSecret, getComponentVerifyTicketFunc, getAuthorizerRefreshTokenFunc, authorizerTokenRefreshedFunc, name).ConfigureAwait(false);
            //}).ConfigureAwait(false);
        }


        /// <summary>
        /// 检查AppId是否已经注册，如果没有，则创建
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getNewToken"></param>
        /// <param name="name">标记Component名称（如微信公众号名称），帮助管理员识别</param>
        private static void TryRegister(string componentAppId, string componentAppSecret, bool getNewToken = false, string name = null)
        {
            if (!CheckRegistered(componentAppId) || getNewToken)
            {
                Register(componentAppId, componentAppSecret, null, null, null, name);
            }
        }

        #region component_verify_ticket

        /// <summary>
        /// 获取ComponentVerifyTicket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken"></param>
        /// <returns>如果不存在，则返回null</returns>
        public static string TryGetComponentVerifyTicket(string componentAppId, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var bag = TryGetItem(componentAppId);
            var componentVerifyTicket = bag.ComponentVerifyTicket;
            if (getNewToken || componentVerifyTicket == default(string) || bag.ComponentVerifyTicketExpireTime < SystemTime.Now)
            {
                if (GetComponentVerifyTicketFunc == null)
                {
                    throw new WeixinOpenException("GetComponentVerifyTicketFunc必须在注册时提供！", bag);
                }
                componentVerifyTicket = GetComponentVerifyTicketFunc(componentAppId).GetAwaiter().GetResult(); //获取最新的componentVerifyTicket
                bag.ComponentVerifyTicket = componentVerifyTicket;
                bag.ComponentVerifyTicketExpireTime = ApiUtility.GetExpireTime(COMPONENT_VERIFY_TICKET_UPDATE_MINUTES * 60);
                Update(bag, null);//更新到缓存
            }
            return componentVerifyTicket;
        }

        /// <summary>
        /// 更新ComponentVerifyTicket信息
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket"></param>
        public static void UpdateComponentVerifyTicket(string componentAppId, string componentVerifyTicket)
        {
            Update(componentAppId, bag =>
            {
                bag.ComponentVerifyTicket = componentVerifyTicket;
                Update(bag, null);//更新到缓存
            }, null);
        }

        #endregion

        #region component_access_token

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="componentVerifyTicket">如果为null则自动获取</param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetComponentAccessToken(string componentAppId, string componentAppSecret, string componentVerifyTicket = null, bool getNewToken = false)
        {
            TryRegister(componentAppId, componentAppSecret, getNewToken);
            return GetComponentAccessToken(componentAppId, componentVerifyTicket, getNewToken);
        }

        /// <summary>
        /// 获取可用AccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket">如果为null则自动获取</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetComponentAccessToken(string componentAppId, string componentVerifyTicket = null, bool getNewToken = false)
        {
            return GetComponentAccessTokenResult(componentAppId, componentVerifyTicket, getNewToken).component_access_token;
        }

        /// <summary>
        /// 获取可用AccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket">如果为null则自动获取</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static ComponentAccessTokenResult GetComponentAccessTokenResult(string componentAppId, string componentVerifyTicket = null, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var accessTokenBag = TryGetItem(componentAppId);
            using (Cache.BeginCacheLock(LockResourceName + ".GetComponentAccessTokenResult", componentAppId))//同步锁
            {
                if (getNewToken || accessTokenBag.ComponentAccessTokenExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    componentVerifyTicket = componentVerifyTicket ?? TryGetComponentVerifyTicket(componentAppId);

                    var componentAccessTokenResult = ComponentApi.GetComponentAccessToken(accessTokenBag.ComponentAppId, accessTokenBag.ComponentAppSecret, componentVerifyTicket);

                    accessTokenBag.ComponentAccessTokenResult = componentAccessTokenResult;
                    accessTokenBag.ComponentAccessTokenExpireTime = ApiUtility.GetExpireTime(componentAccessTokenResult.expires_in);
                    Update(accessTokenBag, null);//更新到缓存
                }
            }
            return accessTokenBag.ComponentAccessTokenResult;
        }
        #endregion

        #region pre_auth_code

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetPreAuthCode(string componentAppId, string componentAppSecret, bool getNewToken = false)
        {
            TryRegister(componentAppId, componentAppSecret, getNewToken);
            return GetPreAuthCode(componentAppId);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetPreAuthCode(string componentAppId, bool getNewToken = false)
        {
            return GetPreAuthCodeResult(componentAppId, getNewToken).pre_auth_code;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static PreAuthCodeResult GetPreAuthCodeResult(string componentAppId, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var componentBag = TryGetItem(componentAppId);
            using (Cache.BeginCacheLock(LockResourceName + ".GetPreAuthCodeResult", componentAppId))//同步锁
            {
                if (getNewToken || componentBag.PreAuthCodeExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var componentVerifyTicket = TryGetComponentVerifyTicket(componentAppId);

                    var accessToken = TryGetComponentAccessToken(componentAppId, componentBag.ComponentAppSecret, componentVerifyTicket);

                    var preAuthCodeResult = ComponentApi.GetPreAuthCode(componentBag.ComponentAppId, accessToken);
                    componentBag.PreAuthCodeExpireTime = ApiUtility.GetExpireTime(preAuthCodeResult.expires_in);


                    componentBag.PreAuthCodeResult = preAuthCodeResult;

                    Update(componentBag, null);//更新到缓存


                    ////TODO:这里有出现expires_in=0的情况，导致始终处于过期状态（也可能是因为参数过期等原因没有返回正确的数据，待观察）
                    //var expiresIn = componentBag.PreAuthCodeResult.expires_in > 0
                    //    ? componentBag.PreAuthCodeResult.expires_in
                    //    : 60 * 20;//默认为20分钟
                    //componentBag.PreAuthCodeExpireTime = SystemTime.Now.AddSeconds(expiresIn);
                }
            }
            return componentBag.PreAuthCodeResult;
        }
        #endregion

        #region api_query_auth

        /// <summary>
        /// 获取QueryAuthResult（此方法每次都会发出请求，不缓存）
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizationCode"></param>
        /// <param name="updateToAuthorizerContanier">是否将Authorization更新到AuthorizerContanier</param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        /// <exception cref="WeixinOpenException"></exception>
        public static QueryAuthResult GetQueryAuthResult(string componentAppId, string authorizationCode, bool updateToAuthorizerContanier = true, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var componentBag = TryGetItem(componentAppId);
            using (Cache.BeginCacheLock(LockResourceName + ".GetQueryAuthResult", componentAppId))//同步锁
            {
                var accessToken = TryGetComponentAccessToken(componentAppId, componentBag.ComponentAppSecret, null, getNewToken);
                var queryAuthResult = ComponentApi.QueryAuth(accessToken, componentAppId, authorizationCode);

                if (updateToAuthorizerContanier)
                {
                    //更新到AuthorizerContainer
                    AuthorizerContainer.TryUpdateAuthorizationInfo(componentAppId, queryAuthResult.authorization_info.authorizer_appid, queryAuthResult.authorization_info);
                }

                return queryAuthResult;
            }
        }
        #endregion
        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getComponentVerifyTicketFunc">获取ComponentVerifyTicket的方法</param>
        /// <param name="getAuthorizerRefreshTokenFunc">从数据库中获取已存的AuthorizerAccessToken的方法</param>
        /// <param name="authorizerTokenRefreshedFunc">AuthorizerAccessToken更新后的回调</param>
        /// <param name="name">标记名称（如开放平台名称），帮助管理员识别</param>
        public static async Task RegisterAsync(string componentAppId, string componentAppSecret,
            Func<string, Task<string>> getComponentVerifyTicketFunc,
            Func<string, string, Task<string>> getAuthorizerRefreshTokenFunc,
            Action<string, string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc,
            string name = null)
        {
            //激活消息队列线程

            if (GetComponentVerifyTicketFunc == null)
            {
                GetComponentVerifyTicketFunc = getComponentVerifyTicketFunc;
                GetAuthorizerRefreshTokenFunc = getAuthorizerRefreshTokenFunc;
                AuthorizerTokenRefreshedFunc = authorizerTokenRefreshedFunc;
            }

            RegisterFuncCollection[componentAppId] = async () =>
            {
                //using (FlushCache.CreateInstance())
                //{
                var bag = new ComponentBag()
                {
                    Name = name,
                    ComponentAppId = componentAppId,
                    ComponentAppSecret = componentAppSecret,
                };
                await UpdateAsync(componentAppId, bag, null).ConfigureAwait(false);
                return bag;
                //}
            };
            await RegisterFuncCollection[componentAppId]().ConfigureAwait(false);

            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].Component_Appid = componentAppId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].Component_Secret = componentAppSecret;
            }
        }

        /// <summary>
        /// 【异步方法】检查AppId是否已经注册，如果没有，则创建
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getNewToken"></param>
        /// <param name="name">标记Component名称（如微信公众号名称），帮助管理员识别</param>
        private static async Task TryRegisterAsync(string componentAppId, string componentAppSecret, bool getNewToken = false, string name = null)
        {
            if (!await CheckRegisteredAsync(componentAppId).ConfigureAwait(false) || getNewToken)
            {
                await RegisterAsync(componentAppId, componentAppSecret, null, null, null, name).ConfigureAwait(false);
            }
        }

        #region component_verify_ticket

        /// <summary>
        /// 获取ComponentVerifyTicket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken"></param>
        /// <returns>如果不存在，则返回null</returns>
        public static async Task<string> TryGetComponentVerifyTicketAsync(string componentAppId, bool getNewToken = false)
        {
            if (!await CheckRegisteredAsync(componentAppId).ConfigureAwait(false))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var bag = await TryGetItemAsync(componentAppId).ConfigureAwait(false);
            var componentVerifyTicket = bag.ComponentVerifyTicket;
            if (getNewToken || componentVerifyTicket == default(string) || bag.ComponentVerifyTicketExpireTime < SystemTime.Now)
            {
                if (GetComponentVerifyTicketFunc == null)
                {
                    throw new WeixinOpenException("GetComponentVerifyTicketFunc必须在注册时提供！", bag);
                }
                componentVerifyTicket = await GetComponentVerifyTicketFunc(componentAppId).ConfigureAwait(false); //获取最新的componentVerifyTicket
                bag.ComponentVerifyTicket = componentVerifyTicket;
                bag.ComponentVerifyTicketExpireTime = ApiUtility.GetExpireTime(COMPONENT_VERIFY_TICKET_UPDATE_MINUTES * 60);
                await UpdateAsync(bag, null).ConfigureAwait(false);//更新到缓存
            }
            return componentVerifyTicket;
        }

        /// <summary>
        /// 更新ComponentVerifyTicket信息
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket"></param>
        public static async Task UpdateComponentVerifyTicketAsync(string componentAppId, string componentVerifyTicket)
        {
            await UpdateAsync(componentAppId, async bag =>
            {
                bag.ComponentVerifyTicket = componentVerifyTicket;
                await UpdateAsync(bag, null).ConfigureAwait(false);//更新到缓存
            }, null).ConfigureAwait(false);
        }

        #endregion

        #region component_access_token

        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="componentVerifyTicket">如果为null则自动获取</param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static async Task<string> TryGetComponentAccessTokenAsync(string componentAppId, string componentAppSecret, string componentVerifyTicket = null, bool getNewToken = false)
        {
            await TryRegisterAsync(componentAppId, componentAppSecret, getNewToken).ConfigureAwait(false);
            return await GetComponentAccessTokenAsync(componentAppId, componentVerifyTicket, getNewToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取可用AccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket">如果为null则自动获取</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<string> GetComponentAccessTokenAsync(string componentAppId, string componentVerifyTicket = null, bool getNewToken = false)
        {
            var result = await GetComponentAccessTokenResultAsync(componentAppId, componentVerifyTicket, getNewToken).ConfigureAwait(false);

            return result.component_access_token;
        }

        /// <summary>
        /// 【异步方法】获取可用AccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket">如果为null则自动获取</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<ComponentAccessTokenResult> GetComponentAccessTokenResultAsync(string componentAppId, string componentVerifyTicket = null, bool getNewToken = false)
        {
            if (!await CheckRegisteredAsync(componentAppId).ConfigureAwait(false))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var accessTokenBag = await TryGetItemAsync(componentAppId).ConfigureAwait(false);
            using (await Cache.BeginCacheLockAsync(LockResourceName + ".GetComponentAccessTokenResult", componentAppId).ConfigureAwait(false))//同步锁
            {
                if (getNewToken || accessTokenBag.ComponentAccessTokenExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    componentVerifyTicket = componentVerifyTicket ?? await TryGetComponentVerifyTicketAsync(componentAppId).ConfigureAwait(false);

                    var componentAccessTokenResult = await ComponentApi.GetComponentAccessTokenAsync(accessTokenBag.ComponentAppId, accessTokenBag.ComponentAppSecret, componentVerifyTicket).ConfigureAwait(false);

                    accessTokenBag.ComponentAccessTokenResult = componentAccessTokenResult;
                    accessTokenBag.ComponentAccessTokenExpireTime = ApiUtility.GetExpireTime(componentAccessTokenResult.expires_in);
                    await UpdateAsync(accessTokenBag, null).ConfigureAwait(false);//更新到缓存
                }
            }
            return accessTokenBag.ComponentAccessTokenResult;
        }
        #endregion

        #region pre_auth_code

        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static async Task<string> TryGetPreAuthCodeAsync(string componentAppId, string componentAppSecret, bool getNewToken = false)
        {
            await TryRegisterAsync(componentAppId, componentAppSecret, getNewToken).ConfigureAwait(false);
            return await GetPreAuthCodeAsync(componentAppId, getNewToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<string> GetPreAuthCodeAsync(string componentAppId, bool getNewToken = false)
        {
            var result = await GetPreAuthCodeResultAsync(componentAppId, getNewToken).ConfigureAwait(false);
            return result.pre_auth_code;
        }

        /// <summary>
        ///【异步方法】获取可用Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<PreAuthCodeResult> GetPreAuthCodeResultAsync(string componentAppId, bool getNewToken = false)
        {
            if (!await CheckRegisteredAsync(componentAppId).ConfigureAwait(false))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var componentBag = await TryGetItemAsync(componentAppId).ConfigureAwait(false);
            using (await Cache.BeginCacheLockAsync(LockResourceName + ".GetPreAuthCodeResult", componentAppId).ConfigureAwait(false))//同步锁
            {
                if (getNewToken || componentBag.PreAuthCodeExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var componentVerifyTicket = await TryGetComponentVerifyTicketAsync(componentAppId).ConfigureAwait(false);

                    var accessToken = await TryGetComponentAccessTokenAsync(componentAppId, componentBag.ComponentAppSecret, componentVerifyTicket).ConfigureAwait(false);

                    var preAuthCodeResult = await ComponentApi.GetPreAuthCodeAsync(componentBag.ComponentAppId, accessToken).ConfigureAwait(false);
                    componentBag.PreAuthCodeExpireTime = ApiUtility.GetExpireTime(preAuthCodeResult.expires_in);

                    componentBag.PreAuthCodeResult = preAuthCodeResult;

                    await UpdateAsync(componentBag, null).ConfigureAwait(false);//更新到缓存

                    ////TODO:这里有出现expires_in=0的情况，导致始终处于过期状态（也可能是因为参数过期等原因没有返回正确的数据，待观察）
                    //var expiresIn = componentBag.PreAuthCodeResult.expires_in > 0
                    //    ? componentBag.PreAuthCodeResult.expires_in
                    //    : 60 * 20;//默认为20分钟
                    //componentBag.PreAuthCodeExpireTime = SystemTime.Now.AddSeconds(expiresIn);
                }
            }
            return componentBag.PreAuthCodeResult;
        }
        #endregion

        #region api_query_auth

        /// <summary>
        /// 【异步方法】获取QueryAuthResult（此方法每次都会发出请求，不缓存）
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizationCode"></param>
        /// <param name="updateToAuthorizerContanier">是否将Authorization更新到AuthorizerContanier</param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        /// <exception cref="WeixinOpenException"></exception>
        public static async Task<QueryAuthResult> GetQueryAuthResultAsync(string componentAppId, string authorizationCode, bool updateToAuthorizerContanier = true, bool getNewToken = false)
        {
            if (!await CheckRegisteredAsync(componentAppId).ConfigureAwait(false))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var componentBag = await TryGetItemAsync(componentAppId).ConfigureAwait(false);
            using (await Cache.BeginCacheLockAsync(LockResourceName + ".GetQueryAuthResult", componentAppId).ConfigureAwait(false))//同步锁
            {
                var accessToken = await TryGetComponentAccessTokenAsync(componentAppId, componentBag.ComponentAppSecret, null, getNewToken).ConfigureAwait(false);
                var queryAuthResult = await ComponentApi.QueryAuthAsync(accessToken, componentAppId, authorizationCode).ConfigureAwait(false);

                if (updateToAuthorizerContanier)
                {
                    //更新到AuthorizerContainer
                    await AuthorizerContainer.TryUpdateAuthorizationInfoAsync(componentAppId, queryAuthResult.authorization_info.authorizer_appid, queryAuthResult.authorization_info).ConfigureAwait(false);
                }

                return queryAuthResult;
            }
        }
        #endregion
        #endregion
    }
}
