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
    Copyright (C) 2020 Senparc

    文件名：OAuthContainer.cs
    文件功能描述：用户OAuth容器，用于自动管理OAuth的AccessToken，如果过期会重新获取


    创建标识：Senparc - 20160801

    修改标识：Senparc - 20160803
    修改描述：v14.2.3 使用ApiUtility.GetExpireTime()方法处理过期
 
    修改标识：Senparc - 20160804
    修改描述：v14.2.4 增加TryGetOAuthAccessTokenAsync，GetOAuthAccessTokenAsync，GetOAuthAccessTokenResultAsync的异步方法

    修改标识：Senparc - 20160808
    修改描述：v14.3.0 删除 ItemCollection 属性，直接使用ContainerBag加入到缓存
        
    修改标识：Senparc - 20160813
    修改描述：v14.3.4 添加TryReRegister()方法，处理分布式缓存重启（丢失）的情况
    
    修改标识：Senparc - 20160813
    修改描述：v14.3.6 完善getNewToken参数传递

    修改标识：Senparc - 20180614
    修改描述：CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存

    修改标识：Senparc - 20180707
    修改描述：v15.0.9 Container 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下

    修改标识：Senparc - 20181226
    修改描述：v16.6.2 修改 DateTime 为 DateTimeOffset

    修改标识：Senparc - 20190421
    修改描述：v17.0.0 支持异步 Container

    修改标识：Senparc - 20190503
    修改描述：v16.7.2 完善 Container 注册委托的储存类型，解决多账户下的注册冲突问题
    
    修改标识：Senparc - 20190822
    修改描述：v16.7.13 完善同步方法的 OAuthAccessTokenContainer.Register() 对异步方法的调用，避免可能的线程锁死问题

    修改标识：Senparc - 20190826
    修改描述：v16.7.15 优化 Register() 方法

    修改标识：Senparc - 20191014
    修改描述：v16.9.102 正式启用 OAuthAccessTokenContainer

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.CO2NET.CacheUtility;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Utilities.WeixinUtility;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.MP.Containers
{
    /// <summary>
    /// OAuth包
    /// </summary>
    [Serializable]
    public class OAuthAccessTokenBag : BaseContainerBag, IBaseContainerBag_AppId
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }

        public OAuthAccessTokenResult OAuthAccessTokenResult { get; set; }

        public DateTimeOffset OAuthAccessTokenExpireTime { get; set; }
        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();
    }

    /// <summary>
    /// 用户 OAuth 容器，用于自动管理 OAuth的AccessToken，如果过期会重新获取
    /// </summary>
    public class OAuthAccessTokenContainer : BaseContainer<OAuthAccessTokenBag>
    {
        const string LockResourceName = "MP.OAuthAccessTokenContainer";

        /// <summary>
        /// 创建统一格式的键
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string BuildKey(string appId, string code)
        {
            return $"{appId}:{code}";
        }

        #region 同步方法

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="code">code</param>
        /// <param name="name">标记JsApiTicket名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        /// 此接口不提供异步方法
        [Obsolete("请使用 RegisterAsync() 方法")]
        public static void Register(string appId, string appSecret, string code, string name = null)
        {
            var task = RegisterAsync(appId, appSecret, name);
            Task.WaitAll(new[] { task }, 10000);
        }

        /// <summary>
        /// 【异步方法】注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="code">code</param>
        /// <param name="name">标记JsApiTicket名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        /// 此接口不提供异步方法
        public static async Task RegisterAsync(string appId, string appSecret, string code, string name = null)
        {
            var key = BuildKey(appId, code);
            RegisterFuncCollection[key] = async () =>
            {
                //using (FlushCache.CreateInstance())
                //{
                var bag = new OAuthAccessTokenBag()
                {
                    Name = name,
                    AppId = appId,
                    AppSecret = appSecret,
                    OAuthAccessTokenExpireTime = DateTimeOffset.MinValue,
                    OAuthAccessTokenResult = new OAuthAccessTokenResult()
                };
                await UpdateAsync(key, bag, null).ConfigureAwait(false);
                return bag;
                //}
            };

            await RegisterFuncCollection[key]().ConfigureAwait(false);

            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppId = appId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppSecret = appSecret;
            }
        }


        #region OAuthAccessToken

        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetOAuthAccessToken(string appId, string appSecret, string code, bool getNewToken = false)
        {
            var key = BuildKey(appId, code);

            if (!CheckRegistered(key) || getNewToken)
            {
                Register(appId, appSecret, code);
            }
            return GetOAuthAccessToken(appId, code, getNewToken);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetOAuthAccessToken(string appId, string code, bool getNewToken = false)
        {
            return GetOAuthAccessTokenResult(appId, code, getNewToken).access_token;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static OAuthAccessTokenResult GetOAuthAccessTokenResult(string appId, string code, bool getNewToken = false)
        {
            var key = BuildKey(appId, code);
            if (!CheckRegistered(key))
            {
                throw new UnRegisterAppIdException(null, "此 appId code 尚未注册，请先使用 OAuthAccessTokenContainer.Register 完成注册（全局执行一次即可）！");
            }

            var oAuthAccessTokenBag = TryGetItem(key);
            using (Cache.BeginCacheLock(LockResourceName, key))//同步锁
            {
                if (getNewToken || oAuthAccessTokenBag.OAuthAccessTokenExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    oAuthAccessTokenBag.OAuthAccessTokenResult = OAuthApi.GetAccessToken(oAuthAccessTokenBag.AppId, oAuthAccessTokenBag.AppSecret, code);
                    oAuthAccessTokenBag.OAuthAccessTokenExpireTime =
                        ApiUtility.GetExpireTime(oAuthAccessTokenBag.OAuthAccessTokenResult.expires_in);
                    Update(oAuthAccessTokenBag, null);
                }
            }
            return oAuthAccessTokenBag.OAuthAccessTokenResult;
        }

        #endregion
        #endregion


        #region 异步方法
        #region OAuthAccessToken

        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static async Task<string> TryGetOAuthAccessTokenAsync(string appId, string appSecret, string code, bool getNewToken = false)
        {
            var key = BuildKey(appId, code);

            if (!await CheckRegisteredAsync(key).ConfigureAwait(false) || getNewToken)
            {
                await RegisterAsync(appId, appSecret, code).ConfigureAwait(false);
            }
            return await GetOAuthAccessTokenAsync(appId, code, getNewToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<string> GetOAuthAccessTokenAsync(string appId, string code, bool getNewToken = false)
        {
            var result = await GetOAuthAccessTokenResultAsync(appId, code, getNewToken).ConfigureAwait(false);
            return result.access_token;
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<OAuthAccessTokenResult> GetOAuthAccessTokenResultAsync(string appId, string code, bool getNewToken = false)
        {
            var key = BuildKey(appId, code);
            if (!await CheckRegisteredAsync(key).ConfigureAwait(false))
            {
                throw new UnRegisterAppIdException(null, "此 appId code 尚未注册，请先使用 OAuthAccessTokenContainer.Register 完成注册（全局执行一次即可）！");
            }

            var oAuthAccessTokenBag = await TryGetItemAsync(key).ConfigureAwait(false);
            using (await Cache.BeginCacheLockAsync(LockResourceName, key).ConfigureAwait(false))//同步锁
            {
                if (getNewToken || oAuthAccessTokenBag.OAuthAccessTokenExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var oAuthAccessTokenResult = await OAuthApi.GetAccessTokenAsync(oAuthAccessTokenBag.AppId, oAuthAccessTokenBag.AppSecret, code).ConfigureAwait(false);
                    oAuthAccessTokenBag.OAuthAccessTokenResult = oAuthAccessTokenResult;
                    //oAuthAccessTokenBag.OAuthAccessTokenResult =  OAuthApi.GetAccessToken(oAuthAccessTokenBag.AppId, oAuthAccessTokenBag.AppSecret, code);
                    oAuthAccessTokenBag.OAuthAccessTokenExpireTime =
                        ApiUtility.GetExpireTime(oAuthAccessTokenBag.OAuthAccessTokenResult.expires_in);
                    await UpdateAsync(oAuthAccessTokenBag, null).ConfigureAwait(false);
                }
            }
            return oAuthAccessTokenBag.OAuthAccessTokenResult;
        }

        #endregion
        #endregion
    }
}
