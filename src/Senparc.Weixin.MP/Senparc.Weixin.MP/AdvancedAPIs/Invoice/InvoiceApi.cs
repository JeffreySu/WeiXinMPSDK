#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
 
    文件名：InvoiceApi.cs
    文件功能描述：电子发票接口
    
    
    创建标识：Senparc - 20180927

   
----------------------------------------------------------------*/


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
