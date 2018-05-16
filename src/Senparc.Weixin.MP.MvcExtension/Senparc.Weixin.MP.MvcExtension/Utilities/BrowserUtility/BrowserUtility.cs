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

	文件名：BrowserUtility.cs
	文件功能描述：浏览器公共类


	创建标识：Senparc - 20150419

    修改标识：Senparc - 20161219
    修改描述：v4.9.6 修改错别字：Browser->Browser

    修改标识：Senparc - 20161219
    修改描述：v4.11.2 修改SideInWeixinBrowser判断逻辑

----------------------------------------------------------------*/

using System;
#if NET35 || NET40 || NET45 || NET461
using System.Web;
#else
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#endif

namespace Senparc.Weixin.MP.MvcExtension.BrowserUtility
{
    /// <summary>
    /// BrowserUtility
    /// </summary>
    public static class BrowserUtility
    {
        /// <summary>
        /// 判断是否在微信内置浏览器中
        /// </summary>
        /// <param name="httpContext">HttpContextBase对象</param>
        /// <returns>true：在微信内置浏览器内。false：不在微信内置浏览器内。</returns>
#if NET35 || NET40 || NET45 || NET461
       [Obsolete("当前方法已经过时，请使用 Senparc.Weixin.BrowserUtility.BrowserUtility.SideInWeixinBrowser()")]
        public static bool SideInWeixinBrowser(this HttpContextBase httpContext)
        {
            var userAgent = httpContext.Request.UserAgent;
            if (userAgent != null
                && (userAgent.Contains("MicroMessenger") || userAgent.Contains("Windows Phone")))
            {
                return true;//在微信内部
            }
            else
            {
                return false;//在微信外部
            }
        }
#else
        [Obsolete("当前方法已经过时，请使用 Senparc.Weixin.BrowserUtility.BrowserUtility.SideInWeixinBrowser()")]
        public static bool SideInWeixinBrowser(this HttpContext httpContext)
        {
            string ustr = string.Empty;
            var userAgent = httpContext.Request.Headers["User-Agent"];
            if (userAgent.Count > 0)
            {
                ustr = userAgent[0];
            }

            if (!string.IsNullOrEmpty(ustr)
                && (ustr.Contains("MicroMessenger") || ustr.Contains("Windows Phone")))
            {
                return true;//在微信内部
            }
            else
            {
                return false;//在微信外部
            }
        }
#endif
    }
}
