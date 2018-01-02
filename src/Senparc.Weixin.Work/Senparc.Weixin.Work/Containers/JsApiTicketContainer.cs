/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：JsApiTicketContainer.cs
    文件功能描述：通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取


    创建标识：Senparc - 20150313

    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20160312
    修改描述：升级Container，继承自BaseContainer<JsApiTicketBag>

    修改标识：Senparc - 20160318
    修改描述：v3.3.4 使用FlushCache.CreateInstance使注册过程立即生效

    修改标识：Senparc - 20160717
    修改描述：v3.3.8 添加注册过程中的Name参数
    
    修改标识：Senparc - 20160803
    修改描述：v4.1.2 使用ApiUtility.GetExpireTime()方法处理过期
 
    修改标识：Senparc - 20160804
    修改描述：v4.1.3 增加TryGetTicketAsync，GetTicketAsync，GetTicketResultAsync的异步方法
    
    修改标识：Senparc - 20160813
    修改描述：v4.1.5 添加TryReRegister()方法，处理分布式缓存重启（丢失）的情况

    修改标识：Senparc - 20160813
    修改描述：v4.1.6 完善GetToken()方法
    
    修改标识：Senparc - 20160813
    修改描述：v4.1.8 修改命名空间为Senparc.Weixin.Work.Containers

    修改标识：Senparc - 20161003
    修改描述：v4.1.11 修复GetTicketResult()方法中的CheckRegistered()参数错误（少了appSecret）
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.Weixin.CacheUtility;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Exceptions;
using Senparc.Weixin.Utilities.WeixinUtility;

namespace Senparc.Weixin.Work.Containers
{
    /// <summary>
    /// JsApiTicketBag
    /// </summary>
    [Serializable]
    public class JsApiTicketBag : BaseContainerBag
    {
        public string AppId
        {
            get { return _appId; }
#if NET35 || NET40
            set { this.SetContainerProperty(ref _appId, value, "AppId"); }
#else
            set { this.SetContainerProperty(ref _appId, value); }
#endif
        }
        public string AppSecret
        {
            get { return _appSecret; }
#if NET35 || NET40
            set { this.SetContainerProperty(ref _appSecret, value, "AppSecret"); }
#else
            set { this.SetContainerProperty(ref _appSecret, value); }
#endif
        }

        public JsApiTicketResult JsApiTicketResult
        {
            get { return _jsApiTicketResult; }
#if NET35 || NET40
            set { this.SetContainerProperty(ref _jsApiTicketResult, value, "JsApiTicketResult"); }
#else
            set { this.SetContainerProperty(ref _jsApiTicketResult, value); }
#endif
        }

        public DateTime ExpireTime
        {
            get { return _expireTime; }
#if NET35 || NET40
            set { this.SetContainerProperty(ref _expireTime, value, "ExpireTime"); }
#else
            set { this.SetContainerProperty(ref _expireTime, value); }
#endif
        }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();

        private DateTime _expireTime;
        private JsApiTicketResult _jsApiTicketResult;
        private string _appSecret;
        private string _appId;
    }

    /// <summary>
    /// 通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    /// </summary>
    public class JsApiTicketContainer : BaseContainer<JsApiTicketBag>
    {
        private const string UN_REGISTER_ALERT = "此AppId尚未注册，JsApiTicketContainer.Register完成注册（全局执行一次即可）！";

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="name">标记JsApiTicket名称（如微信公众号名称），帮助管理员识别</param>
        /// 此接口无异步方法
        private static string BuildingKey(string corpId, string corpSecret)
        {
            return corpId + corpSecret;
        }
        public static void Register(string appId, string appSecret, string name = null)
        {
            //记录注册信息，RegisterFunc委托内的过程会在缓存丢失之后自动重试
            RegisterFunc = () =>
            {
                using (FlushCache.CreateInstance())
                {
                    var bag = new JsApiTicketBag()
                    {
                        Name = name,
                        AppId = appId,
                        AppSecret = appSecret,
                        ExpireTime = DateTime.MinValue,
                        JsApiTicketResult = new JsApiTicketResult()
                    };
                    Update(BuildingKey(appId,appSecret), bag);
                    return bag;
                }
            };
            RegisterFunc();
        }

        #region 同步方法


        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetTicket(string appId, string appSecret, bool getNewTicket = false)
        {
            if (!CheckRegistered(BuildingKey(appId, appSecret)) || getNewTicket)
            {
                Register(appId, appSecret);
            }
            return GetTicket(appId,appSecret,getNewTicket);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetTicket(string appId,string appSecret, bool getNewTicket = false)
        {
            return GetTicketResult(appId,appSecret,getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicketResult(string appId,string appSecret, bool getNewTicket = false)
        {
            if (!CheckRegistered(BuildingKey(appId, appSecret)))
            {
                throw new WeixinWorkException(UN_REGISTER_ALERT);
            }

            var jsApiTicketBag = TryGetItem(BuildingKey(appId, appSecret));
            lock (jsApiTicketBag.Lock)
            {
                if (getNewTicket || jsApiTicketBag.ExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    jsApiTicketBag.JsApiTicketResult = CommonApi.GetTicket(jsApiTicketBag.AppId, jsApiTicketBag.AppSecret);
                    jsApiTicketBag.ExpireTime = ApiUtility.GetExpireTime(jsApiTicketBag.JsApiTicketResult.expires_in);
                }
            }
            return jsApiTicketBag.JsApiTicketResult;
        }

        ///// <summary>
        ///// 检查是否已经注册
        ///// </summary>
        ///// <param name="appId"></param>
        ///// <returns></returns>
        ///// 此接口无异步方法
        //public new static bool CheckRegistered(string appId)
        //{
        //    return Cache.CheckExisted(appId);
        //}

        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static async Task<string> TryGetTicketAsync(string appId, string appSecret, bool getNewTicket = false)
        {
            if (!CheckRegistered(BuildingKey(appId, appSecret)) || getNewTicket)
            {
                Register(appId, appSecret);
            }
            return await GetTicketAsync(appId,appSecret,getNewTicket);
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<string> GetTicketAsync(string appId,string appSecret, bool getNewTicket = false)
        {
            var result = await GetTicketResultAsync(appId, appSecret,getNewTicket);
            return result.ticket;
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetTicketResultAsync(string appId,string appSecret,bool getNewTicket = false)
        {
            if (!CheckRegistered(BuildingKey(appId, appSecret)))
            {
                throw new WeixinWorkException(UN_REGISTER_ALERT);
            }

            var jsApiTicketBag = TryGetItem(BuildingKey(appId, appSecret));
            //lock (jsApiTicketBag.Lock)
            {
                if (getNewTicket || jsApiTicketBag.ExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    var jsApiTicketResult = await CommonApi.GetTicketAsync(jsApiTicketBag.AppId, jsApiTicketBag.AppSecret);
                    jsApiTicketBag.JsApiTicketResult = jsApiTicketResult;
                    //jsApiTicketBag.JsApiTicketResult = CommonApi.GetTicket(jsApiTicketBag.AppId, jsApiTicketBag.AppSecret);
                    jsApiTicketBag.ExpireTime = ApiUtility.GetExpireTime(jsApiTicketBag.JsApiTicketResult.expires_in);
                }
            }
            return jsApiTicketBag.JsApiTicketResult;
        }
        #endregion
#endif
    }
}
