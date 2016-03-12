/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
 
    文件名：TenPayRights.cs
    文件功能描述：微信支付维权接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

/*
    官方API：https://mp.weixin.qq.com/htmledition/res/bussiness-course2/wxm-payment-kf-api.pdf
 */

using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微信支付维权接口，官方API：https://mp.weixin.qq.com/htmledition/res/bussiness-course2/wxm-payment-kf-api.pdf
    /// </summary>
    public static class TenPayRights
    {
        /// <summary>
        /// 标记客户的投诉处理状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">支付该笔订单的用户 ID</param>
        /// <param name="feedBackId">投诉单号</param>
        /// <returns></returns>
        public static WxJsonResult UpDateFeedBack(string accessToken, string openId, string feedBackId)
        {
            var urlFormat = "https://api.weixin.qq.com/payfeedback/update?access_token={0}&openid={1}&feedbackid={2}";
            var url = string.Format(urlFormat, accessToken.AsUrlData(), openId.AsUrlData(), feedBackId.AsUrlData());

            return Get.GetJson<WxJsonResult>(url);
        }
    }
}
