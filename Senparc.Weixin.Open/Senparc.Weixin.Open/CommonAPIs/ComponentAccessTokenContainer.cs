/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ComponentAccessTokenContainer.cs
    文件功能描述：通用接口ComponentAccessToken容器，用于自动管理ComponentAccessToken，如果过期会重新获取
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.Entities;

namespace Senparc.Weixin.Open.CommonAPIs
{
    class ComponentAccessTokenBag
    {
        public string ComponentAppId { get; set; }
        public string ComponentAppSecret { get; set; }
        public DateTime ExpireTime { get; set; }
        public ComponentAccessTokenResult ComponentAccessTokenResult { get; set; }
        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        public object Lock = new object();
    }

    /// <summary>
    /// 通用接口ComponentAccessToken容器，用于自动管理ComponentAccessToken，如果过期会重新获取
    /// </summary>
    public class ComponentAccessTokenContainer
    {
        static Dictionary<string, ComponentAccessTokenBag> ComponentAccessTokenCollection =
           new Dictionary<string, ComponentAccessTokenBag>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        public static void Register(string componentAppId, string componentAppSecret)
        {
            ComponentAccessTokenCollection[componentAppId] = new ComponentAccessTokenBag()
            {
                ComponentAppId = componentAppId,
                ComponentAppSecret = componentAppSecret,
                ExpireTime = DateTime.MinValue,
                ComponentAccessTokenResult = new ComponentAccessTokenResult()
            };
        }

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="componentVerifyTicket"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetToken(string componentAppId, string componentAppSecret, string componentVerifyTicket, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId) || getNewToken)
            {
                Register(componentAppId, componentAppSecret);
            }
            return GetToken(componentAppId, componentVerifyTicket);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetToken(string componentAppId,string componentVerifyTicket, bool getNewToken = false)
        {
            return GetTokenResult(componentAppId, componentVerifyTicket, getNewToken).component_access_token;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static ComponentAccessTokenResult GetTokenResult(string componentAppId, string componentVerifyTicket, bool getNewToken = false)
        {
            if (!ComponentAccessTokenCollection.ContainsKey(componentAppId))
            {
                throw new WeixinException("此appId尚未注册，请先使用ComponentAccessTokenContainer.Register完成注册（全局执行一次即可）！");
            }

            var accessTokenBag = ComponentAccessTokenCollection[componentAppId];
            lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.ExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    accessTokenBag.ComponentAccessTokenResult = CommonApi.GetComponentAccessToken(accessTokenBag.ComponentAppId, accessTokenBag.ComponentAppSecret, componentVerifyTicket);
                    accessTokenBag.ExpireTime = DateTime.Now.AddSeconds(accessTokenBag.ComponentAccessTokenResult.expires_in);
                }
            }
            return accessTokenBag.ComponentAccessTokenResult;
        }

        /// <summary>
        /// 检查是否已经注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <returns></returns>
        public static bool CheckRegistered(string componentAppId)
        {
            return ComponentAccessTokenCollection.ContainsKey(componentAppId);
        }
    }
}
