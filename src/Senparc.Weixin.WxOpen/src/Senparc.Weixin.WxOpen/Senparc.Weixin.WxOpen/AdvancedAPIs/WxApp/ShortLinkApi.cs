/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：ShortLinkApi.cs
    文件功能描述：小程序 Short Link
    官方文档：https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/short-link/shortlink.generate.html
    
    
    创建标识：Senparc - 20210930
    
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp
{
    /// <summary>
    /// 小程序 Short Link
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class ShortLinkApi
    {
        #region 同步方法
        /// <summary>
        /// 获取小程序 Short Link，适用于微信内拉起小程序的业务场景。目前只开放给电商类目(具体包含以下一级类目：电商平台、商家自营、跨境电商)。通过该接口，可以选择生成到期失效和永久有效的小程序短链，详见<see  href="https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/shortlink.html">获取 Short Link</see>。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="pageUrl">（必须）通过 Short Link 进入的小程序页面路径，必须是已经发布的小程序存在的页面，可携带 query，最大1024个字符</param>
        /// <param name="pageTitle">（必须）页面标题，不能包含违法信息，超过20字符会用... 截断代替</param>
        /// <param name="isPermanent">生成的 Short Link 类型，短期有效：false，永久有效：true</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ShortLink_GenerateResult Generate(string accessTokenOrAppId, string pageUrl, string pageTitle, bool isPermanent = false, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/genwxashortlink?access_token={0}";
                var postBody = new
                {
                    page_url = pageUrl,
                    page_title = pageTitle,
                    is_permanent = isPermanent,
                };
                return CommonJsonSend.Send<ShortLink_GenerateResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 获取小程序 Short Link，适用于微信内拉起小程序的业务场景。目前只开放给电商类目(具体包含以下一级类目：电商平台、商家自营、跨境电商)。通过该接口，可以选择生成到期失效和永久有效的小程序短链，详见<see  href="https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/shortlink.html">获取 Short Link</see>。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="pageUrl">（必须）通过 Short Link 进入的小程序页面路径，必须是已经发布的小程序存在的页面，可携带 query，最大1024个字符</param>
        /// <param name="pageTitle">（必须）页面标题，不能包含违法信息，超过20字符会用... 截断代替</param>
        /// <param name="isPermanent">生成的 Short Link 类型，短期有效：false，永久有效：true</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ShortLink_GenerateResult> GenerateAsync(string accessTokenOrAppId, string pageUrl, string pageTitle, bool isPermanent = false, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/genwxashortlink?access_token={0}";
                var postBody = new
                {
                    page_url = pageUrl,
                    page_title = pageTitle,
                    is_permanent = isPermanent,
                };
                return await CommonJsonSend.SendAsync<ShortLink_GenerateResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
