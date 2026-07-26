/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalCatalogApi.cs
    文件功能描述：客户群转换、商品图册与聊天敏感词接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增客户群转换、商品图册与聊天敏感词接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.External;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>企业微信客户群转换、商品图册与聊天敏感词接口。</summary>
    public static partial class ExternalApi
    {
        /// <summary>将微信客户群 OpenGID 转换为企业微信客户群 ChatID。</summary>
        public static OpenGidToChatIdResult OpenGidToChatId(string accessTokenOrAppKey,
            OpenGidToChatIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<OpenGidToChatIdResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/opengid_to_chatid", request, timeOut);

        /// <summary>异步将微信客户群 OpenGID 转换为企业微信客户群 ChatID。</summary>
        public static Task<OpenGidToChatIdResult> OpenGidToChatIdAsync(string accessTokenOrAppKey,
            OpenGidToChatIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<OpenGidToChatIdResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/opengid_to_chatid", request, timeOut);

        /// <summary>创建商品图册。</summary>
        public static ProductAlbumCreateResult CreateProductAlbum(string accessTokenOrAppKey,
            ProductAlbumCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<ProductAlbumCreateResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/add_product_album", request, timeOut);

        /// <summary>异步创建商品图册。</summary>
        public static Task<ProductAlbumCreateResult> CreateProductAlbumAsync(string accessTokenOrAppKey,
            ProductAlbumCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<ProductAlbumCreateResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/add_product_album", request, timeOut);

        /// <summary>获取商品图册详情。</summary>
        public static ProductAlbumResult GetProductAlbum(string accessTokenOrAppKey,
            ProductAlbumIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<ProductAlbumResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_product_album", request, timeOut);

        /// <summary>异步获取商品图册详情。</summary>
        public static Task<ProductAlbumResult> GetProductAlbumAsync(string accessTokenOrAppKey,
            ProductAlbumIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<ProductAlbumResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_product_album", request, timeOut);

        /// <summary>获取商品图册列表。</summary>
        public static ProductAlbumListResult GetProductAlbumList(string accessTokenOrAppKey,
            ProductAlbumListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<ProductAlbumListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_product_album_list", request, timeOut);

        /// <summary>异步获取商品图册列表。</summary>
        public static Task<ProductAlbumListResult> GetProductAlbumListAsync(string accessTokenOrAppKey,
            ProductAlbumListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<ProductAlbumListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_product_album_list", request, timeOut);

        /// <summary>更新商品图册。</summary>
        public static WorkJsonResult UpdateProductAlbum(string accessTokenOrAppKey,
            ProductAlbumUpdateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/update_product_album", request, timeOut);

        /// <summary>异步更新商品图册。</summary>
        public static Task<WorkJsonResult> UpdateProductAlbumAsync(string accessTokenOrAppKey,
            ProductAlbumUpdateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/update_product_album", request, timeOut);

        /// <summary>删除商品图册。</summary>
        public static WorkJsonResult DeleteProductAlbum(string accessTokenOrAppKey,
            ProductAlbumIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/delete_product_album", request, timeOut);

        /// <summary>异步删除商品图册。</summary>
        public static Task<WorkJsonResult> DeleteProductAlbumAsync(string accessTokenOrAppKey,
            ProductAlbumIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/delete_product_album", request, timeOut);

        /// <summary>创建聊天敏感词规则。</summary>
        public static InterceptRuleCreateResult CreateInterceptRule(string accessTokenOrAppKey,
            InterceptRuleCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<InterceptRuleCreateResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/add_intercept_rule", request, timeOut);

        /// <summary>异步创建聊天敏感词规则。</summary>
        public static Task<InterceptRuleCreateResult> CreateInterceptRuleAsync(string accessTokenOrAppKey,
            InterceptRuleCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<InterceptRuleCreateResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/add_intercept_rule", request, timeOut);

        /// <summary>获取聊天敏感词规则列表。</summary>
        public static InterceptRuleListResult GetInterceptRuleList(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => GetP1<InterceptRuleListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_intercept_rule_list", string.Empty, timeOut);

        /// <summary>异步获取聊天敏感词规则列表。</summary>
        public static Task<InterceptRuleListResult> GetInterceptRuleListAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => GetP1Async<InterceptRuleListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_intercept_rule_list", string.Empty, timeOut);

        /// <summary>获取聊天敏感词规则详情。</summary>
        public static InterceptRuleResult GetInterceptRule(string accessTokenOrAppKey,
            InterceptRuleIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<InterceptRuleResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_intercept_rule", request, timeOut);

        /// <summary>异步获取聊天敏感词规则详情。</summary>
        public static Task<InterceptRuleResult> GetInterceptRuleAsync(string accessTokenOrAppKey,
            InterceptRuleIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<InterceptRuleResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_intercept_rule", request, timeOut);

        /// <summary>更新聊天敏感词规则。</summary>
        public static WorkJsonResult UpdateInterceptRule(string accessTokenOrAppKey,
            InterceptRuleUpdateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/update_intercept_rule", request, timeOut);

        /// <summary>异步更新聊天敏感词规则。</summary>
        public static Task<WorkJsonResult> UpdateInterceptRuleAsync(string accessTokenOrAppKey,
            InterceptRuleUpdateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/update_intercept_rule", request, timeOut);

        /// <summary>删除聊天敏感词规则。</summary>
        public static WorkJsonResult DeleteInterceptRule(string accessTokenOrAppKey,
            InterceptRuleIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/del_intercept_rule", request, timeOut);

        /// <summary>异步删除聊天敏感词规则。</summary>
        public static Task<WorkJsonResult> DeleteInterceptRuleAsync(string accessTokenOrAppKey,
            InterceptRuleIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/del_intercept_rule", request, timeOut);
    }
}
