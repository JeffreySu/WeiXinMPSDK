using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.CV;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Face;
using Senparc.Weixin.WxOpen.AdvancedAPIs.RedPacketCover;
using Senparc.Weixin.WxOpen.AdvancedAPIs.ServiceMarket;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Soter;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.Industry
{
    [TestClass]
    public class RemainingP3ContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(VisualProcessingApi.AiCrop)] = "/cv/img/aicrop",
                [nameof(VisualProcessingApi.AiCropByFile)] = "/cv/img/aicrop",
                [nameof(VisualProcessingApi.QrCode)] = "/cv/img/qrcode",
                [nameof(VisualProcessingApi.QrCodeByFile)] = "/cv/img/qrcode",
                [nameof(VisualProcessingApi.SuperResolution)] = "/cv/img/superresolution",
                [nameof(VisualProcessingApi.SuperResolutionByFile)] = "/cv/img/superresolution",
                [nameof(VisualProcessingApi.DrivingLicense)] = "/cv/ocr/drivinglicense",
                [nameof(VisualProcessingApi.DrivingLicenseByFile)] = "/cv/ocr/drivinglicense",
                [nameof(RedPacketCoverApi.GetCoverUrl)] = "/redpacketcover/wxapp/cover_url/get_by_token",
                [nameof(ServiceMarketApi.InvokeService)] = "/wxa/servicemarket",
                [nameof(ServiceMarketApi.RetrieveResult)] = "/wxa/servicemarketretrieve",
                [nameof(SoterApi.VerifySignature)] = "/cgi-bin/soter/verify_signature",
                [nameof(FaceApi.GetVerifyId)] = "/cityservice/face/identify/getverifyid",
                [nameof(FaceApi.QueryVerifyInfo)] = "/cityservice/face/identify/queryverifyinfo"
            };

        [TestMethod]
        public void RemainingTenOfficialApisExposeExpectedSyncAndAsyncEntries()
        {
            var visualMethods = PublicMethods(typeof(VisualProcessingApi));
            var redPacketMethods = PublicMethods(typeof(RedPacketCoverApi));
            var serviceMethods = PublicMethods(typeof(ServiceMarketApi));
            var soterMethods = PublicMethods(typeof(SoterApi));
            var faceMethods = PublicMethods(typeof(FaceApi));

            Assert.AreEqual(16, visualMethods.Length, "四个图像接口均应提供 URL/文件、同步/异步入口。");
            Assert.AreEqual(2, redPacketMethods.Length);
            Assert.AreEqual(4, serviceMethods.Length);
            Assert.AreEqual(2, soterMethods.Length);
            Assert.AreEqual(5, faceMethods.Length, "两个人脸核身接口各有同步/异步入口，并提供证件摘要辅助方法。");

            foreach (var pair in OfficialEndpoints)
            {
                var apiType = GetApiType(pair.Key);
                Assert.IsNotNull(GetPublicMethod(apiType, pair.Key), pair.Key);
                Assert.IsNotNull(GetPublicMethod(apiType, pair.Key + "Async"), pair.Key + "Async");
            }

            Assert.IsTrue(soterMethods.All(method => method.GetParameters()[0].Name == "accessToken"));
            Assert.IsTrue(faceMethods.Where(method => method.Name != nameof(FaceApi.CreateCertificateHash))
                .All(method => method.GetParameters()[0].Name == "accessToken"));
        }

        [TestMethod]
        public void RemainingPublicEntriesUseOfficialCaseSensitiveEndpoints()
        {
            foreach (var pair in OfficialEndpoints)
            {
                var apiType = GetApiType(pair.Key);
                AssertMethodContainsEndpoint(apiType, pair.Key, pair.Value);
                AssertMethodContainsEndpoint(apiType, pair.Key + "Async", pair.Value);
            }
        }

        [TestMethod]
        public void VisualProcessingEncodesUrlsAndSeparatesRatiosFromFiles()
        {
            var buildUrl = typeof(VisualProcessingApi).GetMethod("BuildUrl", BindingFlags.NonPublic | BindingFlags.Static);
            var createPostData = typeof(VisualProcessingApi).GetMethod("CreatePostData", BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(buildUrl);
            Assert.IsNotNull(createPostData);

            var url = (string)buildUrl.Invoke(null, new object[] { "/cv/img/aicrop", "token+中文", "https://example.com/a b.jpg?x=1&y=2" });
            var postData = (Dictionary<string, string>)createPostData.Invoke(null, new object[] { "1,1,4,3" });

            StringAssert.Contains(url, "/cv/img/aicrop?access_token=");
            Assert.IsFalse(url.Contains("token+中文"));
            Assert.IsFalse(url.Contains("a b.jpg?x=1&y=2"));
            StringAssert.Contains(url.ToUpperInvariant(), "%E4");
            Assert.AreEqual("1,1,4,3", postData["ratios"]);
            Assert.IsFalse(postData.ContainsKey("img"), "ratios 必须作为普通 multipart 字段发送，不能当作文件路径。");

            foreach (var name in new[] { nameof(VisualProcessingApi.SuperResolution), nameof(VisualProcessingApi.SuperResolutionByFile), nameof(VisualProcessingApi.SuperResolutionAsync), nameof(VisualProcessingApi.SuperResolutionByFileAsync) })
            {
                Assert.IsNotNull(GetPublicMethod(typeof(VisualProcessingApi), name).GetCustomAttribute<ObsoleteAttribute>(), name);
            }
        }

        [TestMethod]
        public void RedPacketAndServiceMarketModelsFollowOfficialPayloads()
        {
            var coverRequest = new RedPacketCoverUrlRequest { openid = "openid-1", ctoken = "cover-token" };
            var serviceRequest = new ServiceMarketInvokeRequest<object>
            {
                service = "wx-service",
                api = "OcrAllInOne",
                data = new { img_url = "https://example.com/a.jpg", ocr_type = 1 },
                client_msg_id = "message-1",
                @async = true
            };

            using var coverDocument = JsonDocument.Parse(Serialize(coverRequest));
            using var serviceDocument = JsonDocument.Parse(Serialize(serviceRequest));
            var coverResult = JsonConvert.DeserializeObject<RedPacketCoverUrlJsonResult>(
                "{\"errcode\":0,\"errmsg\":\"success\",\"data\":{\"url\":\"https://cover.example/claim\"}}");
            var serviceResult = JsonConvert.DeserializeObject<ServiceMarketInvokeJsonResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"data\":\"{\\\"result\\\":true}\",\"request_id\":\"request-1\"}");

            Assert.AreEqual("cover-token", coverDocument.RootElement.GetProperty("ctoken").GetString());
            Assert.AreEqual("OcrAllInOne", serviceDocument.RootElement.GetProperty("api").GetString());
            Assert.AreEqual(JsonValueKind.Object, serviceDocument.RootElement.GetProperty("data").ValueKind,
                "官方参数表称 data 为 string，但 HTTPS 示例发送 JSON 对象，泛型模型应保留对象结构。");
            Assert.IsTrue(serviceDocument.RootElement.GetProperty("async").GetBoolean());
            Assert.AreEqual("https://cover.example/claim", coverResult.data.url);
            Assert.AreEqual("request-1", serviceResult.request_id);
        }

        [TestMethod]
        public void SoterAndFaceModelsPreserveSecurityFieldsAndResultCodes()
        {
            var soter = new SoterVerifySignatureRequest
            {
                openid = "openid-1",
                json_string = "{\"raw\":true}",
                json_signature = "signature-1"
            };
            var face = new FaceGetVerifyIdRequest
            {
                out_seq_no = "sequence-1",
                openid = "openid-1",
                cert_info = new FaceCertificateInfo
                {
                    cert_type = "IDENTITY_CARD",
                    cert_name = "张三",
                    cert_no = "310101199801011234"
                }
            };

            using var soterDocument = JsonDocument.Parse(Serialize(soter));
            using var faceDocument = JsonDocument.Parse(Serialize(face));
            var verifyId = JsonConvert.DeserializeObject<FaceGetVerifyIdJsonResult>(
                "{\"errcode\":0,\"verify_id\":\"verify-id-1\",\"expires_in\":3600}");
            var verifyResult = JsonConvert.DeserializeObject<FaceQueryVerifyInfoJsonResult>(
                "{\"errcode\":0,\"verify_ret\":10000}");

            Assert.AreEqual("signature-1", soterDocument.RootElement.GetProperty("json_signature").GetString());
            Assert.AreEqual("IDENTITY_CARD", faceDocument.RootElement.GetProperty("cert_info").GetProperty("cert_type").GetString());
            Assert.AreEqual("verify-id-1", verifyId.verify_id);
            Assert.AreEqual(3600, verifyId.expires_in);
            Assert.AreEqual(10000, verifyResult.verify_ret);
        }

        [TestMethod]
        public void FaceCertificateHashMatchesOfficialExample()
        {
            var hash = FaceApi.CreateCertificateHash(new FaceCertificateInfo
            {
                cert_type = "IDENTITY_CARD",
                cert_name = "张三",
                cert_no = "310101199801011234"
            });

            Assert.AreEqual("3c241f7ff324977aeb91f173bb2a7b06569e6fd784d5573db34a636d8671108b", hash);
        }

        private static MethodInfo[] PublicMethods(Type apiType)
        {
            return apiType.GetMethods(BindingFlags.Public | BindingFlags.Static);
        }

        private static Type GetApiType(string methodName)
        {
            if (GetPublicMethod(typeof(VisualProcessingApi), methodName) != null) return typeof(VisualProcessingApi);
            if (GetPublicMethod(typeof(RedPacketCoverApi), methodName) != null) return typeof(RedPacketCoverApi);
            if (GetPublicMethod(typeof(ServiceMarketApi), methodName) != null) return typeof(ServiceMarketApi);
            if (GetPublicMethod(typeof(SoterApi), methodName) != null) return typeof(SoterApi);
            return typeof(FaceApi);
        }

        private static MethodInfo GetPublicMethod(Type apiType, string methodName)
        {
            return PublicMethods(apiType).SingleOrDefault(method => method.Name == methodName);
        }

        private static void AssertMethodContainsEndpoint(Type apiType, string methodName, string endpoint)
        {
            var method = GetPublicMethod(apiType, methodName);
            Assert.IsNotNull(method, methodName);
            Assert.IsTrue(GetStringLiterals(method).Any(value => value.Contains(endpoint)), methodName);
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
