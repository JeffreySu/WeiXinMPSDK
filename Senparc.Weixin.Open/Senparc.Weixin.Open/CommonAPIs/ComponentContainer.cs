/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ComponentAccessTokenContainer.cs
    文件功能描述：通用接口ComponentAccessToken容器，用于自动管理ComponentAccessToken，如果过期会重新获取
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.Entities;

namespace Senparc.Weixin.Open.CommonAPIs
{
    /// <summary>
    /// 第三方APP信息包
    /// </summary>
    public class ComponentBag : BaseContainerBag
    {
        /// <summary>
        /// 第三方平台AppId
        /// </summary>
        public string ComponentAppId { get; set; }
        /// <summary>
        /// 第三方平台AppSecret
        /// </summary>
        public string ComponentAppSecret { get; set; }

        /// <summary>
        /// 第三方平台ComponentVerifyTicket（每隔10分钟微信会主动推送到服务器，IP必须在白名单内）
        /// </summary>
        public string ComponentVerifyTicket { get; set; }

        /// <summary>
        /// ComponentAccessTokenResult
        /// </summary>
        public ComponentAccessTokenResult ComponentAccessTokenResult { get; set; }
        /// <summary>
        /// ComponentAccessToken过期时间
        /// </summary>
        public DateTime ComponentAccessTokenExpireTime { get; set; }


        /// <summary>
        /// PreAuthCodeResult 预授权码结果
        /// </summary>
        public PreAuthCodeResult PreAuthCodeResult { get; set; }
        /// <summary>
        /// 预授权码过期时间
        /// </summary>
        public DateTime PreAuthCodeExpireTime { get; set; }


        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        public object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        public ComponentBag()
        {
            ComponentAccessTokenResult = new ComponentAccessTokenResult();
            ComponentAccessTokenExpireTime = DateTime.MinValue;

            PreAuthCodeResult = new PreAuthCodeResult();
            PreAuthCodeExpireTime = DateTime.MaxValue;
        }
    }

    /// <summary>
    /// 通用接口ComponentAccessToken容器，用于自动管理ComponentAccessToken，如果过期会重新获取
    /// </summary>
    public class ComponentContainer : BaseContainer<ComponentBag>
    {
        private static void TryRegister(string componentAppId, string componentAppSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId) || getNewToken)
            {
                Register(componentAppId, componentAppSecret);
            }
        }

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        public static void Register(string componentAppId, string componentAppSecret)
        {
            Update(componentAppId, new ComponentBag()
            {
                ComponentAppId = componentAppId,
                ComponentAppSecret = componentAppSecret,
            });
        }

        /// <summary>
        /// 检查是否已经注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <returns></returns>
        public static bool CheckRegistered(string componentAppId)
        {
            return ItemCollection.ContainsKey(componentAppId);
        }


        #region component_verify_ticket

        /// <summary>
        /// 获取ComponentVerifyTicket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <returns>如果不存在，则返回null</returns>
        public static string TryGetComponentVerifyTicket(string componentAppId)
        {
            return TryGetItem(componentAppId, bag => bag.ComponentVerifyTicket);
        }

        #endregion

        #region component_access_token

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetAccessToken(string componentAppId, string componentAppSecret, bool getNewToken = false)
        {
            TryRegister(componentAppId, componentAppSecret, getNewToken);
            return GetAccessToken(componentAppId);
        }

        /// <summary>
        /// 获取可用AccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetAccessToken(string componentAppId, bool getNewToken = false)
        {
            return GetAccessTokenResult(componentAppId, getNewToken).component_access_token;
        }

        /// <summary>
        /// 获取可用AccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static ComponentAccessTokenResult GetAccessTokenResult(string componentAppId, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinException("此appId尚未注册，请先使用ComponentAccessTokenContainer.Register完成注册（全局执行一次即可）！");
            }

            var accessTokenBag = ItemCollection[componentAppId];
            lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.ComponentAccessTokenExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    var componentVerifyTicket = TryGetComponentVerifyTicket(componentAppId);

                    accessTokenBag.ComponentAccessTokenResult = CommonApi.GetComponentAccessToken(accessTokenBag.ComponentAppId, accessTokenBag.ComponentAppSecret, componentVerifyTicket);

                    accessTokenBag.ComponentAccessTokenExpireTime = DateTime.Now.AddSeconds(accessTokenBag.ComponentAccessTokenResult.expires_in);
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
        /// <param name="componentVerifyTicket"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetPreAuthCode(string componentAppId, string componentAppSecret, string componentVerifyTicket, bool getNewToken = false)
        {
            TryRegister(componentAppId, componentAppSecret, getNewToken);
            return GetGetPreAuthCode(componentAppId);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetGetPreAuthCode(string componentAppId, bool getNewToken = false)
        {
            return GetPreAuthCodeResult(componentAppId, getNewToken).pre_auth_code;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static PreAuthCodeResult GetPreAuthCodeResult(string componentAppId, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinException("此appId尚未注册，请先使用PreAuthCodeContainer.Register完成注册（全局执行一次即可）！");
            }

            var accessTokenBag = ItemCollection[componentAppId];
            lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.PreAuthCodeExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    var componentVerifyTicket = TryGetComponentVerifyTicket(componentAppId);

                    accessTokenBag.PreAuthCodeResult = CommonApi.GetPreAuthCode(accessTokenBag.ComponentAppId, accessTokenBag.ComponentAppSecret, componentVerifyTicket);

                    accessTokenBag.PreAuthCodeExpireTime = DateTime.Now.AddSeconds(accessTokenBag.PreAuthCodeResult.expires_in);
                }
            }
            return accessTokenBag.PreAuthCodeResult;
        }
        #endregion

    }
}
