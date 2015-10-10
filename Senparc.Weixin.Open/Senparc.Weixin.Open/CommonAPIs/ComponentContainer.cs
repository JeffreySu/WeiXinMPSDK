﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ComponentContainer.cs
    文件功能描述：通用接口ComponentAccessToken容器，用于自动管理ComponentAccessToken，如果过期会重新获取
    
    
    创建标识：Senparc - 20150430

    修改标识：Senparc - 20151004
    修改描述：v1.4.1 改名为ComponentContainer.cs，合并多个ComponentApp相关容器

    修改标识：Senparc - 20151005
    修改描述：v1.4.3 添加ComponentVerifyTicketExpireTime及自动更新机制

----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.Entities;
using Senparc.Weixin.Open.Exceptions;

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
        /// 第三方平台ComponentVerifyTicket过期时间（实际上过期之后仍然可以使用一段时间）
        /// </summary>
        public DateTime ComponentVerifyTicketExpireTime { get; set; }

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


        public string AuthorizerAccessToken { get; set; }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        public object Lock = new object();

        /// <summary>
        /// ComponentBag
        /// </summary>
        public ComponentBag()
        {
            ComponentAccessTokenResult = new ComponentAccessTokenResult();
            ComponentAccessTokenExpireTime = DateTime.MinValue;

            PreAuthCodeResult = new PreAuthCodeResult();
            PreAuthCodeExpireTime = DateTime.MinValue;
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

        /// <summary>
        /// 检查AppId是否已经注册，如果没有，则创建
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getNewToken"></param>
        private static void TryRegister(string componentAppId, string componentAppSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId) || getNewToken)
            {
                Register(componentAppId, componentAppSecret, null);
            }
        }

        /// <summary>
        /// 获取ComponentVerifyTicket的方法
        /// </summary>
        public static Func<string, string> GetComponentVerifyTicketFunc = null;

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getComponentVerifyTicketFunc">获取ComponentVerifyTicket的方法</param>
        public static void Register(string componentAppId, string componentAppSecret, Func<string, string> getComponentVerifyTicketFunc)
        {
            if (GetComponentVerifyTicketFunc == null)
            {
                GetComponentVerifyTicketFunc = getComponentVerifyTicketFunc;
            }

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
        public new static bool CheckRegistered(string componentAppId)
        {
            return ItemCollection.ContainsKey(componentAppId);
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
            if (getNewToken || componentVerifyTicket == default(string) || bag.ComponentVerifyTicketExpireTime < DateTime.Now)
            {
                if (GetComponentVerifyTicketFunc == null)
                {
                    throw new WeixinOpenException("GetComponentVerifyTicketFunc必须在注册时提供！", TryGetItem(componentAppId));
                }
                componentVerifyTicket = GetComponentVerifyTicketFunc(componentAppId); //获取最新的componentVerifyTicket
                bag.ComponentVerifyTicket = componentVerifyTicket;
                bag.ComponentVerifyTicketExpireTime = DateTime.Now.AddMinutes(COMPONENT_VERIFY_TICKET_UPDATE_MINUTES);
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
            });
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
        public static string TryGetComponentAccessToken(string componentAppId, string componentAppSecret, string componentVerifyTicket, bool getNewToken = false)
        {
            TryRegister(componentAppId, componentAppSecret, getNewToken);
            return GetComponentAccessToken(componentAppId, componentVerifyTicket);
        }

        /// <summary>
        /// 获取可用AccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetComponentAccessToken(string componentAppId, string componentVerifyTicket, bool getNewToken = false)
        {
            return GetComponentAccessTokenResult(componentAppId, componentVerifyTicket, getNewToken).component_access_token;
        }

        /// <summary>
        /// 获取可用AccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static ComponentAccessTokenResult GetComponentAccessTokenResult(string componentAppId, string componentVerifyTicket, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var accessTokenBag = ItemCollection[componentAppId];
            lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.ComponentAccessTokenExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    //var componentVerifyTicket = TryGetComponentVerifyTicket(componentAppId);

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

            var componentBag = ItemCollection[componentAppId];
            lock (componentBag.Lock)
            {
                if (getNewToken || componentBag.PreAuthCodeExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    var componentVerifyTicket = TryGetComponentVerifyTicket(componentAppId);

                    var accessToken = TryGetComponentAccessToken(componentAppId, componentBag.ComponentAppSecret, componentVerifyTicket);

                    var preAuthCodeResult = CommonApi.GetPreAuthCode(componentBag.ComponentAppId, accessToken);

                    //if (preAuthCodeResult)
                    //{

                    //}

                    componentBag.PreAuthCodeResult = preAuthCodeResult;

                    //TODO:这里有出现expires_in=0的情况，导致始终处于过期状态（也可能是因为参数过期等原因没有返回正确的数据，待观察）
                    var expiresIn = componentBag.PreAuthCodeResult.expires_in > 0
                        ? componentBag.PreAuthCodeResult.expires_in
                        : 60 * 20;//默认为20分钟
                    componentBag.PreAuthCodeExpireTime = DateTime.Now.AddSeconds(expiresIn);

                }
            }
            return componentBag.PreAuthCodeResult;
        }
        #endregion

    }
}
