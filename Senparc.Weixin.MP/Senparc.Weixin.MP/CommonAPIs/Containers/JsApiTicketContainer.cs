/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：JsApiTicketContainer.cs
    文件功能描述：通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    
    
    创建标识：Senparc - 20160206
 
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// JsApiTicket包
    /// </summary>
    public class JsApiTicketBag : BaseContainerBag
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }


        public JsApiTicketResult JsApiTicketResult
        {
            get { return _jsApiTicketResult; }
            set { base.SetContainerProperty(ref _jsApiTicketResult, value); }
        }

        public DateTime JsApiTicketExpireTime
        {
            get { return _jsApiTicketExpireTime; }
            set { base.SetContainerProperty(ref _jsApiTicketExpireTime, value); }
        }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        public object Lock = new object();

        private DateTime _jsApiTicketExpireTime;
        private JsApiTicketResult _jsApiTicketResult;
        private string _appSecret;
        private string _appId;
    }

    /// <summary>
    /// 通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    /// </summary>
    public class JsApiTicketContainer : BaseContainer<JsApiTicketBag>
    {
        static Dictionary<string, JsApiTicketBag> JsApiTicketCollection =
           new Dictionary<string, JsApiTicketBag>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public static void Register(string appId, string appSecret)
        {
            JsApiTicketCollection[appId] = new JsApiTicketBag()
            {
                AppId = appId,
                AppSecret = appSecret,
                JsApiTicketExpireTime = DateTime.MinValue,
                JsApiTicketResult = new JsApiTicketResult()
            };
        }


        #region JsApiTicket

        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetJsApiTicket(string appId, string appSecret, bool getNewTicket = false)
        {
            if (!CheckRegistered(appId) || getNewTicket)
            {
                Register(appId, appSecret);
            }
            return GetJsApiTicket(appId);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetJsApiTicket(string appId, bool getNewTicket = false)
        {
            return GetJsApiTicketResult(appId, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetJsApiTicketResult(string appId, bool getNewTicket = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new WeixinException("此appId尚未注册，请先使用JsApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            var accessTokenBag = (JsApiTicketBag)ItemCollection[appId];
            lock (accessTokenBag.Lock)
            {
                if (getNewTicket || accessTokenBag.JsApiTicketExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    accessTokenBag.JsApiTicketResult = CommonApi.GetTicket(accessTokenBag.AppId, accessTokenBag.AppSecret);
                    accessTokenBag.JsApiTicketExpireTime = DateTime.Now.AddSeconds(accessTokenBag.JsApiTicketResult.expires_in);
                }
            }
            return accessTokenBag.JsApiTicketResult;
        }

        #endregion

    }
}
