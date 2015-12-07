/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：JsApiTicketContainer.cs
    文件功能描述：通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.CommonAPIs
{
    class JsApiTicketBag
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public DateTime ExpireTime { get; set; }

        public JsApiTicketResult JsApiTicketResult { get; set; }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        public object Lock = new object();
    }

    /// <summary>
    /// 通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    /// </summary>
    public class JsApiTicketContainer
    {
        static string jsTicketTokenCollection = "AccessTokenCollection-{0}-{1}";

        static Dictionary<string, JsApiTicketBag> JsApiTicketCollection =
           new Dictionary<string, JsApiTicketBag>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public static void Register(string appId, string appSecret, string connectString = "")
        {
            if (!string.IsNullOrEmpty(connectString))
                RedisCacheManager.RedisCacheManagerInint(connectString);

            if (JsApiTicketCollection == null)
                JsApiTicketCollection = new Dictionary<string, JsApiTicketBag>(StringComparer.OrdinalIgnoreCase);

            JsApiTicketCollection[appId] = new JsApiTicketBag()
            {
                AppId = appId,
                AppSecret = appSecret,
                ExpireTime = DateTime.MinValue,
                JsApiTicketResult = new JsApiTicketResult()
            };
        }

        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetTicket(string appId, string appSecret, string type = "jsapi", bool getNewTicket = false)
        {
            if (!CheckRegistered(appId) || getNewTicket)
            {
                Register(appId, appSecret);
            }
            return GetTicket(appId);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetTicket(string appId, string type = "jsapi", bool getNewTicket = false)
        {
            return GetTicketResult(appId, type, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicketResult(string appId, string type = "jsapi", bool getNewTicket = false)
        {
            if (!JsApiTicketCollection.ContainsKey(appId))
            {
                throw new WeixinException("此appId尚未注册，请先使用JsApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            RedisCacheManager.RedisCacheOpenConnect();
            string key = string.Format(jsTicketTokenCollection, appId, type);
            var accessTicketBag = RedisCacheManager.Get(key, () =>
            {
                return JsApiTicketCollection[appId];
            });

            lock (accessTicketBag.Lock)
            {
                if (getNewTicket || accessTicketBag.ExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    accessTicketBag.JsApiTicketResult = CommonApi.GetTicket(accessTicketBag.AppId, accessTicketBag.AppSecret, type);
                    accessTicketBag.ExpireTime = DateTime.Now.AddSeconds(accessTicketBag.JsApiTicketResult.expires_in);

                    RedisCacheManager.Set(key, accessTicketBag, 120);
                }
            }
            //RedisCacheManager.Dispose();
            return accessTicketBag.JsApiTicketResult;
        }

        /// <summary>
        /// 检查是否已经注册
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static bool CheckRegistered(string appId)
        {
            return JsApiTicketCollection.ContainsKey(appId);
        }
    }
}
