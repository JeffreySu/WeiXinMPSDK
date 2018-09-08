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

    文件名：UrlUtility.cs
    文件功能描述：URL工具类


    创建标识：Senparc - 20170912

    修改标识：Senparc - 20170917
    修改描述：修改GenerateOAuthCallbackUrl()，更方便移植到.NET Core

    修改标识：Senparc - 20180502
    修改描述：v4.20.3 为 .NET Core 优化 UrlUtility.GenerateOAuthCallbackUrl() 方法中的端口获取过程

    修改标识：Senparc - 20180502
    修改描述：v5.1.3 优化 UrlUtility.GenerateOAuthCallbackUrl() 方法

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.CO2NET.Extensions;
#if NET35 || NET40 || NET45
using System.Web;
#else
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
#endif
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// URL工具类
    /// </summary>
    public class UrlUtility
    {
        /// <summary>
        /// 生成OAuth用的CallbackUrl参数（原始状态，未整体进行UrlEncode）
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="oauthCallbackUrl"></param>
        /// <returns></returns>
#if NET40 || NET45
        public static string GenerateOAuthCallbackUrl(HttpContextBase httpContext, string oauthCallbackUrl)
#else
        public static string GenerateOAuthCallbackUrl(HttpContext httpContext, string oauthCallbackUrl)
#endif
        {

#if NET35 || NET40 || NET45

            if (httpContext.Request.Url == null)
            {
                throw new WeixinNullReferenceException("httpContext.Request.Url 不能为null！", httpContext.Request);
            }

            var returnUrl = httpContext.Request.Url.ToString();
            var urlData = httpContext.Request.Url;
            var scheme = urlData.Scheme;//协议
            var host = urlData.Host;//主机名（不带端口）
            var port = urlData.Port;//端口
            string portSetting = null;//Url中的端口部分
            string schemeUpper = scheme.ToUpper();//协议（大写）
#else
            if (httpContext.Request == null)
            {
                throw new WeixinNullReferenceException("httpContext.Request 不能为null！", httpContext);
            }

            var request = httpContext.Request;
            //var location = new Uri($"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}");
            //var returnUrl = location.AbsoluteUri; //httpContext.Request.Url.ToString();    
            var returnUrl = request.AbsoluteUri();
            var urlData = httpContext.Request;
            var scheme = urlData.Scheme;//协议
            var host = urlData.Host.Host;//主机名（不带端口）
            var port = urlData.Host.Port ?? -1;//端口（因为从.NET Framework移植，因此不直接使用urlData.Host）
            string portSetting = null;//Url中的端口部分
            string schemeUpper = scheme.ToUpper();//协议（大写）
#endif
            if ( port == -1 || //这个条件只有在 .net core 中， Host.Port == null 的情况下才会发生
                (schemeUpper == "HTTP" && port == 80) ||
                (schemeUpper == "HTTPS" && port == 443))
            {
                portSetting = "";//使用默认值
            }
            else
            {
                portSetting = ":" + port;//添加端口
            }

            //授权回调字符串
            var callbackUrl = string.Format("{0}://{1}{2}{3}{4}returnUrl={5}",
                scheme,
                host,
                portSetting,
                oauthCallbackUrl,
                oauthCallbackUrl.Contains("?") ? "&" : "?",
                returnUrl.UrlEncode()
            );
            return callbackUrl;
        }
    }
}
