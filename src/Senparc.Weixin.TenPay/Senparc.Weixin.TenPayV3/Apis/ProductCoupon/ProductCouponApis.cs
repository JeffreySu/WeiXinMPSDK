#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ProductCouponApis.cs
    文件功能描述：微信支付 V3 服务商商品券单券模式接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐商品券单券模式服务端接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Apis.ProductCoupon;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付 V3 普通服务商商品券接口。
    /// <para>覆盖商品券、批次、门店、券 Code、发放、核销、查询、退券、通知配置、图片生成和图片上传。</para>
    /// </summary>
    public class ProductCouponApis
    {
        private const string Root =
            "v3/marketing/partner/product-coupon";
        private const int MaxImageBytes = 2 * 1024 * 1024;
        private readonly TenPayApiRequest _request;

        /// <summary>
        /// 创建商品券接口实例。
        /// </summary>
        /// <param name="setting">微信支付 V3 服务商配置；为空时使用全局默认配置。</param>
        public ProductCouponApis(ISenparcWeixinSettingForTenpayV3 setting = null)
        {
            _request = new TenPayApiRequest(setting);
        }

        /// <summary>
        /// 创建单券模式商品券及首个批次。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781289</para>
        /// </summary>
        /// <param name="data">商品券展示、优惠规则、首批次和品牌信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建后的商品券及首批次详情。</returns>
        public Task<ProductCouponResultJson> CreateProductCouponAsync(
            ProductCouponCreateRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path =
                "v3/marketing/partner/product-coupon/product-coupons";
            return PostAsync<ProductCouponResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 修改商品券展示信息；修改只对后续发放的券生效。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781296</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="data">修改请求单号、展示信息和品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>修改后的商品券详情。</returns>
        public Task<ProductCouponResultJson> UpdateProductCouponAsync(
            string productCouponId, ProductCouponModifyRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = ProductCouponPath(productCouponId);
            return PatchAsync<ProductCouponResultJson>(path, data, timeOut);
        }

        /// <summary>修改商品券展示信息。</summary>
        public Task<ProductCouponResultJson> ModifyProductCouponAsync(
            string productCouponId, ProductCouponModifyRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            return UpdateProductCouponAsync(productCouponId, data, timeOut);
        }

        /// <summary>
        /// 查询商品券详情，不包含商品券批次列表。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781292</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="brandId">微信支付分配的品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商品券详情。</returns>
        public Task<ProductCouponResultJson> QueryProductCouponAsync(
            string productCouponId, string brandId,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildQuery(ProductCouponPath(productCouponId),
                "brand_id", brandId);
            return GetAsync<ProductCouponResultJson>(path, timeOut);
        }

        /// <summary>
        /// 失效商品券及其全部批次。
        /// <para>历史已发放给用户的商品券仍按原规则有效。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781290</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="data">失效请求单号、原因和品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>失效后的商品券详情。</returns>
        public Task<ProductCouponResultJson> DeactivateProductCouponAsync(
            string productCouponId, ProductCouponDeactivateRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = $"{ProductCouponPath(productCouponId)}/deactivate";
            return PostAsync<ProductCouponResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 为已有商品券添加批次。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781304</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="data">创建请求单号、批次配置和品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建后的商品券批次。</returns>
        public Task<ProductCouponStockResultJson> CreateStockAsync(
            string productCouponId, ProductCouponStockCreateRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = $"{ProductCouponPath(productCouponId)}/stocks";
            return PostAsync<ProductCouponStockResultJson>(path, data,
                timeOut);
        }

        /// <summary>为多次优惠商品券添加批次组。</summary>
        public Task<ProductCouponStockBundleResultJson>
            CreateStockBundleAsync(string productCouponId,
                ProductCouponStockBundleCreateRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            return PostAsync<ProductCouponStockBundleResultJson>(
                StockBundlesPath(productCouponId), data, timeOut);
        }

        /// <summary>
        /// 分页查询商品券批次列表。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781553</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="data">品牌、状态、分页和可选批次组筛选条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商品券批次总数、列表和下一页标记。</returns>
        public Task<ProductCouponStockListResultJson> QueryStocksAsync(
            string productCouponId,
            ProductCouponStockListQueryRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildQuery(
                $"{ProductCouponPath(productCouponId)}/stocks",
                "state", data?.state,
                "page_size", data?.page_size?.ToString(
                    CultureInfo.InvariantCulture),
                "page_token", data?.page_token,
                "brand_id", data?.brand_id,
                "stock_bundle_id", data?.stock_bundle_id);
            return GetAsync<ProductCouponStockListResultJson>(path, timeOut);
        }

        /// <summary>
        /// 查询商品券指定批次。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781542</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="stockId">微信支付生成的商品券批次 ID。</param>
        /// <param name="brandId">微信支付分配的品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>指定商品券批次详情。</returns>
        public Task<ProductCouponStockResultJson> QueryStockAsync(
            string productCouponId, string stockId, string brandId,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildQuery(StockPath(productCouponId, stockId),
                "brand_id", brandId);
            return GetAsync<ProductCouponStockResultJson>(path, timeOut);
        }

        /// <summary>
        /// 修改商品券批次展示、通知和门店范围信息。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781556</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="stockId">微信支付生成的商品券批次 ID。</param>
        /// <param name="data">修改请求单号和需要更新的批次字段。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>修改后的商品券批次详情。</returns>
        public Task<ProductCouponStockResultJson> UpdateStockAsync(
            string productCouponId, string stockId,
            ProductCouponStockModifyRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            return PatchAsync<ProductCouponStockResultJson>(
                StockPath(productCouponId, stockId), data, timeOut);
        }

        /// <summary>修改商品券批次展示与通知信息。</summary>
        public Task<ProductCouponStockResultJson> ModifyStockAsync(
            string productCouponId, string stockId,
            ProductCouponStockModifyRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            return UpdateStockAsync(productCouponId, stockId, data,
                timeOut);
        }

        /// <summary>修改多次优惠商品券批次组。</summary>
        public Task<ProductCouponStockBundleResultJson>
            ModifyStockBundleAsync(string productCouponId,
                string stockBundleId,
                ProductCouponStockModifyRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            return PatchAsync<ProductCouponStockBundleResultJson>(
                StockBundlePath(productCouponId, stockBundleId), data,
                timeOut);
        }

        /// <summary>
        /// 修改商品券批次发放预算。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781561</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="stockId">微信支付生成的商品券批次 ID。</param>
        /// <param name="data">预算更新模式、更新前后数量和品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预算更新后的商品券批次详情。</returns>
        public Task<ProductCouponStockResultJson> UpdateStockBudgetAsync(
            string productCouponId, string stockId,
            ProductCouponStockBudgetRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = $"{StockPath(productCouponId, stockId)}/update-budget";
            return PostAsync<ProductCouponStockResultJson>(path, data,
                timeOut);
        }

        /// <summary>修改多次优惠商品券批次组发放次数上限。</summary>
        public Task<ProductCouponStockBundleResultJson>
            UpdateStockBundleBudgetAsync(string productCouponId,
                string stockBundleId,
                ProductCouponStockBudgetRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path = $"{StockBundlePath(productCouponId, stockBundleId)}" +
                       "/update-budget";
            return PostAsync<ProductCouponStockBundleResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 失效指定商品券批次。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781532</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="stockId">微信支付生成的商品券批次 ID。</param>
        /// <param name="data">失效请求单号、原因和品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>失效后的商品券批次详情。</returns>
        public Task<ProductCouponStockResultJson> DeactivateStockAsync(
            string productCouponId, string stockId,
            ProductCouponDeactivateRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = $"{StockPath(productCouponId, stockId)}/deactivate";
            return PostAsync<ProductCouponStockResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 将门店关联到商品券批次。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781302</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="stockId">微信支付生成的商品券批次 ID。</param>
        /// <param name="data">待关联门店和品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>关联成功、失败的门店列表。</returns>
        public Task<ProductCouponStoreOperationResultJson>
            AssociateStoresAsync(string productCouponId, string stockId,
                ProductCouponStoresRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"{StockPath(productCouponId, stockId)}/associate-stores";
            return PostAsync<ProductCouponStoreOperationResultJson>(path,
                data, timeOut);
        }

        /// <summary>批量关联多次优惠批次组可用门店。</summary>
        public Task<ProductCouponStoreOperationResultJson>
            AssociateStockBundleStoresAsync(string productCouponId,
                string stockBundleId, ProductCouponStoresRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"{StockBundlePath(productCouponId, stockBundleId)}" +
                "/associate-stores";
            return PostAsync<ProductCouponStoreOperationResultJson>(path,
                data, timeOut);
        }

        /// <summary>
        /// 分页查询商品券批次已关联门店。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781546</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="stockId">微信支付生成的商品券批次 ID。</param>
        /// <param name="data">品牌和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>关联门店总数、列表和下一页标记。</returns>
        public Task<ProductCouponStoreListResultJson> QueryAssociatedStoresAsync(
            string productCouponId, string stockId,
            ProductCouponStoreListQueryRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildQuery(
                $"{StockPath(productCouponId, stockId)}/associated-stores",
                "page_size", data?.page_size?.ToString(
                    CultureInfo.InvariantCulture),
                "page_token", data?.page_token,
                "brand_id", data?.brand_id);
            return GetAsync<ProductCouponStoreListResultJson>(path, timeOut);
        }

        /// <summary>
        /// 取消商品券批次与门店的关联。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781537</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="stockId">微信支付生成的商品券批次 ID。</param>
        /// <param name="data">待取消关联的门店和品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>取消成功、失败的门店列表。</returns>
        public Task<ProductCouponStoreOperationResultJson>
            DisassociateStoresAsync(string productCouponId, string stockId,
                ProductCouponStoresRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"{StockPath(productCouponId, stockId)}/disassociate-stores";
            return PostAsync<ProductCouponStoreOperationResultJson>(path,
                data, timeOut);
        }

        /// <summary>批量取消多次优惠批次组与门店的关联。</summary>
        public Task<ProductCouponStoreOperationResultJson>
            DisassociateStockBundleStoresAsync(string productCouponId,
                string stockBundleId, ProductCouponStoresRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"{StockBundlePath(productCouponId, stockBundleId)}" +
                "/disassociate-stores";
            return PostAsync<ProductCouponStoreOperationResultJson>(path,
                data, timeOut);
        }

        /// <summary>
        /// 为上传模式的商品券批次预上传券 Code。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781572</para>
        /// </summary>
        /// <param name="productCouponId">微信支付生成的商品券 ID。</param>
        /// <param name="stockId">微信支付生成的商品券批次 ID。</param>
        /// <param name="data">请求单号、券 Code 列表和品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功、失败、已存在和重复的券 Code 列表。</returns>
        public Task<ProductCouponCodeUploadResultJson> UploadCouponCodesAsync(
            string productCouponId, string stockId,
            ProductCouponCodeUploadRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"{StockPath(productCouponId, stockId)}/upload-coupon-codes";
            return PostAsync<ProductCouponCodeUploadResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 向指定用户发放商品券。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781605</para>
        /// </summary>
        /// <param name="openid">用户在请求中 AppID 下的 OpenId。</param>
        /// <param name="data">商品券、批次、AppID、请求单号和品牌信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>用户商品券详情。</returns>
        public Task<ProductCouponUserCouponResultJson> SendCouponAsync(
            string openid,
            ProductCouponSendRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = $"{UserCouponRootPath(openid)}/coupons";
            return PostAsync<ProductCouponUserCouponResultJson>(path, data,
                timeOut);
        }

        /// <summary>向指定用户发放多次优惠商品券组。</summary>
        public Task<ProductCouponUserCouponBundleResultJson>
            SendCouponBundleAsync(string openid,
                ProductCouponSendBundleRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            return PostAsync<ProductCouponUserCouponBundleResultJson>(
                UserCouponBundlesPath(openid), data, timeOut);
        }

        /// <summary>
        /// 确认品牌侧已完成用户商品券发放。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781575</para>
        /// </summary>
        /// <param name="openid">用户在请求中 AppID 下的 OpenId。</param>
        /// <param name="couponCode">用户商品券 Code。</param>
        /// <param name="data">商品券、批次、AppID、请求单号和品牌信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>确认后的用户商品券详情。</returns>
        public Task<ProductCouponUserCouponResultJson> ConfirmCouponAsync(
            string openid, string couponCode,
            ProductCouponConfirmRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = $"{UserCouponPath(openid, couponCode)}/confirm";
            return PostAsync<ProductCouponUserCouponResultJson>(path, data,
                timeOut);
        }

        /// <summary>预发商品券并取得小程序领券组件 Token。</summary>
        public Task<ProductCouponPreSendResultJson> PreSendCouponAsync(
            string openid, ProductCouponPreSendRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = $"{UserCouponRootPath(openid)}/pre-send-coupon";
            return PostAsync<ProductCouponPreSendResultJson>(path, data,
                timeOut);
        }

        /// <summary>预发多次优惠商品券组并取得领券组件 Token。</summary>
        public Task<ProductCouponPreSendResultJson>
            PreSendCouponBundleAsync(string openid,
                ProductCouponPreSendBundleRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"{UserCouponRootPath(openid)}/pre-send-coupon-bundle";
            return PostAsync<ProductCouponPreSendResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 核销用户商品券。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781608</para>
        /// </summary>
        /// <param name="openid">用户在请求中 AppID 下的 OpenId。</param>
        /// <param name="couponCode">用户商品券 Code。</param>
        /// <param name="data">核销时间、请求单号、订单、门店和品牌信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>核销后的用户商品券详情。</returns>
        public Task<ProductCouponUserCouponResultJson> UseCouponAsync(
            string openid, string couponCode, ProductCouponUseRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = $"{UserCouponPath(openid, couponCode)}/use";
            return PostAsync<ProductCouponUserCouponResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 查询用户商品券详情。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781582</para>
        /// </summary>
        /// <param name="openid">用户在请求中 AppID 下的 OpenId。</param>
        /// <param name="couponCode">用户商品券 Code。</param>
        /// <param name="data">商品券、批次、AppID 和品牌查询条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>用户商品券详情。</returns>
        public Task<ProductCouponUserCouponResultJson> QueryUserCouponAsync(
            string openid, string couponCode,
            ProductCouponUserCouponQueryRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildQuery(UserCouponPath(openid, couponCode),
                "product_coupon_id", data?.product_coupon_id,
                "stock_id", data?.stock_id,
                "appid", data?.appid,
                "brand_id", data?.brand_id);
            return GetAsync<ProductCouponUserCouponResultJson>(path,
                timeOut);
        }

        /// <summary>
        /// 按状态分页查询指定用户的商品券列表。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781590</para>
        /// </summary>
        /// <param name="openid">用户在请求中 AppID 下的 OpenId。</param>
        /// <param name="data">商品券、批次、状态、批次组、AppID、品牌和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>用户商品券总数、列表和下一页标记。</returns>
        public Task<ProductCouponUserCouponListResultJson>
            QueryUserCouponsAsync(string openid,
                ProductCouponUserCouponListQueryRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildQuery($"{UserCouponRootPath(openid)}/coupons",
                "product_coupon_id", data?.product_coupon_id,
                "stock_id", data?.stock_id,
                "appid", data?.appid,
                "coupon_state", data?.coupon_state,
                "user_coupon_bundle_id", data?.user_coupon_bundle_id,
                "page_size", data?.page_size?.ToString(
                    CultureInfo.InvariantCulture),
                "page_token", data?.page_token,
                "brand_id", data?.brand_id);
            return GetAsync<ProductCouponUserCouponListResultJson>(path,
                timeOut);
        }

        /// <summary>
        /// 失效指定用户商品券。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781578</para>
        /// </summary>
        /// <param name="openid">用户在请求中 AppID 下的 OpenId。</param>
        /// <param name="couponCode">用户商品券 Code。</param>
        /// <param name="data">商品券、批次、AppID、失效请求单号、原因和品牌信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>失效后的用户商品券详情。</returns>
        public Task<ProductCouponUserCouponResultJson>
            DeactivateUserCouponAsync(
            string openid, string couponCode,
            ProductCouponUserCouponDeactivateRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = $"{UserCouponPath(openid, couponCode)}/deactivate";
            return PostAsync<ProductCouponUserCouponResultJson>(path, data,
                timeOut);
        }

        /// <summary>失效指定用户的整个多次优惠商品券组。</summary>
        public Task<ProductCouponUserCouponBundleResultJson>
            DeactivateUserCouponBundleAsync(string openid,
                string userCouponBundleId,
                ProductCouponUserCouponBundleDeactivateRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"{UserCouponBundlePath(openid, userCouponBundleId)}" +
                "/deactivate";
            return PostAsync<ProductCouponUserCouponBundleResultJson>(path,
                data, timeOut);
        }

        /// <summary>
        /// 退回已核销的用户商品券。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781599</para>
        /// </summary>
        /// <param name="openid">用户在请求中 AppID 下的 OpenId。</param>
        /// <param name="couponCode">用户商品券 Code。</param>
        /// <param name="data">商品券、批次、AppID、退券请求单号和品牌信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>退券后的用户商品券详情。</returns>
        public Task<ProductCouponUserCouponResultJson> ReturnCouponAsync(
            string openid, string couponCode,
            ProductCouponUserCouponReturnRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = $"{UserCouponPath(openid, couponCode)}/return";
            return PostAsync<ProductCouponUserCouponResultJson>(path, data,
                timeOut);
        }

        /// <summary>退回已核销的用户商品券。</summary>
        public Task<ProductCouponUserCouponResultJson>
            ReturnUserCouponAsync(string openid, string couponCode,
                ProductCouponUserCouponReturnRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            return ReturnCouponAsync(openid, couponCode, data, timeOut);
        }

        /// <summary>
        /// 获取商品券事件通知地址。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781284</para>
        /// </summary>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>当前通知地址及最后更新时间。</returns>
        public Task<ProductCouponNotifyConfigResultJson> QueryNotifyConfigAsync(
            int timeOut = Config.TIME_OUT)
        {
            const string path =
                "v3/marketing/partner/product-coupon/notify-configs";
            return GetAsync<ProductCouponNotifyConfigResultJson>(path,
                timeOut);
        }

        /// <summary>
        /// 设置商品券事件通知地址。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781286</para>
        /// </summary>
        /// <param name="data">HTTPS 通知地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置后的通知地址及更新时间。</returns>
        public Task<ProductCouponNotifyConfigResultJson> SetNotifyConfigAsync(
            ProductCouponNotifyConfigRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path =
                "v3/marketing/partner/product-coupon/notify-configs";
            return PostAsync<ProductCouponNotifyConfigResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 提交商品券头图合成或抠图任务。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4017327735</para>
        /// </summary>
        /// <param name="data">品牌、任务 ID、生成类型和生成参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已受理的图片生成任务。</returns>
        public Task<ProductCouponImageGenerationTaskResultJson>
            CreateImageGenerationTaskAsync(
                ProductCouponImageGenerationTaskRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path =
                "v3/marketing/partner/product-coupon/image-generation-tasks";
            return PostAsync<ProductCouponImageGenerationTaskResultJson>(path,
                data, timeOut);
        }

        /// <summary>
        /// 查询商品券图片生成任务执行结果。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4017327739</para>
        /// </summary>
        /// <param name="taskId">服务商提交的图片生成任务 ID。</param>
        /// <param name="brandId">微信支付分配的品牌 ID。</param>
        /// <param name="imageGenerationType">图片生成类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>任务状态和生成图片地址。</returns>
        public Task<ProductCouponImageGenerationTaskResultJson>
            QueryImageGenerationTaskAsync(string taskId, string brandId,
                string imageGenerationType,
                int timeOut = Config.TIME_OUT)
        {
            var path = BuildQuery(
                "v3/marketing/partner/product-coupon/" +
                $"image-generation-tasks/{Escape(taskId)}",
                "brand_id", brandId,
                "image_generation_type", imageGenerationType);
            return GetAsync<ProductCouponImageGenerationTaskResultJson>(path,
                timeOut);
        }

        /// <summary>
        /// 上传商品券图片。
        /// <para>请求使用 <c>meta.filename</c>、<c>meta.sha256</c> 和文件流，支持 JPG、JPEG、BMP、PNG，最大 2 MiB。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4015781275</para>
        /// </summary>
        /// <param name="fileName">带受支持扩展名的图片文件名。</param>
        /// <param name="fileStream">待上传图片流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付生成的图片 URL。</returns>
        public Task<ProductCouponImageUploadResultJson> UploadImageAsync(
            string fileName, Stream fileStream,
            int timeOut = Config.TIME_OUT)
        {
            return UploadImageAsync(fileName, fileStream,
                CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 上传商品券图片，并支持取消。
        /// </summary>
        /// <param name="fileName">带受支持扩展名的图片文件名。</param>
        /// <param name="fileStream">待上传图片流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付生成的图片 URL。</returns>
        public Task<ProductCouponImageUploadResultJson> UploadImageAsync(
            string fileName, Stream fileStream,
            CancellationToken cancellationToken,
            int timeOut = Config.TIME_OUT)
        {
            ValidateImageFileName(fileName);
            const string path =
                "v3/marketing/partner/product-coupon/media/upload-image";
            return _request.RequestMultipartWithMaxSizeAsync<
                ProductCouponImageUploadResultJson>(GetUrl(path), fileName,
                fileStream, cancellationToken, MaxImageBytes, timeOut);
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

        private static string ProductCouponPath(string productCouponId)
        {
            return "v3/marketing/partner/product-coupon/product-coupons/" +
                   Escape(productCouponId);
        }

        private static string StockPath(string productCouponId,
            string stockId)
        {
            return $"{ProductCouponPath(productCouponId)}/stocks/" +
                   Escape(stockId);
        }

        private static string StockBundlesPath(string productCouponId)
        {
            return $"{ProductCouponPath(productCouponId)}/stock-bundles";
        }

        private static string StockBundlePath(string productCouponId,
            string stockBundleId)
        {
            return $"{StockBundlesPath(productCouponId)}/" +
                   Escape(stockBundleId);
        }

        private static string UserCouponRootPath(string openid)
        {
            return "v3/marketing/partner/product-coupon/users/" +
                   Escape(openid);
        }

        private static string UserCouponPath(string openid,
            string couponCode)
        {
            return $"{UserCouponRootPath(openid)}/coupons/" +
                   Escape(couponCode);
        }

        private static string UserCouponBundlesPath(string openid)
        {
            return $"{UserCouponRootPath(openid)}/coupon-bundles";
        }

        private static string UserCouponBundlePath(string openid,
            string userCouponBundleId)
        {
            return $"{UserCouponBundlesPath(openid)}/" +
                   Escape(userCouponBundleId);
        }

        private static string GetUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string BuildQuery(string path, params string[] query)
        {
            var parts = new List<string>();
            for (var index = 0; index + 1 < query.Length; index += 2)
            {
                if (!string.IsNullOrEmpty(query[index + 1]))
                {
                    parts.Add($"{Escape(query[index])}=" +
                              Escape(query[index + 1]));
                }
            }

            return parts.Count == 0
                ? path
                : $"{path}?{string.Join("&", parts)}";
        }

        private static string Escape(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }

        private static void ValidateImageFileName(string fileName)
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
                        "商品券图片仅支持 JPG、JPEG、BMP 或 PNG。",
                        nameof(fileName));
            }
        }
    }
}
