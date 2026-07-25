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

    文件名：ImageApi.cs
    文件功能描述：ImageApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.CO2NET.HttpUtility;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs.CV.Image
{
    /// <summary>
    /// 微信智能图像接口。
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, true)]
    public static class ImageApi
    {
        #region 同步方法

        /// <summary>
        /// 智能裁剪图片（图片 URL 方式）。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或 AppId。</param>
        /// <param name="imgUrl">需要裁剪的图片 URL，图片大小不能超过 2 MB。</param>
        /// <param name="ratios">可选裁剪宽高比，按官方格式填写，最多指定 5 个比例；为空时由微信自动选择比例。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>裁剪坐标及原图尺寸。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/openpoc/image/api_imgaicrop"/>。
        /// </remarks>
        public static AiCropJsonResult AiCrop(string accessTokenOrAppId, string imgUrl, string ratios = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cv/img/aicrop?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(imgUrl), accessToken);
                if (string.IsNullOrWhiteSpace(ratios))
                {
                    return CommonJsonSend.Send<AiCropJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
                }

                return Post.PostFileGetJson<AiCropJsonResult>(CommonDI.CommonSP, url, null, null, CreatePostData(ratios), timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 智能裁剪图片（本地文件上传方式）。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或 AppId。</param>
        /// <param name="file">需要裁剪的图片绝对路径，图片大小不能超过 2 MB。</param>
        /// <param name="ratios">可选裁剪宽高比，按官方格式填写，最多指定 5 个比例；为空时由微信自动选择比例。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>裁剪坐标及原图尺寸。</returns>
        /// <remarks>
        /// 请求使用官方定义的 <c>img</c> 和可选 <c>ratios</c> multipart/form-data 字段。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/openpoc/image/api_imgaicrop"/>。
        /// </remarks>
        public static AiCropJsonResult AiCropByFile(string accessTokenOrAppId, string file, string ratios = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cv/img/aicrop?access_token={0}", accessToken);
                return Post.PostFileGetJson<AiCropJsonResult>(CommonDI.CommonSP, url, null, CreateFileDictionary(file), CreatePostData(ratios), timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 识别图片中的二维码或条码（图片 URL 方式）。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或 AppId。</param>
        /// <param name="imgUrl">需要识别的图片 URL，图片大小不能超过 2 MB。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二维码、条码、DataMatrix 或 PDF417 的识别结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/openpoc/image/api_imgqrcode"/>。
        /// </remarks>
        public static QrCodeJsonResult QrCode(string accessTokenOrAppId, string imgUrl, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cv/img/qrcode?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(imgUrl), accessToken);
                return CommonJsonSend.Send<QrCodeJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 识别图片中的二维码或条码（本地文件上传方式）。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或 AppId。</param>
        /// <param name="file">需要识别的图片绝对路径，图片大小不能超过 2 MB。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二维码、条码、DataMatrix 或 PDF417 的识别结果。</returns>
        /// <remarks>
        /// 请求使用官方定义的 <c>img</c> multipart/form-data 字段。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/openpoc/image/api_imgqrcode"/>。
        /// </remarks>
        public static QrCodeJsonResult QrCodeByFile(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cv/img/qrcode?access_token={0}", accessToken);
                var fileDictionary = new Dictionary<string, string>
                {
                    ["img"] = file
                };
                return Post.PostFileGetJson<QrCodeJsonResult>(CommonDI.CommonSP, url, null, fileDictionary, null, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 异步智能裁剪图片（图片 URL 方式）。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或 AppId。</param>
        /// <param name="imgUrl">需要裁剪的图片 URL，图片大小不能超过 2 MB。</param>
        /// <param name="ratios">可选裁剪宽高比，按官方格式填写，最多指定 5 个比例；为空时由微信自动选择比例。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>裁剪坐标及原图尺寸。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/openpoc/image/api_imgaicrop"/>。
        /// </remarks>
        public static async Task<AiCropJsonResult> AiCropAsync(string accessTokenOrAppId, string imgUrl, string ratios = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cv/img/aicrop?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(imgUrl), accessToken);
                if (string.IsNullOrWhiteSpace(ratios))
                {
                    return await CommonJsonSend.SendAsync<AiCropJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json").ConfigureAwait(false);
                }

                return await Post.PostFileGetJsonAsync<AiCropJsonResult>(CommonDI.CommonSP, url, null, null, CreatePostData(ratios), timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步智能裁剪图片（本地文件上传方式）。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或 AppId。</param>
        /// <param name="file">需要裁剪的图片绝对路径，图片大小不能超过 2 MB。</param>
        /// <param name="ratios">可选裁剪宽高比，按官方格式填写，最多指定 5 个比例；为空时由微信自动选择比例。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>裁剪坐标及原图尺寸。</returns>
        /// <remarks>
        /// 请求使用官方定义的 <c>img</c> 和可选 <c>ratios</c> multipart/form-data 字段。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/openpoc/image/api_imgaicrop"/>。
        /// </remarks>
        public static async Task<AiCropJsonResult> AiCropByFileAsync(string accessTokenOrAppId, string file, string ratios = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cv/img/aicrop?access_token={0}", accessToken);
                return await Post.PostFileGetJsonAsync<AiCropJsonResult>(CommonDI.CommonSP, url, null, CreateFileDictionary(file), CreatePostData(ratios), timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步识别图片中的二维码或条码（图片 URL 方式）。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或 AppId。</param>
        /// <param name="imgUrl">需要识别的图片 URL，图片大小不能超过 2 MB。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二维码、条码、DataMatrix 或 PDF417 的识别结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/openpoc/image/api_imgqrcode"/>。
        /// </remarks>
        public static async Task<QrCodeJsonResult> QrCodeAsync(string accessTokenOrAppId, string imgUrl, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cv/img/qrcode?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(imgUrl), accessToken);
                return await CommonJsonSend.SendAsync<QrCodeJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json").ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步识别图片中的二维码或条码（本地文件上传方式）。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或 AppId。</param>
        /// <param name="file">需要识别的图片绝对路径，图片大小不能超过 2 MB。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二维码、条码、DataMatrix 或 PDF417 的识别结果。</returns>
        /// <remarks>
        /// 请求使用官方定义的 <c>img</c> multipart/form-data 字段。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/openpoc/image/api_imgqrcode"/>。
        /// </remarks>
        public static async Task<QrCodeJsonResult> QrCodeByFileAsync(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cv/img/qrcode?access_token={0}", accessToken);
                var fileDictionary = new Dictionary<string, string>
                {
                    ["img"] = file
                };
                return await Post.PostFileGetJsonAsync<QrCodeJsonResult>(CommonDI.CommonSP, url, null, fileDictionary, null, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        private static Dictionary<string, string> CreateFileDictionary(string file)
        {
            return string.IsNullOrWhiteSpace(file)
                ? null
                : new Dictionary<string, string> { ["img"] = file };
        }

        private static Dictionary<string, string> CreatePostData(string ratios)
        {
            return string.IsNullOrWhiteSpace(ratios)
                ? null
                : new Dictionary<string, string> { ["ratios"] = ratios };
        }
    }
}
