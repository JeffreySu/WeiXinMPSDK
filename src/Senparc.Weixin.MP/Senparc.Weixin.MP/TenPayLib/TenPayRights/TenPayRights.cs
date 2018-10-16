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
 
    文件名：TenPayRights.cs
    文件功能描述：微信支付维权接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法
----------------------------------------------------------------*/

/*
    官方API：https://mp.weixin.qq.com/htmledition/res/bussiness-course2/wxm-payment-kf-api.pdf
 */

using System;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微信支付维权接口，官方API：https://mp.weixin.qq.com/htmledition/res/bussiness-course2/wxm-payment-kf-api.pdf
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V2 中的对应方法")]
    public static class TenPayRights
    {
        #region 同步方法


        /// <summary>
        /// 标记客户的投诉处理状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">支付该笔订单的用户 ID</param>
        /// <param name="feedBackId">投诉单号</param>
        /// <returns></returns>
        [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V2 中的对应方法")]
        public static WxJsonResult UpDateFeedBack(string accessToken, string openId, string feedBackId)
        {
            var urlFormat = Config.ApiMpHost + "/payfeedback/update?access_token={0}&openid={1}&feedbackid={2}";
            var url = string.Format(urlFormat, accessToken.AsUrlData(), openId.AsUrlData(), feedBackId.AsUrlData());

            return Get.GetJson<WxJsonResult>(url);
        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法
         
        /// <summary>
        /// 【异步方法】标记客户的投诉处理状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">支付该笔订单的用户 ID</param>
        /// <param name="feedBackId">投诉单号</param>
        /// <returns></returns>
        [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V2 中的对应方法")]
        public static async Task<WxJsonResult> UpDateFeedBackAsync(string accessToken, string openId, string feedBackId)
        {
            var urlFormat = Config.ApiMpHost + "/payfeedback/update?access_token={0}&openid={1}&feedbackid={2}";
            var url = string.Format(urlFormat, accessToken.AsUrlData(), openId.AsUrlData(), feedBackId.AsUrlData());

            return await Get.GetJsonAsync<WxJsonResult>(url);
        }
        #endregion
#endif
    }
}
