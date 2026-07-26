/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ProductCardApi.cs
    文件功能描述：ProductCardApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.CommonAPIs;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft
{
    /// <summary>
    /// 商品卡片可插入的文章类型。
    /// </summary>
    public enum ProductCardArticleType
    {
        /// <summary>图片消息文章。</summary>
        newspic,

        /// <summary>图文消息文章。</summary>
        news
    }

    /// <summary>
    /// 商品卡片展示样式。
    /// </summary>
    public enum ProductCardType
    {
        /// <summary>大卡片。</summary>
        Large = 0,

        /// <summary>小卡片。</summary>
        Small = 1,

        /// <summary>文字链接。</summary>
        TextLink = 2,

        /// <summary>横条卡片。</summary>
        Bar = 3
    }

    /// <summary>
    /// ProductCardInfo 接口返回结果。
    /// </summary>
    public class ProductCardInfoJsonResult : WxJsonResult
    {
        /// <summary>商品唯一标识，后续校验或更新商品卡片时使用。</summary>
        public string product_key { get; set; }

        /// <summary>可插入公众号文章正文的商品卡片 DOM 结构。</summary>
        public string DOM { get; set; }
    }

    /// <summary>
    /// 文章商品卡片接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, true)]
    public static class ProductCardApi
    {
        /// <summary>
        /// 获取在文章中插入商品卡片所需的 DOM 结构
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="productId">视频号商品 ID。</param>
        /// <param name="articleType">承载商品卡片的文章类型。</param>
        /// <param name="cardType">商品卡片样式。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ProductCardInfoJsonResult GetProductCardInfo(string accessTokenOrAppId, string productId,
            ProductCardArticleType articleType, ProductCardType cardType, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiMpHost + "/channels/ec/service/product/getcardinfo?access_token={0}";
                var data = new
                {
                    product_id = productId,
                    article_type = articleType.ToString(),
                    card_type = (int)cardType
                };

                return CommonJsonSend.Send<ProductCardInfoJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取在文章中插入商品卡片所需的 DOM 结构
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="productId">视频号商品 ID。</param>
        /// <param name="articleType">承载商品卡片的文章类型。</param>
        /// <param name="cardType">商品卡片样式。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<ProductCardInfoJsonResult> GetProductCardInfoAsync(string accessTokenOrAppId, string productId,
            ProductCardArticleType articleType, ProductCardType cardType, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiMpHost + "/channels/ec/service/product/getcardinfo?access_token={0}";
                var data = new
                {
                    product_id = productId,
                    article_type = articleType.ToString(),
                    card_type = (int)cardType
                };

                return await CommonJsonSend.SendAsync<ProductCardInfoJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
    }
}
