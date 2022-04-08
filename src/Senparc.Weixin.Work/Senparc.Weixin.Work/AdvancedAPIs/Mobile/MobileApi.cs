﻿/*----------------------------------------------------------------
    Copyright (C) 2022 Senparc
    
    文件名：MobileApi.cs
    文件功能描述：移动端SDK
    
    
    创建标识：Senparc - 20181009
    
    
----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc#10029
 */

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.Mobile;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 移动端SDK
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class MobileApi
    {
        #region 同步方法


        /// <summary>
        /// 获取电子发票ticket
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetTicketResultJson GetTicket(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/ticket/get?access_token={0}&type=wx_card", accessToken);

                return CommonJsonSend.Send<GetTicketResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 获取应用的jsapi_ticket
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetTicketResultJson GetJsApiTicket(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/ticket/get?access_token={0}&type=agent_config", accessToken);

                return CommonJsonSend.Send<GetTicketResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);

        }

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】获取电子发票ticket
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetTicketResultJson> GetTicketAsync(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/ticket/get?access_token={0}&type=wx_card", accessToken);

                return await CommonJsonSend.SendAsync<GetTicketResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】获取应用的jsapi_ticket
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetTicketResultJson> GetJsApiTicketAsync(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/ticket/get?access_token={0}&type=agent_config", accessToken);

                return await CommonJsonSend.SendAsync<GetTicketResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        #endregion
    }
}
