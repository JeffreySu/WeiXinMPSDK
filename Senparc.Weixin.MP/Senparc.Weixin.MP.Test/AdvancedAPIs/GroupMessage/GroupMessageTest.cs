using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            var accessToken = AccessTokenContainer.GetToken(_appId);
            var mediaId = MediaAPI.Upload(accessToken, UploadMediaFileType.image, file).media_id;

            var result = GroupMessageAPI.SendGroupMessageByGroupId(accessToken, groupId, mediaId,GroupMessageType.image,false);

            Assert.IsTrue(result.msg_id.Length > 0);
        }

        [TestMethod]
        public void SendTextByGroupIdTest()
        {
            string content = "文本内容";
            string groupId = "";//分组Id

            var accessToken = AccessTokenContainer.GetToken(_appId);
            //发送给指定分组
            var result = GroupMessageAPI.SendTextGroupMessageByGroupId(accessToken, groupId, content, false);
            Assert.IsTrue(result.msg_id.Length > 0);

            //发送给所有人
            var result_All = GroupMessageAPI.SendTextGroupMessageByGroupId(accessToken, null, content, true);
            Assert.IsTrue(result.msg_id.Length > 0);
        }

        [TestMethod]
        public void SendTextByOpenIdTest()
        {
            string content = "文本内容";
            string[] openIds = new string[] { _testOpenId };

            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = GroupMessageAPI.SendTextGroupMessageByOpenId(accessToken, content, openIds);

            Assert.IsTrue(result.msg_id.Length > 0);
        }

        //[TestMethod]
        public string SendImageByOpenIdTest()
        {
            string file = "";//文件路径，以下以图片为例
            string[] openIds = new string[] { _testOpenId };

            var accessToken = AccessTokenContainer.GetToken(_appId);
            var mediaId = MediaAPI.Upload(accessToken, UploadMediaFileType.image, file).media_id;
            var result = GroupMessageAPI.SendGroupMessageByOpenId(accessToken, GroupMessageType.image, mediaId, openIds);

            Assert.IsTrue(result.msg_id.Length > 0);

            return result.msg_id;
        }

        [TestMethod]
        public void GetGroupMessageResultTest()
        {
            var msgId = SendImageByOpenIdTest();

            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = GroupMessageAPI.GetGroupMessageResult(accessToken, msgId);

            Assert.IsTrue(result.msg_id.Length > 0);
            Assert.AreEqual(result.msg_status, "SEND_SUCCESS");
        }
    }
}
