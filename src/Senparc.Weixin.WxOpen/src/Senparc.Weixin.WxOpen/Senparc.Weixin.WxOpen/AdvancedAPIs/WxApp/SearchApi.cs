/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：SearchApi.cs
    文件功能描述：小程序搜索
    
    
    创建标识：lishewen - 20191221
    
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp
{
    /// <summary>
    /// 小程序搜索
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class SearchApi
    {
        #region 同步方法
        /// <summary>
        /// 小程序开发者可以通过本接口提交小程序页面url及参数信息，让微信可以更及时的收录到小程序的页面信息，开发者提交的页面信息将可能被用于小程序搜索结果展示。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="pages">小程序页面信息列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SubmitPages(string accessTokenOrAppId, List<Page> pages, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/search/wxaapi_submitpages?access_token={0}";
                var postBody = new
                {
                    pages
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 小程序开发者可以通过本接口提交小程序页面url及参数信息，让微信可以更及时的收录到小程序的页面信息，开发者提交的页面信息将可能被用于小程序搜索结果展示。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="pages">小程序页面信息列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SubmitPagesAsync(string accessTokenOrAppId, List<Page> pages, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/search/wxaapi_submitpages?access_token={0}";
                var postBody = new
                {
                    pages
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
