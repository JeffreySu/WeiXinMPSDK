using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs.CV.Image;
using Senparc.Weixin.MP.AdvancedAPIs.CV.OCR;
using Senparc.Weixin.MP.AdvancedAPIs.MedicalAssistant;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs.CV
{
    [TestClass]
    public class P2ContractTests
    {
        [TestMethod]
        public void P2ApiSurfaceContainsUrlFileSyncAndAsyncEntries()
        {
            var ocrMethods = typeof(OCRApi).GetMethods().Select(z => z.Name).ToArray();
            CollectionAssert.Contains(ocrMethods, nameof(OCRApi.DrivingLicense));
            CollectionAssert.Contains(ocrMethods, nameof(OCRApi.DrivingLicenseByFile));
            CollectionAssert.Contains(ocrMethods, nameof(OCRApi.DrivingLicenseAsync));
            CollectionAssert.Contains(ocrMethods, nameof(OCRApi.DrivingLicenseByFileAsync));

            var imageMethods = typeof(ImageApi).GetMethods().Select(z => z.Name).ToArray();
            CollectionAssert.Contains(imageMethods, nameof(ImageApi.AiCrop));
            CollectionAssert.Contains(imageMethods, nameof(ImageApi.AiCropByFile));
            CollectionAssert.Contains(imageMethods, nameof(ImageApi.AiCropAsync));
            CollectionAssert.Contains(imageMethods, nameof(ImageApi.AiCropByFileAsync));
            CollectionAssert.Contains(imageMethods, nameof(ImageApi.QrCode));
            CollectionAssert.Contains(imageMethods, nameof(ImageApi.QrCodeByFile));
            CollectionAssert.Contains(imageMethods, nameof(ImageApi.QrCodeAsync));
            CollectionAssert.Contains(imageMethods, nameof(ImageApi.QrCodeByFileAsync));

            var medicalMethods = typeof(MedicalAssistantApi).GetMethods().Select(z => z.Name).ToArray();
            CollectionAssert.Contains(medicalMethods, nameof(MedicalAssistantApi.SendChannelMessage));
            CollectionAssert.Contains(medicalMethods, nameof(MedicalAssistantApi.SendChannelMessageAsync));
        }

        [TestMethod]
        public void AiCropSeparatesFileAndOrdinaryMultipartFields()
        {
            var fileMethod = typeof(ImageApi).GetMethod("CreateFileDictionary", BindingFlags.NonPublic | BindingFlags.Static);
            var dataMethod = typeof(ImageApi).GetMethod("CreatePostData", BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(fileMethod);
            Assert.IsNotNull(dataMethod);

            var files = fileMethod.Invoke(null, new object[] { "/tmp/image.jpg" }) as Dictionary<string, string>;
            var postData = dataMethod.Invoke(null, new object[] { "1,1" }) as Dictionary<string, string>;

            Assert.IsNotNull(files);
            Assert.IsNotNull(postData);
            Assert.AreEqual("/tmp/image.jpg", files["img"]);
            Assert.IsFalse(files.ContainsKey("ratios"), "ratios 是普通表单字段，不能被当成文件路径上传。");
            Assert.AreEqual("1,1", postData["ratios"]);
            Assert.IsFalse(postData.ContainsKey("img"));
        }

        [TestMethod]
        public void DrivingLicenseResponseMapsOfficialFields()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""id_num"": ""320100199001010011"",
  ""name"": ""张三"",
  ""sex"": ""男"",
  ""nationality"": ""中国"",
  ""address"": ""示例地址"",
  ""birth_date"": ""1990-01-01"",
  ""issue_date"": ""2010-01-01"",
  ""car_class"": ""C1"",
  ""valid_from"": ""2020-01-01"",
  ""valid_to"": ""2030-01-01"",
  ""official_seal"": ""示例车辆管理所""
}";

            var result = JsonSerializer.Deserialize<DrivingLicenseJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("320100199001010011", result.id_num);
            Assert.AreEqual("中国", result.nationality);
            Assert.AreEqual("C1", result.car_class);
            Assert.AreEqual("2030-01-01", result.valid_to);
            Assert.AreEqual("示例车辆管理所", result.official_seal);
        }

        [TestMethod]
        public void IntelligentImageResponsesMapCoordinatesAndCodePosition()
        {
            const string cropJson = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""results"": [{ ""crop_left"": 10, ""crop_top"": 20, ""crop_right"": 610, ""crop_bottom"": 620 }],
  ""img_size"": { ""w"": 800, ""h"": 1000 }
}";
            const string codeJson = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""code_results"": [{
    ""type_name"": ""QR_CODE"",
    ""data"": ""https://www.senparc.com"",
    ""pos"": {
      ""left_top"": { ""x"": 1, ""y"": 2 },
      ""right_top"": { ""x"": 101, ""y"": 2 },
      ""right_bottom"": { ""x"": 101, ""y"": 102 },
      ""left_bottom"": { ""x"": 1, ""y"": 102 }
    }
  }],
  ""img_size"": { ""w"": 300, ""h"": 400 }
}";

            var crop = JsonSerializer.Deserialize<AiCropJsonResult>(cropJson);
            var code = JsonSerializer.Deserialize<QrCodeJsonResult>(codeJson);

            Assert.IsNotNull(crop);
            Assert.AreEqual(610, crop.results[0].crop_right);
            Assert.AreEqual(1000, crop.img_size.h);
            Assert.IsNotNull(code);
            Assert.AreEqual("QR_CODE", code.code_results[0].type_name);
            Assert.AreEqual("https://www.senparc.com", code.code_results[0].data);
            Assert.AreEqual(101, code.code_results[0].pos.right_bottom.x);
        }

        [TestMethod]
        public void MedicalAssistantRequestUsesOfficialJsonFields()
        {
            var request = new SendChannelMessageRequest
            {
                status = 1,
                open_id = "open-id",
                order_id = "order-id",
                msg_id = "message-id",
                app_id = "wx-app-id",
                business_info = new MedicalAssistantBusinessInfo
                {
                    pat_hospital_id = "patient-id",
                    pat_name = "张三",
                    doc_name = "李医生",
                    department_name = "内科",
                    department_location = "门诊楼 2 层",
                    appointment_time = "2026-07-25 09:00:00",
                    redirect_page = new MedicalAssistantRedirectPage
                    {
                        page_type = "mini_program",
                        app_id = "wx-mini-app-id",
                        fullpath = "pages/order/detail?id=order-id"
                    }
                }
            };

            var json = JsonSerializer.Serialize(request);

            StringAssert.Contains(json, "\"status\":1");
            StringAssert.Contains(json, "\"open_id\":\"open-id\"");
            StringAssert.Contains(json, "\"order_id\":\"order-id\"");
            StringAssert.Contains(json, "\"msg_id\":\"message-id\"");
            StringAssert.Contains(json, "\"app_id\":\"wx-app-id\"");
            StringAssert.Contains(json, "\"business_id\":150");
            StringAssert.Contains(json, "\"business_info\"");
            StringAssert.Contains(json, "\"page_type\":\"mini_program\"");
            StringAssert.Contains(json, "\"fullpath\":\"pages/order/detail?id=order-id\"");
        }
    }
}
