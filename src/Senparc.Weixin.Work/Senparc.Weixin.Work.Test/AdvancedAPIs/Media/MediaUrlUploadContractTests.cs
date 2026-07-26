using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Media;
using WorkMediaApi = Senparc.Weixin.Work.AdvancedAPIs.MediaApi;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Media
{
    [TestClass]
    public class MediaUrlUploadContractTests
    {
        [TestMethod]
        public void ApiExposesBothSyncAndAsyncOperations()
        {
            var methods = typeof(WorkMediaApi).GetMethods(BindingFlags.Public | BindingFlags.Static);

            Assert.AreEqual(typeof(MediaUploadByUrlResult),
                methods.Single(method => method.Name == nameof(WorkMediaApi.UploadByUrl)).ReturnType);
            Assert.AreEqual(typeof(Task<MediaUploadByUrlResult>),
                methods.Single(method => method.Name == nameof(WorkMediaApi.UploadByUrlAsync)).ReturnType);
            Assert.AreEqual(typeof(MediaUploadByUrlTaskResult),
                methods.Single(method => method.Name == nameof(WorkMediaApi.GetUploadByUrlResult)).ReturnType);
            Assert.AreEqual(typeof(Task<MediaUploadByUrlTaskResult>),
                methods.Single(method => method.Name == nameof(WorkMediaApi.GetUploadByUrlResultAsync)).ReturnType);
        }

        [TestMethod]
        public void ApiUsesOfficialPostPaths()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Static;

            Assert.AreEqual("/cgi-bin/media/upload_by_url",
                typeof(WorkMediaApi).GetField("UploadByUrlPath", flags)?.GetRawConstantValue());
            Assert.AreEqual("/cgi-bin/media/get_upload_by_url_result",
                typeof(WorkMediaApi).GetField("GetUploadByUrlResultPath", flags)?.GetRawConstantValue());
        }

        [TestMethod]
        public void ModelsPreserveOfficialRequestAndResultFields()
        {
            var requestJson = JsonSerializer.Serialize(new MediaUploadByUrlRequest
            {
                scene = 1,
                type = "video",
                filename = "video.mp4",
                url = "https://example.test/video.mp4",
                md5 = "MD5"
            });
            StringAssert.Contains(requestJson, "\"scene\":1");
            StringAssert.Contains(requestJson, "\"filename\":\"video.mp4\"");

            var result = JsonSerializer.Deserialize<MediaUploadByUrlTaskResult>(
                "{\"errcode\":0,\"status\":2,\"detail\":{\"errcode\":0,\"errmsg\":\"ok\"," +
                "\"media_id\":\"MEDIA_ID\",\"created_at\":2147484000}}");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.status);
            Assert.AreEqual("MEDIA_ID", result.detail.media_id);
            Assert.AreEqual(2147484000L, result.detail.created_at);
        }
    }
}
