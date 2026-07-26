using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.TransactionGuarantee;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.TransactionGuarantee
{
    [TestClass]
    public class TransactionGuaranteeContractTests
    {
        [TestMethod]
        public void ApiSurfaceContainsSixteenSyncAndAsyncEntries()
        {
            var methods = typeof(TransactionGuaranteeApi).GetMethods().Select(z => z.Name).ToArray();
            var expected = new[]
            {
                nameof(TransactionGuaranteeApi.GetPenaltyList),
                nameof(TransactionGuaranteeApi.GetGuaranteeStatus),
                nameof(TransactionGuaranteeApi.GetCommentList),
                nameof(TransactionGuaranteeApi.GetCommentReplyList),
                nameof(TransactionGuaranteeApi.GetCommentInfo),
                nameof(TransactionGuaranteeApi.AddReply),
                nameof(TransactionGuaranteeApi.DeleteReply),
                nameof(TransactionGuaranteeApi.AddCommentReply),
                nameof(TransactionGuaranteeApi.DeleteCommentReply),
                nameof(TransactionGuaranteeApi.ResetApiCustomerServiceQuota),
                nameof(TransactionGuaranteeApi.ConfirmCompromise),
                nameof(TransactionGuaranteeApi.RespondComplaint),
                nameof(TransactionGuaranteeApi.SupplyComplaintProof),
                nameof(TransactionGuaranteeApi.SubmitComplaintRefund),
                nameof(TransactionGuaranteeApi.GetComplaintOrderDetail),
                nameof(TransactionGuaranteeApi.SubmitComplaintAppeal)
            };

            foreach (var method in expected)
            {
                CollectionAssert.Contains(methods, method);
                CollectionAssert.Contains(methods, method + "Async");
            }
        }

        [TestMethod]
        public void CommentListQueryUsesOfficialCamelCaseAndOmitsUnsetOptions()
        {
            var request = new TransactionGuaranteeCommentListRequest
            {
                startTime = 1588237130,
                endTime = 1588237131,
                filterType = 1,
                limit = 8
            };
            var method = typeof(TransactionGuaranteeApi).GetMethod("BuildCommentListQuery", BindingFlags.NonPublic | BindingFlags.Static);

            var query = (string)method.Invoke(null, new object[] { request });

            StringAssert.Contains(query, "&startTime=1588237130");
            StringAssert.Contains(query, "&endTime=1588237131");
            StringAssert.Contains(query, "&filterType=1");
            StringAssert.Contains(query, "&limit=8");
            Assert.IsFalse(query.Contains("offset="));
        }

        [TestMethod]
        public void PenaltyListMapsStringIdsAndAlphanumericOrderId()
        {
            const string json = @"{
  ""errcode"": 0,
  ""appealList"": [{
    ""illegalOrderId"": ""12345"",
    ""complaintOrderId"": ""54321"",
    ""illegalWording"": ""质量缺陷"",
    ""status"": 6,
    ""minusScore"": 2,
    ""orderId"": ""payorder@_4200001450"",
    ""illegalTime"": 1656658706,
    ""updateTime"": 1656907435
  }],
  ""currentScore"": 55,
  ""totalNum"": 1
}";

            var result = JsonConvert.DeserializeObject<TransactionGuaranteePenaltyListJsonResult>(json);

            Assert.AreEqual("12345", result.appealList[0].illegalOrderId);
            Assert.AreEqual("54321", result.appealList[0].complaintOrderId);
            Assert.AreEqual("payorder@_4200001450", result.appealList[0].orderId);
            Assert.AreEqual(55, result.currentScore);
        }

        [TestMethod]
        public void CommentListMapsNestedMediaProductAndExtraInfo()
        {
            const string json = @"{
  ""errcode"": 0,
  ""success"": true,
  ""commentList"": [{
    ""commentId"": ""2797755680173"",
    ""amount"": 100,
    ""orderId"": ""payorder@4200"",
    ""createTime"": ""1676351504"",
    ""payTime"": ""1675915718"",
    ""wxPayId"": ""4200001761"",
    ""orderInfo"": { ""busiOrderId"": ""merchant-order"" },
    ""userInfo"": { ""openid"": ""openid-1"", ""headImg"": ""user.png"", ""nickName"": ""用户"" },
    ""bizInfo"": { ""appid"": ""wx123"", ""headImg"": ""biz.png"", ""nickName"": ""商家"" },
    ""score"": 200,
    ""content"": { ""txt"": ""一般"", ""media"": [{ ""img"": ""image.png"", ""thumbImg"": ""thumb.png"" }] },
    ""extInfo"": { ""isAlreadySendTmpl"": false },
    ""productInfo"": { ""productList"": [{ ""name"": ""纸巾"", ""picUrl"": ""product.png"" }] }
  }],
  ""total"": 1,
  ""offset"": 0
}";

            var result = JsonConvert.DeserializeObject<TransactionGuaranteeCommentListJsonResult>(json);
            var comment = result.commentList[0];

            Assert.IsTrue(result.success);
            Assert.AreEqual("merchant-order", comment.orderInfo.busiOrderId);
            Assert.AreEqual("image.png", comment.content.media[0].img);
            Assert.AreEqual("纸巾", comment.productInfo.productList[0].name);
            Assert.IsFalse(comment.extInfo.isAlreadySendTmpl);
        }

        [TestMethod]
        public void CommentInfoMapsProgressAndOldComment()
        {
            const string json = @"{
  ""errcode"": 0,
  ""info"": { ""content"": {
    ""commentId"": ""comment-new"", ""amount"": ""1"", ""score"": ""200"",
    ""content"": { ""txt"": ""新评价"", ""media"": [] }
  }},
  ""processInfo"": { ""commentId"": ""comment-new"", ""actionList"": [
    { ""type"": 1, ""updateTime"": 1669031402 }, { ""type"": 2 }
  ]},
  ""oldComment"": {
    ""commentId"": ""comment-old"", ""createTime"": ""1669030000"", ""score"": 100,
    ""content"": { ""ext"": ""旧评价"", ""media"": [] }
  }
}";

            var result = JsonConvert.DeserializeObject<TransactionGuaranteeCommentInfoJsonResult>(json);

            Assert.AreEqual(1L, result.info.content.amount);
            Assert.AreEqual(200, result.info.content.score);
            Assert.AreEqual(1669031402L, result.processInfo.actionList[0].updateTime);
            Assert.IsNull(result.processInfo.actionList[1].updateTime);
            Assert.AreEqual("旧评价", result.oldComment.content.ext);
        }

        [TestMethod]
        public void ReplyListMapsFirstCommentAndFollowingReplies()
        {
            const string json = @"{
  ""errcode"": 0,
  ""list"": {
    ""reply"": {
      ""commentId"": ""123"", ""replyId"": ""1"",
      ""replyContent"": { ""content"": ""商家评论"" },
      ""replyObject"": { ""nickname"": ""小程序名称"", ""imgUrl"": ""biz.png"" }
    },
    ""commentReplyList"": [{
      ""commentId"": ""123"", ""commentReplyId"": ""2"",
      ""commentReplyContent"": { ""content"": ""用户回复"" },
      ""commentReplyObject"": { ""nickname"": ""用户"", ""imgUrl"": ""user.png"" }
    }]
  }
}";

            var result = JsonConvert.DeserializeObject<TransactionGuaranteeReplyListJsonResult>(json);

            Assert.AreEqual("商家评论", result.list.reply.replyContent.content);
            Assert.AreEqual("用户回复", result.list.commentReplyList[0].commentReplyContent.content);
            Assert.AreEqual("用户", result.list.commentReplyList[0].commentReplyObject.nickname);
        }

        [TestMethod]
        public void OptionalComplaintFieldsAreOmittedUntilReturnFlowRequiresThem()
        {
            var ordinary = new TransactionGuaranteeRefundProofRequest
            {
                complaintOrderId = 123456,
                content = "已完成退款"
            };
            var returned = new TransactionGuaranteeRefundProofRequest
            {
                complaintOrderId = 123456,
                mediaIdList = new List<string> { "media-1" },
                acceptReturn = 1,
                returnId = 987654
            };

            using var ordinaryDocument = JsonDocument.Parse(Serialize(ordinary));
            using var returnedDocument = JsonDocument.Parse(Serialize(returned));

            Assert.IsFalse(ordinaryDocument.RootElement.TryGetProperty("acceptReturn", out _));
            Assert.IsFalse(ordinaryDocument.RootElement.TryGetProperty("returnId", out _));
            Assert.AreEqual(1, returnedDocument.RootElement.GetProperty("acceptReturn").GetInt32());
            Assert.AreEqual(987654L, returnedDocument.RootElement.GetProperty("returnId").GetInt64());
            Assert.AreEqual("media-1", returnedDocument.RootElement.GetProperty("mediaIdList")[0].GetString());
        }

        [TestMethod]
        public void ComplaintDetailMapsProgressArrayAndNumericPhonesAsStrings()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""complaintOrder"": {
    ""complaintOrderId"": ""complaint-1"", ""openId"": ""openid-1"",
    ""createTime"": 123124124, ""phoneNumber"": 15622222222,
    ""type"": 611, ""status"": 206,
    ""customerMaterial"": { ""content"": ""未发货"", ""mediaIdList"": [""proof.png""] },
    ""orderId"": ""pay-order"", ""outTradeNo"": ""merchant-order"",
    ""productName"": ""商品"", ""payTime"": 123123, ""totalCost"": 1213,
    ""expireTime"": 1231231, ""appealState"": 401
  },
  ""item"": [{
    ""itemType"": 31, ""time"": 1233234234, ""phoneNumber"": 15622222222,
    ""content"": ""待处理"", ""mediaIdList"": [], ""blameResult"": 0, ""appealItemType"": 401
  }],
  ""returnBill"": {
    ""returnId"": ""return-1"", ""waybillId"": ""waybill-1"",
    ""deliveryName"": ""顺丰"", ""orderStatus"": 4
  }
}";

            var result = JsonConvert.DeserializeObject<TransactionGuaranteeComplaintDetailJsonResult>(json);

            Assert.AreEqual("openid-1", result.complaintOrder.openid);
            Assert.AreEqual("15622222222", result.complaintOrder.phoneNumber);
            Assert.AreEqual("1213", result.complaintOrder.totalCost);
            Assert.AreEqual(31, result.item[0].itemType);
            Assert.AreEqual("15622222222", result.item[0].phoneNumber);
            Assert.AreEqual("顺丰", result.returnBill.deliveryName);
        }

        [TestMethod]
        public void ComplaintRequestsPreserveOfficialCamelCaseFields()
        {
            var request = new TransactionGuaranteeRespondComplaintRequest
            {
                complaintOrderId = 123456,
                bussiHandle = 1,
                content = "同意和解",
                mediaIdList = new List<string> { "media-1" }
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var root = document.RootElement;

            Assert.AreEqual(123456L, root.GetProperty("complaintOrderId").GetInt64());
            Assert.AreEqual(1, root.GetProperty("bussiHandle").GetInt32());
            Assert.AreEqual("media-1", root.GetProperty("mediaIdList")[0].GetString());
        }

        private static string Serialize(object value)
        {
            return SerializerHelper.GetJsonString(value, new JsonSetting(ignoreNulls: true));
        }
    }
}
