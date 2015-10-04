using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class GroupMessageTest : CommonApiTest
    {
        [TestMethod]
        public void SendImageByGroupIdTest()
        {
            string file = "";//文件路径，以下以图片为例
            string groupId = "";//分组Id

            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var mediaId = MediaApi.UploadTemporaryMedia(accessToken, UploadMediaFileType.image, file).media_id;

            var result = GroupMessageApi.SendGroupMessageByGroupId(accessToken, groupId, mediaId,GroupMessageType.image,false);

            Assert.IsTrue(result.msg_id.Length > 0);
        }

        //[TestMethod]
        public string SendImageByOpenIdTest()
        {
            string file = "";//文件路径，以下以图片为例
            string[] openIds = new string[] { _testOpenId };

            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var mediaId = MediaApi.UploadTemporaryMedia(accessToken, UploadMediaFileType.image, file).media_id;
            var result = GroupMessageApi.SendGroupMessageByOpenId(accessToken, GroupMessageType.image, mediaId, Config.TIME_OUT, openIds);

            Assert.IsTrue(result.msg_id.Length > 0);

            return result.msg_id;
        }

        [TestMethod]
        public void GetGroupMessageResultTest()
        {
            var msgId = SendImageByOpenIdTest();

            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = GroupMessageApi.GetGroupMessageResult(accessToken, msgId);

            Assert.IsTrue(result.msg_id.Length > 0);
            Assert.AreEqual(result.msg_status, "SEND_SUCCESS");
        }

        [TestMethod]
        public void GetVideoMediaIdResultTest()
        {
            string mediaId = "Qk7qR9oZGG1CyzJ8ik3j3nElgY5xETEFAiTLrMsZJs9iAKarM7DopvxbREE7fINU";

            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = GroupMessageApi.GetVideoMediaIdResult(accessToken, mediaId, "test", "test");

            Assert.IsTrue(result.media_id.Length > 0);
        }
    }
}
