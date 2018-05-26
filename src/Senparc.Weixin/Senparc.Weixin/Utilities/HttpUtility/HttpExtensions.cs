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
    Copyright (C) 2018 Senparc COCONET

    文件名：HttpExtensions.cs
    文件功能描述：ASP.NET Core 中的 Http 一系列扩展


    创建标识：Senparc - 20180526

----------------------------------------------------------------*/

#if NETCOREAPP2_0 || NETCOREAPP2_1

using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// RequestExtension
    /// </summary>
    public static class RequestExtension
    {
        private const string NullIpAddress = "::1";

        public static bool IsLocal(this HttpRequest req)
        {
            var connection = req.HttpContext.Connection;
            if (connection.RemoteIpAddress.IsSet())
            {
                //We have a remote address set up
                return connection.LocalIpAddress.IsSet()
                    //Is local is same as remote, then we are local
                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                    //else we are remote if the remote IP address is not a loopback address
                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            return true;
        }

        private static bool IsSet(this IPAddress address)
        {
            return address != null && address.ToString() != NullIpAddress;
        }


        /// <summary>
        /// Determines whether the specified HTTP request is an AJAX request.
        /// </summary>
        /// 
        /// <returns>
        /// true if the specified HTTP request is an AJAX request; otherwise, false.
        /// </returns>
        /// <param name="request">The HTTP request.</param><exception cref="T:System.ArgumentNullException">The <paramref name="request"/> parameter is null (Nothing in Visual Basic).</exception>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }

        /// <summary>
        /// 通常是以/开头的完整路径
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string PathAndQuery(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            return request.Path + request.QueryString;
        }

        /// <summary>
        /// 获取来源页面
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string UrlReferrer(this HttpRequest request)
        {
            return request.Headers["Referer"].ToString();
        }

        /// <summary>
        /// 返回绝对地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string AbsoluteUri(this HttpRequest request)
        {
            var absoluteUri = string.Concat(
                          request.Scheme,
                          "://",
                          request.Host.ToUriComponent(),
                          request.PathBase.ToUriComponent(),
                          request.Path.ToUriComponent(),
                          request.QueryString.ToUriComponent());

            return absoluteUri;
        }

        /// <summary>
        /// 获取客户端信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string UserAgent(this HttpRequest request)
        {
            return request.Headers["User-Agent"].ToString();
        }

        /// <summary>
        /// 获取客户端地址（IP）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IPAddress UserHostAddress(this HttpContext httpContext)
        {
            return httpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;
        }
    }
}
#endif