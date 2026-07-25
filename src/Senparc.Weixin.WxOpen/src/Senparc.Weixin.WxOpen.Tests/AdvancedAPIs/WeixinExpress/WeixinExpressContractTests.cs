using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.WeixinExpress
{
    [TestClass]
    public class WeixinExpressContractTests
    {
        [TestMethod]
        public void ApiSurfaceContainsCoreAndReturnSyncAndAsyncEntries()
        {
            var methods = typeof(WeixinExpressApi).GetMethods().Select(z => z.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.QueryTrace));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.QueryTraceAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.GetDeliveryList));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.GetDeliveryListAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.TraceWaybill));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.TraceWaybillAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.UpdateWaybillGoods));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.UpdateWaybillGoodsAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.UpdateFollowWaybillGoods));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.UpdateFollowWaybillGoodsAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.QueryFollowTrace));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.QueryFollowTraceAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.FollowWaybill));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.FollowWaybillAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.UnbindReturnId));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.UnbindReturnIdAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.GetReturnId));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.GetReturnIdAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.AddReturnId));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.AddReturnIdAsync));
        }

        [TestMethod]
        public void TraceWaybillSerializesOfficialNestedFieldsAndOmitsNulls()
        {
            var request = new WeixinExpressTraceWaybillRequest
            {
                openid = "openid",
                receiver_phone = "13800000000",
                waybill_id = "waybill-id",
                goods_info = new WeixinExpressGoodsInfo
                {
                    detail_list = new List<WeixinExpressGoodsItem>
                    {
                        new WeixinExpressGoodsItem
                        {
                            goods_name = "测试商品",
                            goods_img_url = "https://example.com/goods.png"
                        }
                    }
                },
                trans_id = "4200000000000000000",
                order_detail_path = "pages/order/detail"
            };

            var json = SerializerHelper.GetJsonString(request, new JsonSetting(ignoreNulls: true));
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            Assert.AreEqual("waybill-id", root.GetProperty("waybill_id").GetString());
            Assert.AreEqual("测试商品", root.GetProperty("goods_info").GetProperty("detail_list")[0].GetProperty("goods_name").GetString());
            Assert.IsFalse(root.TryGetProperty("sender_phone", out _));
            Assert.IsFalse(root.TryGetProperty("delivery_id", out _));
            Assert.IsFalse(root.GetProperty("goods_info").GetProperty("detail_list")[0].TryGetProperty("goods_desc", out _));
        }

        [TestMethod]
        public void UpdateGoodsPreservesOpenIdFromOfficialExample()
        {
            var request = new WeixinExpressUpdateGoodsRequest
            {
                waybill_token = "waybill-token",
                openid = "openid",
                goods_info = new WeixinExpressGoodsInfo
                {
                    detail_list = new List<WeixinExpressGoodsItem>
                    {
                        new WeixinExpressGoodsItem { goods_name = "更新商品", goods_img_url = "https://example.com/new.png" }
                    }
                }
            };

            var json = SerializerHelper.GetJsonString(request, new JsonSetting(ignoreNulls: true));
            using var document = JsonDocument.Parse(json);

            Assert.AreEqual("waybill-token", document.RootElement.GetProperty("waybill_token").GetString());
            Assert.AreEqual("openid", document.RootElement.GetProperty("openid").GetString());
        }

        [TestMethod]
        public void QueryTraceResponseMapsNestedWaybillShopAndDeliveryFields()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""waybill_info"": { ""status"": 2, ""waybill_id"": ""waybill-id"" },
  ""shop_info"": {
    ""goods_info"": {
      ""detail_list"": [{ ""goods_name"": ""测试商品"", ""goods_img_url"": ""https://example.com/goods.png"" }]
    }
  },
  ""delivery_info"": { ""delivery_id"": ""SF"", ""delivery_name"": ""顺丰"" }
}";

            var result = JsonConvert.DeserializeObject<WeixinExpressQueryTraceJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.waybill_info.status);
            Assert.AreEqual("waybill-id", result.waybill_info.waybill_id);
            Assert.AreEqual("测试商品", result.shop_info.goods_info.detail_list[0].goods_name);
            Assert.AreEqual("SF", result.delivery_info.delivery_id);
        }

        [TestMethod]
        public void DeliveryListResponseMapsSharedCatalogEndpoint()
        {
            const string json = @"{
  ""errcode"": 0,
  ""delivery_list"": [{ ""delivery_id"": ""YD"", ""delivery_name"": ""韵达速递"" }],
  ""count"": 1
}";

            var result = JsonConvert.DeserializeObject<WeixinExpressDeliveryListJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.count);
            Assert.AreEqual("YD", result.delivery_list[0].delivery_id);
        }

        [TestMethod]
        public void AddReturnIdSerializesAddressesGoodsAndRequiredPaymentReference()
        {
            var request = new WeixinExpressAddReturnIdRequest
            {
                shop_order_id = "return-order-id",
                biz_addr = new WeixinExpressReturnAddress
                {
                    name = "商家",
                    mobile = "13800000000",
                    country = "中国",
                    province = "江苏省",
                    city = "苏州市",
                    area = "工业园区",
                    address = "测试路 1 号"
                },
                openid = "openid",
                order_path = "pages/return/detail",
                goods_list = new List<WeixinExpressReturnGoodsItem>
                {
                    new WeixinExpressReturnGoodsItem { name = "退货商品", url = "https://example.com/return.png" }
                },
                order_price = 99.5m,
                wx_pay_id = "4200000000000000000"
            };

            var json = SerializerHelper.GetJsonString(request, new JsonSetting(ignoreNulls: true));
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            Assert.AreEqual("return-order-id", root.GetProperty("shop_order_id").GetString());
            Assert.AreEqual("苏州市", root.GetProperty("biz_addr").GetProperty("city").GetString());
            Assert.AreEqual("退货商品", root.GetProperty("goods_list")[0].GetProperty("name").GetString());
            Assert.AreEqual("4200000000000000000", root.GetProperty("wx_pay_id").GetString());
            Assert.IsFalse(root.TryGetProperty("user_addr", out _));
        }

        [TestMethod]
        public void GetReturnIdMapsStringStatusAndLogisticsFields()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": ""OK"",
  ""status"": ""1"",
  ""waybill_id"": ""JD123"",
  ""order_status"": 2,
  ""delivery_id"": ""JD"",
  ""delivery_name"": ""京东物流""
}";

            var result = JsonConvert.DeserializeObject<WeixinExpressGetReturnIdJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.status);
            Assert.AreEqual(2, result.order_status);
            Assert.AreEqual("JD", result.delivery_id);
        }
    }
}
