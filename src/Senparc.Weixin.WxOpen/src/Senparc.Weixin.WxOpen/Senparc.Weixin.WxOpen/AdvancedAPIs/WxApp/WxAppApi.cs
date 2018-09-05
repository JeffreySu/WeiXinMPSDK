#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：WxAppApi.cs
    文件功能描述：小程序WxApp目录下面的接口
    
    
    创建标识：Senparc - 20170103
    
    修改标识：Senparc - 20170129
    修改描述：v1.0.1 完善CreateWxaQrCode方法

    修改标识：Senparc - 20170726
    修改描述：完成接口开放平台-代码管理及小程序码获取

    修改标识：Senparc - 20180106
    修改描述：完成接口-附近的小程序API

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
//using Senparc.Weixin.Helpers;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using System.Collections.Generic;

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
        #region 同步方法

        /// <summary>
        /// 获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult GetWxaCode(string accessTokenOrAppId, Stream stream, string path,
            int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getwxacode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                if (auto_color && lineColor == null)
                {
                    lineColor = new LineColor();//提供默认值
                }

                var data = new { path = path, width = width, line_color = lineColor, is_hyaline = isHyaline };
                JsonSetting jsonSetting = new JsonSetting(true);
                Post.Download(url, SerializerHelper.GetJsonString(data, jsonSetting), stream);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult GetWxaCode(string accessTokenOrAppId, string filePath, string path, int width = 430,
            bool auto_color = false, LineColor lineColor = null, bool isHyaline = false, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = WxAppApi.GetWxaCode(accessTokenOrAppId, ms, path, width, auto_color, lineColor, isHyaline, timeOut);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    ms.CopyTo(fs);
                    fs.Flush();
                }
                return result;
            }
        }

        /// <summary>
        /// 获取小程序页面的小程序码 不限制
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult GetWxaCodeUnlimit(string accessTokenOrAppId, Stream stream, string scene,
            string page, int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getwxacodeunlimit?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                if (auto_color && lineColor == null)
                {
                    lineColor = new LineColor();//提供默认值
                }

                var data = new { scene = scene, page = page, width = width, line_color = lineColor, is_hyaline = isHyaline };
                JsonSetting jsonSetting = new JsonSetting(true);
                Post.Download(url, SerializerHelper.GetJsonString(data, jsonSetting), stream);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult GetWxaCodeUnlimit(string accessTokenOrAppId, string filePath, string scene, string page, int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = WxAppApi.GetWxaCodeUnlimit(accessTokenOrAppId, ms, scene, page, width, auto_color, lineColor, isHyaline, timeOut);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    ms.CopyTo(fs);
                    fs.Flush();
                }
                return result;
            }
        }

        /// <summary>
        /// 获取小程序页面二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存二维码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult CreateWxQrCode(string accessTokenOrAppId, Stream stream, string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxaapp/createwxaqrcode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                var data = new { path = path, width = width };
                Post.Download(url, SerializerHelper.GetJsonString(data), stream);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序页面二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult CreateWxQrCode(string accessTokenOrAppId, string filePath, string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = WxAppApi.CreateWxQrCode(accessTokenOrAppId, ms, path, width);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    ms.CopyTo(fs);
                    fs.Flush();
                }
                return result;
            }
        }

        /// <summary>
        /// session_key 合法性校验
        /// https://mp.weixin.qq.com/debug/wxagame/dev/tutorial/http-signature.html
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">用户唯一标识符</param>
        /// <param name="sessionKey">用户登录态签名</param>
        /// <param name="buffer">托管数据，类型为字符串，长度不超过1000字节（官方文档没有提供说明，可留空）</param>
        /// <param name="sigMethod">用户登录态签名的哈希方法，默认为hmac_sha256</param>
        public static WxJsonResult CheckSession(string accessTokenOrAppId, string openId, string sessionKey, string buffer, string sigMethod = "hmac_sha256")
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/checksession?access_token={0}&signature={1}&openid={2}&sig_method={3}";
                var signature = EncryptHelper.GetHmacSha256("", sessionKey);
                var url = urlFormat.FormatWith(accessToken, signature, openId, sigMethod);

                return CommonJsonSend.Send<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 添加地点
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="relatedName">经营资质主体(与小程序同主体--不填,与小程序非同主体--必填)</param>
        /// <param name="relatedCredential">经营资质证件号</param>
        /// <param name="relatedAddress">经营资质地址</param>
        /// <param name="relatedProofMaterial">相关证明材料照片临时素材mediaid(与小程序同主体--不填,与小程序非同主体--必填)</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddNearbyPoiJsonResult AddNearbyPoi(string accessTokenOrAppId, string relatedName, string relatedCredential, string relatedAddress, string relatedProofMaterial, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/addnearbypoi?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { related_name = relatedName, related_credential = relatedCredential, related_address = relatedAddress, related_proof_material = relatedProofMaterial };

                return CommonJsonSend.Send<AddNearbyPoiJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查看地点列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="page">起始页id（从1开始计数）</param>
        /// <param name="page_rows">每页展示个数（最多1000个）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetNearbyPoiListJsonResult GetNearbyPoiList(string accessTokenOrAppId, int page = 1, int page_rows = 10, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getnearbypoilist?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { page = page, page_rows = page_rows };

                return CommonJsonSend.Send<GetNearbyPoiListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除地点
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">附近地点ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult DelNearbyPoi(string accessTokenOrAppId, string poi_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/delnearbypoi?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { poi_id = poi_id };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 展示/取消展示附近小程序
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">附近地点ID</param>
        /// <param name="status">0：取消展示；1：展示</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SetNearbyPoiShowStatus(string accessTokenOrAppId, string poi_id, int status, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/setnearbypoishowstatus?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { poi_id = poi_id, status = status };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 检查一段文本是否含有违法违规内容
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/msgSecCheck.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="content">要检测的文本内容，长度不超过 500KB，编码格式为utf-8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult MsgSecCheck(string accessTokenOrAppId, string content, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/msg_sec_check?access_token={0}";

                var data = new { content = content };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 校验一张图片是否含有违法违规内容
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/imgSecCheck.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">文件完整物理路径<para>格式支持PNG、JPEG、JPG、GIF，图片尺寸不超过 750px * 1334px</para></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ImgSecCheck(string accessTokenOrAppId, string filePath, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/img_sec_check?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);
                var fileDic = new Dictionary<string, string>();
                fileDic["media"] = filePath;
                return CO2NET.HttpUtility.Post.PostFileGetJson<WxJsonResult>(url, fileDictionary: fileDic, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> GetWxaCodeAsync(string accessTokenOrAppId, string filePath, string path,
            int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = await WxAppApi.GetWxaCodeAsync(accessTokenOrAppId, ms, path, width, auto_color, lineColor, isHyaline, timeOut);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    await ms.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                return result;
            }
        }


        /// <summary>
        /// 【异步方法】获取小程序页面的小程序码 不受限制
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> GetWxaCodeUnlimitAsync(string accessTokenOrAppId, Stream stream,
            string scene, string page, int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getwxacodeunlimit?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                if (auto_color && lineColor == null)
                {
                    lineColor = new LineColor();//提供默认值
                }

                var data = new { scene = scene, page = page, width = width, line_color = lineColor, is_hyaline = isHyaline };
                JsonSetting jsonSetting = new JsonSetting(true);
                await Post.DownloadAsync(url, SerializerHelper.GetJsonString(data, jsonSetting), stream);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> GetWxaCodeUnlimitAsync(string accessTokenOrAppId, string filePath,
            string scene, string page, int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false,
            int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = await WxAppApi.GetWxaCodeUnlimitAsync(accessTokenOrAppId, ms, scene, page, width, auto_color, lineColor, isHyaline, timeOut);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    await ms.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                return result;
            }
        }

        /// <summary>
        /// 【异步方法】获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> GetWxaCodeAsync(string accessTokenOrAppId, Stream stream, string path,
            int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getwxacode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                if (auto_color && lineColor == null)
                {
                    lineColor = new LineColor();//提供默认值
                }

                var data = new { path = path, width = width, line_color = lineColor, is_hyaline = isHyaline };
                JsonSetting jsonSetting = new JsonSetting(true);
                await Post.DownloadAsync(url, SerializerHelper.GetJsonString(data, jsonSetting), stream);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取小程序页面二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存二维码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1,注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CreateWxQrCodeAsync(string accessTokenOrAppId, Stream stream,
            string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxaapp/createwxaqrcode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                var data = new { path = path, width = width };
                await Post.DownloadAsync(url, SerializerHelper.GetJsonString(data), stream);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取小程序页面二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CreateWxQrCodeAsync(string accessTokenOrAppId, string filePath, string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = await WxAppApi.CreateWxQrCodeAsync(accessTokenOrAppId, ms, path, width);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    await ms.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                return result;
            }
        }


        /// <summary>
        /// 【异步方法】session_key 合法性校验
        /// https://mp.weixin.qq.com/debug/wxagame/dev/tutorial/http-signature.html
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">用户唯一标识符</param>
        /// <param name="sessionKey">用户登录态签名</param>
        /// <param name="buffer">托管数据，类型为字符串，长度不超过1000字节（官方文档没有提供说明，可留空）</param>
        /// <param name="sigMethod">用户登录态签名的哈希方法，默认为hmac_sha256</param>
        public static async Task<WxJsonResult> CheckSessionAsync(string accessTokenOrAppId, string openId, string sessionKey, string buffer, string sigMethod = "hmac_sha256")
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/checksession?access_token={0}&signature={1}&openid={2}&sig_method={3}";
                var signature = EncryptHelper.GetHmacSha256("", sessionKey);
                var url = urlFormat.FormatWith(accessToken, signature, openId, sigMethod);

                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】添加地点
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="relatedName">经营资质主体(与小程序同主体--不填,与小程序非同主体--必填)</param>
        /// <param name="relatedCredential">经营资质证件号</param>
        /// <param name="relatedAddress">经营资质地址</param>
        /// <param name="relatedProofMaterial">相关证明材料照片临时素材mediaid(与小程序同主体--不填,与小程序非同主体--必填)</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AddNearbyPoiJsonResult> AddNearbyPoiAsync(string accessTokenOrAppId, string relatedName, string relatedCredential, string relatedAddress, string relatedProofMaterial, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/addnearbypoi?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { related_name = relatedName, related_credential = relatedCredential, related_address = relatedAddress, related_proof_material = relatedProofMaterial };

                return await CommonJsonSend.SendAsync<AddNearbyPoiJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查看地点列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="page">起始页id（从1开始计数）</param>
        /// <param name="page_rows">每页展示个数（最多1000个）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetNearbyPoiListJsonResult> GetNearbyPoiListAsync(string accessTokenOrAppId, int page = 1, int page_rows = 10, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getnearbypoilist?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { page = page, page_rows = page_rows };

                return await CommonJsonSend.SendAsync<GetNearbyPoiListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】删除地点
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">附近地点ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DelNearbyPoiAsync(string accessTokenOrAppId, string poi_id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/delnearbypoi?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { poi_id = poi_id };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】展示/取消展示附近小程序
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">附近地点ID</param>
        /// <param name="status">0：取消展示；1：展示</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SetNearbyPoiListAsync(string accessTokenOrAppId, string poi_id, int status, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/setnearbypoishowstatus?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { poi_id = poi_id, status = status };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 检查一段文本是否含有违法违规内容
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/msgSecCheck.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="content">要检测的文本内容，长度不超过 500KB，编码格式为utf-8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> MsgSecCheckAsync(string accessTokenOrAppId, string content, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/msg_sec_check?access_token={0}";

                var data = new { content = content };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】校验一张图片是否含有违法违规内容
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/imgSecCheck.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">文件完整物理路径<para>格式支持PNG、JPEG、JPG、GIF，图片尺寸不超过 750px * 1334px</para></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ImgSecCheckAsync(string accessTokenOrAppId, string filePath, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/img_sec_check?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);
                var fileDic = new Dictionary<string, string>();
                fileDic["media"] = filePath;
                return await CO2NET.HttpUtility.Post.PostFileGetJsonAsync<WxJsonResult>(url, fileDictionary: fileDic, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion
#endif
    }
}