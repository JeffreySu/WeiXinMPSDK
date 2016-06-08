using System.Collections.Generic;
using System.IO;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    /// <summary>
    /// 扫一扫商品
    /// </summary>
    public static class ScanProductApi
    {
        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <returns></returns>
        public static MerchantInfoResult GetMerchanstInfo(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/scan/merchantinfo/get?access_token={0}";

                var url = string.Format(urlFormat, accessToken.AsUrlData());
                return HttpUtility.Get.GetJson<MerchantInfoResult>(url);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="product"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static OperateProductResult CreateProduct(string accessTokenOrAppId, ProductModel product, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/scan/product/create?access_token={0}";

                return CommonJsonSend.Send<OperateProductResult>(accessToken, urlFormat, product, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 调用该接口,可对商品的基本信息(base_info)、详情信息(detail_info)、推广服务区(action_info) 和组件区(modul_info)四部分进行独立或整体的更新。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="product"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static OperateProductResult UpdateProduct(string accessTokenOrAppId, ProductModel product, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/scan/product/update?access_token={0}";

                return CommonJsonSend.Send<OperateProductResult>(accessToken, urlFormat, product, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        public static OperateProductResult DeleteProduct(string accessTokenOrAppId, string keyStr, ProductKeystandardOptions keyStandard, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/scan/product/clear?access_token={0}";
                var msgData = new
                {
                    keystandard = keyStandard.ToString().ToLower(),
                    keystr = keyStr,
                };
                return CommonJsonSend.Send<OperateProductResult>(accessToken, urlFormat, msgData, timeOut: timeOut);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 提交审核/取消发布商品
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="keyStr">商品编码</param>
        /// <param name="status">商品目标状态</param>
        /// <param name="keyStandard">商品编码标准</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static PublicProductResult PublicProduct(string accessTokenOrAppId, string keyStr, ProductKeystandardOptions keyStandard, ProductPublicStatus status, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/scan/product/modstatus?access_token={0}";
                var msgData = new
                {
                    keystandard = keyStandard.ToString().ToLower(),
                    keystr = keyStr,
                    status = status.ToString().ToLower(),
                };
                return CommonJsonSend.Send<PublicProductResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 重置测试人员白名单（白名单可以看到未发布的商品）
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openIds"></param>
        /// <param name="userNames"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ResetTestUserWhiteList(string accessTokenOrAppId, string[] openIds, string[] userNames, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/scan/testwhitelist/set?access_token={0}";
                var msgData = new
                {
                    openId = openIds,
                    username = userNames,
                };
                return CommonJsonSend.Send<PublicProductResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取商品二维码
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="keyStr"></param>
        /// <param name="extInfo"></param>
        /// <param name="qrcodeSize"></param>
        /// <param name="keyStandard"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QrCodeResult GetProductQrCode(string accessTokenOrAppId, string keyStr, ProductKeystandardOptions keyStandard, string extInfo = "", int qrcodeSize = 100, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/scan/product/get?access_token={0}";
                var msgData = new
                {
                    keystr = keyStr,
                    keyStandard = keyStandard.ToString().ToLower(),
                    extinfo = extInfo,
                    qrcode_size = qrcodeSize
                };
                return CommonJsonSend.Send<QrCodeResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取指定的商品
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="keyStr"></param>
        /// <param name="keyStandard"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetProductResult GetProduct(string accessTokenOrAppId, string keyStr, ProductKeystandardOptions keyStandard, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/scan/product/get?access_token={0}";
                var msgData = new
                {
                    keystr = keyStr,
                    keyStandard = keyStandard.ToString().ToLower(),
                };
                return CommonJsonSend.Send<GetProductResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="status"></param>
        /// <param name="keystr"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProductListResult GetProductList(string accessTokenOrAppId, int offset, int limit, string status = null, string keystr = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/scan/product/getlist?access_token={0}";
                var msgData = new
                {
                    offset = offset.ToString(),
                    limit = limit.ToString(),
                    status = status,
                    keystr = keystr,
                };
                return CommonJsonSend.Send<ProductListResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 检查ticket参数，获取当前用户openid，商品来源
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="ticket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetWxTicketResult CheckTicket(string accessTokenOrAppId, string ticket, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/scan/scanticket/check?access_token={0}";
                var msgData = new
                {
                    ticket = ticket,
                };
                return CommonJsonSend.Send<GetWxTicketResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }



    }

}
