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
    
    文件名：SnsApi.cs
    文件功能描述：小程序微信登录
    
    创建标识：Senparc - 20170827

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.Open.WxaAPIs.Sns
{
    /// <summary>
    /// 微信SNS接口
    /// </summary>
    public static class SnsApi
    {
        #region 同步方法

        /// <summary>
        /// code 换取 session_key
        /// </summary>
        /// <param name="appId">小程序的AppID</param>
        /// <param name="componentAppId">第三方平台appid</param>
        /// <param name="componentAccessToken">	第三方平台的component_access_token</param>
        /// <param name="jsCode">登录时获取的 code</param>
        /// <param name="grantType">保持默认：authorization_code</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "SnsApi.JsCode2Json", true)]
        public static JsCode2JsonResult JsCode2Json(string appId, string componentAppId, string componentAccessToken, string jsCode, string grantType = "authorization_code", int timeOut = Config.TIME_OUT)
        {
            string urlFormat =
                Config.ApiMpHost + "/sns/component/jscode2session?appid={0}&js_code={1}&grant_type={2}&component_appid={3}&component_access_token={4}";

            var url = string.Format(urlFormat, appId, jsCode, grantType, componentAppId, componentAccessToken);
            var result = CommonJsonSend.Send<JsCode2JsonResult>(null, url, null, CommonJsonSendType.GET);
            return result;
        }

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】code 换取 session_key
        /// </summary>
        /// <param name="appId">小程序的AppID</param>
        /// <param name="componentAppId">第三方平台appid</param>
        /// <param name="componentAccessToken">	第三方平台的component_access_token</param>
        /// <param name="jsCode">登录时获取的 code</param>
        /// <param name="grantType">保持默认：authorization_code</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "SnsApi.JsCode2JsonAsync", true)]
        public static async Task<JsCode2JsonResult> JsCode2JsonAsync(string appId, string componentAppId, string componentAccessToken, string jsCode, string grantType = "authorization_code", int timeOut = Config.TIME_OUT)
        {
            string urlFormat =
                Config.ApiMpHost + "/sns/component/jscode2session?appid={0}&js_code={1}&grant_type={2}&component_appid={3}&component_access_token={4}";

            var url = string.Format(urlFormat, appId, jsCode, grantType, componentAppId, componentAccessToken);
            var result = await CommonJsonSend.SendAsync<JsCode2JsonResult>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}