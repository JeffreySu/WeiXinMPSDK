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

    文件名：WeixinExpressReturnApi.cs
    文件功能描述：WeixinExpressReturnApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress
{
    /// <summary>
    /// 微信物流退货组件接口。
    /// </summary>
    public static partial class WeixinExpressApi
    {
        /// <summary>
        /// 解绑商家退货单与微信退货 ID。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待解绑的退货 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>解绑结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-return/api_unbindreturnid"/>；支持第三方平台代商家调用。</remarks>
        public static WxJsonResult UnbindReturnId(string accessTokenOrAppId, WeixinExpressReturnIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/no_worry_return/unbind", request, timeOut);
        }

        /// <summary>
        /// 查询退货 ID 的填写状态和退货物流。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待查询的退货 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>退货方式、运单和物流状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-return/api_getreturnid"/>；支持第三方平台代商家调用。</remarks>
        public static WeixinExpressGetReturnIdJsonResult GetReturnId(string accessTokenOrAppId, WeixinExpressReturnIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressGetReturnIdJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/no_worry_return/get", request, timeOut);
        }

        /// <summary>
        /// 为商家退货单创建微信退货 ID。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">退货编号、地址、用户、商品、价格和投保支付单。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信生成的退货 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-return/api_addreturnid"/>；支持第三方平台代商家调用。</remarks>
        public static WeixinExpressAddReturnIdJsonResult AddReturnId(string accessTokenOrAppId, WeixinExpressAddReturnIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressAddReturnIdJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/no_worry_return/add", request, timeOut);
        }

        /// <summary>
        /// 异步解绑商家退货单与微信退货 ID。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待解绑的退货 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>解绑结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-return/api_unbindreturnid"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WxJsonResult> UnbindReturnIdAsync(string accessTokenOrAppId, WeixinExpressReturnIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/no_worry_return/unbind", request, timeOut);
        }

        /// <summary>
        /// 异步查询退货 ID 的填写状态和退货物流。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待查询的退货 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>退货方式、运单和物流状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-return/api_getreturnid"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WeixinExpressGetReturnIdJsonResult> GetReturnIdAsync(string accessTokenOrAppId, WeixinExpressReturnIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressGetReturnIdJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/no_worry_return/get", request, timeOut);
        }

        /// <summary>
        /// 异步为商家退货单创建微信退货 ID。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">退货编号、地址、用户、商品、价格和投保支付单。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信生成的退货 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/express-return/api_addreturnid"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WeixinExpressAddReturnIdJsonResult> AddReturnIdAsync(string accessTokenOrAppId, WeixinExpressAddReturnIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressAddReturnIdJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/no_worry_return/add", request, timeOut);
        }
    }
}
