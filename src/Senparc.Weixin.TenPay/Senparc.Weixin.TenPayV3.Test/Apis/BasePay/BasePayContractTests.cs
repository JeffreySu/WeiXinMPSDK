using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.TenPayV3.Apis.BasePay;

namespace Senparc.Weixin.TenPayV3.Test.Apis.BasePay
{
    [TestClass]
    public class BasePayContractTests
    {
        [TestMethod]
        public void OrderReturnJsonPromotionDetailAcceptsNumericContributeFields()
        {
            var result = SerializerHelper.GetObject<OrderReturnJson>(
                "{\"promotion_detail\":[{\"coupon_id\":\"coupon-1\",\"scope\":\"GLOBAL\"," +
                "\"type\":\"CASH\",\"amount\":100,\"stock_id\":\"931386\"," +
                "\"wechatpay_contribute\":100,\"merchant_contribute\":0," +
                "\"other_contribute\":0,\"currency\":\"CNY\"}]}"
            );

            Assert.AreEqual("100", result.promotion_detail[0].wechatpay_contribute);
            Assert.AreEqual("0", result.promotion_detail[0].merchant_contribute);
            Assert.AreEqual("0", result.promotion_detail[0].other_contribute);
        }

        [TestMethod]
        public void OrderReturnJsonPromotionDetailAcceptsStringContributeFields()
        {
            var result = SerializerHelper.GetObject<OrderReturnJson>(
                "{\"promotion_detail\":[{\"coupon_id\":\"coupon-1\",\"scope\":\"GLOBAL\"," +
                "\"type\":\"CASH\",\"amount\":100,\"stock_id\":\"931386\"," +
                "\"wechatpay_contribute\":\"100\",\"merchant_contribute\":\"0\"," +
                "\"other_contribute\":\"0\",\"currency\":\"CNY\"}]}"
            );

            Assert.AreEqual("100", result.promotion_detail[0].wechatpay_contribute);
            Assert.AreEqual("0", result.promotion_detail[0].merchant_contribute);
            Assert.AreEqual("0", result.promotion_detail[0].other_contribute);
        }
    }
}
