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

	文件名：EnableRequestRewindMiddleware.cs
	文件功能描述：EnableRequestRewind中间件，开启.net core 2 中的 
                  RequestRewind模式，Request.Body可以二次读取，以
                  解决某些特殊情况下netcore默认机制导致的Request.Body
                  为空而引发的WeixinSDK错误的问题。
                  https://github.com/JeffreySu/WeiXinMPSDK/issues/1090


	创建标识：lishewen - 20180516

----------------------------------------------------------------*/

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// <para>EnableRequestRewind中间件，开启.net core 2 中的RequestRewind模式，Request.Body可以二次读取，</para>
    /// <para>以解决某些特殊情况下netcore默认机制导致的Request.Body为空而引发的WeixinSDK错误的问题。</para>
    /// <para>https://github.com/JeffreySu/WeiXinMPSDK/issues/1090</para>
    /// </summary>
    public class EnableRequestRewindMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// EnableRequestRewindMiddleware
        /// </summary>
        /// <param name="next"></param>
        public EnableRequestRewindMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableRewind();
            await _next(context).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// EnableRequestRewindExtension
    /// </summary>
    public static class EnableRequestRewindExtension
    {
        /// <summary>
        /// UseEnableRequestRewind
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseEnableRequestRewind(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EnableRequestRewindMiddleware>();
        }
    }
}
#endif