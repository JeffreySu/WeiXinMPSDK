/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：JsApiTicketContainer.cs
    文件功能描述：通用接口JsApiTicket容器，用于OPEN第三方JSSDK自动管理JsApiTicket，如果过期会重新获取
    
    
    创建标识：Senparc - 20150211
    
    修改标识：renny - 20150921
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.Entities;
using Senparc.Weixin.Open.Exceptions;

namespace Senparc.Weixin.Open.CommonAPIs
{
    /// <summary>
    /// JsApiTicketBag
    /// </summary>
    public class JsApiTicketBag : BaseContainerBag
    {
        public string ComponentAppId { get; set; }

        public ComponentBag ComponentBag { get; set; }

        public string authorizer_access_token { get; set; }
        public string authorizer_refresh_token { get; set; }
        public string authorizer_appid { get; set; }

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
    public class JsApiTicketContainer : BaseContainer<JsApiTicketBag>
    {
        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        public static void Register(string componentAppId, string _authorizer_appid)
        {
            var componentBag = ComponentContainer.TryGetItem(componentAppId);
            if (componentBag == null)
            {
                throw new WeixinOpenException("注册JsApiTicketContainer之前，必须先注册对应的ComponentContainer！当前ComponentAppId：" + componentAppId);
            }

            Update(componentAppId, new JsApiTicketBag()
            {
                ComponentBag = componentBag,
                authorizer_appid = _authorizer_appid,

                //authorizer_access_token = _authorizer_access_token,
                //authorizer_refresh_token = _authorizer_refresh_token,

                ExpireTime = DateTime.MinValue,
                JsApiTicketResult = new JsApiTicketResult()
            });
        }


        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="_componentAppId"></param>
        /// <param name="_componentAppSecret"></param>
        /// /// <param name="_componentVerifyTicket"></param>
        /// /// <param name="_authorizer_appid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetTicket(string _componentAppId, string _authorizer_appid, bool getNewTicket = false)
        {
            if (!CheckRegistered(_authorizer_appid) || getNewTicket)
            {
                Register(_componentAppId, _authorizer_appid);
            }
            return GetTicket(_authorizer_appid);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="_authorizer_appid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetTicket(string _authorizer_appid, bool getNewTicket = false)
        {
            return GetTicketResult(_authorizer_appid, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="_authorizer_appid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicketResult(string _authorizer_appid, bool getNewTicket = false)
        {
            if (!CheckRegistered(_authorizer_appid))
            {
                throw new WeixinException("此authorizer_appid尚未注册，请先使用JsApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            var accessTicketBag = ItemCollection[_authorizer_appid];
            lock (accessTicketBag.Lock)
            {
                if (getNewTicket || accessTicketBag.ExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    accessTicketBag.JsApiTicketResult = CommonApi.GetTicket(accessTicketBag.authorizer_access_token);

                    accessTicketBag.ExpireTime = DateTime.Now.AddSeconds(accessTicketBag.JsApiTicketResult.expires_in);
                }
            }
            return accessTicketBag.JsApiTicketResult;
        }

    }
}
