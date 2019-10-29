﻿#region Apache License Version 2.0
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

    文件名：AccessTokenContainer.cs
    文件功能描述：通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取


    创建标识：Senparc - 20150313

    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20160206
    修改描述：将public object Lock更改为internal object Lock

    修改标识：Senparc - 20160312
    修改描述：1、升级Container，继承BaseContainer
              2、使用新的AccessToken有效期机制

    修改标识：Senparc - 20160318
    修改描述：v3.3.4 使用FlushCache.CreateInstance使注册过程立即生效
    
    修改标识：Senparc - 20160717
    修改描述：v3.3.8 添加注册过程中的Name参数
    
    修改标识：Senparc - 20160803
    修改描述：v4.1.2 使用ApiUtility.GetExpireTime()方法处理过期
 
    修改标识：Senparc - 20160804
    修改描述：v4.1.3 增加TryGetTokenAsync，GetTokenAsync，GetTokenResultAsync的异步方法
    
    修改标识：Senparc - 20160813
    修改描述：v4.1.5 添加TryReRegister()方法，处理分布式缓存重启（丢失）的情况

    修改标识：Senparc - 20160813
    修改描述：v4.1.6 完善GetToken()方法
    
    修改标识：Senparc - 20160813
    修改描述：v4.1.8 修改命名空间为Senparc.Weixin.Work.Containers

    修改标识：Senparc - 20180614
    修改描述：CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存
    
    修改标识：Senparc - 20180707
    修改描述：v15.0.9 Container 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下
    
    修改标识：Senparc - 20181226
    修改描述：v3.3.2 修改 DateTime 为 DateTimeOffset

    修改标识：Senparc - 20190320
    修改描述：v3.3.10 修改 Copr 错别字，修正为 Corp

    修改标识：Senparc - 20190422
    修改描述：v3.4.0 支持异步 Container

    修改标识：Senparc - 20190504
    修改描述：v3.5.10 完善 Container 注册委托的储存类型，解决多账户下的注册冲突问题

    修改标识：Senparc - 20190822
    修改描述：v3.5.11 完善同步方法的 AccessTokenContainer.Register() 对异步方法的调用，避免可能的线程锁死问题
    
    修改标识：Senparc - 20190826
    修改描述：v3.5.13 优化 Register() 方法

    修改标识：Senparc - 20190929
    修改描述：v3.7.101 优化 Container 异步注册方法

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.CO2NET.CacheUtility;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Exceptions;
using Senparc.Weixin.Utilities.WeixinUtility;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.Work.Containers
{
    [Serializable]
    public class AccessTokenBag : BaseContainerBag
    {
        /// <summary>
        /// CorpId
        /// </summary>
        public string CorpId { get; set; }

        [Obsolete("请使用 CorpId 属性")]
        public string CoprId { get { return CorpId; } set { CorpId = value; } }
        //        {
        //            get { return _corpId; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _corpId, value, "CorpId"); }
        //#else
        //            set { this.SetContainerProperty(ref _corpId, value); }
        //#endif
        //        }

        /// <summary>
        /// CorpSecret
        /// </summary>
        public string CorpSecret { get; set; }
        //        {
        //            get { return _corpSecret; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _corpSecret, value, "CorpSecret"); }
        //#else
        //            set { this.SetContainerProperty(ref _corpSecret, value); }
        //#endif
        //        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTimeOffset ExpireTime { get; set; }
        //        {
        //            get { return _expireTime; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _expireTime, value, "ExpireTime"); }
        //#else
        //            set { this.SetContainerProperty(ref _expireTime, value); }
        //#endif
        //        }

        /// <summary>
        /// AccessTokenResult
        /// </summary>
        public AccessTokenResult AccessTokenResult { get; set; }
        //        {
        //            get { return _accessTokenResult; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _accessTokenResult, value, "AccessTokenResult"); }
        //#else
        //            set { this.SetContainerProperty(ref _accessTokenResult, value); }
        //#endif
        //        }

        /// <summary>
        /// 只针对这个CorpId的锁
        /// </summary>
        internal object Lock = new object();

        //private string _corpId;
        //private string _corpSecret;
        //private DateTime _expireTime;
        //private AccessTokenResult _accessTokenResult;
    }

    /// <summary>
    /// 通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取
    /// </summary>
    public class AccessTokenContainer : BaseContainer<AccessTokenBag>
    {
        private const string UN_REGISTER_ALERT = "此CorpId尚未注册，AccessTokenContainer.Register完成注册（全局执行一次即可）！";

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token。
        /// 执行此注册过程，会连带注册ProviderTokenContainer。
        /// </summary>
        /// <param name="corpId">corpId</param>
        /// <param name="corpSecret">corpSecret</param>
        /// 此接口无异步方法
        public static string BuildingKey(string corpId, string corpSecret)
        {
            return string.Format("{0}@{1}", corpId, corpSecret);
        }

        /// <summary>
        /// 根据Key获取corpId和corpSecret
        /// </summary>
        /// <param name="appKey">由BuildingKey()方法生成的Key</param>
        /// <param name="corpId">corpId</param>
        /// <param name="corpSecret">corpSecret</param>
        public static void GetCorpIdAndSecretFromKey(string appKey, out string corpId, out string corpSecret)
        {
            var keyArr = appKey.Split('@');
            corpId = keyArr[0];
            corpSecret = keyArr[1];
        }

        /// <summary>
        /// 根据Key获取corpId和corpSecret
        /// </summary>
        /// <param name="appKey">由BuildingKey()方法生成的Key</param>
        /// <param name="corpId">corpId</param>
        /// <param name="corpSecret">corpSecret</param>
        [Obsolete("请使用 GetCorpIdAndSecretFromKey() 方法")]
        public static void GetCoprIdAndSecretFromKey(string appKey, out string corpId, out string corpSecret)
        {
            GetCorpIdAndSecretFromKey(appKey, out corpId, out corpSecret);
        }


        #region 同步方法

        /// <summary>
        /// 注册每个corpId和corpSecret，在调用高级接口时可以仅使用AppKey（由 AccessTokenContainer.BuildingKey() 方法获得）
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        [Obsolete("请使用 RegisterAsync() 方法")]
        public static void Register(string corpId, string corpSecret, string name = null)
        {
            var task = RegisterAsync(corpId, corpSecret, name);
            Task.WaitAll(new[] { task }, 10000);
            //Task.Factory.StartNew(() =>
            //{
            //    RegisterAsync(corpId, corpSecret, name).ConfigureAwait(false);
            //}).ConfigureAwait(false);
        }


        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetToken(string corpId, string corpSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(BuildingKey(corpId, corpSecret)) || getNewToken)
            {
                Register(corpId, corpSecret);
            }
            return GetToken(corpId, corpSecret, getNewToken);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="appKey">由BuildingKey()方法生成的Key</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetToken(string appKey, bool getNewToken = false)
        {
            return GetTokenResult(appKey, getNewToken).access_token;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetToken(string corpId, string corpSecret, bool getNewToken = false)
        {
            var appKey = BuildingKey(corpId, corpSecret);
            return GetTokenResult(appKey, getNewToken).access_token;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static AccessTokenResult GetTokenResult(string corpId, string corpSecret, bool getNewToken = false)
        {
            var appKey = BuildingKey(corpId, corpSecret);
            return GetTokenResult(appKey, getNewToken);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="appKey">由BuildingKey()方法生成的Key</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static AccessTokenResult GetTokenResult(string appKey, bool getNewToken = false)
        {
            if (!CheckRegistered(appKey))
            {
                string corpId;
                string corpSecret;
                GetCorpIdAndSecretFromKey(appKey, out corpId, out corpSecret);

                Register(corpId, corpSecret);
                //throw new WeixinWorkException(UN_REGISTER_ALERT);
            }

            var accessTokenBag = TryGetItem(appKey);
            lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.ExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    accessTokenBag.AccessTokenResult = CommonApi.GetToken(accessTokenBag.CorpId, accessTokenBag.CorpSecret);
                    accessTokenBag.ExpireTime = ApiUtility.GetExpireTime(accessTokenBag.AccessTokenResult.expires_in);
                    Update(accessTokenBag, null);//更新到缓存
                }
            }
            return accessTokenBag.AccessTokenResult;
        }


        ///// <summary>
        ///// 检查是否已经注册
        ///// </summary>
        ///// <param name="corpId"></param>
        ///// <returns></returns>
        ///// 此接口无异步方法
        //public new static bool CheckRegistered(string corpId)
        //{
        //    return Cache.CheckExisted(corpId);
        //}

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】注册每个corpId和corpSecret，在调用高级接口时可以仅使用AppKey（由 AccessTokenContainer.BuildingKey() 方法获得）
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        public static async Task RegisterAsync(string corpId, string corpSecret, string name = null)
        {
            //记录注册信息，RegisterFunc委托内的过程会在缓存丢失之后自动重试
            var shortKey = BuildingKey(corpId, corpSecret);
            RegisterFuncCollection[shortKey] = async () =>
             {
                 //using (FlushCache.CreateInstance())
                 //{
                 var bag = new AccessTokenBag()
                 {
                     Name = name,
                     CorpId = corpId,
                     CorpSecret = corpSecret,
                     ExpireTime = DateTimeOffset.MinValue,
                     AccessTokenResult = new AccessTokenResult()
                 };
                 await UpdateAsync(BuildingKey(corpId, corpSecret), bag, null).ConfigureAwait(false);
                 return bag;
                 //}
             };

            var registerTask = RegisterFuncCollection[shortKey]();

            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinCorpId = corpId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinCorpSecret = corpSecret;
            }

            var registerJsApiTask = JsApiTicketContainer.RegisterAsync(corpId, corpSecret);//连带注册JsApiTicketContainer

            var registerProviderTask = ProviderTokenContainer.RegisterAsync(corpId, corpSecret);//连带注册ProviderTokenContainer

            await Task.WhenAll(new[] { registerTask, registerJsApiTask, registerProviderTask });//等待所有任务完成
        }


        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static async Task<string> TryGetTokenAsync(string corpId, string corpSecret, bool getNewToken = false)
        {
            if (!await CheckRegisteredAsync(BuildingKey(corpId, corpSecret)) || getNewToken)
            {
                await RegisterAsync(corpId, corpSecret).ConfigureAwait(false);
            }
            return await GetTokenAsync(corpId, corpSecret, getNewToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<string> GetTokenAsync(string corpId, string corpSecret, bool getNewToken = false)
        {
            var result = await GetTokenResultAsync(corpId, corpSecret, getNewToken).ConfigureAwait(false);
            return result.access_token;
        }

        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<IAccessTokenResult> GetTokenResultAsync(string corpId, string corpSecret, bool getNewToken = false)
        {
            var appKey = BuildingKey(corpId, corpSecret);
            return await GetTokenResultAsync(appKey, getNewToken).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="appKey">由BuildingKey()方法生成的Key</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<IAccessTokenResult> GetTokenResultAsync(string appKey, bool getNewToken = false)
        {
            if (!await CheckRegisteredAsync(appKey).ConfigureAwait(false))
            {
                string corpId;
                string corpSecret;
                GetCorpIdAndSecretFromKey(appKey, out corpId, out corpSecret);

                await RegisterAsync(corpId, corpSecret).ConfigureAwait(false);
                //throw new WeixinWorkException(UN_REGISTER_ALERT);
            }

            var accessTokenBag = await TryGetItemAsync(appKey).ConfigureAwait(false);
            // lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.ExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var accessTokenResult = await CommonApi.GetTokenAsync(accessTokenBag.CorpId, accessTokenBag.CorpSecret).ConfigureAwait(false);
                    //accessTokenBag.AccessTokenResult = CommonApi.GetToken(accessTokenBag.CorpId,
                    //    accessTokenBag.CorpSecret);
                    accessTokenBag.AccessTokenResult = accessTokenResult;
                    accessTokenBag.ExpireTime = ApiUtility.GetExpireTime(accessTokenBag.AccessTokenResult.expires_in);
                    await UpdateAsync(accessTokenBag, null).ConfigureAwait(false);//更新到缓存
                }
            }
            return accessTokenBag.AccessTokenResult;
        }
        #endregion
    }
}
