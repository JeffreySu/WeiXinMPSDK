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

    文件名：UrlUtility.cs
    文件功能描述：URL工具类


    创建标识：Senparc - 20170912

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// URL工具类
    /// </summary>
    public class UrlUtility
    {
        /// <summary>
        /// 生成OAuth用的额CallbackUrl参数（原始状态，未整体进行UrlEncode）
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="oauthCallbackUrl"></param>
        /// <returns></returns>
        public static string GenerateOAuthCallbackUrl(HttpContextBase httpContext, string oauthCallbackUrl)
        {
            if (httpContext.Request.Url==null)
            {
                throw new WeixinNullReferenceException("httpContext.Request.Url 不能为null！", httpContext.Request);
            }

            var returnUrl = httpContext.Request.Url.ToString();
            var urlData = httpContext.Request.Url;
            string portSetting = null;
            string schemeUpper = urlData.Scheme.ToUpper();
            if ((schemeUpper == "HTTP" && urlData.Port == 80) ||
                (schemeUpper == "HTTPS" && urlData.Port == 443))
            {
                portSetting = "";//使用默认值
            }
            else
            {
                portSetting = ":" + urlData.Port;//添加端口
            }

            //授权回调字符串
            var callbackUrl = string.Format("{0}://{1}{2}{3}{4}returnUrl={5}",
                urlData.Scheme,
                urlData.Host,
                portSetting,
                oauthCallbackUrl,
                oauthCallbackUrl.Contains("?") ? "&" : "?",
                returnUrl.UrlEncode()
            );
            return callbackUrl;
        }
    }
}
