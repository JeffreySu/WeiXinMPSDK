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

    文件名：VisualProcessingApi.cs
    文件功能描述：VisualProcessingApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.CO2NET;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.CV.Image;
using Senparc.Weixin.MP.AdvancedAPIs.CV.OCR;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.CV
{
    /// <summary>小程序图像处理与文字识别增量接口。</summary>
    /// <remarks>复用 MP 模块中与微信协议一致的裁剪、二维码和驾驶证返回模型，并使用 WxOpen AccessToken 容器处理小程序 AppId。</remarks>
    public static class VisualProcessingApi
    {
        /// <summary>通过图片 URL 智能裁剪图片。</summary>
        /// <param name="accessTokenOrAppId">小程序 AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="imgUrl">待处理图片 URL，图片大小必须小于 2 MB。</param>
        /// <param name="ratios">可选宽高比，多个比例使用英文逗号分隔，最多五个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>裁剪区域及原图尺寸。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 117。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/img-ocr/img/api_imgaicrop"/>。</remarks>
        public static AiCropJsonResult AiCrop(string accessTokenOrAppId, string imgUrl, string ratios = null, int timeOut = Config.TIME_OUT)
        {
            return SendImageUrl<AiCropJsonResult>(accessTokenOrAppId, "/cv/img/aicrop", imgUrl, CreatePostData(ratios), timeOut);
        }

        /// <summary>通过本地图片文件智能裁剪图片。</summary>
        /// <param name="accessTokenOrAppId">小程序 AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="file">待处理图片绝对路径，图片大小必须小于 2 MB。</param>
        /// <param name="ratios">可选宽高比，多个比例使用英文逗号分隔，最多五个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>裁剪区域及原图尺寸。</returns>
        /// <remarks>请求使用 <c>img</c> 文件字段和可选 <c>ratios</c> 普通表单字段。本接口支持第三方平台代调用，权限集 ID 为 117。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/img-ocr/img/api_imgaicrop"/>。</remarks>
        public static AiCropJsonResult AiCropByFile(string accessTokenOrAppId, string file, string ratios = null, int timeOut = Config.TIME_OUT)
        {
            return SendImageFile<AiCropJsonResult>(accessTokenOrAppId, "/cv/img/aicrop", file, CreatePostData(ratios), timeOut);
        }

        /// <summary>异步通过图片 URL 智能裁剪图片。</summary>
        /// <inheritdoc cref="AiCrop"/>
        public static Task<AiCropJsonResult> AiCropAsync(string accessTokenOrAppId, string imgUrl, string ratios = null, int timeOut = Config.TIME_OUT)
        {
            return SendImageUrlAsync<AiCropJsonResult>(accessTokenOrAppId, "/cv/img/aicrop", imgUrl, CreatePostData(ratios), timeOut);
        }

        /// <summary>异步通过本地图片文件智能裁剪图片。</summary>
        /// <inheritdoc cref="AiCropByFile"/>
        public static Task<AiCropJsonResult> AiCropByFileAsync(string accessTokenOrAppId, string file, string ratios = null, int timeOut = Config.TIME_OUT)
        {
            return SendImageFileAsync<AiCropJsonResult>(accessTokenOrAppId, "/cv/img/aicrop", file, CreatePostData(ratios), timeOut);
        }

        /// <summary>通过图片 URL 识别二维码、条码、DataMatrix 或 PDF417。</summary>
        /// <param name="accessTokenOrAppId">小程序 AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="imgUrl">待识别图片 URL，图片大小必须小于 2 MB。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>编码内容、类型、位置及原图尺寸。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 117。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/img-ocr/img/api_imgqrcode"/>。</remarks>
        public static QrCodeJsonResult QrCode(string accessTokenOrAppId, string imgUrl, int timeOut = Config.TIME_OUT)
        {
            return SendImageUrl<QrCodeJsonResult>(accessTokenOrAppId, "/cv/img/qrcode", imgUrl, null, timeOut);
        }

        /// <summary>通过本地图片文件识别二维码、条码、DataMatrix 或 PDF417。</summary>
        /// <param name="accessTokenOrAppId">小程序 AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="file">待识别图片绝对路径，图片大小必须小于 2 MB。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>编码内容、类型、位置及原图尺寸。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 117。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/img-ocr/img/api_imgqrcode"/>。</remarks>
        public static QrCodeJsonResult QrCodeByFile(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return SendImageFile<QrCodeJsonResult>(accessTokenOrAppId, "/cv/img/qrcode", file, null, timeOut);
        }

        /// <summary>异步通过图片 URL 识别二维码、条码、DataMatrix 或 PDF417。</summary>
        /// <inheritdoc cref="QrCode"/>
        public static Task<QrCodeJsonResult> QrCodeAsync(string accessTokenOrAppId, string imgUrl, int timeOut = Config.TIME_OUT)
        {
            return SendImageUrlAsync<QrCodeJsonResult>(accessTokenOrAppId, "/cv/img/qrcode", imgUrl, null, timeOut);
        }

        /// <summary>异步通过本地图片文件识别二维码、条码、DataMatrix 或 PDF417。</summary>
        /// <inheritdoc cref="QrCodeByFile"/>
        public static Task<QrCodeJsonResult> QrCodeByFileAsync(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return SendImageFileAsync<QrCodeJsonResult>(accessTokenOrAppId, "/cv/img/qrcode", file, null, timeOut);
        }

        /// <summary>通过图片 URL 将图片高清化为原分辨率的两倍。</summary>
        /// <param name="accessTokenOrAppId">小程序 AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="imgUrl">待高清化图片 URL，图片大小必须小于 2 MB。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>高清化图片的临时素材 MediaId。</returns>
        /// <remarks>微信官方已明确本接口因系统维护而下架；保留入口仅用于协议兼容。本接口原支持第三方平台代调用，权限集 ID 为 117。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/img-ocr/img/api_imgsuperresolution"/>。</remarks>
        [Obsolete("微信官方已下架图片高清化接口；此入口仅用于协议兼容。")]
        public static SuperResolutionJsonResult SuperResolution(string accessTokenOrAppId, string imgUrl, int timeOut = Config.TIME_OUT)
        {
            return SendImageUrl<SuperResolutionJsonResult>(accessTokenOrAppId, "/cv/img/superresolution", imgUrl, null, timeOut);
        }

        /// <summary>通过本地图片文件将图片高清化为原分辨率的两倍。</summary>
        /// <param name="accessTokenOrAppId">小程序 AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="file">待高清化图片绝对路径，图片大小必须小于 2 MB。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>高清化图片的临时素材 MediaId。</returns>
        /// <remarks>微信官方已明确本接口因系统维护而下架；保留入口仅用于协议兼容。本接口原支持第三方平台代调用，权限集 ID 为 117。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/img-ocr/img/api_imgsuperresolution"/>。</remarks>
        [Obsolete("微信官方已下架图片高清化接口；此入口仅用于协议兼容。")]
        public static SuperResolutionJsonResult SuperResolutionByFile(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return SendImageFile<SuperResolutionJsonResult>(accessTokenOrAppId, "/cv/img/superresolution", file, null, timeOut);
        }

        /// <summary>异步通过图片 URL 将图片高清化为原分辨率的两倍。</summary>
        /// <inheritdoc cref="SuperResolution"/>
        [Obsolete("微信官方已下架图片高清化接口；此入口仅用于协议兼容。")]
        public static Task<SuperResolutionJsonResult> SuperResolutionAsync(string accessTokenOrAppId, string imgUrl, int timeOut = Config.TIME_OUT)
        {
            return SendImageUrlAsync<SuperResolutionJsonResult>(accessTokenOrAppId, "/cv/img/superresolution", imgUrl, null, timeOut);
        }

        /// <summary>异步通过本地图片文件将图片高清化为原分辨率的两倍。</summary>
        /// <inheritdoc cref="SuperResolutionByFile"/>
        [Obsolete("微信官方已下架图片高清化接口；此入口仅用于协议兼容。")]
        public static Task<SuperResolutionJsonResult> SuperResolutionByFileAsync(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return SendImageFileAsync<SuperResolutionJsonResult>(accessTokenOrAppId, "/cv/img/superresolution", file, null, timeOut);
        }

        /// <summary>通过图片 URL 识别机动车驾驶证。</summary>
        /// <param name="accessTokenOrAppId">小程序 AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="imgUrl">待识别驾驶证图片 URL，图片大小必须小于 2 MB。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>驾驶证证号、姓名、准驾车型和有效期等信息。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 117。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/img-ocr/ocr/api_drivinglicenseocr"/>。</remarks>
        public static DrivingLicenseJsonResult DrivingLicense(string accessTokenOrAppId, string imgUrl, int timeOut = Config.TIME_OUT)
        {
            return SendImageUrl<DrivingLicenseJsonResult>(accessTokenOrAppId, "/cv/ocr/drivinglicense", imgUrl, null, timeOut);
        }

        /// <summary>通过本地图片文件识别机动车驾驶证。</summary>
        /// <param name="accessTokenOrAppId">小程序 AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="file">待识别驾驶证图片绝对路径，图片大小必须小于 2 MB。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>驾驶证证号、姓名、准驾车型和有效期等信息。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 117。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/img-ocr/ocr/api_drivinglicenseocr"/>。</remarks>
        public static DrivingLicenseJsonResult DrivingLicenseByFile(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return SendImageFile<DrivingLicenseJsonResult>(accessTokenOrAppId, "/cv/ocr/drivinglicense", file, null, timeOut);
        }

        /// <summary>异步通过图片 URL 识别机动车驾驶证。</summary>
        /// <inheritdoc cref="DrivingLicense"/>
        public static Task<DrivingLicenseJsonResult> DrivingLicenseAsync(string accessTokenOrAppId, string imgUrl, int timeOut = Config.TIME_OUT)
        {
            return SendImageUrlAsync<DrivingLicenseJsonResult>(accessTokenOrAppId, "/cv/ocr/drivinglicense", imgUrl, null, timeOut);
        }

        /// <summary>异步通过本地图片文件识别机动车驾驶证。</summary>
        /// <inheritdoc cref="DrivingLicenseByFile"/>
        public static Task<DrivingLicenseJsonResult> DrivingLicenseByFileAsync(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return SendImageFileAsync<DrivingLicenseJsonResult>(accessTokenOrAppId, "/cv/ocr/drivinglicense", file, null, timeOut);
        }

        private static Dictionary<string, string> CreatePostData(string ratios)
        {
            return string.IsNullOrWhiteSpace(ratios)
                ? null
                : new Dictionary<string, string> { ["ratios"] = ratios };
        }

        private static T SendImageUrl<T>(string accessTokenOrAppId, string path, string imgUrl, Dictionary<string, string> postData, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = BuildUrl(path, accessToken, imgUrl);
                return postData == null
                    ? CommonJsonSend.Send<T>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json")
                    : Post.PostFileGetJson<T>(CommonDI.CommonSP, url, null, null, postData, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        private static T SendImageFile<T>(string accessTokenOrAppId, string path, string file, Dictionary<string, string> postData, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = BuildUrl(path, accessToken, null);
                var files = new Dictionary<string, string> { ["img"] = file };
                return Post.PostFileGetJson<T>(CommonDI.CommonSP, url, null, files, postData, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        private static Task<T> SendImageUrlAsync<T>(string accessTokenOrAppId, string path, string imgUrl, Dictionary<string, string> postData, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = BuildUrl(path, accessToken, imgUrl);
                return postData == null
                    ? await CommonJsonSend.SendAsync<T>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json").ConfigureAwait(false)
                    : await Post.PostFileGetJsonAsync<T>(CommonDI.CommonSP, url, null, null, postData, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId);
        }

        private static Task<T> SendImageFileAsync<T>(string accessTokenOrAppId, string path, string file, Dictionary<string, string> postData, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = BuildUrl(path, accessToken, null);
                var files = new Dictionary<string, string> { ["img"] = file };
                return await Post.PostFileGetJsonAsync<T>(CommonDI.CommonSP, url, null, files, postData, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId);
        }

        private static string BuildUrl(string path, string accessToken, string imgUrl)
        {
            var query = string.IsNullOrWhiteSpace(imgUrl) ? string.Empty : "&img_url=" + imgUrl.AsUrlData();
            return Config.ApiMpHost + path + "?access_token=" + accessToken.AsUrlData() + query;
        }
    }
}
