﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc

    文件名：JsApiTicketContainer.cs
    文件功能描述：通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取


    创建标识：Senparc - 20160206

    修改标识：Senparc - 20160206
    修改描述：将public object Lock更改为internal object Lock

    修改标识：Senparc - 20160318
    修改描述：13.6.10 使用FlushCache.CreateInstance使注册过程立即生效

    修改标识：Senparc - 20160717
    修改描述：v13.8.11 添加注册过程中的Name参数
    
    修改标识：Senparc - 20160801
    修改描述：v14.2.1 转移到Senparc.Weixin.MP.Containers命名空间下

    修改标识：Senparc - 20160803
    修改描述：v14.2.3 使用ApiUtility.GetExpireTime()方法处理过期
 
    修改标识：Senparc - 20160804
    修改描述：v14.2.4 增加TryGetJsApiTicketAsync，GetJsApiTicketAsync，GetJsApiTicketResultAsync的异步方法


    修改标识：Senparc - 20160808
    修改描述：v14.3.0 删除 ItemCollection 属性，直接使用ContainerBag加入到缓存
    
    修改标识：Senparc - 20160813
    修改描述：v14.3.4 添加TryReRegister()方法，处理分布式缓存重启（丢失）的情况
    
    修改标识：Senparc - 20160813
    修改描述：v14.3.6 完善getNewToken参数传递

    修改标识：Senparc - 20180614
    修改描述：CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存

    修改标识：Senparc - 20180707
    修改描述：v15.0.9 Container 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下

    修改标识：Senparc - 20181226
    修改描述：v16.6.2 修改 DateTime 为 DateTimeOffset

    修改标识：Senparc - 20190418
    修改描述：v17.0.0 支持异步 Container

    修改标识：Senparc - 20190503
    修改描述：v16.7.2 完善 Container 注册委托的储存类型，解决多账户下的注册冲突问题
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.CO2NET.CacheUtility;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Utilities.WeixinUtility;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.MP.Containers
{
    /// <summary>
    /// JsApiTicket包
    /// </summary>
    [Serializable]
    public class JsApiTicketBag : BaseContainerBag, IBaseContainerBag_AppId
    {
        public string AppId { get; set; }
        //        {
        //            get { return _appId; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _appId, value, "AppId"); }
        //#else
        //            set { this.SetContainerProperty(ref _appId, value); }
        //#endif
        //        }
        public string AppSecret { get; set; }
        //        {
        //            get { return _appSecret; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _appSecret, value, "AppSecret"); }
        //#else
        //            set { this.SetContainerProperty(ref _appSecret, value); }
        //#endif
        //        }

        public JsApiTicketResult JsApiTicketResult { get; set; }
        //        {
        //            get { return _jsApiTicketResult; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _jsApiTicketResult, value, "JsApiTicketResult"); }
        //#else
        //            set { this.SetContainerProperty(ref _jsApiTicketResult, value); }
        //#endif
        //        }

        public DateTimeOffset JsApiTicketExpireTime { get; set; }
        //        {
        //            get { return _jsApiTicketExpireTime; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _jsApiTicketExpireTime, value, "JsApiTicketExpireTime"); }
        //#else
        //            set { this.SetContainerProperty(ref _jsApiTicketExpireTime, value); }
        //#endif
        //        }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();

        //private DateTimeOffset _jsApiTicketExpireTime;
        //private JsApiTicketResult _jsApiTicketResult;
        //private string _appSecret;
        //private string _appId;
    }

    /// <summary>
    /// 通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    /// </summary>
    public class JsApiTicketContainer : BaseContainer<JsApiTicketBag>
    {
        const string LockResourceName = "MP.JsApiTicketContainer";

        #region 同步方法

        #region JsApiTicket

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="name">标记JsApiTicket名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        [Obsolete("请使用 RegisterAsync() 方法")]
        public static void Register(string appId, string appSecret, string name = null)
        {
            RegisterAsync(appId, appSecret, name).Wait();
        }


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
                throw new UnRegisterAppIdException(null, "此appId尚未注册，请先使用JsApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            var jsApiTicketBag = TryGetItem(appId);
            using (Cache.BeginCacheLock(LockResourceName, appId))//同步锁
            {
                if (getNewTicket || jsApiTicketBag.JsApiTicketExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    jsApiTicketBag.JsApiTicketResult = CommonApi.GetTicket(jsApiTicketBag.AppId, jsApiTicketBag.AppSecret);
                    jsApiTicketBag.JsApiTicketExpireTime = ApiUtility.GetExpireTime(jsApiTicketBag.JsApiTicketResult.expires_in);
                    Update(jsApiTicketBag, null);
                }
            }
            return jsApiTicketBag.JsApiTicketResult;
        }

        #endregion

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="name">标记JsApiTicket名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        public static async Task RegisterAsync(string appId, string appSecret, string name = null)
        {
            //记录注册信息，RegisterFunc委托内的过程会在缓存丢失之后自动重试
            RegisterFuncCollection[appId] = async () =>
            {
                //using (FlushCache.CreateInstance())
                //{
                var bag = new JsApiTicketBag()
                {
                    Name = name,
                    AppId = appId,
                    AppSecret = appSecret,
                    JsApiTicketExpireTime = DateTimeOffset.MinValue,
                    JsApiTicketResult = new JsApiTicketResult()
                };
                await UpdateAsync(appId, bag, null).ConfigureAwait(false);
                return bag;
                //}
            };

            await RegisterFuncCollection[appId]().ConfigureAwait(false);

            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppId = appId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppSecret = appSecret;
            }
        }

        #region JsApiTicket

        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static async Task<string> TryGetJsApiTicketAsync(string appId, string appSecret, bool getNewTicket = false)
        {
            if (!await CheckRegisteredAsync(appId).ConfigureAwait(false) || getNewTicket)
            {
                await RegisterAsync(appId, appSecret).ConfigureAwait(false);
            }
            return await GetJsApiTicketAsync(appId, getNewTicket).ConfigureAwait(false);
        }

        /// <summary>
        ///【异步方法】 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<string> GetJsApiTicketAsync(string appId, bool getNewTicket = false)
        {
            var result = await GetJsApiTicketResultAsync(appId, getNewTicket).ConfigureAwait(false);
            return result.ticket;
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetJsApiTicketResultAsync(string appId, bool getNewTicket = false)
        {
            if (!await CheckRegisteredAsync(appId).ConfigureAwait(false))
            {
                throw new UnRegisterAppIdException(null, "此appId尚未注册，请先使用JsApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            var jsApiTicketBag = await TryGetItemAsync(appId).ConfigureAwait(false);
            using (await Cache.BeginCacheLockAsync(LockResourceName, appId).ConfigureAwait(false))//同步锁
            {
                if (getNewTicket || jsApiTicketBag.JsApiTicketExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var jsApiTicketResult = await CommonApi.GetTicketAsync(jsApiTicketBag.AppId, jsApiTicketBag.AppSecret).ConfigureAwait(false);
                    //jsApiTicketBag.JsApiTicketResult = CommonApi.GetTicket(jsApiTicketBag.AppId, jsApiTicketBag.AppSecret);
                    jsApiTicketBag.JsApiTicketResult = jsApiTicketResult;
                    jsApiTicketBag.JsApiTicketExpireTime = SystemTime.Now.AddSeconds(jsApiTicketBag.JsApiTicketResult.expires_in);
                    await UpdateAsync(jsApiTicketBag, null).ConfigureAwait(false);
                }
            }
            return jsApiTicketBag.JsApiTicketResult;
        }

        #endregion
        #endregion
    }
}
