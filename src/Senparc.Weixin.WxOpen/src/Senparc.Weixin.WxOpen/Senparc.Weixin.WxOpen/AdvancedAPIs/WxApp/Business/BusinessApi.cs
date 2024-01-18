using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Business.JsonResult;
#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
  
    文件名：BusinessApi.cs
    文件功能描述：wxa/business 接口
    
    
    创建标识：Senparc - 20220112

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp
{
    /// <summary>
    /// wxa/business 接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class BusinessApi
    {
        #region 同步方法

        /// <summary>
        /// code换取用户手机号。 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">每个code只能使用一次，code的有效期为5min</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetUserPhoneNumberJsonResult GetUserPhoneNumber(string accessTokenOrAppId, string code, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/business/getuserphonenumber?access_token={0}";
                string url = string.Format(urlFormat, accessToken);
                var data = new { code = code };
                return CommonJsonSend.Send<GetUserPhoneNumberJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }


        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】code换取用户手机号。 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">每个code只能使用一次，code的有效期为5min</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetUserPhoneNumberJsonResult> GetUserPhoneNumberAsync(string accessTokenOrAppId, string code, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/business/getuserphonenumber?access_token={0}";
                string url = string.Format(urlFormat, accessToken);
                var data = new { code = code };
                return await CommonJsonSend.SendAsync<GetUserPhoneNumberJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }


        #endregion
    }
}
