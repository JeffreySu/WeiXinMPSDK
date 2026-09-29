using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using System.Text.Json;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class MediaResultSerializationTests
    {
        [TestMethod]
        public void UploadTemporaryMediaResult_StringType()
        {
            const string json = "{\"type\":\"image\",\"media_id\":\"MEDIA_ID\",\"created_at\":123456789}";

            var result = JsonSerializer.Deserialize<UploadTemporaryMediaResult>(json);
            Assert.AreEqual(UploadMediaFileType.image, result.type);
            Assert.AreEqual("MEDIA_ID", result.media_id);
            Assert.AreEqual(123456789L, result.created_at);
            Assert.AreEqual(UploadMediaFileType.image, Post.GetResult<UploadTemporaryMediaResult>(json).type);
        }

        [TestMethod]
        public void SendResult_StringType()
        {
            const string json = "{\"type\":\"news\",\"msg_id\":\"123\"}";

            var result = JsonSerializer.Deserialize<SendResult>(json);
            Assert.AreEqual(UploadMediaFileType.news, result.type);
            Assert.AreEqual("123", result.msg_id);
            Assert.AreEqual(UploadMediaFileType.news, Post.GetResult<SendResult>(json).type);
        }

        [TestMethod]
        public void MediaResults_NumericTypeRemainsSupported()
        {
            Assert.AreEqual(UploadMediaFileType.thumb,
                JsonSerializer.Deserialize<UploadTemporaryMediaResult>("{\"type\":3}").type);
            Assert.AreEqual(UploadMediaFileType.video,
                JsonSerializer.Deserialize<SendResult>("{\"type\":2}").type);
        }
    }
}
