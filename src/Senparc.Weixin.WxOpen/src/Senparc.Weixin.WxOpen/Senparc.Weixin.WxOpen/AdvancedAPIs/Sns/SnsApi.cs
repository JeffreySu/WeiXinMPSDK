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
    文件功能描述：小程序Sns下接口
    
    创建标识：Senparc - 20170105
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sns
{
    /* 
    tip：通过该接口，仅能生成已发布的小程序的二维码。
    tip：可以在开发者工具预览时生成开发版的带参二维码。
    tip：带参二维码只有 10000 个，请谨慎调用。
    */

    /// <summary>
    /// WxApp接口
    /// </summary>
    public static class SnsApi
    {
        #region 同步方法

        /// <summary>
        /// code 换取 session_key
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="jsCode"></param>
        /// <param name="grantType">保持默认：authorization_code</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "SnsApi.JsCode2Json", true)]
        public static JsCode2JsonResult JsCode2Json(string appId, string secret, string jsCode, string grantType = "authorization_code", int timeOut = Config.TIME_OUT)
        {
            string urlFormat =
                Config.ApiMpHost + "/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type={3}";

            var url = string.Format(urlFormat, appId, secret, jsCode, grantType);
            var result = CommonJsonSend.Send<JsCode2JsonResult>(null, url, null, CommonJsonSendType.GET);
            return result;
        }

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】code 换取 session_key
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="jsCode"></param>
        /// <param name="grantType">保持默认：authorization_code</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "SnsApi.JsCode2JsonAsync", true)]
        public static async Task<JsCode2JsonResult> JsCode2JsonAsync(string appId, string secret, string jsCode, string grantType = "authorization_code", int timeOut = Config.TIME_OUT)
        {
            string urlFormat =
                Config.ApiMpHost + "/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type={3}";

            var url = string.Format(urlFormat, appId, secret, jsCode, grantType);

            var result = await CommonJsonSend.SendAsync<JsCode2JsonResult>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}