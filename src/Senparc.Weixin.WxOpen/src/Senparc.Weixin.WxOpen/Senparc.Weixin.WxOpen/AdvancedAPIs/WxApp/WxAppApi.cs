﻿/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：WxAppApi.cs
    文件功能描述：小程序WxApp目录下面的接口
    
    
    创建标识：Senparc - 20170103
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Template.TemplateJson;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp
{
    /* 
    tip：通过该接口，仅能生成已发布的小程序的二维码。
    tip：可以在开发者工具预览时生成开发版的带参二维码。
    tip：带参二维码只有 10000 个，请谨慎调用。
    */

    /// <summary>
    /// WxApp接口
    /// </summary>
    public static class WxAppApi
    {
        #region 同步请求

        /// <summary>
        /// 获取小程序页面二维码
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="stream">储存二维码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult CreateWxaQrCode(string accessTokenOrAppId, Stream stream, string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxaapp/createwxaqrcode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);
                Get.Download(url, stream);
                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步请求
        /// <summary>
        /// 【异步方法】获取小程序页面二维码
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="stream">储存二维码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1,注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CreateWxaQrCodeAsync(string accessTokenOrAppId, Stream stream, string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxaapp/createwxaqrcode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);
                await Get.DownloadAsync(url, stream);
                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        #endregion
    }
}