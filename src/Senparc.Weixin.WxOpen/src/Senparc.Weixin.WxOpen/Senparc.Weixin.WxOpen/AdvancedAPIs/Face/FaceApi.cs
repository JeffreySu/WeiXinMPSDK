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

    文件名：FaceApi.cs
    文件功能描述：FaceApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Face
{
    /// <summary>微信人脸核身服务端接口。</summary>
    public static class FaceApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        /// <summary>获取用户人脸核身会话唯一标识。</summary>
        /// <param name="accessToken">发起人脸核身的小程序 AccessToken。</param>
        /// <param name="request">业务流水号、证件信息和用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>核身会话 VerifyId 及有效期。</returns>
        /// <remarks>请求 OpenId 所属 AppId 必须与前端调用 <c>wx.requestFacialVerify</c> 的小程序 AppId 一致。本接口不支持第三方平台代调用。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/face/api_getverifyid"/>。</remarks>
        public static FaceGetVerifyIdJsonResult GetVerifyId(string accessToken, FaceGetVerifyIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<FaceGetVerifyIdJsonResult>(accessToken, "/cityservice/face/identify/getverifyid", request, timeOut);
        }

        /// <summary>异步获取用户人脸核身会话唯一标识。</summary>
        /// <inheritdoc cref="GetVerifyId"/>
        public static Task<FaceGetVerifyIdJsonResult> GetVerifyIdAsync(string accessToken, FaceGetVerifyIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<FaceGetVerifyIdJsonResult>(accessToken, "/cityservice/face/identify/getverifyid", request, timeOut);
        }

        /// <summary>查询用户人脸核身的真实验证结果。</summary>
        /// <param name="accessToken">发起人脸核身的小程序 AccessToken。</param>
        /// <param name="request">VerifyId、原业务流水号、证件摘要和原用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>人脸核身验证结果码。</returns>
        /// <remarks>核身通过必须同时满足 <c>errcode == 0</c> 和 <c>verify_ret == 10000</c>。本接口不支持第三方平台代调用。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/face/api_queryverifyinfo"/>。</remarks>
        public static FaceQueryVerifyInfoJsonResult QueryVerifyInfo(string accessToken, FaceQueryVerifyInfoRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<FaceQueryVerifyInfoJsonResult>(accessToken, "/cityservice/face/identify/queryverifyinfo", request, timeOut);
        }

        /// <summary>异步查询用户人脸核身的真实验证结果。</summary>
        /// <inheritdoc cref="QueryVerifyInfo"/>
        public static Task<FaceQueryVerifyInfoJsonResult> QueryVerifyInfoAsync(string accessToken, FaceQueryVerifyInfoRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<FaceQueryVerifyInfoJsonResult>(accessToken, "/cityservice/face/identify/queryverifyinfo", request, timeOut);
        }

        /// <summary>按照微信官方规则生成查询人脸核身结果所需的证件信息摘要。</summary>
        /// <param name="certificate">获取 VerifyId 时使用的证件类型、姓名和号码。</param>
        /// <returns>小写十六进制 SHA-256 摘要。</returns>
        /// <remarks>各字段先按 UTF-8 转 Base64，再按 <c>cert_type=...&amp;cert_name=...&amp;cert_no=...</c> 拼接后计算 SHA-256。</remarks>
        public static string CreateCertificateHash(FaceCertificateInfo certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            var source = "cert_type=" + ToBase64(certificate.cert_type)
                + "&cert_name=" + ToBase64(certificate.cert_name)
                + "&cert_no=" + ToBase64(certificate.cert_no);
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(source));
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }
        }

        private static string ToBase64(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value ?? string.Empty));
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
