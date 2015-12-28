/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：AccessTokenContainer.cs
    文件功能描述：通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150702
    修改描述：添加GetFirstOrDefaultAppId()方法
    
    修改标识：Senparc - 20151004
    修改描述：v13.3.0 将JsApiTicketContainer整合到AccessTokenContainer

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// AccessToken及JsApiTicket包
    /// </summary>
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

        public ApiTicketResult JsApiTicketResult
        {
            get { return _jsApiTicketResult; }
            set { base.SetContainerProperty(ref _jsApiTicketResult, value); }
        }

        public DateTime JsApiTicketExpireTime
        {
            get { return _jsApiTicketExpireTime; }
            set { base.SetContainerProperty(ref _jsApiTicketExpireTime, value); }
        }
        public ApiTicketResult Wx_CardTicketResult
        {
            get { return _wx_cardTicketResult; }
            set { base.SetContainerProperty(ref _wx_cardTicketResult, value); }
        }
        public DateTime Wx_CardApiTicketExpireTime
        {
            get { return _wx_cardTicketExpireTime; }
            set { base.SetContainerProperty(ref _wx_cardTicketExpireTime, value); }
        }
        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        public object Lock = new object();

        private DateTime _jsApiTicketExpireTime;
        private ApiTicketResult _jsApiTicketResult;
        // 微信卡券api_ticket和jssdk api_ticket要区分开来
        // 详情看：http://mp.weixin.qq.com/wiki/7/aaa137b55fb2e0456bf8dd9148dd613f.html#.E9.99.84.E5.BD.954-.E5.8D.A1.E5.88.B8.E6.89.A9.E5.B1.95.E5.AD.97.E6.AE.B5.E5.8F.8A.E7.AD.BE.E5.90.8D.E7.94.9F.E6.88.90.E7.AE.97.E6.B3.95
        private ApiTicketResult _wx_cardTicketResult;
        private DateTime _wx_cardTicketExpireTime;

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
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public static void Register(string appId, string appSecret)
        {
            Update(appId, new AccessTokenBag()
            {
                AppId = appId,
                AppSecret = appSecret,
                AccessTokenExpireTime = DateTime.MinValue,
                AccessTokenResult = new AccessTokenResult()
            });
        }

        /// <summary>
        /// 返回已经注册的第一个AppId
        /// </summary>
        /// <returns></returns>
        public static string GetFirstOrDefaultAppId()
        {
            return ItemCollection.Keys.FirstOrDefault();
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
        /// 获取可用Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static AccessTokenResult GetAccessTokenResult(string appId, bool getNewToken = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new WeixinException("此appId尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！");
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

        #region ApiTicket,目前发现两种apiticket,分别是jsapi和wx_card

        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="ticketType">jsapi和wx_card两种api_ticket是不同的</param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetApiTicket(string appId, string appSecret, string ticketType = "jsapi", bool getNewTicket = false)
        {
            if (!CheckRegistered(appId) || getNewTicket)
            {
                Register(appId, appSecret);
            }
            return GetApiTicket(appId, ticketType);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        ///<param name="ticketType">jsapi和wx_card两种api_ticket是不同的</param>
        /// <returns></returns>
        public static string GetApiTicket(string appId, string ticketType = "jsapi", bool getNewTicket = false)
        {
            return GetApiTicketResult(appId, ticketType, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// /// <param name="ticketType">jsapi和wx_card两种api_ticket是不同的</param>
        /// <returns></returns>
        public static string GetJsApiTicket(string appId, string ticketType = "jsapi", bool getNewTicket = false)
        {
            return GetApiTicketResult(appId, ticketType, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static ApiTicketResult GetApiTicketResult(string appId, string ticketType = "jsapi", bool getNewTicket = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new WeixinException("此appId尚未注册，请先使用JsApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            var accessTokenBag = (AccessTokenBag)ItemCollection[appId];
            lock (accessTokenBag.Lock)
            {
                switch (ticketType)
                {
                    case "wx_card":
                        if (getNewTicket || accessTokenBag.Wx_CardApiTicketExpireTime <= DateTime.Now)
                        {
                            accessTokenBag.Wx_CardTicketResult = CommonApi.GetTicket(accessTokenBag.AppId, accessTokenBag.AppSecret, ticketType);
                            accessTokenBag.Wx_CardApiTicketExpireTime = DateTime.Now.AddSeconds(accessTokenBag.Wx_CardTicketResult.expires_in);
                        }
                        return accessTokenBag.Wx_CardTicketResult;
                    default:
                        if (getNewTicket || accessTokenBag.JsApiTicketExpireTime <= DateTime.Now)
                        {
                            //已过期，重新获取
                            accessTokenBag.JsApiTicketResult = CommonApi.GetTicket(accessTokenBag.AppId, accessTokenBag.AppSecret);
                            accessTokenBag.JsApiTicketExpireTime = DateTime.Now.AddSeconds(accessTokenBag.JsApiTicketResult.expires_in);
                        }
                        return accessTokenBag.JsApiTicketResult;
                }
            }
        }
        #endregion
    }
}
