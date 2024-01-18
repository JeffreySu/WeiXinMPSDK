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
    
    文件名：OCRApi.cs
    文件功能描述：智能接口 /OCR识别
    
    
    创建标识：yaofeng - 20231204

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    /// <summary>
    /// 智能接口 /OCR识别
    /// https://developers.weixin.qq.com/doc/offiaccount/Intelligent_Interface/OCR.html
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, true)]
    public static class OCRApi
    {
        #region 同步方法
        /// <summary>
        /// 身份证OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static IdCardJsonResult IdCard(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cv/ocr/idcard?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return CommonJsonSend.Send<IdCardJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 银行卡OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static BankCardJsonResult BankCard(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/bankcard?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return CommonJsonSend.Send<BankCardJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 行驶证OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static DrivingJsonResult Driving(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/driving?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return CommonJsonSend.Send<DrivingJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 营业执照OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static BizLicenseJsonResult BizLicense(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/bizlicense?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return CommonJsonSend.Send<BizLicenseJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 通用印刷体OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CommJsonResult Comm(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/comm?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return CommonJsonSend.Send<CommJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 车牌OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static PlateNumJsonResult PlateNum(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/platenum?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return CommonJsonSend.Send<PlateNumJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 菜单OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MenuJsonResult Menu(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/menu?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return CommonJsonSend.Send<MenuJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 身份证OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<IdCardJsonResult> IdCardAsync(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/idcard?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return await CommonJsonSend.SendAsync<IdCardJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 银行卡OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<BankCardJsonResult> BankCardAsync(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/bankcard?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return await CommonJsonSend.SendAsync<BankCardJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 行驶证OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<DrivingJsonResult> DrivingAsync(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/driving?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return await CommonJsonSend.SendAsync<DrivingJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 营业执照OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<BizLicenseJsonResult> BizLicenseAsync(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/bizlicense?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return await CommonJsonSend.SendAsync<BizLicenseJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 通用印刷体OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CommJsonResult> CommAsync(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/comm?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return await CommonJsonSend.SendAsync<CommJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 车牌OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<PlateNumJsonResult> PlateNumAsync(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/platenum?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return await CommonJsonSend.SendAsync<PlateNumJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 菜单OCR识别
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="img_url">图片地址</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MenuJsonResult> MenuAsync(string accessTokenOrAppId, string img_url, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cv/ocr/menu?img_url={0}&access_token={1}", System.Web.HttpUtility.UrlEncode(img_url), accessToken);
                return await CommonJsonSend.SendAsync<MenuJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut: timeOut, contentType: "application/json");
            }, accessTokenOrAppId);
        }
        #endregion
    }
}
