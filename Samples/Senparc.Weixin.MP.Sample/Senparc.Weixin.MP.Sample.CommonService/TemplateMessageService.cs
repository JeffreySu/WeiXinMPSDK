using Senparc.CO2NET.Cache;
using Senparc.Weixin.MP.Sample.CommonService.TemplateMessage.WxOpen;
using Senparc.Weixin.TenPay.V3;
using Senparc.Weixin.WxOpen.Containers;
using System;

namespace Senparc.Weixin.MP.Sample.CommonService
{
    public class TemplateMessageService
    {
        public SessionBag RunTemplateTest(string wxOpenAppId, string sessionId, string formId)
        {

            var sessionBag = SessionContainer.GetSession(sessionId);
            var openId = sessionBag != null ? sessionBag.OpenId : "用户未正确登陆";

            string title = null;
            decimal price = 1;//单位：分，实际使用过程中，通过数据库获取订单并读取
            string productName = null;
            string orderNumber = null;

            if (formId.StartsWith("prepay_id="))
            {
                formId = formId.Replace("prepay_id=", "");
                title = "这是来自小程序支付的模板消息（仅测试接收，数据不一定真实）";

                var cacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance();
                var unifiedorderRequestData = cacheStrategy.Get<TenPayV3UnifiedorderRequestData>($"WxOpenUnifiedorderRequestData-{openId}");//获取订单请求信息缓存
                var unifedorderResult = cacheStrategy.Get<UnifiedorderResult>($"WxOpenUnifiedorderResultData-{openId}");//获取订单信息缓存

                if (unifedorderResult != null && formId == unifedorderResult.prepay_id)
                {
                    price = unifiedorderRequestData.TotalFee;
                    productName = unifiedorderRequestData.Body + "/缓存获取 prepay_id 成功";
                    orderNumber = unifiedorderRequestData.OutTradeNo;
                }
                else
                {
                    productName = "缓存获取 prepay_id 失败";
                    orderNumber = "1234567890";
                }
                productName += " | 注意：这条消息是从小程序发起的！仅作为UI上支付成功的演示！不能确定支付真实成功！ | prepay_id：" + unifedorderResult.prepay_id;
            }
            else
            {
                title = "在线购买（仅测试小程序接收模板消息，数据不一定真实）";
                productName = "商品名称-模板消息测试";
                orderNumber = "9876543210";
            }

            var data = new WxOpenTemplateMessage_PaySuccessNotice(title, DateTime.Now, productName, orderNumber, price,
                            "400-031-8816", "https://sdk.senparc.weixin.com");

            Senparc.Weixin.WxOpen.AdvancedAPIs
                .Template.TemplateApi
                .SendTemplateMessage(
                    wxOpenAppId, openId, data.TemplateId, data, formId, "pages/index/index", "图书", "#fff00");

            return sessionBag;

        }
    }
}