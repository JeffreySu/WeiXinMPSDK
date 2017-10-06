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

    文件名：RequestUtility.Post.cs
    文件功能描述：获取请求结果（Post）


    创建标识：Senparc - 20171006

    修改描述：移植Post方法过来

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Helpers;
#if NET45
using System.Web;
#else
using System.Net.Http;
using System.Net.Http.Headers;
#endif
#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETCOREAPP2_0
using Microsoft.AspNetCore.Http;
using Senparc.Weixin.WebProxy;
#endif

namespace Senparc.Weixin.Utilities.HttpUtility
{
    /// <summary>
    /// HTTP 请求工具类
    /// </summary>
    public static partial class RequestUtility
    {


    }
}
