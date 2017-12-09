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

    文件名：SenparcHttpClient.cs
    文件功能描述：全局 HttpClient 单例（为.NET Core准备）


    创建标识：Senparc - 20171203

----------------------------------------------------------------*/

#if NET45 || NET461 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETCOREAPP2_0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.WebProxy;
using System.Net.Http;
using System.Threading.Tasks;

#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETCOREAPP2_0
using Microsoft.AspNetCore.Http;
#endif

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// SenparcHttpClient，全局 HttpClient 单例
    /// </summary>
    public class SenparcHttpClient
    {

        /// <summary>
        /// 默认HttpClient配置
        /// </summary>
        public static Func<HttpClient> DefaultHttpClientInstance = () => new HttpClient();

        #region 全局 HttpClient 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        SenparcHttpClient()
        {
        }

        //静态LocalCacheStrategy
        public static System.Net.Http.HttpClient Instance
        {
            get
            {
                return Nested.instance;//返回Nested类中的静态成员instance
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            //将instance设为一个初始化的LocalCacheStrategy新实例
            internal static readonly System.Net.Http.HttpClient instance = DefaultHttpClientInstance();
        }

        #endregion
    }
}
#endif
