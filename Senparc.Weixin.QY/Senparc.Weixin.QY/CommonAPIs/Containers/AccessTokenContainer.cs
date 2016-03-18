/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

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

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.CacheUtility;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.QY.Exceptions;

namespace Senparc.Weixin.QY.CommonAPIs
{
    [Serializable]
    public class AccessTokenBag : BaseContainerBag
    {
        /// <summary>
        /// CorpId
        /// </summary>
        public string CorpId
        {
            get { return _corpId; }
            set { base.SetContainerProperty(ref _corpId, value); }
        }
        /// <summary>
        /// CorpSecret
        /// </summary>
        public string CorpSecret
        {
            get { return _corpSecret; }
            set { base.SetContainerProperty(ref _corpSecret, value); }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime
        {
            get { return _expireTime; }
            set { base.SetContainerProperty(ref _expireTime, value); }
        }
        /// <summary>
        /// AccessTokenResult
        /// </summary>
        public AccessTokenResult AccessTokenResult
        {
            get { return _accessTokenResult; }
            set { base.SetContainerProperty(ref _accessTokenResult, value); }
        }

        /// <summary>
        /// 只针对这个CorpId的锁
        /// </summary>
        internal object Lock = new object();

        private string _corpId;
        private string _corpSecret;
        private DateTime _expireTime;
        private AccessTokenResult _accessTokenResult;
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
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        public static void Register(string corpId, string corpSecret)
        {
            using (FlushCache.CreateInstance())
            {
                Update(corpId, new AccessTokenBag()
                {
                    CorpId = corpId,
                    CorpSecret = corpSecret,
                    ExpireTime = DateTime.MinValue,
                    AccessTokenResult = new AccessTokenResult()
                });
            }

            ProviderTokenContainer.Register(corpId, corpSecret);//连带注册ProviderTokenContainer
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
            if (!CheckRegistered(corpId) || getNewToken)
            {
                Register(corpId, corpSecret);
            }
            return GetToken(corpId);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetToken(string corpId, bool getNewToken = false)
        {
            return GetTokenResult(corpId, getNewToken).access_token;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static AccessTokenResult GetTokenResult(string corpId, bool getNewToken = false)
        {
            if (!CheckRegistered(corpId))
            {
                throw new WeixinQyException(UN_REGISTER_ALERT);
            }

            var accessTokenBag = (AccessTokenBag)ItemCollection[corpId];
            lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.ExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    accessTokenBag.AccessTokenResult = CommonApi.GetToken(accessTokenBag.CorpId,
                        accessTokenBag.CorpSecret);
                    accessTokenBag.ExpireTime = DateTime.Now.AddSeconds(accessTokenBag.AccessTokenResult.expires_in);
                }
            }
            return accessTokenBag.AccessTokenResult;
        }

        /// <summary>
        /// 检查是否已经注册
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public new static bool CheckRegistered(string corpId)
        {
            return ItemCollection.CheckExisted(corpId);
        }
    }
}
