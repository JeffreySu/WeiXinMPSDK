#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2020 Senparc

	�ļ�����EnableRequestRewindMiddleware.cs
	�ļ�����������EnableRequestRewind�м��������.net core 2 �е� 
                  RequestRewindģʽ��Request.Body���Զ��ζ�ȡ����
                  ���ĳЩ���������netcoreĬ�ϻ��Ƶ��µ�Request.Body
                  Ϊ�ն�������WeixinSDK��������⡣
                  https://github.com/JeffreySu/WeiXinMPSDK/issues/1090


	������ʶ��lishewen - 20180516

    �޸ı�ʶ��Senparc - 20190929
    �޸�������v7.4.101 ֧�� .NET Core 3.0���޸� Request.EnableRewind() ����Ϊ Request.EnableBuffering()

----------------------------------------------------------------*/

#if !NET45
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
#if !NETCOREAPP3_1
using Microsoft.AspNetCore.Http.Internal;
#endif

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
#if NETSTANDARD2_0
            context.Request.EnableRewind();
#else
            context.Request.EnableBuffering();//.NET Core 3.0 ����ʹ�� EnableRewind()����Ϊ EnableBuffering()��https://github.com/aspnet/AspNetCore/issues/12505
#endif
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