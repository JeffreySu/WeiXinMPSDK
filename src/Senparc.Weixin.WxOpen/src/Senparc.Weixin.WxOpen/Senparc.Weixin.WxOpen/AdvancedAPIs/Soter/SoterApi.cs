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

    文件名：SoterApi.cs
    文件功能描述：SoterApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Soter
{
    /// <summary>微信 SOTER 生物认证服务端接口。</summary>
    public static class SoterApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        /// <summary>验证 SOTER 生物认证密钥签名。</summary>
        /// <param name="accessToken">发起生物认证的小程序 AccessToken。</param>
        /// <param name="request">用户 OpenId、认证结果 JSON 和结果签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>签名是否验证通过。</returns>
        /// <remarks>本接口不支持第三方平台代调用。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/soter/api_verifysignature"/>。</remarks>
        public static SoterVerifySignatureJsonResult VerifySignature(string accessToken, SoterVerifySignatureRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<SoterVerifySignatureJsonResult>(accessToken, "/cgi-bin/soter/verify_signature", request, timeOut);
        }

        /// <summary>异步验证 SOTER 生物认证密钥签名。</summary>
        /// <inheritdoc cref="VerifySignature"/>
        public static Task<SoterVerifySignatureJsonResult> VerifySignatureAsync(string accessToken, SoterVerifySignatureRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<SoterVerifySignatureJsonResult>(accessToken, "/cgi-bin/soter/verify_signature", request, timeOut);
        }

        private static T SendPost<T>(string accessToken, string path, object request, int timeOut)
            where T : Weixin.Entities.WxJsonResult, new()
        {
            var url = Config.ApiMpHost + path + "?access_token=" + accessToken.AsUrlData();
            return CommonJsonSend.Send<T>(null, url, request, CommonJsonSendType.POST,
                timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        private static Task<T> SendPostAsync<T>(string accessToken, string path, object request, int timeOut)
            where T : Weixin.Entities.WxJsonResult, new()
        {
            var url = Config.ApiMpHost + path + "?access_token=" + accessToken.AsUrlData();
            return CommonJsonSend.SendAsync<T>(null, url, request, CommonJsonSendType.POST,
                timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }
    }
}
