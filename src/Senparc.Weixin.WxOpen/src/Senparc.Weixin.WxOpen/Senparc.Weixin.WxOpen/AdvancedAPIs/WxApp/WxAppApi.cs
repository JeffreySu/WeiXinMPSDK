#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
    
    文件名：WxAppApi.cs
    文件功能描述：小程序WxApp目录下面的接口
    
    
    创建标识：Senparc - 20170103
    
    修改标识：Senparc - 20170129
    修改描述：v1.0.1 完善CreateWxaQrCode方法

    修改标识：Senparc - 20170726
    修改描述：完成接口开放平台-代码管理及小程序码获取

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
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
        #region 同步方法

        /// <summary>
        /// 获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult GetWxaCode(string accessTokenOrAppId, Stream stream, string path, int width = 430, bool auto_color = false, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/wxa/getwxacode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                var data = new { path = path, width = width };
                SerializerHelper serializerHelper = new SerializerHelper();
                Post.Download(url, serializerHelper.GetJsonString(data), stream);

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
        /// <param name="auto_color">自动配置线条颜色</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult GetWxaCode(string accessTokenOrAppId, string filePath, string path, int width = 430, bool auto_color = false, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = WxAppApi.GetWxaCode(accessTokenOrAppId, ms, path, width);
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
        /// <param name="auto_color">自动配置线条颜色</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult GetWxaCodeUnlimit(string accessTokenOrAppId, Stream stream, string scene, string page, int width = 430, bool auto_color = false, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                var data = new { scene = scene, page = page, width = width };
                SerializerHelper serializerHelper = new SerializerHelper();
                Post.Download(url, serializerHelper.GetJsonString(data), stream);

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
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult GetWxaCodeUnlimit(string accessTokenOrAppId, string filePath, string scene, string page, int width = 430, bool auto_color = false, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = WxAppApi.GetWxaCodeUnlimit(accessTokenOrAppId, ms, scene, page, width);
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
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxaapp/createwxaqrcode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                var data = new { path = path, width = width };
                SerializerHelper serializerHelper = new SerializerHelper();
                Post.Download(url, serializerHelper.GetJsonString(data), stream);

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

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> GetWxaCodeAsync(string accessTokenOrAppId, string filePath, string path, int width = 430, bool auto_color = false, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = await WxAppApi.GetWxaCodeAsync(accessTokenOrAppId, ms, path, width);
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
        /// 获取小程序页面的小程序码 不受限制
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> GetWxaCodeUnlimitAsync(string accessTokenOrAppId, Stream stream, string scene, string page, int width = 430, bool auto_color = false, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                var data = new { scene = scene, page = page, width = width };
                SerializerHelper serializerHelper = new SerializerHelper();
                await Post.DownloadAsync(url, serializerHelper.GetJsonString(data), stream);

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
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> GetWxaCodeUnlimitAsync(string accessTokenOrAppId, string filePath, string scene, string page, int width = 430, bool auto_color = false, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = await WxAppApi.GetWxaCodeUnlimitAsync(accessTokenOrAppId, ms, scene, page, width);
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
        /// 获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> GetWxaCodeAsync(string accessTokenOrAppId, Stream stream, string path, int width = 430, bool auto_color = false, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/wxa/getwxacode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                var data = new { path = path, width = width };
                SerializerHelper serializerHelper = new SerializerHelper();
                await Post.DownloadAsync(url, serializerHelper.GetJsonString(data), stream);

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
        public static async Task<WxJsonResult> CreateWxQrCodeAsync(string accessTokenOrAppId, Stream stream, string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxaapp/createwxaqrcode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                var data = new { path = path, width = width };
                SerializerHelper serializerHelper = new SerializerHelper();
                await Post.DownloadAsync(url, serializerHelper.GetJsonString(data), stream);

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


        #endregion
#endif
    }
}