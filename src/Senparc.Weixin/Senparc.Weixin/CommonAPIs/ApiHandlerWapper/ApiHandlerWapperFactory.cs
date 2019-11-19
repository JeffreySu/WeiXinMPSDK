#region Apache License Version 2.0
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
    
    文件名：ApiHandlerWapperFactory.cs
    文件功能描述：针对AccessToken无效或过期的自动处理方法的工厂
    
    
    创建标识：Senparc - 20170327
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.Utilities.WeixinUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.CommonAPIs
{
    ///// <summary>
    ///// ApiHandlerWapperFactory的当前平台
    ///// </summary>
    //public enum ApiHandlerWapperPlatform
    //{
    //    /// <summary>
    //    /// 公众号
    //    /// </summary>
    //    MP,
    //    /// <summary>
    //    /// 开放平台
    //    /// </summary>
    //    OPEN
    //}

    ///// <summary>
    ///// 针对AccessToken无效或过期的自动处理类（基类）
    ///// </summary>
    //public static class ApiHandlerWapperFactory
    //{
    //    /// <summary>
    //    /// 同步方法集合
    //    /// </summary>
    //    public static Dictionary<ApiHandlerWapperPlatform, Func<WxJsonResult>> Collection = new Dictionary<ApiHandlerWapperPlatform, Func<WxJsonResult>>();

    //    /// <summary>
    //    /// 异步方法集合
    //    /// </summary>
    //    public static Dictionary<ApiHandlerWapperPlatform, Func<Task<WxJsonResult>>> CollectionAsync = new Dictionary<ApiHandlerWapperPlatform, Func<Task<WxJsonResult>>>();

    //    ///// <summary>
    //    ///// 平台凭证队列
    //    ///// </summary>
    //    //public static Dictionary<string, ApiHandlerWapperPlatform> PlatformQueue = new Dictionary<string, ApiHandlerWapperPlatform>();

    //    /// <summary>
    //    /// ApiHandlerWapperFactory锁
    //    /// </summary>
    //    public static object ApiHandlerWapperFactoryLock = new object();

    //    private static ApiHandlerWapperPlatform _currentPlatform = ApiHandlerWapperPlatform.MP;

    //    /// <summary>
    //    /// 当前平台，值：
    //    /// </summary>
    //    public static ApiHandlerWapperPlatform CurrentPlatform
    //    {
    //        get
    //        {
    //            return _currentPlatform;
    //        }
    //        set
    //        {
    //            _currentPlatform = value;
    //        }
    //    }


    //    #region 同步方法

    //    public static Func<T> GetWapperFunc<T>(ApiHandlerWapperPlatform platform) where T : WxJsonResult
    //    {
    //        if (!Collection.ContainsKey(platform))
    //        {
    //            return null;//也可以抛出异常
    //        }

    //        var funWapper = Collection[platform];
    //        return funWapper as Func<T>;
    //    }

    //    /// <summary>
    //    /// 提供给ApiHandlerWapper使用的全局统一的基础调用方法，兼容公众号、开放平台在不同情况下对接口的统一调用
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="funWapper"></param>
    //    /// <returns></returns>
    //    public static WxJsonResult RunWapper<T>(ApiHandlerWapperPlatform platform) where T : WxJsonResult
    //    {
    //        if (!Collection.ContainsKey(platform))
    //        {
    //            return null;//也可以抛出异常
    //        }

    //        var funWapper = Collection[platform];
    //        return funWapper();
    //    }

    //    #endregion

    //    #region 异步方法

    //    /// <summary>
    //    /// 【异步方法】提供给ApiHandlerWapper使用的全局统一的基础调用方法，兼容公众号、开放平台在不同情况下对接口的统一调用
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="funWapper"></param>
    //    /// <returns></returns>
    //    public static async Task<T> RunWapperAsync<T>(Func<Task<T>> funWapper) where T : WxJsonResult
    //    {
    //        if (funWapper == null)
    //        {
    //            return null;
    //        }
    //        return await funWapper().ConfigureAwait(false);
    //    }

    //    #endregion
    //}
}
