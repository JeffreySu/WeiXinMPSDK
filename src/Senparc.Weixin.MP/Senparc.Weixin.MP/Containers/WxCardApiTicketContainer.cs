using System;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.CacheUtility;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Utilities.WeixinUtility;

namespace Senparc.Weixin.MP.Containers
{
    /// <summary>
    /// WxCardApiTicket包
    /// </summary>
    [Serializable]
    public class WxCardApiTicketBag : BaseContainerBag, IBaseContainerBag_AppId
    {
        public string AppId
        {
            get
            {
                return _appId;
            }
#if NET35 || NET40
            set { this.SetContainerProperty(ref _appId, value, "AppId"); }
#else
            set
            {
                this.SetContainerProperty(ref _appId, value);
            }
#endif
        }

        public string AppSecret
        {
            get
            {
                return _appSecret;
            }
#if NET35 || NET40
            set { this.SetContainerProperty(ref _appSecret, value, "AppSecret"); }
#else
            set
            {
                this.SetContainerProperty(ref _appSecret, value);
            }
#endif
        }

        public JsApiTicketResult WxCardApiTicketResult
        {
            get
            {
                return _WxCardApiTicketResult;
            }
#if NET35 || NET40
            set { this.SetContainerProperty(ref _WxCardApiTicketResult, value, "WxCardApiTicketResult"); }
#else
            set
            {
                this.SetContainerProperty(ref _WxCardApiTicketResult, value);
            }
#endif
        }

        public DateTime WxCardApiTicketExpireTime
        {
            get
            {
                return _WxCardApiTicketExpireTime;
            }
#if NET35 || NET40
            set { this.SetContainerProperty(ref _WxCardApiTicketExpireTime, value, "WxCardApiExpireTime"); }
#else
            set
            {
                this.SetContainerProperty(ref _WxCardApiTicketExpireTime, value);
            }
#endif
        }

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
        /// <param name="name">标记JsApiTicket名称（如微信公众号名称），帮助管理员识别</param>
        /*此接口不提供异步方法*/
        public static void Register(string appId, string appSecret, string name = null)
        {
            //记录注册信息，RegisterFunc委托内的过程会在缓存丢失之后自动重试
            RegisterFunc = () =>
            {
                using(FlushCache.CreateInstance())
                {
                    WxCardApiTicketBag bag = new WxCardApiTicketBag()
                    {
                        Name = name,
                        AppId = appId,
                        AppSecret = appSecret,
                        WxCardApiTicketExpireTime = DateTime.MinValue,
                        WxCardApiTicketResult = new JsApiTicketResult()
                    };
                    Update(appId, bag);
                    return bag;
                }
            };
            RegisterFunc();
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
            if(!CheckRegistered(appId) || getNewTicket)
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
            if(!CheckRegistered(appId))
            {
                throw new UnRegisterAppIdException(null, "此appId尚未注册，请先使用WxCardApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            WxCardApiTicketBag wxCardApiTicketBag = TryGetItem(appId);
            using(Cache.BeginCacheLock(LockResourceName, appId))//同步锁
            {
                if(getNewTicket || wxCardApiTicketBag.WxCardApiTicketExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    wxCardApiTicketBag.WxCardApiTicketResult = CommonApi.GetTicket(wxCardApiTicketBag.AppId,
                                                                                   wxCardApiTicketBag.AppSecret,
                                                                                   "wx_card");
                    wxCardApiTicketBag.WxCardApiTicketExpireTime = ApiUtility.GetExpireTime(wxCardApiTicketBag.WxCardApiTicketResult.expires_in);
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
            if(!CheckRegistered(appId) || getNewTicket)
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
            if(!CheckRegistered(appId))
            {
                throw new UnRegisterAppIdException(null, "此appId尚未注册，请先使用WxCardApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            WxCardApiTicketBag wxCardApiTicketBag = TryGetItem(appId);
            using(Cache.BeginCacheLock(LockResourceName, appId))//同步锁
            {
                if(getNewTicket || wxCardApiTicketBag.WxCardApiTicketExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    JsApiTicketResult wxCardApiTicketResult = await CommonApi.GetTicketAsync(wxCardApiTicketBag.AppId,
                                                                                             wxCardApiTicketBag.AppSecret);

                    wxCardApiTicketBag.WxCardApiTicketResult = wxCardApiTicketResult;
                    wxCardApiTicketBag.WxCardApiTicketExpireTime = DateTime.Now.AddSeconds(wxCardApiTicketBag.WxCardApiTicketResult.expires_in);
                }
            }
            return wxCardApiTicketBag.WxCardApiTicketResult;
        }
        #endregion
        #endregion
#endif
    }
}
