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

    文件名：MiniDramaPlayerApi.cs
    文件功能描述：MiniDramaPlayerApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.MiniDrama
{
    /// <summary>小程序短剧播放器配置与推广接口。</summary>
    public static partial class MiniDramaApi
    {
        /// <summary>设置短剧播放器原始视频或推荐位开关。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">入口类型和开关状态。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>官方首个示例把布尔值写成字符串，参数表和第二个示例均为 boolean，本模型使用 <see cref="bool"/>。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/dramaOthersAPI/api_setplayerdramarecmdswitch.html"/>。</remarks>
        public static WxJsonResult SetPlayerDramaRecommendedSwitch(string accessTokenOrAppId, MiniDramaPlayerSwitchRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxadrama/setplayerdramarecmdswitch", request, timeOut);
        }

        /// <summary>异步设置短剧播放器原始视频或推荐位开关。</summary>
        /// <inheritdoc cref="SetPlayerDramaRecommendedSwitch"/>
        public static Task<WxJsonResult> SetPlayerDramaRecommendedSwitchAsync(string accessTokenOrAppId, MiniDramaPlayerSwitchRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxadrama/setplayerdramarecmdswitch", request, timeOut);
        }

        /// <summary>设置短剧播放器刷剧剧目。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">提审方 AppId、剧目 ID 和剧目名称列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/dramaOthersAPI/api_developersetflushdrama.html"/>。</remarks>
        public static WxJsonResult SetFlushDrama(string accessTokenOrAppId, MiniDramaSetFlushDramaRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxadrama/developersetflushdrama", request, timeOut);
        }

        /// <summary>异步设置短剧播放器刷剧剧目。</summary>
        /// <inheritdoc cref="SetFlushDrama"/>
        public static Task<WxJsonResult> SetFlushDramaAsync(string accessTokenOrAppId, MiniDramaSetFlushDramaRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxadrama/developersetflushdrama", request, timeOut);
        }

        /// <summary>设置短剧播放器推荐剧目。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">推荐入口、当前剧目和推荐剧目列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/dramaOthersAPI/api_developersetrecmddrama.html"/>。</remarks>
        public static WxJsonResult SetRecommendedDrama(string accessTokenOrAppId, MiniDramaSetRecommendedDramaRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxadrama/developersetrecmddrama", request, timeOut);
        }

        /// <summary>异步设置短剧播放器推荐剧目。</summary>
        /// <inheritdoc cref="SetRecommendedDrama"/>
        public static Task<WxJsonResult> SetRecommendedDramaAsync(string accessTokenOrAppId, MiniDramaSetRecommendedDramaRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxadrama/developersetrecmddrama", request, timeOut);
        }

        /// <summary>批量设置短剧上架时间。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">短剧及其上架时间列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/dramaOthersAPI/api_developerpublishdrama.html"/>。</remarks>
        public static WxJsonResult PublishDrama(string accessTokenOrAppId, MiniDramaPublishDramaRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxadrama/developerpublishdrama", request, timeOut);
        }

        /// <summary>异步批量设置短剧上架时间。</summary>
        /// <inheritdoc cref="PublishDrama"/>
        public static Task<WxJsonResult> PublishDramaAsync(string accessTokenOrAppId, MiniDramaPublishDramaRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxadrama/developerpublishdrama", request, timeOut);
        }

        /// <summary>获取当前小程序已设置上架的短剧。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已上架短剧和上架时间。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/dramaOthersAPI/api_developergetpublisheddrama.html"/>。</remarks>
        public static MiniDramaGetPublishedDramaJsonResult GetPublishedDrama(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetPublishedDramaJsonResult>(accessTokenOrAppId, "/wxadrama/developergetpublisheddrama", new { }, timeOut);
        }

        /// <summary>异步获取当前小程序已设置上架的短剧。</summary>
        /// <inheritdoc cref="GetPublishedDrama"/>
        public static Task<MiniDramaGetPublishedDramaJsonResult> GetPublishedDramaAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetPublishedDramaJsonResult>(accessTokenOrAppId, "/wxadrama/developergetpublisheddrama", new { }, timeOut);
        }

        /// <summary>批量设置短剧变现类型。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">短剧、IAA/IAP/IAAP 类型和会员标记列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/dramaOthersAPI/api_developersetiaadrama.html"/>。</remarks>
        public static WxJsonResult SetMonetization(string accessTokenOrAppId, MiniDramaSetMonetizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxadrama/developersetiaadrama", request, timeOut);
        }

        /// <summary>异步批量设置短剧变现类型。</summary>
        /// <inheritdoc cref="SetMonetization"/>
        public static Task<WxJsonResult> SetMonetizationAsync(string accessTokenOrAppId, MiniDramaSetMonetizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxadrama/developersetiaadrama", request, timeOut);
        }

        /// <summary>批量查询短剧变现类型。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待查询短剧列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>变现类型和会员功能标记。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/dramaOthersAPI/api_developergetiaadrama.html"/>。</remarks>
        public static MiniDramaGetMonetizationJsonResult GetMonetization(string accessTokenOrAppId, MiniDramaGetMonetizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetMonetizationJsonResult>(accessTokenOrAppId, "/wxadrama/developergetiaadrama", request, timeOut);
        }

        /// <summary>异步批量查询短剧变现类型。</summary>
        /// <inheritdoc cref="GetMonetization"/>
        public static Task<MiniDramaGetMonetizationJsonResult> GetMonetizationAsync(string accessTokenOrAppId, MiniDramaGetMonetizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetMonetizationJsonResult>(accessTokenOrAppId, "/wxadrama/developergetiaadrama", request, timeOut);
        }

        /// <summary>批量加入、查询或退出短剧合作推广计划。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">操作类型和短剧列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>查询操作时返回的逐剧目计划状态。</returns>
        /// <remarks>调用前需阅读并同意微信小程序平台短剧推广计划合作协议。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/dramaOthersAPI/api_batchprocessdramapromotion.html"/>。</remarks>
        public static MiniDramaPromotionJsonResult BatchProcessPromotion(string accessTokenOrAppId, MiniDramaPromotionRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaPromotionJsonResult>(accessTokenOrAppId, "/wxadrama/batchprocessdramapromotion", request, timeOut);
        }

        /// <summary>异步批量加入、查询或退出短剧合作推广计划。</summary>
        /// <inheritdoc cref="BatchProcessPromotion"/>
        public static Task<MiniDramaPromotionJsonResult> BatchProcessPromotionAsync(string accessTokenOrAppId, MiniDramaPromotionRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaPromotionJsonResult>(accessTokenOrAppId, "/wxadrama/batchprocessdramapromotion", request, timeOut);
        }

        /// <summary>获取当前小程序短剧关联的合作推广活动。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">可选活动 ID 列表；空数组表示全量读取。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>活动 ID、名称、链接及关联剧目。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/dramaOthersAPI/api_getfinderevent.html"/>。</remarks>
        public static MiniDramaGetFinderEventJsonResult GetFinderEvent(string accessTokenOrAppId, MiniDramaGetFinderEventRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetFinderEventJsonResult>(accessTokenOrAppId, "/wxadrama/getfinderevent", request, timeOut);
        }

        /// <summary>异步获取当前小程序短剧关联的合作推广活动。</summary>
        /// <inheritdoc cref="GetFinderEvent"/>
        public static Task<MiniDramaGetFinderEventJsonResult> GetFinderEventAsync(string accessTokenOrAppId, MiniDramaGetFinderEventRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetFinderEventJsonResult>(accessTokenOrAppId, "/wxadrama/getfinderevent", request, timeOut);
        }
    }
}
