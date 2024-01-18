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
  
    文件名：AccessTokenContainer.cs
    文件功能描述：小程序的通用接口 AccessToken 容器，用于自动管理 AccessToken，如果过期会重新获取
    
    
    修改标识：gokeiyou - 20201230
    修改描述：新建 WxOpen 专属的 AccessTokenContainer

    修改标识：Senparc - 20220916
    修改描述：v3.15.7.1 RegisterAsync() 方法添加 ConfigureAwait(false) 标记

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.Utilities.WeixinUtility;
using System;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.Containers
{

    /// <summary>
    /// 小程序 AccessToken 包
    /// </summary>
    public class AccessTokenBag : BaseContainerBag
    {
        public string WxOpenAppId { get; set; }
        public string WxOpenAppSecret { get; set; }
        public DateTimeOffset AccessTokenExpireTime { get; set; }
        public AccessTokenResult AccessTokenResult { get; set; }
    }

    /// <summary>
    /// 小程序的通用接口 AccessToken 容器，用于自动管理 AccessToken，如果过期会重新获取
    /// </summary>
    public class AccessTokenContainer : BaseContainer<AccessTokenBag>
    {
        const string LockResourceName = "WxOpen.AccessTokenContainer";

        #region 同步方法

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token
        /// </summary>
        /// <param name="wxOpenAppId">微信小程序后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="wxOpenAppSecret">微信小程序后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信小程序名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        [Obsolete("请使用 RegisterAsync() 方法")]
        public static void Register(string wxOpenAppId, string wxOpenAppSecret, string name = null)
        {
            var task = RegisterAsync(wxOpenAppId, wxOpenAppSecret, name);
            Task.WaitAll(new[] { task }, 10000);
            //Task.Factory.StartNew(() =>
            //{
            //    RegisterAsync(wxOpenAppId, wxOpenAppSecret, name).ConfigureAwait(false);
            //}).ConfigureAwait(false);
        }

        #region AccessToken

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="wxOpenAppId"></param>
        /// <param name="wxOpenAppSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetAccessToken(string wxOpenAppId, string wxOpenAppSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(wxOpenAppId) || getNewToken)
            {
                Register(wxOpenAppId, wxOpenAppSecret);
            }
            return GetAccessToken(wxOpenAppId, getNewToken);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="wxOpenAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetAccessToken(string wxOpenAppId, bool getNewToken = false)
        {
            return GetAccessTokenResult(wxOpenAppId, getNewToken).access_token;
        }

        /// <summary>
        /// 获取可用AccessTokenResult对象
        /// </summary>
        /// <param name="wxOpenAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static AccessTokenResult GetAccessTokenResult(string wxOpenAppId, bool getNewToken = false)
        {
            if (!CheckRegistered(wxOpenAppId))
            {
                throw new UnRegisterAppIdException(wxOpenAppId, string.Format("此wxOpenAppId（{0}）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！", wxOpenAppId));
            }

            var accessTokenBag = TryGetItem(wxOpenAppId);

            using (Cache.BeginCacheLock(LockResourceName, wxOpenAppId))//同步锁
            {
                if (getNewToken || accessTokenBag.AccessTokenExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    accessTokenBag.AccessTokenResult = CommonApi.GetToken(accessTokenBag.WxOpenAppId, accessTokenBag.WxOpenAppSecret);
                    accessTokenBag.AccessTokenExpireTime = ApiUtility.GetExpireTime(accessTokenBag.AccessTokenResult.expires_in);
                    Update(accessTokenBag, null);//更新到缓存
                }
            }
            return accessTokenBag.AccessTokenResult;
        }

        #endregion

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token
        /// </summary>
        /// <param name="wxOpenAppId">微信小程序后台的【开发】>【基本配置】中的N“AppID(应用ID)”</param>
        /// <param name="wxOpenAppSecret">微信小程序后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信小程序名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        public static async Task RegisterAsync(string wxOpenAppId, string wxOpenAppSecret, string name = null)
        {
            //记录注册信息，RegisterFunc委托内的过程会在缓存丢失之后自动重试
            RegisterFuncCollection[wxOpenAppId] = async () =>
            {
                //using (FlushCache.CreateInstance())
                //{
                var bag = new AccessTokenBag()
                {
                    //Key = wxOpenAppId,
                    Name = name,
                    WxOpenAppId = wxOpenAppId,
                    WxOpenAppSecret = wxOpenAppSecret,
                    AccessTokenExpireTime = DateTimeOffset.MinValue,
                    AccessTokenResult = new AccessTokenResult()
                };
                await UpdateAsync(wxOpenAppId, bag, null).ConfigureAwait(false);//第一次添加，此处已经立即更新
                return bag;
                //}
            };

            var registerTask = RegisterFuncCollection[wxOpenAppId]();

            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WxOpenAppId = wxOpenAppId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WxOpenAppSecret = wxOpenAppSecret;
            }

            await Task.WhenAll(new[] { registerTask }).ConfigureAwait(false);//等待所有任务完成
        }


        #region AccessToken

        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="wxOpenAppId"></param>
        /// <param name="wxOpenAppSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static async Task<string> TryGetAccessTokenAsync(string wxOpenAppId, string wxOpenAppSecret, bool getNewToken = false)
        {
            if (!await CheckRegisteredAsync(wxOpenAppId).ConfigureAwait(false) || getNewToken)
            {
                await RegisterAsync(wxOpenAppId, wxOpenAppSecret).ConfigureAwait(false);
            }
            return await GetAccessTokenAsync(wxOpenAppId, getNewToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="wxOpenAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<string> GetAccessTokenAsync(string wxOpenAppId, bool getNewToken = false)
        {
            var result = await GetAccessTokenResultAsync(wxOpenAppId, getNewToken).ConfigureAwait(false);
            return result.access_token;
        }

        /// <summary>
        /// 获取可用AccessTokenResult对象
        /// </summary>
        /// <param name="wxOpenAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<IAccessTokenResult> GetAccessTokenResultAsync(string wxOpenAppId, bool getNewToken = false)
        {
            if (!await CheckRegisteredAsync(wxOpenAppId).ConfigureAwait(false))
            {
                throw new UnRegisterAppIdException(wxOpenAppId, string.Format("此wxOpenAppId（{0}）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！", wxOpenAppId));
            }

            var accessTokenBag = await TryGetItemAsync(wxOpenAppId).ConfigureAwait(false);

            using (await Cache.BeginCacheLockAsync(LockResourceName, wxOpenAppId).ConfigureAwait(false))//同步锁
            {
                if (getNewToken || accessTokenBag.AccessTokenExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var accessTokenResult = await CommonApi.GetTokenAsync(accessTokenBag.WxOpenAppId, accessTokenBag.WxOpenAppSecret).ConfigureAwait(false);
                    accessTokenBag.AccessTokenResult = accessTokenResult;
                    accessTokenBag.AccessTokenExpireTime = ApiUtility.GetExpireTime(accessTokenBag.AccessTokenResult.expires_in);
                    await UpdateAsync(accessTokenBag, null).ConfigureAwait(false);//更新到缓存
                }
            }
            return accessTokenBag.AccessTokenResult;
        }


        #endregion


        #endregion
    }
}
