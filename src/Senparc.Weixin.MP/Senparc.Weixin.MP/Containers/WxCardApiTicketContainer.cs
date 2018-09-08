#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：WxCardApiTicketContainer.cs
    文件功能描述：WxCardApiTicketContainer


    创建标识：Senparc - 20180419

    修改标识：Senparc - 20180614
    修改描述：CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存
   
    修改标识：Senparc - 20180707
    修改描述：v15.0.9 Container 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下


----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Utilities.WeixinUtility;
using Senparc.CO2NET.CacheUtility;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.MP.Containers
{
    /// <summary>
    /// WxCardApiTicket包
    /// </summary>
    [Serializable]
    public class WxCardApiTicketBag : BaseContainerBag, IBaseContainerBag_AppId
    {
        public string AppId { get; set; }
        //        {
        //            get
        //            {
        //                return _appId;
        //            }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _appId, value, "AppId"); }
        //#else
        //            set
        //            {
        //                this.SetContainerProperty(ref _appId, value);
        //            }
        //#endif
        //        }

        public string AppSecret { get; set; }
        //        {
        //            get
        //            {
        //                return _appSecret;
        //            }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _appSecret, value, "AppSecret"); }
        //#else
        //            set
        //            {
        //                this.SetContainerProperty(ref _appSecret, value);
        //            }
        //#endif
        //        }

        public JsApiTicketResult WxCardApiTicketResult { get; set; }
        //        {
        //            get
        //            {
        //                return _WxCardApiTicketResult;
        //            }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _WxCardApiTicketResult, value, "WxCardApiTicketResult"); }
        //#else
        //            set
        //            {
        //                this.SetContainerProperty(ref _WxCardApiTicketResult, value);
        //            }
        //#endif
        //        }

        public DateTime WxCardApiTicketExpireTime { get; set; }
        //        {
        //            get
        //            {
        //                return _WxCardApiTicketExpireTime;
        //            }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _WxCardApiTicketExpireTime, value, "WxCardApiExpireTime"); }
        //#else
        //            set
        //            {
        //                this.SetContainerProperty(ref _WxCardApiTicketExpireTime, value);
        //            }
        //#endif
        //        }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();

        private DateTime _WxCardApiTicketExpireTime;
        private JsApiTicketResult _WxCardApiTicketResult;
        private string _appSecret;
        private string _appId;
    }

    /// <summary>
    /// 通用接口WxCardApiTicket容器，用于自动管理WxCardApiTicket，如果过期会重新获取
    /// </summary>
    public class WxCardApiTicketContainer : BaseContainer<WxCardApiTicketBag>
    {
        const string LockResourceName = "MP.WxCardApiTicketContainer";

        #region 同步方法


        //static Dictionary<string,WxCardApiTicketBag> WxCardApiTicketCollection =
        //   new Dictionary<string, WxCardApiTicketBag>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="name">标记JsApiTicket名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        /*此接口不提供异步方法*/
        public static void Register(string appId, string appSecret, string name = null)
        {
            //记录注册信息，RegisterFunc委托内的过程会在缓存丢失之后自动重试
            RegisterFunc = () =>
            {
                //using(FlushCache.CreateInstance())
                //{
                WxCardApiTicketBag bag = new WxCardApiTicketBag()
                {
                    Name = name,
                    AppId = appId,
                    AppSecret = appSecret,
                    WxCardApiTicketExpireTime = DateTime.MinValue,
                    WxCardApiTicketResult = new JsApiTicketResult()
                };
                Update(appId, bag, null);
                return bag;
                //}
            };
            RegisterFunc();

            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppId = appId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppSecret = appSecret;
            }

        }


        #region WxCardApiTicket

        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetWxCardApiTicket(string appId, string appSecret, bool getNewTicket = false)
        {
            if (!CheckRegistered(appId) || getNewTicket)
            {
                Register(appId, appSecret);
            }
            return GetWxCardApiTicket(appId);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetWxCardApiTicket(string appId, bool getNewTicket = false)
        {
            return GetWxCardApiTicketResult(appId, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetWxCardApiTicketResult(string appId, bool getNewTicket = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new UnRegisterAppIdException(null, "此appId尚未注册，请先使用WxCardApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            WxCardApiTicketBag wxCardApiTicketBag = TryGetItem(appId);
            using (Cache.BeginCacheLock(LockResourceName, appId))//同步锁
            {
                if (getNewTicket || wxCardApiTicketBag.WxCardApiTicketExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    wxCardApiTicketBag.WxCardApiTicketResult = CommonApi.GetTicket(wxCardApiTicketBag.AppId,
                                                                                   wxCardApiTicketBag.AppSecret,
                                                                                   "wx_card");
                    wxCardApiTicketBag.WxCardApiTicketExpireTime = ApiUtility.GetExpireTime(wxCardApiTicketBag.WxCardApiTicketResult.expires_in);
                    Update(wxCardApiTicketBag, null);
                }
            }
            return wxCardApiTicketBag.WxCardApiTicketResult;
        }

        #endregion

        #endregion

#if !NET35 && !NET40
        #region 异步方法
        #region WxCardApiTicket

        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static async Task<string> TryGetWxCardApiTicketAsync(string appId,
                                                                    string appSecret,
                                                                    bool getNewTicket = false)
        {
            if (!CheckRegistered(appId) || getNewTicket)
            {
                Register(appId, appSecret);
            }
            return await GetWxCardApiTicketAsync(appId, getNewTicket);
        }

        /// <summary>
        ///【异步方法】 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<string> GetWxCardApiTicketAsync(string appId, bool getNewTicket = false)
        {
            JsApiTicketResult result = await GetWxCardApiTicketResultAsync(appId, getNewTicket);
            return result.ticket;
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetWxCardApiTicketResultAsync(string appId,
                                                                                  bool getNewTicket = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new UnRegisterAppIdException(null, "此appId尚未注册，请先使用WxCardApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            WxCardApiTicketBag wxCardApiTicketBag = TryGetItem(appId);
            using (Cache.BeginCacheLock(LockResourceName, appId))//同步锁
            {
                if (getNewTicket || wxCardApiTicketBag.WxCardApiTicketExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    JsApiTicketResult wxCardApiTicketResult = await CommonApi.GetTicketAsync(wxCardApiTicketBag.AppId,
                                                                                             wxCardApiTicketBag.AppSecret);

                    wxCardApiTicketBag.WxCardApiTicketResult = wxCardApiTicketResult;
                    wxCardApiTicketBag.WxCardApiTicketExpireTime = DateTime.Now.AddSeconds(wxCardApiTicketBag.WxCardApiTicketResult.expires_in);
                    Update(wxCardApiTicketBag, null);
                }
            }
            return wxCardApiTicketBag.WxCardApiTicketResult;
        }
        #endregion
        #endregion
#endif
    }
}
