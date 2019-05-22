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

	�ļ�����EnableRequestRewindMiddleware.cs
	�ļ�����������EnableRequestRewind�м��������.net core 2 �е� 
                  RequestRewindģʽ��Request.Body���Զ��ζ�ȡ����
                  ���ĳЩ���������netcoreĬ�ϻ��Ƶ��µ�Request.Body
                  Ϊ�ն�������WeixinSDK��������⡣
                  https://github.com/JeffreySu/WeiXinMPSDK/issues/1090


	������ʶ��lishewen - 20180516

----------------------------------------------------------------*/

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// <para>EnableRequestRewind�м��������.net core 2 �е�RequestRewindģʽ��Request.Body���Զ��ζ�ȡ��</para>
    /// <para>�Խ��ĳЩ���������netcoreĬ�ϻ��Ƶ��µ�Request.BodyΪ�ն�������WeixinSDK��������⡣</para>
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