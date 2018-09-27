using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 电子发票接口
    /// </summary>
    public static class InvoiceApi
    {
        #region 同步方法


        #region 非税票据 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=21530623533CgUdj

        /// <summary>
        /// 更新电子票据状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="code"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.UpdateStatus", true)]
        public static WxJsonResult UpdateStatus(string accessTokenOrAppId, string cardId, string code, string reimburseStatus, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/platform/updatestatus?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    code = code,
                    reimburse_status = reimburseStatus
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        #endregion
        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】更新电子票据状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="code"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CardApi.UpdateStatusAsync", true)]
        public static async Task<WxJsonResult> UpdateStatusAsync(string accessTokenOrAppId, string cardId, string code, string reimburseStatus, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/platform/updatestatus?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    code = code,
                    reimburse_status = reimburseStatus
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion
#endif

    }
}
