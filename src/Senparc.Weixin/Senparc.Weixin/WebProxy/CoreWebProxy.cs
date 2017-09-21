﻿#region Apache License Version 2.0
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
  
    文件名：CoreWebProxy.cs
    文件功能描述：.NET Core 使用的 WebProxy 类
    
    
    创建标识：Senparc - 20170917
    
----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Senparc.Weixin.WebProxy
{
    /// <summary>
    /// .NET Core 使用的 WebProxy 类
    /// 参考：http://www.abelliu.com/dotnet/dotnet%20core/2017/03/14/dotnetcore-proxy/
    /// </summary>
    public class CoreWebProxy : IWebProxy
    {
        public readonly Uri Uri;
        private readonly bool bypass;

        public CoreWebProxy(Uri uri, ICredentials credentials = null, bool bypass = false)
        {
            Uri = uri;
            this.bypass = bypass;
            Credentials = credentials;
        }

        public ICredentials Credentials { get; set; }

        public Uri GetProxy(Uri destination) => Uri;

        public bool IsBypassed(Uri host) => bypass;

        public override int GetHashCode()
        {
            if (Uri == null)
            {
                return -1;
            }

            return Uri.GetHashCode();
        }
    }
}
