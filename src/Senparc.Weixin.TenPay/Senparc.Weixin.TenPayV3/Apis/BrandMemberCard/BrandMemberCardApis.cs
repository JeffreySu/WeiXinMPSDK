#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BrandMemberCardApis.cs
    文件功能描述：微信支付商家名片会员卡接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v2.5.1 补齐会员卡模板创建、查询、修改和作废接口；补齐用户会员卡查询、列表、修改和作废接口；补齐会员预授权、导入确认、动态、积分和图片上传接口

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.BrandMemberCard;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付商家名片会员卡管理接口。
    /// <para>用于管理会员卡模板和用户会员卡，所有请求均使用品牌 API 专用 RSA 鉴权。</para>
    /// </summary>
    public class BrandMemberCardApis
    {
        private readonly TenPayApiRequest _request;

        /// <summary>
        /// 创建商家名片会员卡模板接口实例。
        /// </summary>
        /// <param name="brandApiCredentials">品牌 ID、品牌 API 证书和微信支付公钥凭据。</param>
        public BrandMemberCardApis(
            TenPayBrandApiCredentials brandApiCredentials)
        {
            _request = TenPayApiRequest.CreateForBrand(brandApiCredentials);
        }

        /// <summary>
        /// 创建会员卡模板。
        /// <para>创建成功后返回会员卡模板 ID，当前官方仅支持普通会员卡类型 NORMAL。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582711</para>
        /// </summary>
        /// <param name="data">卡面、Code、有效期、会员入口、开卡信息和通知地址配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建后的完整会员卡模板。</returns>
        public Task<BrandMemberCardResultJson> CreateCardAsync(
            BrandMemberCardCreateRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path = "brand/card-member/cards";
            return PostAsync<BrandMemberCardResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 分页查询品牌下的会员卡模板列表。
        /// <para>可按 CARD_EFFECTIVE 或 CARD_INVALID 状态筛选，offset 从 0 开始，limit 最大为 20。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582712</para>
        /// </summary>
        /// <param name="data">模板状态和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>会员卡模板列表、总数及分页信息。</returns>
        public Task<BrandMemberCardListResultJson> QueryCardsAsync(
            BrandMemberCardListQueryRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildBrandMemberCardQuery("brand/card-member/cards",
                "state", data?.state,
                "offset", data?.offset.ToString(CultureInfo.InvariantCulture),
                "limit", data?.limit.ToString(CultureInfo.InvariantCulture));
            return GetAsync<BrandMemberCardListResultJson>(path, timeOut);
        }

        /// <summary>
        /// 查询指定会员卡模板的完整信息。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582715</para>
        /// </summary>
        /// <param name="cardId">微信支付生成的会员卡模板 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>指定会员卡模板的完整配置和状态。</returns>
        public Task<BrandMemberCardResultJson> QueryCardAsync(string cardId,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/card-member/cards/{EscapeBrandMemberCardValue(cardId)}";
            return GetAsync<BrandMemberCardResultJson>(path, timeOut);
        }

        /// <summary>
        /// 修改指定会员卡模板的信息。
        /// <para>接口仅更新请求中出现的可修改字段，采用 HTTP PATCH。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582722</para>
        /// </summary>
        /// <param name="cardId">微信支付生成的会员卡模板 ID。</param>
        /// <param name="data">需要更新的卡面、有效期、会员入口或开卡信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>修改后的完整会员卡模板。</returns>
        public Task<BrandMemberCardResultJson> UpdateCardAsync(string cardId,
            BrandMemberCardUpdateRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/card-member/cards/{EscapeBrandMemberCardValue(cardId)}";
            return PatchAsync<BrandMemberCardResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 作废指定会员卡模板。
        /// <para>请求不包含业务正文；作废后商家无法再通过任何渠道投放该会员卡。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582724</para>
        /// </summary>
        /// <param name="cardId">微信支付生成的会员卡模板 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>状态变为 CARD_INVALID 的完整会员卡模板。</returns>
        public Task<BrandMemberCardResultJson> InvalidateCardAsync(
            string cardId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/card-member/cards/{EscapeBrandMemberCardValue(cardId)}/invalidate";
            return _request.RequestWithoutBodyAsync<BrandMemberCardResultJson>(
                GetUrl(path), timeOut, ApiRequestMethod.POST);
        }

        /// <summary>
        /// 查询指定用户会员卡的信息。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582716</para>
        /// </summary>
        /// <param name="userCardCode">用户领取会员卡后获得的会员卡 Code。</param>
        /// <param name="data">会员卡模板 ID 和用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>用户会员卡详情。</returns>
        public Task<BrandMemberCardUserCardResultJson> QueryUserCardAsync(
            string userCardCode,
            BrandMemberCardUserCardQueryRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildBrandMemberCardQuery(
                $"brand/card-member/user-cards/{EscapeBrandMemberCardValue(userCardCode)}",
                "card_id", data?.card_id,
                "openid", data?.openid);
            return GetAsync<BrandMemberCardUserCardResultJson>(path, timeOut);
        }

        /// <summary>
        /// 分页查询用户在当前品牌下领取的会员卡。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582718</para>
        /// </summary>
        /// <param name="data">用户 OpenId、卡状态及分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>用户会员卡列表及分页信息。</returns>
        public Task<BrandMemberCardUserCardListResultJson> QueryUserCardsAsync(
            BrandMemberCardUserCardListQueryRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildBrandMemberCardQuery(
                "brand/card-member/user-cards",
                "openid", data?.openid,
                "user_card_state", data?.user_card_state,
                "offset", data?.offset.ToString(CultureInfo.InvariantCulture),
                "limit", data?.limit.ToString(CultureInfo.InvariantCulture));
            return GetAsync<BrandMemberCardUserCardListResultJson>(path,
                timeOut);
        }

        /// <summary>
        /// 修改指定用户会员卡的信息。
        /// <para>可更新卡面、手机号、等级、有效期及开卡信息，采用 HTTP PATCH。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582719</para>
        /// </summary>
        /// <param name="userCardCode">用户领取会员卡后获得的会员卡 Code。</param>
        /// <param name="data">用户会员卡更新数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新后的用户会员卡详情。</returns>
        public Task<BrandMemberCardUserCardResultJson> UpdateUserCardAsync(
            string userCardCode,
            BrandMemberCardUserCardUpdateRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/card-member/user-cards/{EscapeBrandMemberCardValue(userCardCode)}";
            return PatchAsync<BrandMemberCardUserCardResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 作废指定用户会员卡。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582726</para>
        /// </summary>
        /// <param name="userCardCode">用户领取会员卡后获得的会员卡 Code。</param>
        /// <param name="data">会员卡模板 ID、用户 OpenId 和可选作废原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>作废后的用户会员卡详情。</returns>
        public Task<BrandMemberCardUserCardResultJson> InvalidateUserCardAsync(
            string userCardCode,
            BrandMemberCardUserCardInvalidateRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/card-member/user-cards/{EscapeBrandMemberCardValue(userCardCode)}/invalidate";
            return PostAsync<BrandMemberCardUserCardResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 获取拉起品牌会员入会组件所需的预授权 Token。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582733</para>
        /// </summary>
        /// <param name="data">会员卡模板 ID 和用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>一小时内有效的预授权 Token。</returns>
        public Task<BrandMemberCardPreAuthTokenResultJson>
            CreatePreAuthTokenAsync(
                BrandMemberCardPreAuthTokenRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path = "brand/card-member/pre-auth-tokens";
            return PostAsync<BrandMemberCardPreAuthTokenResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 根据 OpenId 导入用户会员卡。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582744</para>
        /// </summary>
        /// <param name="data">用户、会员卡 Code、卡面及开卡信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>导入后的用户会员卡详情。</returns>
        public Task<BrandMemberCardUserCardResultJson>
            ImportUserCardByOpenIdAsync(
                BrandMemberCardUserCardImportRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path =
                "brand/card-member/user-cards/import-by-openid";
            return PostAsync<BrandMemberCardUserCardResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 同步商家侧会员开通结果。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582750</para>
        /// </summary>
        /// <param name="userCardCode">会员卡 Code。</param>
        /// <param name="data">开卡状态、卡面和用户信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>同步后的用户会员卡详情。</returns>
        public Task<BrandMemberCardUserCardResultJson> ConfirmUserCardAsync(
            string userCardCode,
            BrandMemberCardUserCardConfirmRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/card-member/user-cards/{EscapeBrandMemberCardValue(userCardCode)}/confirm";
            return PostAsync<BrandMemberCardUserCardResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 创建用户会员动态信息。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582679</para>
        /// </summary>
        /// <param name="data">会员卡、唯一请求单号及动态 Cell 内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已创建的用户动态信息。</returns>
        public Task<BrandMemberCardUserFeedResultJson> CreateUserFeedAsync(
            BrandMemberCardUserFeedRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path = "brand/card-member/user-feeds";
            return PostAsync<BrandMemberCardUserFeedResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 同步用户会员卡积分余额。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015897319</para>
        /// </summary>
        /// <param name="data">用户会员卡和当前积分余额。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已同步的积分余额。</returns>
        public Task<BrandMemberCardPointBalanceResultJson> SyncUserPointsAsync(
            BrandMemberCardPointBalanceRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path = "brand/card-member/user-points/sync";
            return PostAsync<BrandMemberCardPointBalanceResultJson>(path,
                data, timeOut);
        }

        /// <summary>
        /// 同步用户积分兑券结果。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015897310</para>
        /// </summary>
        /// <param name="data">兑券记录、用户会员卡及允许或拒绝结果。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商品券发放状态和券 Code。</returns>
        public Task<BrandMemberCardPointExchangeResultJson>
            ConfirmPointExchangeCouponAsync(
                BrandMemberCardPointExchangeRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path =
                "brand/card-member/user-points/exchange-coupon/confirm";
            return PostAsync<BrandMemberCardPointExchangeResultJson>(path,
                data, timeOut);
        }

        /// <summary>
        /// 上传商家名片会员图片。
        /// <para>仅支持 JPG、BMP、PNG，文件大小不得超过 2 MiB。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015680641</para>
        /// </summary>
        /// <param name="fileName">图片文件名。</param>
        /// <param name="fileStream">图片流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>永久有效的媒体文件 URL。</returns>
        public Task<BrandMemberCardImageUploadResultJson> UploadMemberImageAsync(
            string fileName, Stream fileStream,
            int timeOut = Config.TIME_OUT)
        {
            return UploadMemberImageAsync(fileName, fileStream,
                CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 上传商家名片会员图片，并支持取消。
        /// </summary>
        /// <param name="fileName">图片文件名。</param>
        /// <param name="fileStream">图片流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>永久有效的媒体文件 URL。</returns>
        public Task<BrandMemberCardImageUploadResultJson> UploadMemberImageAsync(
            string fileName, Stream fileStream,
            CancellationToken cancellationToken,
            int timeOut = Config.TIME_OUT)
        {
            ValidateBrandMemberImageFileName(fileName);
            const string path = "brand/card-member/media/image-upload";
            return _request
                .RequestMultipartWithFilenameAndFileDigestAsync<
                    BrandMemberCardImageUploadResultJson>(GetUrl(path),
                    fileName, fileStream, cancellationToken, timeOut);
        }

        private static void ValidateBrandMemberImageFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("文件名不能为空。",
                    nameof(fileName));
            }

            switch (Path.GetExtension(fileName)?.ToLowerInvariant())
            {
                case ".jpg":
                case ".jpeg":
                case ".bmp":
                case ".png":
                    return;
                default:
                    throw new ArgumentException(
                        "商家名片会员图片仅支持 JPG、JPEG、BMP 或 PNG。",
                        nameof(fileName));
            }
        }

        private Task<T> PostAsync<T>(string path, object data, int timeOut)
            where T : ReturnJsonBase, new()
        {
            return _request.RequestAsync<T>(GetUrl(path), data, timeOut);
        }

        private Task<T> PatchAsync<T>(string path, object data, int timeOut)
            where T : ReturnJsonBase, new()
        {
            return _request.RequestAsync<T>(GetUrl(path), data, timeOut,
                ApiRequestMethod.PATCH);
        }

        private Task<T> GetAsync<T>(string path, int timeOut)
            where T : ReturnJsonBase, new()
        {
            return _request.RequestAsync<T>(GetUrl(path), null, timeOut,
                ApiRequestMethod.GET);
        }

        private static string GetUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string BuildBrandMemberCardQuery(string path,
            params string[] query)
        {
            var parts = new List<string>();
            for (var index = 0; index + 1 < query.Length; index += 2)
            {
                if (string.IsNullOrEmpty(query[index + 1]))
                {
                    continue;
                }

                parts.Add($"{EscapeBrandMemberCardValue(query[index])}=" +
                          $"{EscapeBrandMemberCardValue(query[index + 1])}");
            }

            return parts.Count == 0
                ? path
                : $"{path}?{string.Join("&", parts)}";
        }

        private static string EscapeBrandMemberCardValue(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
