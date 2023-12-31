#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    [TestClass]
    public class CustomTest : CommonApiTest
    {
        private string openId = "oxRg0uLsnpHjb8o93uVnwMK_WAVw";

        [TestMethod]
        public void SendTextTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = CustomApi.SendText(accessToken, openId, "来自平台的回复<>&\n换行了");
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public void SendImageTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = CustomApi.SendImage(accessToken, openId, "10001037");
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public void SendVoiceTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            try
            {
                var result = CustomApi.SendVoice(accessToken, openId, "1000018");
                Assert.Fail();//因为这里写测试代码的时候，微信账号还没有权限，所以会抛出异常（故意的），如果是已经开通的应该是“请求成功”
            }
            catch (ErrorJsonResultException ex)
            {
                Assert.AreEqual(ReturnCode.api功能未授权, ex.JsonResult.errcode);
            }
        }

        [TestMethod]
        public void SendVideoTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            try
            {
                var result = CustomApi.SendVideo(accessToken, openId, "1000018", "1000012", "[description]");
                Assert.Fail();//因为这里写测试代码的时候，微信账号还没有权限，所以会抛出异常（故意的），如果是已经开通的应该是“请求成功”
            }
            catch (ErrorJsonResultException ex)
            {
                Assert.AreEqual(ReturnCode.api功能未授权, ex.JsonResult.errcode);
            }
        }

        [TestMethod]
        public void SendNewsTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var articles = new List<Article>();
            articles.Add(new Article()
            {
                Title = "测试标题",
                Description = "测试描述",
                Url = "https://sdk.weixin.senparc.com",
                PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg"
            });
            articles.Add(new Article()
            {
                Title = "测试更多标题",
                Description = "测试更多描述",
                Url = "https://sdk.weixin.senparc.com",
                PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg"
            });

            var result = CustomApi.SendNews(accessToken, openId, articles);
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public async Task TestMaxLegth()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var content = new string('盛', 682);
            var result = await CustomApi.SendTextAsync(accessToken, openId, content);
            await Console.Out.WriteLineAsync(result.ToJson(true));

            //超出
            try
            {
                content = new string('盛', 683);
                result = await CustomApi.SendTextAsync(accessToken, openId, content, 0);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                //不限制则会出错
                await Console.Out.WriteLineAsync(ex.ToString());
                await Console.Out.WriteLineAsync();
                //Assert.IsTrue(ex.Message.Contains("limit"));
            }

            //进行切割
            content = new string('盛', 683);//最后一个字会被切割，因此最后一个“盛”会被放到吓一条
            content += "派";
            content += "\r\n上面第二条自动消息会包含“盛派”两个字";
            result = await CustomApi.SendTextAsync(accessToken, openId, content);
            await Console.Out.WriteLineAsync(result.ToJson(true));

        }
    }
}
