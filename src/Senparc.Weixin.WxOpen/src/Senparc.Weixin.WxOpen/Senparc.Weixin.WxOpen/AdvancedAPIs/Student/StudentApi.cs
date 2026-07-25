#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：StudentApi.cs
    文件功能描述：StudentApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Student
{
    /// <summary>微信学生身份快速验证服务端接口。</summary>
    public static class StudentApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        /// <summary>使用插件授权 Code 快速获取用户学生身份。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">用户 OpenId 和快速验证插件授权 Code。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>学生身份绑定状态和判断结果。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 144。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/student/api_quickcheckstudentidentity.html"/>。</remarks>
        public static QuickCheckStudentIdentityJsonResult QuickCheckStudentIdentity(string accessTokenOrAppId, QuickCheckStudentIdentityRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<QuickCheckStudentIdentityJsonResult>(accessTokenOrAppId, "/intp/quickcheckstudentidentity", request, timeOut);
        }

        /// <summary>异步使用插件授权 Code 快速获取用户学生身份。</summary>
        /// <inheritdoc cref="QuickCheckStudentIdentity"/>
        public static Task<QuickCheckStudentIdentityJsonResult> QuickCheckStudentIdentityAsync(string accessTokenOrAppId, QuickCheckStudentIdentityRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<QuickCheckStudentIdentityJsonResult>(accessTokenOrAppId, "/intp/quickcheckstudentidentity", request, timeOut);
        }

        private static T SendPost<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : Weixin.Entities.WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}", request,
                    CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting), accessTokenOrAppId);
        }

        private static Task<T> SendPostAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : Weixin.Entities.WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}", request,
                    CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false), accessTokenOrAppId);
        }
    }
}
