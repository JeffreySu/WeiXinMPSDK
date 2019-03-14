#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Utilities;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
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

            var result = GroupMessageApi.SendGroupMessageByGroupId(accessToken, groupId, mediaId, GroupMessageType.image, false);

            Assert.IsTrue(result.msg_id.Length > 0);
        }

        //[TestMethod]
        public string SendImageByOpenIdTest()
        {
            string file = "";//文件路径，以下以图片为例
            string[] openIds = new string[] { _testOpenId };

            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var mediaId = MediaApi.UploadTemporaryMedia(accessToken, UploadMediaFileType.image, file).media_id;
            var clientMsgId = SystemTime.Now.Ticks.ToString();
            var result = GroupMessageApi.SendGroupMessageByOpenId(accessToken, GroupMessageType.image, mediaId, clientMsgId, Config.TIME_OUT, openIds);

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
        public VideoMediaIdResult GetVideoMediaIdResultTest()
        {
            var videoFilePath = ServerUtility.ContentRootMapPath("video-test.mp4");
            Console.WriteLine("Video Path:" + videoFilePath);

            //上传视频
            var uploadResult = MediaApi.UploadTemporaryMedia(_appId, UploadMediaFileType.video, videoFilePath);
            Console.WriteLine("Video Upload Result:" + uploadResult);

            string mediaId = uploadResult.media_id;//也可以通过对公众号发送视频获得

            var result = GroupMessageApi.GetVideoMediaIdResult(_appId, mediaId, "test", "test");
            Assert.IsNotNull(result);
            Console.WriteLine("GetVideoMediaIdResult" + result.ToJson());
            Assert.IsNotNull(result.media_id);
            Assert.IsTrue(result.media_id.Length > 0);
            return result;
        }

        [TestMethod]
        public void SendGroupMessagePreviewTest()
        {
            var videoMediaIdResult = GetVideoMediaIdResultTest();
            var groupSendResult = GroupMessageApi.SendGroupMessagePreview(_appId, GroupMessageType.video, videoMediaIdResult.media_id, _testOpenId);

            Console.WriteLine("AppId:" + _appId);
            Console.WriteLine("OpenId:" + _testOpenId);
            Assert.IsNotNull(groupSendResult);
            Console.WriteLine(groupSendResult.ToJson());
        }
    }
}
