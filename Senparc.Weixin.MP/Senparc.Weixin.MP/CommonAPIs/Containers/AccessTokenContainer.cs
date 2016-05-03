/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

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
    修改描述：13.6.10 使用FlushCache.CreateInstance使注册过程立即生效

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.CacheUtility;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// AccessToken包
    /// </summary>
    [Serializable]
    public class AccessTokenBag : BaseContainerBag
    {
        public string AppId
        {
            get { return _appId; }
            set { base.SetContainerProperty(ref _appId, value); }
        }

        public string AppSecret
        {
            get { return _appSecret; }
            set { base.SetContainerProperty(ref _appSecret, value); }
        }

        public DateTime AccessTokenExpireTime
        {
            get { return _accessTokenExpireTime; }
            set { base.SetContainerProperty(ref _accessTokenExpireTime, value); }
        }

        public AccessTokenResult AccessTokenResult
        {
            get { return _accessTokenResult; }
            set { base.SetContainerProperty(ref _accessTokenResult, value); }
        }


        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();

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
        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token
        /// </summary>
        /// <param name="appId">微信公众号后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="appSecret">微信公众号后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        public static void Register(string appId, string appSecret)
        {
            using (FlushCache.CreateInstance())
            {
                Update(appId, new AccessTokenBag()
                {
                    AppId = appId,
                    AppSecret = appSecret,
                    AccessTokenExpireTime = DateTime.MinValue,
                    AccessTokenResult = new AccessTokenResult()
                });
            }

            //为JsApiTicketContainer进行自动注册
            JsApiTicketContainer.Register(appId, appSecret);
        }

        /// <summary>
        /// 返回已经注册的第一个AppId
        /// </summary>
        /// <returns></returns>
        public static string GetFirstOrDefaultAppId()
        {
            return ItemCollection.GetAll().Keys.FirstOrDefault();
        }

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
            return GetAccessToken(appId);
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

            var accessTokenBag = (AccessTokenBag)ItemCollection[appId];
            lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.AccessTokenExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    accessTokenBag.AccessTokenResult = CommonApi.GetToken(accessTokenBag.AppId, accessTokenBag.AppSecret);
                    accessTokenBag.AccessTokenExpireTime = DateTime.Now.AddSeconds(accessTokenBag.AccessTokenResult.expires_in);
                }
            }
            return accessTokenBag.AccessTokenResult;
        }


        #endregion

    }
}
