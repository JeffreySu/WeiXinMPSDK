#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
    
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
using System.Threading.Tasks;

namespace Senparc.Weixin.CommonAPIs
{
    /// <summary>
    /// 针对AccessToken无效或过期的自动处理类（基类）
    /// </summary>
    public static class ApiHandlerWapperFactory
    {
        #region 同步方法

        /// <summary>
        /// 提供给ApiHandlerWapper使用的全局统一的基础调用方法，兼容公众号、开放平台在不同情况下对接口的统一调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funWapper"></param>
        /// <returns></returns>
        public static T RunApi<T>(Func<T> funWapper) where T : WxJsonResult
        {
            if (funWapper == null)
            {
                return null;
            }
            return funWapper();
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】提供给ApiHandlerWapper使用的全局统一的基础调用方法，兼容公众号、开放平台在不同情况下对接口的统一调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funWapper"></param>
        /// <returns></returns>
        public static async Task<T> RunApiAsync<T>(Func<Task<T>> funWapper) where T : WxJsonResult
        {
            if (funWapper == null)
            {
                return null;
            }
            return await funWapper();
        }

        #endregion
    }
}
