using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.CityService;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Student;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.Industry
{
    [TestClass]
    public class IndustryP3ContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> CityServiceEndpoints =
            new Dictionary<string, string>
            {
                [nameof(CityServiceApi.GetServicePath)] = "/cityservice/getservicepath",
                [nameof(CityServiceApi.SendMessageData)] = "/cityservice/sendmsgdata",
                [nameof(CityServiceApi.CheckRealName)] = "/intp/realname/checkrealnameinfo",
                [nameof(CityServiceApi.GetBusinessView)] = "/intp/transportcode/getbusinessview",
                [nameof(CityServiceApi.SendMedicalMessage)] = "/cityservice/sendchannelmsg",
                [nameof(CityServiceApi.GetMedicalRealName)] = "/cityservice/getmedrealname",
                [nameof(CityServiceApi.GetMessageRelation)] = "/cityservice/getmsgrelation",
                [nameof(CityServiceApi.GetHospitalNoticeList)] = "/intp/eldermedical/gethospnoticelist",
                [nameof(CityServiceApi.SetHospitalNoticePreview)] = "/intp/eldermedical/previewhopsnotice",
                [nameof(CityServiceApi.PublishHospitalNotice)] = "/intp/eldermedical/publichopsnotice",
                [nameof(CityServiceApi.SetHospitalNotice)] = "/intp/eldermedical/sethopsnotice"
            };

        [TestMethod]
        public void ApiSurfaceContainsTwelveOfficialSyncAndAsyncEntries()
        {
            var studentMethods = typeof(StudentApi).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var cityMethods = typeof(CityServiceApi).GetMethods(BindingFlags.Public | BindingFlags.Static);

            Assert.AreEqual(2, studentMethods.Length);
            Assert.IsNotNull(typeof(StudentApi).GetMethod(nameof(StudentApi.QuickCheckStudentIdentity)));
            Assert.IsNotNull(typeof(StudentApi).GetMethod(nameof(StudentApi.QuickCheckStudentIdentityAsync)));
            Assert.AreEqual(22, cityMethods.Length);

            foreach (var methodName in CityServiceEndpoints.Keys)
            {
                Assert.IsNotNull(GetPublicMethod(typeof(CityServiceApi), methodName), methodName);
                Assert.IsNotNull(GetPublicMethod(typeof(CityServiceApi), methodName + "Async"), methodName + "Async");
            }

            Assert.IsTrue(cityMethods.All(method => method.GetParameters()[0].Name == "accessToken"),
                "城市服务混合使用公众号和小程序令牌，公开入口必须要求直接传入正确账号的 AccessToken。 ");
        }

        [TestMethod]
        public void EveryPublicEntryUsesItsOfficialEndpoint()
        {
            AssertMethodContainsEndpoint(typeof(StudentApi), nameof(StudentApi.QuickCheckStudentIdentity), "/intp/quickcheckstudentidentity");
            AssertMethodContainsEndpoint(typeof(StudentApi), nameof(StudentApi.QuickCheckStudentIdentityAsync), "/intp/quickcheckstudentidentity");

            foreach (var pair in CityServiceEndpoints)
            {
                AssertMethodContainsEndpoint(typeof(CityServiceApi), pair.Key, pair.Value);
                AssertMethodContainsEndpoint(typeof(CityServiceApi), pair.Key + "Async", pair.Value);
            }
        }

        [TestMethod]
        public void StudentIdentityContractUsesOfficialFields()
        {
            var request = new QuickCheckStudentIdentityRequest
            {
                openid = "openid-1",
                wx_studentcheck_code = "student-code"
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var result = JsonConvert.DeserializeObject<QuickCheckStudentIdentityJsonResult>(
                "{\"errcode\":0,\"bind_status\":3,\"is_student\":true}");

            Assert.AreEqual("openid-1", document.RootElement.GetProperty("openid").GetString());
            Assert.AreEqual("student-code", document.RootElement.GetProperty("wx_studentcheck_code").GetString());
            Assert.AreEqual(3, result.bind_status);
            Assert.IsTrue(result.is_student);
        }

        [TestMethod]
        public void ServicePathKeepsParamsAsJsonStringAndSupportsOfficialTypo()
        {
            var request = new CityServiceGetServicePathRequest
            {
                page_type = 0,
                src_channel = 1,
                service_id = 123,
                @params = "[{\"key\":\"order_id\",\"value\":\"A-1\"}]"
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var result = JsonConvert.DeserializeObject<CityServiceGetServicePathJsonResult>(
                "{\"errcode\":0,\"path\":\"pages/index\",\"bussiness_type\":\"topic\"}");

            Assert.AreEqual(JsonValueKind.String, document.RootElement.GetProperty("params").ValueKind);
            StringAssert.Contains(document.RootElement.GetProperty("params").GetString(), "order_id");
            Assert.IsFalse(document.RootElement.TryGetProperty("need_path_type", out _));
            Assert.IsFalse(document.RootElement.TryGetProperty("city_name", out _));
            Assert.AreEqual("topic", result.bussiness_type);
        }

        [TestMethod]
        public void MessageAndMedicalRequestsPreserveDynamicNestedBusinessData()
        {
            var message = new CityServiceSendMessageDataRequest<IDictionary<string, CityServiceMessageTemplateField>>
            {
                openid = "openid-1",
                biz_template_id = "template-1",
                order_no = "order-1",
                data = new Dictionary<string, CityServiceMessageTemplateField>
                {
                    ["thing1"] = new CityServiceMessageTemplateField { value = "预约成功", color = "#00FF00" }
                }
            };
            var medical = new CityServiceMedicalMessageRequest<CityServiceMedicalBusinessInfo>
            {
                status = 1501001,
                open_id = "openid-2",
                order_id = "order-2",
                msg_id = "message-2",
                app_id = "wx-hospital",
                business_id = 150,
                business_info = new CityServiceMedicalBusinessInfo
                {
                    pat_name = "测试患者",
                    department_name = "内科",
                    redirect_page = new CityServiceMedicalRedirectPage
                    {
                        page_type = "mini_program",
                        app_id = "wx-target",
                        fullpath = "pages/order/detail?id=2"
                    }
                }
            };

            using var messageDocument = JsonDocument.Parse(Serialize(message));
            using var medicalDocument = JsonDocument.Parse(Serialize(medical));

            Assert.AreEqual("预约成功", messageDocument.RootElement.GetProperty("data").GetProperty("thing1").GetProperty("value").GetString());
            Assert.IsFalse(messageDocument.RootElement.TryGetProperty("result_page_style_id", out _));
            Assert.AreEqual(150, medicalDocument.RootElement.GetProperty("business_id").GetInt32());
            Assert.AreEqual("内科", medicalDocument.RootElement.GetProperty("business_info").GetProperty("department_name").GetString());
            Assert.AreEqual("pages/order/detail?id=2", medicalDocument.RootElement.GetProperty("business_info").GetProperty("redirect_page").GetProperty("fullpath").GetString());
        }

        [TestMethod]
        public void ElderMedicalResponsesCoverOfficialAliasAndNoticeShapes()
        {
            var realName = JsonConvert.DeserializeObject<CityServiceGetMedicalRealNameJsonResult>(
                "{\"errcode\":0,\"cipher_real_name\":\"base64\",\"openid_id\":\"openid-1\"}");
            var relation = JsonConvert.DeserializeObject<CityServiceGetMessageRelationJsonResult>(
                "{\"err_code\":0,\"err_msg\":\"ok\",\"is_subscribed\":1}");
            var notices = JsonConvert.DeserializeObject<CityServiceGetHospitalNoticeListJsonResult>(
                "{\"errcode\":0,\"notice_list\":[{\"notice_id\":123,\"content\":\"来院须知\"," +
                "\"status\":\"DRAFT\",\"preview_openid\":[\"openid-2\"]}]}");

            Assert.AreEqual("openid-1", realName.openid_id);
            Assert.AreEqual(0, relation.err_code);
            Assert.AreEqual("ok", relation.err_msg);
            Assert.IsTrue(relation.is_subscribed, "官方示例以 0/1 返回，但参数表将该字段定义为 boolean。");
            Assert.AreEqual(123L, notices.notice_list[0].notice_id);
            Assert.AreEqual("openid-2", notices.notice_list[0].preview_openid[0]);
        }

        private static MethodInfo GetPublicMethod(Type apiType, string methodName)
        {
            return apiType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .SingleOrDefault(method => method.Name == methodName);
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
