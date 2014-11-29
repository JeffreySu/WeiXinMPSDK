using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class GroupMessageTest : CommonApiTest
    {
        [TestMethod]
        public void SendImageTest()
        {
            string file = "";//文件路径，以下以图片为例
            string groupId = "";//分组Id

            var accessToken = AccessTokenContainer.GetToken(_appId);
            var mediaId = Media.Upload(accessToken, UploadMediaFileType.image, file).media_id;

            var result = GroupMessage.SendGroupMessageByGroupId(accessToken, groupId, GroupMessageType.image, mediaId);

            Assert.IsTrue(result.msg_id.Length > 0);
        }

        [TestMethod]
        public void SendTextTest()
        {
            string content = "文本内容";
            string groupId = "";//分组Id

            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = GroupMessage.SendGroupMessageByGroupId(accessToken, groupId, GroupMessageType.text, null, content);

            Assert.IsTrue(result.msg_id.Length > 0);
        }
    }
}
