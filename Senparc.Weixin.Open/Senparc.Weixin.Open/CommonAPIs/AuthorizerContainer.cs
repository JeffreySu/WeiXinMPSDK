/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：AuthorizerContainer.cs
    文件功能描述：通用接口JsApiTicket容器，用于OPEN第三方JSSDK自动管理JsApiTicket，如果过期会重新获取
    
    
    创建标识：Senparc - 20150211
    
    修改标识：renny - 20150921
    修改描述：整理接口
    
    修改标识：senparc - 20151004
    修改描述：文件名从JsApiTicketContainer.cs变为AuthorizerContainer.cs，用于集成所有授权方信息

    ----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.Entities;
using Senparc.Weixin.Open.Exceptions;

namespace Senparc.Weixin.Open.CommonAPIs
{
    /// <summary>
    /// 之前的JsApiTicketBag
    /// </summary>
    public class AuthorizerBag : BaseContainerBag
    {
        /// <summary>
        /// 授权方AppId，缓存中实际的Key
        /// </summary>
        public string AuthorizerAppId { get; set; }

        /// <summary>
        /// 第三方平台AppId
        /// </summary>
        public string ComponentAppId { get; set; }


        public ComponentBag ComponentBag { get; set; }

        /// <summary>
        /// 授权信息
        /// </summary>
        public GetAuthorizerInfoResult AuthorizerInfoResult { get; set; }

        public JsApiTicketResult JsApiTicketResult { get; set; }
        public DateTime JsApiTicketExpireTime { get; set; }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        public object Lock = new object();
    }

    /// <summary>
    /// 授权方信息（用户的微信公众号）
    /// 包括通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    /// </summary>
    public class AuthorizerContainer : BaseContainer<AuthorizerBag>
    {
        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        public static void Register(string componentAppId, string authorizerAppId)
        {
            var componentBag = ComponentContainer.TryGetItem(componentAppId);
            if (componentBag == null)
            {
                throw new WeixinOpenException(string.Format("注册JsApiTicketContainer之前，必须先注册对应的ComponentContainer！ComponentAppId：{0},AuthorizerAppId:{1}", componentAppId, authorizerAppId));
            }

            Update(componentAppId, new AuthorizerBag()
            {
                AuthorizerAppId = authorizerAppId,
                ComponentAppId = componentAppId,
                ComponentBag = componentBag,

                AuthorizerInfoResult = new GetAuthorizerInfoResult(),//可以进一步初始化

                JsApiTicketResult = new JsApiTicketResult(),
                JsApiTicketExpireTime = DateTime.MinValue,
            });
        }

        #region 授权信息

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetAuthorizerInfoResult(string authorizerAppid, bool getNewTicket = false)
        {

        }

        #endregion

        #region JSTicket


        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="_componentAppId"></param>
        /// <param name="_componentAppSecret"></param>
        /// /// <param name="_componentVerifyTicket"></param>
        /// /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetJsApiTicket(string _componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            if (!CheckRegistered(authorizerAppid) || getNewTicket)
            {
                Register(_componentAppId, authorizerAppid);
            }
            return GetJsApiTicket(authorizerAppid);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetJsApiTicket(string authorizerAppid, bool getNewTicket = false)
        {
            return GetJsApiTicketResult(authorizerAppid, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetJsApiTicketResult(string authorizerAppid, bool getNewTicket = false)
        {
            if (!CheckRegistered(authorizerAppid))
            {
                throw new WeixinException("此authorizer_appid尚未注册，请先使用JsApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            var accessTicketBag = ItemCollection[authorizerAppid];
            lock (accessTicketBag.Lock)
            {
                if (getNewTicket || accessTicketBag.JsApiTicketExpireTime <= DateTime.Now)
                {
                    


                    //已过期，重新获取
                    accessTicketBag.JsApiTicketResult = CommonApi.GetJsApiTicket(accessTicketBag.authorizer_access_token);

                    accessTicketBag.JsApiTicketExpireTime = DateTime.Now.AddSeconds(accessTicketBag.JsApiTicketResult.expires_in);
                }
            }
            return accessTicketBag.JsApiTicketResult;
        }

        #endregion
    }
}
