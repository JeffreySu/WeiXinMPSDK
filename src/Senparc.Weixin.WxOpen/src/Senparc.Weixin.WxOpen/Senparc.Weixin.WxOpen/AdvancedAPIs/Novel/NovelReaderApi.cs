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

    文件名：NovelReaderApi.cs
    文件功能描述：NovelReaderApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Novel
{
    /// <summary>小程序小说阅读器预览和推荐接口。</summary>
    public static partial class NovelApi
    {
        /// <summary>修改小说章节预览字数设置。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">默认预览字数及可选的逐章节覆盖设置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>官方参数表为扁平字段，但请求示例使用 setting.chapter_setting 嵌套结构，本接口遵循示例。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/preview/api_setpreviewsetting.html"/>。</remarks>
        public static WxJsonResult SetPreviewSetting(string accessTokenOrAppId, NovelSetPreviewSettingRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/business/novelreader/setpreviewsetting", request, timeOut);
        }

        /// <summary>异步修改小说章节预览字数设置。</summary>
        /// <inheritdoc cref="SetPreviewSetting"/>
        public static Task<WxJsonResult> SetPreviewSettingAsync(string accessTokenOrAppId, NovelSetPreviewSettingRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/business/novelreader/setpreviewsetting", request, timeOut);
        }

        /// <summary>获取小说章节预览字数设置。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>默认及逐章节预览字数设置。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/preview/api_getpreviewsetting.html"/>。</remarks>
        public static NovelGetPreviewSettingJsonResult GetPreviewSetting(string accessTokenOrAppId, NovelBookIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelGetPreviewSettingJsonResult>(accessTokenOrAppId, "/wxa/business/novelreader/getpreviewsetting", request, timeOut);
        }

        /// <summary>异步获取小说章节预览字数设置。</summary>
        /// <inheritdoc cref="GetPreviewSetting"/>
        public static Task<NovelGetPreviewSettingJsonResult> GetPreviewSettingAsync(string accessTokenOrAppId, NovelBookIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelGetPreviewSettingJsonResult>(accessTokenOrAppId, "/wxa/business/novelreader/getpreviewsetting", request, timeOut);
        }

        /// <summary>设置小说阅读完成后的推荐作品。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">Android 付费或 iOS 免费推荐类型及作品 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/other/api_novelreadersetrecmdnovel.html"/>。</remarks>
        public static WxJsonResult SetRecommendedNovels(string accessTokenOrAppId, NovelSetRecommendedNovelsRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/business/novelreader/setrecmdnovel", request, timeOut);
        }

        /// <summary>异步设置小说阅读完成后的推荐作品。</summary>
        /// <inheritdoc cref="SetRecommendedNovels"/>
        public static Task<WxJsonResult> SetRecommendedNovelsAsync(string accessTokenOrAppId, NovelSetRecommendedNovelsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/business/novelreader/setrecmdnovel", request, timeOut);
        }
    }
}
