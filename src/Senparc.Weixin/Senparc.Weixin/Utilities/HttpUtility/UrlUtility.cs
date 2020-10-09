﻿#region Apache License Version 2.0
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

    文件名：UrlUtility.cs
    文件功能描述：URL工具类


    创建标识：Senparc - 20170912

    修改标识：Senparc - 20170917
    修改描述：修改GenerateOAuthCallbackUrl()，更方便移植到.NET Core

    修改标识：Senparc - 20180502
    修改描述：v4.20.3 为 .NET Core 优化 UrlUtility.GenerateOAuthCallbackUrl() 方法中的端口获取过程

    修改标识：Senparc - 20180502
    修改描述：v5.1.3 优化 UrlUtility.GenerateOAuthCallbackUrl() 方法

    修改标识：Senparc - 20180909
    修改描述：v6.0.4 UrlUtility.GenerateOAuthCallbackUrl() 方法，更好支持反向代理

    修改标识：Senparc - 20180917
    修改描述：v6.1.1 还原上一个版本 v6.0.4 的修改
    
    修改标识：Senparc - 20190123
    修改描述：v6.3.6 支持在子程序环境下获取 OAuth 回调地址
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;

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
        public static string GenerateOAuthCallbackUrl(string scheme, string host, int port, string baseUrl, string returnUrl, string oauthCallbackUrl)
        {
            var schemeUpper = scheme.ToUpper();
            string portSetting = null;//Url中的端口部分
            if (port == -1 || //这个条件只有在 .net core 中， Host.Port == null 的情况下才会发生
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
            var callbackUrl = string.Format("{0}://{1}{2}{6}{3}{4}returnUrl={5}",
                scheme,
                host,
                portSetting,
                oauthCallbackUrl,
                oauthCallbackUrl.Contains("?") ? "&" : "?",
                returnUrl.UrlEncode(),
                //添加应用目录：https://github.com/JeffreySu/WeiXinMPSDK/issues/1552
                baseUrl
            );
            return callbackUrl;
        }
    }
}
