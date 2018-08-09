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

    文件名：AccessTokenContainer.cs
    文件功能描述：通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150702
    修改描述：添加GetFirstOrDefaultAppId()方法

    修改标识：Senparc - 20151004
    修改描述：v13.3.0 将JsApiTicketContainer整合到AccessTokenContainer

    修改标识：Senparc - 20160318
    修改描述：v13.6.10 使用FlushCache.CreateInstance使注册过程立即生效

    修改标识：Senparc - 20160717
    修改描述：v13.8.11 添加注册过程中的Name参数
  
    修改标识：Senparc - 20160721
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20160801
    修改描述：v14.2.1 转移到Senparc.Weixin.MP.Containers命名空间下

    修改标识：Senparc - 20160803
    修改描述：v14.2.3 使用ApiUtility.GetExpireTime()方法处理过期

    修改标识：Senparc - 20160808
    修改描述：v14.3.0 删除 ItemCollection 属性，直接使用ContainerBag加入到缓存

    修改标识：Senparc - 20160810
    修改描述：v14.3.3 修复错误
        
    修改标识：Senparc - 20160813
    修改描述：v14.3.4 添加TryReRegister()方法，处理分布式缓存重启（丢失）的情况

    修改标识：Senparc - 20160813
    修改描述：v14.3.6 完善getNewToken参数传递

    修改标识：Senparc - 20170702
    修改描述：v14.5.0 为了配合新版本ApiHandlerWapper方法，GetAccessTokenResultAsync方法的返回值从Task<AccessTokenResult>改为Task<IAccessTokenResult>
    
    修改标识：Senparc - 20170702
    修改描述：v14.5.5 修改Container中的锁及异步调用方法

    修改标识：Senparc - 20170702
    修改描述：v14.6.2 回滚 v14.5.5中修改的方法（同步方法中调用异步方法）

    修改标识：Senparc - 20180614
    修改描述：CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存

    修改标识：Senparc - 20180707
    修改描述：v15.0.9 Container 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.CO2NET.CacheUtility;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Utilities.WeixinUtility;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.MP.Containers
{
    /// <summary>
    /// AccessToken包
    /// </summary>
    [Serializable]
    public class AccessTokenBag : BaseContainerBag, IBaseContainerBag_AppId
    {
        public string AppId { get; set; }
        //        {
        //            get { return _appId; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _appId, value, "AppId"); }
        //#else
        //            set { this.SetContainerProperty(ref _appId, value); }
        //#endif
        //        }

        public string AppSecret { get; set; }
        //        {
        //            get { return _appSecret; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _appSecret, value, "AppSecret"); }
        //#else
        //            set { this.SetContainerProperty(ref _appSecret, value); }
        //#endif
        //        }

        public DateTime AccessTokenExpireTime { get; set; }
        //        {
        //            get { return _accessTokenExpireTime; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _accessTokenExpireTime, value, "AccessTokenExpireTime"); }
        //#else
        //            set { this.SetContainerProperty(ref _accessTokenExpireTime, value); }
        //#endif
        //        }

        public AccessTokenResult AccessTokenResult { get; set; }
        //        {
        //            get { return _accessTokenResult; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _accessTokenResult, value, "AccessTokenResult"); }
        //#else
        //            set { this.SetContainerProperty(ref _accessTokenResult, value); }
        //#endif
        //        }

        private AccessTokenResult _accessTokenResult;
        private DateTime _accessTokenExpireTime;
        private string _appSecret;
        private string _appId;
    }

    /// <summary>
    /// 通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取
    /// </summary>
    public class AccessTokenContainer : BaseContainer<AccessTokenBag>
    {
        const string LockResourceName = "MP.AccessTokenContainer";

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token
        /// </summary>
        /// <param name="appId">微信公众号后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="appSecret">微信公众号后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        public static void Register(string appId, string appSecret, string name = null)
        {
            //记录注册信息，RegisterFunc委托内的过程会在缓存丢失之后自动重试
            RegisterFunc = () =>
            {
                //using (FlushCache.CreateInstance())
                //{
                var bag = new AccessTokenBag()
                {
                    //Key = appId,
                    Name = name,
                    AppId = appId,
                    AppSecret = appSecret,
                    AccessTokenExpireTime = DateTime.MinValue,
                    AccessTokenResult = new AccessTokenResult()
                };
                Update(appId, bag, null);//第一次添加，此处已经立即更新
                return bag;
                //}
            };
            RegisterFunc();

            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppId = appId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppSecret = appSecret;
            }

            //为JsApiTicketContainer进行自动注册
            JsApiTicketContainer.Register(appId, appSecret, name);

            //OAuthAccessTokenContainer进行自动注册
            OAuthAccessTokenContainer.Register(appId, appSecret, name);

        }

        #region 同步方法

        #region AccessToken

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetAccessToken(string appId, string appSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(appId) || getNewToken)
            {
                Register(appId, appSecret);
            }
            return GetAccessToken(appId, getNewToken);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetAccessToken(string appId, bool getNewToken = false)
        {
            return GetAccessTokenResult(appId, getNewToken).access_token;
        }

        /// <summary>
        /// 获取可用AccessTokenResult对象
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static AccessTokenResult GetAccessTokenResult(string appId, bool getNewToken = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new UnRegisterAppIdException(appId, string.Format("此appId（{0}）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！", appId));
            }

            var accessTokenBag = TryGetItem(appId);

            using (Cache.BeginCacheLock(LockResourceName, appId))//同步锁
            {
                if (getNewToken || accessTokenBag.AccessTokenExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    accessTokenBag.AccessTokenResult = CommonApi.GetToken(accessTokenBag.AppId, accessTokenBag.AppSecret);
                    accessTokenBag.AccessTokenExpireTime = ApiUtility.GetExpireTime(accessTokenBag.AccessTokenResult.expires_in);
                    Update(accessTokenBag, null);//更新到缓存
                }
            }
            return accessTokenBag.AccessTokenResult;
        }

        #endregion

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        #region AccessToken

        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static async Task<string> TryGetAccessTokenAsync(string appId, string appSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(appId) || getNewToken)
            {
                Register(appId, appSecret);
            }
            return await GetAccessTokenAsync(appId, getNewToken);
        }

        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<string> GetAccessTokenAsync(string appId, bool getNewToken = false)
        {
            var result = await GetAccessTokenResultAsync(appId, getNewToken);
            return result.access_token;
        }

        /// <summary>
        /// 获取可用AccessTokenResult对象
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<IAccessTokenResult> GetAccessTokenResultAsync(string appId, bool getNewToken = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new UnRegisterAppIdException(appId, string.Format("此appId（{0}）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！", appId));
            }

            var accessTokenBag = TryGetItem(appId);

            using (Cache.BeginCacheLock(LockResourceName, appId))//同步锁
            {
                if (getNewToken || accessTokenBag.AccessTokenExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    var accessTokenResult = await CommonApi.GetTokenAsync(accessTokenBag.AppId, accessTokenBag.AppSecret);
                    accessTokenBag.AccessTokenResult = accessTokenResult;
                    accessTokenBag.AccessTokenExpireTime = ApiUtility.GetExpireTime(accessTokenBag.AccessTokenResult.expires_in);
                    Update(accessTokenBag, null);//更新到缓存
                }
            }
            return accessTokenBag.AccessTokenResult;
        }


        #endregion

        #endregion
#endif
    }
}

