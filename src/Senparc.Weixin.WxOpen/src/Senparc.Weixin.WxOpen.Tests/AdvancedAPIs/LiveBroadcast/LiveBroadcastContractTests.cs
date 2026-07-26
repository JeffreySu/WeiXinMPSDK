using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.LiveBroadcast;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.MessageContexts;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.LiveBroadcast
{
    [TestClass]
    public class LiveBroadcastContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(LiveBroadcastApi.CreateRoom)] = "/wxaapi/broadcast/room/create",
                [nameof(LiveBroadcastApi.GetLiveInfo)] = "/wxa/business/getliveinfo",
                [nameof(LiveBroadcastApi.DeleteRoom)] = "/wxaapi/broadcast/room/deleteroom",
                [nameof(LiveBroadcastApi.ImportGoods)] = "/wxaapi/broadcast/room/addgoods",
                [nameof(LiveBroadcastApi.EditRoom)] = "/wxaapi/broadcast/room/editroom",
                [nameof(LiveBroadcastApi.GetPushUrl)] = "/wxaapi/broadcast/room/getpushurl",
                [nameof(LiveBroadcastApi.GetSharedCode)] = "/wxaapi/broadcast/room/getsharedcode",
                [nameof(LiveBroadcastApi.GetSubAnchor)] = "/wxaapi/broadcast/room/getsubanchor",
                [nameof(LiveBroadcastApi.ModifySubAnchor)] = "/wxaapi/broadcast/room/modifysubanchor",
                [nameof(LiveBroadcastApi.DeleteSubAnchor)] = "/wxaapi/broadcast/room/deletesubanchor",
                [nameof(LiveBroadcastApi.AddSubAnchor)] = "/wxaapi/broadcast/room/addsubanchor",
                [nameof(LiveBroadcastApi.DeleteRoomGoods)] = "/wxaapi/broadcast/goods/deleteInRoom",
                [nameof(LiveBroadcastApi.PushGoods)] = "/wxaapi/broadcast/goods/push",
                [nameof(LiveBroadcastApi.SetGoodsOnSale)] = "/wxaapi/broadcast/goods/onsale",
                [nameof(LiveBroadcastApi.SortRoomGoods)] = "/wxaapi/broadcast/goods/sort",
                [nameof(LiveBroadcastApi.ModifyAssistant)] = "/wxaapi/broadcast/room/modifyassistant",
                [nameof(LiveBroadcastApi.GetAssistantList)] = "/wxaapi/broadcast/room/getassistantlist",
                [nameof(LiveBroadcastApi.RemoveAssistant)] = "/wxaapi/broadcast/room/removeassistant",
                [nameof(LiveBroadcastApi.AddAssistants)] = "/wxaapi/broadcast/room/addassistant",
                [nameof(LiveBroadcastApi.UpdateComment)] = "/wxaapi/broadcast/room/updatecomment",
                [nameof(LiveBroadcastApi.UpdateFeedPublic)] = "/wxaapi/broadcast/room/updatefeedpublic",
                [nameof(LiveBroadcastApi.UpdateCustomerService)] = "/wxaapi/broadcast/room/updatekf",
                [nameof(LiveBroadcastApi.UpdateReplay)] = "/wxaapi/broadcast/room/updatereplay",
                [nameof(LiveBroadcastApi.GetGoodsVideo)] = "/wxaapi/broadcast/goods/getVideo",
                [nameof(LiveBroadcastApi.SetDefaultGoodsKey)] = "/wxaapi/broadcast/goods/setkey",
                [nameof(LiveBroadcastApi.GetDefaultGoodsKey)] = "/wxaapi/broadcast/goods/getkey",
                [nameof(LiveBroadcastApi.AddGoods)] = "/wxaapi/broadcast/goods/add",
                [nameof(LiveBroadcastApi.ResubmitGoodsAudit)] = "/wxaapi/broadcast/goods/audit",
                [nameof(LiveBroadcastApi.GetGoodsWarehouse)] = "/wxa/business/getgoodswarehouse",
                [nameof(LiveBroadcastApi.ResetGoodsAudit)] = "/wxaapi/broadcast/goods/resetaudit",
                [nameof(LiveBroadcastApi.UpdateGoods)] = "/wxaapi/broadcast/goods/update",
                [nameof(LiveBroadcastApi.GetApprovedGoods)] = "/wxaapi/broadcast/goods/getapproved",
                [nameof(LiveBroadcastApi.DeleteGoods)] = "/wxaapi/broadcast/goods/delete",
                [nameof(LiveBroadcastApi.AddRole)] = "/wxaapi/broadcast/role/addrole",
                [nameof(LiveBroadcastApi.DeleteRole)] = "/wxaapi/broadcast/role/deleterole",
                [nameof(LiveBroadcastApi.GetRoleList)] = "/wxaapi/broadcast/role/getrolelist",
                [nameof(LiveBroadcastApi.PushLiveStartMessage)] = "/wxa/business/push_message",
                [nameof(LiveBroadcastApi.GetFollowers)] = "/wxa/business/get_wxa_followers"
            };

        private static readonly ISet<string> GetEndpoints = new HashSet<string>
        {
            nameof(LiveBroadcastApi.GetPushUrl),
            nameof(LiveBroadcastApi.GetSharedCode),
            nameof(LiveBroadcastApi.GetSubAnchor),
            nameof(LiveBroadcastApi.GetAssistantList),
            nameof(LiveBroadcastApi.GetDefaultGoodsKey),
            nameof(LiveBroadcastApi.GetApprovedGoods),
            nameof(LiveBroadcastApi.GetRoleList)
        };

        [TestMethod]
        public void ApiSurfaceContainsThirtyEightSyncAndAsyncEntries()
        {
            var methods = typeof(LiveBroadcastApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name)
                .ToArray();

            Assert.AreEqual(38, OfficialEndpoints.Count);
            foreach (var method in OfficialEndpoints.Keys)
            {
                CollectionAssert.Contains(methods, method);
                CollectionAssert.Contains(methods, method + "Async");
            }

            Assert.AreEqual(76, methods.Length);
        }

        [TestMethod]
        public void EveryPublicEntryUsesItsCaseSensitiveOfficialEndpoint()
        {
            foreach (var pair in OfficialEndpoints)
            {
                var sync = typeof(LiveBroadcastApi).GetMethod(pair.Key, BindingFlags.Public | BindingFlags.Static);
                var async = typeof(LiveBroadcastApi).GetMethod(pair.Key + "Async", BindingFlags.Public | BindingFlags.Static);

                CollectionAssert.Contains(GetStringLiterals(sync).ToArray(), pair.Value, pair.Key);
                CollectionAssert.Contains(GetStringLiterals(async).ToArray(), pair.Value, pair.Key + "Async");
            }
        }

        [TestMethod]
        public void PublicEntriesUseTheDocumentedHttpMethod()
        {
            foreach (var methodName in OfficialEndpoints.Keys)
            {
                AssertTransport(methodName, GetEndpoints.Contains(methodName) ? "SendGet" : "SendPost");
                AssertTransport(methodName + "Async", GetEndpoints.Contains(methodName) ? "SendGetAsync" : "SendPostAsync");
            }
        }

        [TestMethod]
        public void QueryValuesAreEncodedAndUnsetOptionsAreOmitted()
        {
            var queryMethod = typeof(LiveBroadcastApi).GetMethod("Query", BindingFlags.NonPublic | BindingFlags.Static);
            var encoded = (string)queryMethod.Invoke(null, new object[] { "custom_params", "a&b=中文" });
            var omitted = (string)queryMethod.Invoke(null, new object[] { "keyword", null });

            StringAssert.StartsWith(encoded, "&custom_params=");
            Assert.IsFalse(encoded.Contains("a&b="));
            StringAssert.Contains(encoded.ToUpperInvariant(), "%26");
            StringAssert.Contains(encoded.ToUpperInvariant(), "%E4");
            Assert.AreEqual(string.Empty, omitted);
        }

        [TestMethod]
        public void RoomRequestsUseCamelCaseWhileLiveInfoResponsesUseSnakeCase()
        {
            var create = new LiveBroadcastCreateRoomRequest
            {
                name = "测试直播间",
                coverImg = "cover-media",
                startTime = 1700000000,
                endTime = 1700003600,
                anchorName = "主播",
                anchorWechat = "anchor-id",
                shareImg = "share-media",
                feedsImg = "feeds-media",
                type = 1,
                closeLike = 0,
                closeGoods = 0,
                closeComment = 0
            };

            using var document = JsonDocument.Parse(Serialize(create));
            var result = JsonConvert.DeserializeObject<LiveBroadcastGetLiveInfoJsonResult>(
                "{\"errcode\":0,\"total\":1,\"room_info\":[{\"name\":\"测试直播间\"," +
                "\"roomid\":123,\"start_time\":1700000000,\"live_status\":101," +
                "\"goods\":[{\"goods_id\":9,\"cover_img\":\"cover\",\"price\":100}]}]}");

            Assert.AreEqual("cover-media", document.RootElement.GetProperty("coverImg").GetString());
            Assert.AreEqual(1700000000L, document.RootElement.GetProperty("startTime").GetInt64());
            Assert.IsFalse(document.RootElement.TryGetProperty("subAnchorWechat", out _));
            Assert.IsFalse(document.RootElement.TryGetProperty("closeReplay", out _));
            Assert.AreEqual(123L, result.room_info[0].roomid);
            Assert.AreEqual(9L, result.room_info[0].goods[0].goods_id);
        }

        [TestMethod]
        public void RoomOperationsPreserveOfficialIdFieldsAndNumericGoodsOrder()
        {
            var comment = new LiveBroadcastUpdateCommentRequest { id = 123, banComment = 1 };
            var sort = new LiveBroadcastSortGoodsRequest
            {
                roomId = 123,
                goods = new[]
                {
                    new LiveBroadcastGoodsId { goodsId = 9 },
                    new LiveBroadcastGoodsId { goodsId = 7 }
                }
            };

            using var commentDocument = JsonDocument.Parse(Serialize(comment));
            using var sortDocument = JsonDocument.Parse(Serialize(sort));

            Assert.AreEqual(123L, commentDocument.RootElement.GetProperty("id").GetInt64());
            Assert.IsFalse(commentDocument.RootElement.TryGetProperty("roomId", out _),
                "updatecomment 的参数表使用 id，示例中的 roomId 与参数表冲突。");
            Assert.AreEqual(JsonValueKind.Number,
                sortDocument.RootElement.GetProperty("goods")[0].GetProperty("goodsId").ValueKind);
            Assert.AreEqual(7L, sortDocument.RootElement.GetProperty("goods")[1].GetProperty("goodsId").GetInt64());
        }

        [TestMethod]
        public void GoodsContractsCoverNestedInputAndBothOfficialResponseStyles()
        {
            var request = new LiveBroadcastGoodsRequest
            {
                goodsInfo = new LiveBroadcastGoodsInfoRequest
                {
                    coverImgUrl = "cover-media",
                    name = "测试商品",
                    priceType = 2,
                    price = 10.5m,
                    price2 = 20m,
                    url = "pages/goods/detail"
                }
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var warehouse = JsonConvert.DeserializeObject<LiveBroadcastGoodsListJsonResult>(
                "{\"errcode\":0,\"goods\":[{\"goods_id\":123,\"cover_img_url\":\"a\"," +
                "\"price_type\":2,\"audit_status\":2,\"third_party_tag\":0}]}");
            var approved = JsonConvert.DeserializeObject<LiveBroadcastGoodsListJsonResult>(
                "{\"errcode\":0,\"total\":1,\"goods\":[{\"goodsId\":\"456\"," +
                "\"coverImgUrl\":\"b\",\"priceType\":1,\"thirdPartyTag\":1}]}");

            Assert.AreEqual("测试商品", document.RootElement.GetProperty("goodsInfo").GetProperty("name").GetString());
            Assert.IsFalse(document.RootElement.GetProperty("goodsInfo").TryGetProperty("goodsId", out _));
            Assert.AreEqual("123", warehouse.goods[0].goods_id);
            Assert.AreEqual(2, warehouse.goods[0].audit_status);
            Assert.AreEqual("456", approved.goods[0].goodsId);
            Assert.AreEqual("b", approved.goods[0].coverImgUrl);
        }

        [TestMethod]
        public void RoleAndLongTermSubscriptionModelsMapNestedOfficialExamples()
        {
            var push = new LiveBroadcastPushMessageRequest
            {
                room_id = 123,
                user_openid = new[] { "openid-1", "openid-2" }
            };

            using var document = JsonDocument.Parse(Serialize(push));
            var roles = JsonConvert.DeserializeObject<LiveBroadcastRoleListJsonResult>(
                "{\"errcode\":0,\"total\":1,\"list\":[{\"openid\":\"openid-1\"," +
                "\"roleList\":[1,3],\"updateTimestamp\":\"1700000000\"}]}");
            var followers = JsonConvert.DeserializeObject<LiveBroadcastGetFollowersJsonResult>(
                "{\"errcode\":0,\"followers\":[{\"openid\":\"openid-2\"," +
                "\"subscribe_time\":1700000000,\"room_id\":123,\"room_status\":101}]," +
                "\"page_break\":9007199254740991}");

            Assert.AreEqual("openid-2", document.RootElement.GetProperty("user_openid")[1].GetString());
            Assert.AreEqual(3, roles.list[0].roleList[1]);
            Assert.AreEqual("1700000000", roles.list[0].updateTimestamp);
            Assert.AreEqual(123L, followers.followers[0].room_id);
            Assert.AreEqual(9007199254740991L, followers.page_break);
        }

        [TestMethod]
        public void LiveBroadcastEventsMapTheNestedOfficialXmlPayloads()
        {
            const string followXml = "<xml>"
                + "<ToUserName><![CDATA[toUser]]></ToUserName>"
                + "<FromUserName><![CDATA[fromUser]]></FromUserName>"
                + "<CreateTime>1546924844</CreateTime>"
                + "<MsgType><![CDATA[event]]></MsgType>"
                + "<Event><![CDATA[wxalive_follow_notify]]></Event>"
                + "<FollowNotify><room_id>123</room_id><user_openid><![CDATA[openid-1]]></user_openid>"
                + "<time>1546924844</time><live_status>101</live_status>"
                + "<action><![CDATA[add_follow]]></action></FollowNotify>"
                + "</xml>";
            const string pushXml = "<xml>"
                + "<ToUserName><![CDATA[toUser]]></ToUserName>"
                + "<FromUserName><![CDATA[fromUser]]></FromUserName>"
                + "<CreateTime>1606273828</CreateTime>"
                + "<MsgType><![CDATA[event]]></MsgType>"
                + "<Event><![CDATA[wxalive_push_message_notify]]></Event>"
                + "<PushMessageApiNotify><message_id><![CDATA[msg-1]]></message_id><room_id>123</room_id>"
                + "<total_count>6</total_count><success_count>2</success_count>"
                + "<openid_error_count>1</openid_error_count><relation_error_count>1</relation_error_count>"
                + "<user_recv_limit_count>1</user_recv_limit_count><internal_error_count>1</internal_error_count>"
                + "</PushMessageApiNotify></xml>";

            var follow = RequestMessageFactory.GetRequestEntity(new DefaultWxOpenMessageContext(), followXml)
                as RequestMessageEvent_WxAliveFollowNotify;
            var push = RequestMessageFactory.GetRequestEntity(new DefaultWxOpenMessageContext(), pushXml)
                as RequestMessageEvent_WxAlivePushMessageNotify;

            Assert.IsNotNull(follow);
            Assert.AreEqual(Event.wxalive_follow_notify, follow.Event);
            Assert.AreEqual(123L, follow.FollowNotify.room_id);
            Assert.AreEqual("openid-1", follow.FollowNotify.user_openid);
            Assert.AreEqual("add_follow", follow.FollowNotify.action);
            Assert.IsNotNull(push);
            Assert.AreEqual(Event.wxalive_push_message_notify, push.Event);
            Assert.AreEqual("msg-1", push.PushMessageApiNotify.message_id);
            Assert.AreEqual(6, push.PushMessageApiNotify.total_count);
            Assert.AreEqual(2, push.PushMessageApiNotify.success_count);
            Assert.AreEqual(1, push.PushMessageApiNotify.internal_error_count);
        }

        private static void AssertTransport(string methodName, string expectedHelper)
        {
            var method = typeof(LiveBroadcastApi).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            CollectionAssert.Contains(GetCalledMethodNames(method).ToArray(), expectedHelper, methodName);
        }

        private static IEnumerable<string> GetCalledMethodNames(MethodInfo method)
        {
            var bytes = method?.GetMethodBody()?.GetILAsByteArray();
            if (bytes == null)
            {
                yield break;
            }

            for (var index = 0; index <= bytes.Length - 5; index++)
            {
                if (bytes[index] != 0x28 && bytes[index] != 0x6f)
                {
                    continue;
                }

                MethodBase calledMethod;
                try
                {
                    calledMethod = method.Module.ResolveMethod(BitConverter.ToInt32(bytes, index + 1),
                        method.DeclaringType?.GetGenericArguments(), method.GetGenericArguments());
                }
                catch (ArgumentException)
                {
                    continue;
                }

                if (calledMethod != null)
                {
                    yield return calledMethod.Name;
                }
            }
        }

        private static IEnumerable<string> GetStringLiterals(MethodInfo method)
        {
            var bytes = method?.GetMethodBody()?.GetILAsByteArray();
            if (bytes == null)
            {
                yield break;
            }

            for (var index = 0; index <= bytes.Length - 5; index++)
            {
                if (bytes[index] != 0x72)
                {
                    continue;
                }

                string value;
                try
                {
                    value = method.Module.ResolveString(BitConverter.ToInt32(bytes, index + 1));
                }
                catch (ArgumentException)
                {
                    continue;
                }

                yield return value;
            }
        }

        private static string Serialize(object value)
        {
            return SerializerHelper.GetJsonString(value, new JsonSetting(ignoreNulls: true));
        }
    }
}
