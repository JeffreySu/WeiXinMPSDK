using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Charge
{
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public class ChargeApi
    {
        #region 同步方法
        /// <summary>
        /// 查询购买资源包的用量情况
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="spuId">商品SPU ID</param>
        /// <param name="offset">分页偏移量，从0开始</param>
        /// <param name="limit"每页个数，最大20></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UsageResultJson Usage(string accessTokenOrAppId, string spuId, int offset, int limit, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/wxa/charge/usage/get?access_token={0}&spuId=" + spuId + "&offset=" + offset+ "&limit=" + limit;
                return CommonJsonSend.Send<UsageResultJson>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序某个付费能力的最近用量数据
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="spuId">商品SPU ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetRecentAverageResultJson GetRecentAverage(string accessTokenOrAppId, string spuId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/wxa/charge/usage/get_recent_average?access_token={0}&spuId=" + spuId;
                return CommonJsonSend.Send<GetRecentAverageResultJson>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】查询购买资源包的用量情况
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="spuId">商品SPU ID</param>
        /// <param name="offset">分页偏移量，从0开始</param>
        /// <param name="limit"每页个数，最大20></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UsageResultJson> UsageAsync(string accessTokenOrAppId, string spuId, int offset, int limit, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/wxa/charge/usage/get?access_token={0}&spuId=" + spuId + "&offset=" + offset + "&limit=" + limit;
                return await CommonJsonSend.SendAsync<UsageResultJson>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取小程序某个付费能力的最近用量数据
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="spuId">商品SPU ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetRecentAverageResultJson> GetRecentAverageAsync(string accessTokenOrAppId, string spuId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/wxa/charge/usage/get_recent_average?access_token={0}&spuId=" + spuId;
                return await CommonJsonSend.SendAsync<GetRecentAverageResultJson>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
