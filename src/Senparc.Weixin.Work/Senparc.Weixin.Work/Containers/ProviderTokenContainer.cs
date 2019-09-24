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

    文件名：ProviderTokenContainer.cs
    文件功能描述：通用接口ProviderToken容器，用于自动管理ProviderToken，如果过期会重新获取


    创建标识：Senparc - 20150313

    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20160206
    修改描述：将public object Lock更改为internal object Lock

    修改标识：Senparc - 20160312
    修改描述：升级Container，继承自BaseContainer<JsApiTicketBag>

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
    修改描述：v2.0.9 Container 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下

    修改标识：Senparc - 20181226
    修改描述：v3.3.2 修改 DateTime 为 DateTimeOffset
    
    修改标识：Senparc - 20190422
    修改描述：v3.4.0 支持异步 Container
        
    修改标识：Senparc - 20190504
    修改描述：v3.5.2 完善 Container 注册委托的储存类型，解决多账户下的注册冲突问题

    修改标识：Senparc - 20190822
    修改描述：v3.5.11 完善同步方法的 ProviderTokenContainer.Register() 对异步方法的调用，避免可能的线程锁死问题

    修改标识：Senparc - 20190826
    修改描述：v3.5.13 优化 Register() 方法
----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Exceptions;
using Senparc.Weixin.Utilities.WeixinUtility;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Work.AdvancedAPIs;

namespace Senparc.Weixin.Work.Containers
{
    /// <summary>
    /// ProviderTokenBag
    /// </summary>
    [Serializable]
    public class ProviderTokenBag : BaseContainerBag
    {
        /// <summary>
        /// CorpId
        /// </summary>
        public string CorpId { get; set; }
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
        /// ProviderTokenResult
        /// </summary>
        public ProviderTokenResult ProviderTokenResult { get; set; }
        //        {
        //            get { return _providerTokenResult; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _providerTokenResult, value, "ProviderTokenResult"); }
        //#else
        //            set { this.SetContainerProperty(ref _providerTokenResult, value); }
        //#endif
        //        }

        /// <summary>
        /// 只针对这个CorpId的锁
        /// </summary>
        internal object Lock = new object();

        //private string _corpId;
        //private string _corpSecret;
        //private DateTime _expireTime;
        //private ProviderTokenResult _providerTokenResult;
    }

    /// <summary>
    /// 通用接口ProviderToken容器，用于自动管理ProviderToken，如果过期会重新获取
    /// </summary>
    public class ProviderTokenContainer : BaseContainer<ProviderTokenBag>
    {
        private const string UN_REGISTER_ALERT = "此CorpId尚未注册，ProviderTokenContainer.Register完成注册（全局执行一次即可）！";


        /// <summary>
        /// 获取全局唯一Key
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        private static string BuildingKey(string corpId, string corpSecret)
        {
            return corpId + corpSecret;
        }


        #region 同步方法

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
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
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetToken(string corpId, string corpSecret, bool getNewToken = false)
        {
            return GetTokenResult(corpId, corpSecret, getNewToken).provider_access_token;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static ProviderTokenResult GetTokenResult(string corpId, string corpSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(BuildingKey(corpId, corpSecret)))
            {
                throw new WeixinWorkException(UN_REGISTER_ALERT);
            }

            var providerTokenBag = TryGetItem(BuildingKey(corpId, corpSecret));
            lock (providerTokenBag.Lock)
            {
                if (getNewToken || providerTokenBag.ExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    providerTokenBag.ProviderTokenResult = SsoApi.GetProviderToken(providerTokenBag.CorpId, providerTokenBag.CorpSecret);
                    providerTokenBag.ExpireTime = ApiUtility.GetExpireTime(providerTokenBag.ProviderTokenResult.expires_in);
                    Update(providerTokenBag, null);//更新到缓存
                }
            }
            return providerTokenBag.ProviderTokenResult;
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
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        public static async Task RegisterAsync(string corpId, string corpSecret, string name = null)
        {
            var shortKey = BuildingKey(corpId, corpSecret);
            RegisterFuncCollection[shortKey] = async () =>
            {
                //using (FlushCache.CreateInstance())
                //{
                var bag = new ProviderTokenBag()
                {
                    Name = name,
                    CorpId = corpId,
                    CorpSecret = corpSecret,
                    ExpireTime = DateTimeOffset.MinValue,
                    ProviderTokenResult = new ProviderTokenResult()
                };
                await UpdateAsync(BuildingKey(corpId, corpSecret), bag, null).ConfigureAwait(false);
                return bag;
                //}
            };
            await RegisterFuncCollection[shortKey]().ConfigureAwait(false);

            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinCorpId = corpId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinCorpSecret = corpSecret;
            }
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
            if (!await CheckRegisteredAsync(BuildingKey(corpId, corpSecret)).ConfigureAwait(false) || getNewToken)
            {
                await RegisterAsync(corpId, corpSecret).ConfigureAwait(false);
            }
            return await GetTokenAsync(corpId, corpSecret);
        }

        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<string> GetTokenAsync(string corpId, string corpSecret, bool getNewToken = false)
        {
            var result = await GetTokenResultAsync(corpId, corpSecret, getNewToken).ConfigureAwait(false);
            return result.provider_access_token;
        }

        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<ProviderTokenResult> GetTokenResultAsync(string corpId, string corpSecret, bool getNewToken = false)
        {
            if (!await CheckRegisteredAsync(BuildingKey(corpId, corpSecret)).ConfigureAwait(false))
            {
                throw new WeixinWorkException(UN_REGISTER_ALERT);
            }

            var providerTokenBag = await TryGetItemAsync(BuildingKey(corpId, corpSecret)).ConfigureAwait(false);
            //lock (providerTokenBag.Lock)
            {
                if (getNewToken || providerTokenBag.ExpireTime <= DateTimeOffset.Now)
                {
                    //已过期，重新获取
                    var providerTokenResult = await SsoApi.GetProviderTokenAsync(providerTokenBag.CorpId, providerTokenBag.CorpSecret).ConfigureAwait(false);
                    providerTokenBag.ProviderTokenResult = providerTokenResult;
                    //providerTokenBag.ProviderTokenResult = CommonApi.GetProviderToken(providerTokenBag.CorpId,
                    //    providerTokenBag.CorpSecret);
                    providerTokenBag.ExpireTime = ApiUtility.GetExpireTime(providerTokenBag.ProviderTokenResult.expires_in);
                    await UpdateAsync(providerTokenBag, null).ConfigureAwait(false);//更新到缓存
                }
            }
            return providerTokenBag.ProviderTokenResult;
        }
        #endregion
    }
}
