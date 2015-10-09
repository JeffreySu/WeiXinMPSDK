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
using System.Data;
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

        ///// <summary>
        ///// 从ComponentContainer取过来的对应ComponentAppId的ComponentBag
        ///// </summary>
        //public ComponentBag ComponentBag { get; set; }

        /// <summary>
        /// 授权信息
        /// </summary>
        public GetAuthorizerInfoResult AuthorizerInfoResult { get; set; }
        public DateTime AuthorizerInfoExpireTime { get; set; }


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
        private static void Register(string componentAppId, string authorizerAppId)
        {
            var componentBag = ComponentContainer.TryGetItem(componentAppId);
            if (componentBag == null)
            {
                throw new WeixinOpenException(string.Format("注册AuthorizerContainer之前，必须先注册对应的ComponentContainer！ComponentAppId：{0},AuthorizerAppId:{1}", componentAppId, authorizerAppId));
            }

            Update(componentAppId, new AuthorizerBag()
            {
                AuthorizerAppId = authorizerAppId,
                ComponentAppId = componentAppId,

                AuthorizerInfoResult = new GetAuthorizerInfoResult(),//可以进一步初始化
                AuthorizerInfoExpireTime = DateTime.MaxValue,

                JsApiTicketResult = new JsApiTicketResult(),
                JsApiTicketExpireTime = DateTime.MinValue,
            });
        }

        /// <summary>
        /// 尝试注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <returns></returns>
        private static void TryRegister(string componentAppId, string authorizerAppid)
        {
            if (!CheckRegistered(authorizerAppid))
            {
                Register(componentAppId, authorizerAppid);
            }
        }

        #region 授权信息

        /// <summary>
        /// 获取可用AuthorizerAccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetAuthorizerAccessToken(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            return GetAuthorizerInfoResult(componentAppId, authorizerAppid).authorization_info.authorizer_access_token;
        }

        /// <summary>
        /// 获取可用的GetAuthorizerInfoResult
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        /// <exception cref="WeixinOpenException">此公众号没有高级权限</exception>
        public static GetAuthorizerInfoResult GetAuthorizerInfoResult(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizerBag = ItemCollection[authorizerAppid];
            lock (authorizerBag.Lock)
            {
                if (getNewTicket || authorizerBag.AuthorizerInfoExpireTime <= DateTime.Now)
                {
                    var componentVerifyTicket = ComponentContainer.TryGetComponentVerifyTicket(componentAppId);
                    var componentAccessToken = ComponentContainer.GetComponentAccessToken(componentAppId, componentVerifyTicket);

                    //已过期，重新获取
                    authorizerBag.AuthorizerInfoResult = ComponentApi.GetAuthorizerInfo(componentAccessToken, componentAppId, authorizerAppid);//TODO:如果是过期，可以通过刷新的方式重新获取

                    var componentBag = ComponentContainer.TryGetItem(componentAppId);

                    if (string.IsNullOrEmpty(authorizerBag.AuthorizerInfoResult.authorization_info.authorizer_access_token))
                    {
                        //账号没有此权限
                        throw new WeixinOpenException("此公众号没有高级权限", componentBag);
                    }

                    authorizerBag.AuthorizerInfoExpireTime =
                        DateTime.Now.AddSeconds(authorizerBag.AuthorizerInfoResult.authorization_info.expires_in);
                }
            }
            return authorizerBag.AuthorizerInfoResult;
        }

        #endregion

        #region JSTicket


        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetJsApiTicket(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            return GetJsApiTicket(componentAppId, authorizerAppid);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetJsApiTicket(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            return GetJsApiTicketResult(componentAppId, authorizerAppid, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetJsApiTicketResult(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var accessTicketBag = ItemCollection[authorizerAppid];
            lock (accessTicketBag.Lock)
            {
                if (getNewTicket || accessTicketBag.JsApiTicketExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    var authorizerAccessToken = TryGetAuthorizerAccessToken(componentAppId, authorizerAppid);

                    accessTicketBag.JsApiTicketResult = CommonApi.GetJsApiTicket(authorizerAccessToken);

                    accessTicketBag.JsApiTicketExpireTime = DateTime.Now.AddSeconds(accessTicketBag.JsApiTicketResult.expires_in);
                }
            }
            return accessTicketBag.JsApiTicketResult;
        }

        #endregion
    }
}
