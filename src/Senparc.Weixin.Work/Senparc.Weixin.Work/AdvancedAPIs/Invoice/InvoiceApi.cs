#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
 
    文件名：InvoiceApi.cs
    文件功能描述：电子发票接口
    
    
    创建标识：Senparc - 20181009


----------------------------------------------------------------*/


using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 电子发票接口
    /// </summary>
    public static class InvoiceApi
    {
        #region 同步方法

        /// <summary>
        /// 查询报销发票信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="encryptCode"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "InvoiceApi.GetInvoiceInfo", true)]
        public static GetInvoiceInfoResultJson GetInvoiceInfo(string accessTokenOrAppId, string cardId, string encryptCode, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/card/invoice/reimburse/getinvoiceinfo?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    encrypt_code = encryptCode
                };

                return CommonJsonSend.Send<GetInvoiceInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 批量查询报销发票信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="itemList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "InvoiceApi.GetInvoiceListInfo", true)]
        public static GetInvoiceInfoResultJson GetInvoiceListInfo(string accessTokenOrAppId, List<InvoiceItem> itemList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/card/invoice/reimburse/getinvoicebatch?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    item_list = itemList
                };

                return CommonJsonSend.Send<GetInvoiceInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 报销方更新发票状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="encryptCode"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "InvoiceApi.UpdateInvoiceStatus", true)]
        public static WorkJsonResult UpdateInvoiceStatus(string accessTokenOrAppId, string cardId, string encryptCode, string reimburseStatus, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/card/invoice/reimburse/updateinvoicestatus?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    encrypt_code = encryptCode,
                    reimburse_status = reimburseStatus
                };

                return CommonJsonSend.Send<WorkJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 报销方批量更新发票状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="itemList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "InvoiceApi.UpdateInvoiceListStatus", true)]
        public static WorkJsonResult UpdateInvoiceListStatus(string accessTokenOrAppId, string openId, string reimburseStatus, List<InvoiceItem> itemList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/card/invoice/reimburse/updatestatusbatch?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid = openId,
                    reimburse_status = reimburseStatus,
                    invoice_list = itemList
                };

                return CommonJsonSend.Send<WorkJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】查询报销发票信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="encryptCode"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "InvoiceApi.GetInvoiceInfoAsync", true)]
        public static async Task<GetInvoiceInfoResultJson> GetInvoiceInfoAsync(string accessTokenOrAppId, string cardId, string encryptCode, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/card/invoice/reimburse/getinvoiceinfo?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    encrypt_code = encryptCode
                };

                return await CommonJsonSend.SendAsync<GetInvoiceInfoResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】批量查询报销发票信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="itemList"></param>
        /// <param name="encryptCode"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "InvoiceApi.GetInvoiceListInfoAsync", true)]
        public static async Task<GetInvoiceInfoResultJson> GetInvoiceListInfoAsync(string accessTokenOrAppId, List<InvoiceItem> itemList, string encryptCode, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/card/invoice/reimburse/getinvoicebatch?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    item_list = itemList
                };

                return await CommonJsonSend.SendAsync<GetInvoiceInfoResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】报销方更新发票状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="encryptCode"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "InvoiceApi.UpdateInvoiceStatusAsync", true)]
        public static async Task<WorkJsonResult> UpdateInvoiceStatusAsync(string accessTokenOrAppId, string cardId, string encryptCode, string reimburseStatus, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/card/invoice/reimburse/updateinvoicestatus?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    encrypt_code = encryptCode,
                    reimburse_status = reimburseStatus
                };

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】报销方批量更新发票状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="itemList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "InvoiceApi.UpdateInvoiceListStatusAsync", true)]
        public static async Task<WorkJsonResult> UpdateInvoiceListStatusAsync(string accessTokenOrAppId, string openId, string reimburseStatus, List<InvoiceItem> itemList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/card/invoice/reimburse/updatestatusbatch?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid = openId,
                    reimburse_status = reimburseStatus,
                    invoice_list = itemList
                };

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

    }
}
