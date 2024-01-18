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

    文件名：SecApi.cs
    文件功能描述：小程序微信认证


    创建标识：Yaofeng - 20231130
        
----------------------------------------------------------------*/

//文档：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_wxaauth.html

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Open.WxaAPIs.Sec;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 小程序微信认证
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class SecApi
    {
        #region 同步方法
        /// <summary>
        /// 小程序认证
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_wxaauth.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="auth_data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxaAuthJsonResult WxaAuth(string accessToken, AuthData auth_data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/wxaauth?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                auth_data
            };
            return CommonJsonSend.Send<WxaAuthJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 小程序认证进度查询
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_queryauth.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="taskid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryAuthJsonResult QueryAuth(string accessToken, string taskid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/queryauth?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                taskid
            };
            return CommonJsonSend.Send<QueryAuthJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 小程序认证上传补充材料
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_uploadauthmaterial.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="file">图片（image）: 不超过2M，支持PNG\JPEG\JPG\GIF格式</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UploadAuthMaterialJsonResult UploadAuthMaterial(string accessToken, string file, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/uploadauthmaterial?access_token={0}", accessToken.AsUrlData());
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            return Post.PostFileGetJson<UploadAuthMaterialJsonResult>(CommonDI.CommonSP, url, null, fileDictionary, null, timeOut: timeOut);
        }

        /// <summary>
        /// 小程序认证重新提审
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_reauth.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="auth_data">认证数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ReauthJsonResult Reauth(string accessToken, AuthData auth_data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/reauth?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                auth_data
            };
            return CommonJsonSend.Send<ReauthJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询个人认证身份选项列表
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_authidentitytree.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AuthIdentityTreeJsonResult AuthIdentityTree(string accessToken,  int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/authidentitytree?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<AuthIdentityTreeJsonResult>(null, url, null, CommonJsonSendType.POST, timeOut);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 小程序认证
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_wxaauth.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="auth_data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxaAuthJsonResult> WxaAuthAsync(string accessToken, AuthData auth_data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/wxaauth?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                auth_data
            };
            return await CommonJsonSend.SendAsync<WxaAuthJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 小程序认证进度查询
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_queryauth.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="taskid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryAuthJsonResult> QueryAuthAsync(string accessToken, string taskid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/queryauth?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                taskid
            };
            return await CommonJsonSend.SendAsync<QueryAuthJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 小程序认证上传补充材料
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_uploadauthmaterial.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="file">图片（image）: 不超过2M，支持PNG\JPEG\JPG\GIF格式</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UploadAuthMaterialJsonResult> UploadAuthMaterialAsync(string accessToken, string file, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/uploadauthmaterial?access_token={0}", accessToken.AsUrlData());
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            return await Post.PostFileGetJsonAsync<UploadAuthMaterialJsonResult>(CommonDI.CommonSP, url, null, fileDictionary, null, timeOut: timeOut);
        }

        /// <summary>
        /// 小程序认证重新提审
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_reauth.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="auth_data">认证数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ReauthJsonResult> ReauthAsync(string accessToken, AuthData auth_data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/reauth?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                auth_data
            };
            return await CommonJsonSend.SendAsync<ReauthJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询个人认证身份选项列表
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/weapp-wxverify/secwxaapi_authidentitytree.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AuthIdentityTreeJsonResult> AuthIdentityTreeAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/authidentitytree?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<AuthIdentityTreeJsonResult>(null, url, null, CommonJsonSendType.POST, timeOut);
        }
        #endregion
    }
}
