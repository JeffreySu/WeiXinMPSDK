using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Express;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs
{
    /// <summary>
    /// 即时配送接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public class ExpressApi
    {
        #region 同步方法
        /// <summary>
        /// 获取已支持的配送公司列表接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetAllImmeDeliveryJsonResult GetAllImmeDelivery(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/delivery/getall?access_token={0}";
                var data = new { };
                return CommonJsonSend.Send<GetAllImmeDeliveryJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 拉取已绑定账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetBindAccountJsonResult GetBindAccount(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/shop/get?access_token={0}";
                var data = new { };
                return CommonJsonSend.Send<GetBindAccountJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 第三方代商户发起绑定配送公司帐号的请求
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="delivery_id">配送公司ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static ExpressJsonResult BindAccount(string accessTokenOrAppId, string delivery_id, int timeOut = Config.TIME_OUT)
        {
            var data = new 
            {
                delivery_id = delivery_id
            };
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/shop/add?access_token={0}";
                return CommonJsonSend.Send<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 第三方代商户发起开通即时配送权限
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static ExpressJsonResult OpenDelivery(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/open?access_token={0}";
                var data = new { };
                return CommonJsonSend.Send<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 预下配送单接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static PreAddOrderJsonResult PreAddOrder(string accessTokenOrAppId, PreAddOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/pre_add?access_token={0}";
                return CommonJsonSend.Send<PreAddOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 预取消配送单接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static PreCancelOrderJsonResult PreCancelOrder(string accessTokenOrAppId, PreCancelOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/precancel?access_token={0}";
                return CommonJsonSend.Send<PreCancelOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 下配送单接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AddOrderJsonResult AddOrder(string accessTokenOrAppId, AddOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/add?access_token={0}";
                return CommonJsonSend.Send<AddOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 取消配送单接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CancelOrderJsonResult CancelOrder(string accessTokenOrAppId, CancelOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/cancel?access_token={0}";
                return CommonJsonSend.Send<CancelOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 重新下单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static ReOrderJsonResult ReOrder(string accessTokenOrAppId, ReOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/readd?access_token={0}";
                return CommonJsonSend.Send<ReOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 拉取配送单信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetOrderJsonResult GetOrder(string accessTokenOrAppId, GetOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/get?access_token={0}";
                return CommonJsonSend.Send<GetOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 可以对待接单状态的订单增加小费。需要注意：订单的小费，以最新一次加小费动作的金额为准，故下一次增加小费额必须大于上一次小费额
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static ExpressJsonResult AddTip(string accessTokenOrAppId, AddTipModel data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/addtips?access_token={0}";
                return CommonJsonSend.Send<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 异常件退回商家商家确认收货接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static ExpressJsonResult AbnormalConfirm(string accessTokenOrAppId, AbnormalConfirmModel data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/confirm_return?access_token={0}";
                return CommonJsonSend.Send<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 模拟配送公司更新配送单状态, 该接口用于测试账户下的单，将请求转发到运力测试环境
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static ExpressJsonResult RealMockUpdateOrder(string accessTokenOrAppId, RealMockUpdateOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/realmock_update_order?access_token={0}";
                return CommonJsonSend.Send<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 模拟配送公司更新配送单状态, 该接口只用于沙盒环境，即订单并没有真实流转到运力方
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static ExpressJsonResult MockUpdateOrder(string accessTokenOrAppId, RealMockUpdateOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/test_update_order?access_token={0}";
                return CommonJsonSend.Send<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 获取已支持的配送公司列表接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetAllImmeDeliveryJsonResult> GetAllImmeDeliveryAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/delivery/getall?access_token={0}";
                var data = new { };
                return await CommonJsonSend.SendAsync<GetAllImmeDeliveryJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 拉取已绑定账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetBindAccountJsonResult> GetBindAccountAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/shop/get?access_token={0}";
                var data = new { };
                return await CommonJsonSend.SendAsync<GetBindAccountJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 第三方代商户发起绑定配送公司帐号的请求
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="delivery_id">配送公司ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<ExpressJsonResult> BindAccountAsync(string accessTokenOrAppId, string delivery_id, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                delivery_id = delivery_id
            };
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/shop/add?access_token={0}";
                return await CommonJsonSend.SendAsync<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 第三方代商户发起开通即时配送权限
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<ExpressJsonResult> OpenDeliveryAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/open?access_token={0}";
                var data = new { };
                return await CommonJsonSend.SendAsync<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 预下配送单接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<PreAddOrderJsonResult> PreAddOrderAsync(string accessTokenOrAppId, PreAddOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/pre_add?access_token={0}";
                return await CommonJsonSend.SendAsync<PreAddOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 预取消配送单接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<PreCancelOrderJsonResult> PreCancelOrderAsync(string accessTokenOrAppId, PreCancelOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/precancel?access_token={0}";
                return await CommonJsonSend.SendAsync<PreCancelOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 下配送单接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<AddOrderJsonResult> AddOrderAsync(string accessTokenOrAppId, AddOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/add?access_token={0}";
                return await CommonJsonSend.SendAsync<AddOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 取消配送单接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CancelOrderJsonResult> CancelOrderAsync(string accessTokenOrAppId, CancelOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/cancel?access_token={0}";
                return await CommonJsonSend.SendAsync<CancelOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 重新下单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<ReOrderJsonResult> ReOrderAsync(string accessTokenOrAppId, ReOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/readd?access_token={0}";
                return await CommonJsonSend.SendAsync<ReOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 拉取配送单信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetOrderJsonResult> GetOrderAsync(string accessTokenOrAppId, GetOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/get?access_token={0}";
                return await CommonJsonSend.SendAsync<GetOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 可以对待接单状态的订单增加小费。需要注意：订单的小费，以最新一次加小费动作的金额为准，故下一次增加小费额必须大于上一次小费额
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<ExpressJsonResult> AddTipAsync(string accessTokenOrAppId, AddTipModel data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/addtips?access_token={0}";
                return await CommonJsonSend.SendAsync<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异常件退回商家商家确认收货接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<ExpressJsonResult> AbnormalConfirmAsync(string accessTokenOrAppId, AbnormalConfirmModel data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/order/confirm_return?access_token={0}";
                return await CommonJsonSend.SendAsync<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 模拟配送公司更新配送单状态, 该接口用于测试账户下的单，将请求转发到运力测试环境
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<ExpressJsonResult> RealMockUpdateOrderAsync(string accessTokenOrAppId, RealMockUpdateOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/realmock_update_order?access_token={0}";
                return await CommonJsonSend.SendAsync<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 模拟配送公司更新配送单状态, 该接口只用于沙盒环境，即订单并没有真实流转到运力方
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<ExpressJsonResult> MockUpdateOrderAsync(string accessTokenOrAppId, RealMockUpdateOrderModel data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/local/business/test_update_order?access_token={0}";
                return await CommonJsonSend.SendAsync<ExpressJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
