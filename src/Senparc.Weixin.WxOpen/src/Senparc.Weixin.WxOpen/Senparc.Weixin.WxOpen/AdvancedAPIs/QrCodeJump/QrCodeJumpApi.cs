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

    文件名：QrCodeJumpApi.cs
    文件功能描述：小程序的普通链接二维码接口

----------------------------------------------------------------*/

/*
    API：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/Mini_Programs/qrcode/qrcode.html
 */
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.QrCodeJump
{
    public static class QrCodeJumpApi
    {
        #region 同步方法
        /// <summary>
        /// 获取已设置的二维码规则
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static GetJsonResult Get(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/qrcodejumpget?access_token={0}";

                return CommonJsonSend.Send<GetJsonResult>(accessToken, urlFormat, null, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取校验文件名称及内容
        /// 通过本接口下载随机校验文件，并将文件上传至服务器指定位置的目录下，方可通过所属权校验。
        /// 验证文件放置规则：放置于 URL 中声明的最后一级子目录下，若无子目录，则放置于 host 所属服务器的顶层目录下。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static DownloadJsonResult Download(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/qrcodejumpdownload?access_token={0}";

            return CommonJsonSend.Send<DownloadJsonResult>(accessToken, urlFormat, null, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 增加或修改二维码规则
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="prefix">二维码规则</param>
        /// <param name="permit_sub_rule">是否独占符合二维码前缀匹配规则的所有子规 1 为不占用，2 为占用;</param>
        /// <param name="path">小程序功能页面</param>
        /// <param name="open_version">测试范围1开发版（配置只对开发者生效）,2体验版（配置对管理员、体验者生效),3正式版（配置对开发者、管理员和体验者生效）</param>
        /// <param name="is_edit">编辑标志位，0 表示新增二维码规则，1 表示修改已有二维码规则</param>
        /// <param name="debug_url">测试链接，至多 5 个用于测试的二维码完整链接，此链接必须符合已填写的二维码规则。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult Add(string accessTokenOrAppId, string prefix, int permit_sub_rule, string path, int open_version, int is_edit, string[] debug_url = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/qrcodejumpadd?access_token={0}";

                var data = new {
                    prefix = prefix,
                    permit_sub_rule = permit_sub_rule,
                    path = path,
                    open_version= open_version,
                    debug_url = (debug_url == null) ? new string[] { } : debug_url,
                    is_edit = is_edit
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发布已设置的二维码规则
        /// 需要先添加二维码规则，然后调用本接口将二维码规则发布生效，发布后现网用户扫码命中改规则的普通链接二维码时将调整到正式版小程序指定的页面。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="prefix">二维码规则</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult Publish(string accessTokenOrAppId, string prefix, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {

                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/qrcodejumppublish?access_token={0}";

                var data = new
                {
                    prefix = prefix
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除已设置的二维码规则
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="prefix">二维码规则</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult Delete(string accessTokenOrAppId, string prefix, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/qrcodejumpdelete?access_token={0}";

                var data = new
                {
                    prefix = prefix
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】获取已设置的二维码规则
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "QrCodeJumpApi.GetAsync", true)]
        public static async Task<GetJsonResult> GetAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken => {

                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/qrcodejumpget?access_token={0}";

                return await CommonJsonSend.SendAsync<GetJsonResult>(accessToken, urlFormat, null, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
            
        }

        /// <summary>
        /// 【异步方法】获取校验文件名称及内容
        /// 通过本接口下载随机校验文件，并将文件上传至服务器指定位置的目录下，方可通过所属权校验。
        /// 验证文件放置规则：放置于 URL 中声明的最后一级子目录下，若无子目录，则放置于 host 所属服务器的顶层目录下。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "QrCodeJumpApi.DownloadAsync", true)]
        public static async Task<DownloadJsonResult> DownloadAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken => {

                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/qrcodejumpdownload?access_token={0}";

                return await CommonJsonSend.SendAsync<DownloadJsonResult>(accessToken, urlFormat, null, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】增加或修改二维码规则
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="prefix">二维码规则</param>
        /// <param name="permit_sub_rule">是否独占符合二维码前缀匹配规则的所有子规 1 为不占用，2 为占用;</param>
        /// <param name="path">小程序功能页面</param>
        /// <param name="open_version">测试范围1开发版（配置只对开发者生效）,2体验版（配置对管理员、体验者生效),3正式版（配置对开发者、管理员和体验者生效）</param>
        /// <param name="is_edit">编辑标志位，0 表示新增二维码规则，1 表示修改已有二维码规则</param>
        /// <param name="debug_url">测试链接，至多 5 个用于测试的二维码完整链接，此链接必须符合已填写的二维码规则。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "QrCodeJumpApi.AddAsync", true)]
        public static async Task<WxJsonResult> AddAsync(string accessTokenOrAppId, string prefix, int permit_sub_rule, string path, int open_version, int is_edit, string[] debug_url = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken => {

                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/qrcodejumpadd?access_token={0}";

                var data = new
                {
                    prefix = prefix,
                    permit_sub_rule = permit_sub_rule,
                    path = path,
                    open_version = open_version,
                    debug_url = (debug_url == null) ? new string[] { } : debug_url,
                    is_edit = is_edit
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】发布已设置的二维码规则
        /// 需要先添加二维码规则，然后调用本接口将二维码规则发布生效，发布后现网用户扫码命中改规则的普通链接二维码时将调整到正式版小程序指定的页面。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="prefix">二维码规则</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "QrCodeJumpApi.PublishAsync", true)]
        public static async Task<WxJsonResult> PublishAsync(string accessTokenOrAppId, string prefix, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken => {

                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/qrcodejumppublish?access_token={0}";

                var data = new
                {
                    prefix = prefix
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】删除已设置的二维码规则
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="prefix">二维码规则</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "QrCodeJumpApi.DeleteAsync", true)]
        public static async Task<WxJsonResult> DeleteAsync(string accessTokenOrAppId, string prefix, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken => {

                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/qrcodejumpdelete?access_token={0}";

            var data = new
            {
                prefix = prefix
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);

        }
        #endregion
    }
}
