using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Tests
{
    [TestClass()]
    public class WxAppApiTests : CommonApiTest
    {
        [TestMethod()]
        public void CreateWxaQrCodeTest()
        {
            var ms = new MemoryStream();
            var result = WxAppApi.CreateWxaQrCode(base._appId, ms, "pages/websocket", 400);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);

            ms.Seek(0, SeekOrigin.Begin);
            //储存图片

            var filePath = "../../Config/qr.jpg";
            File.Delete(filePath);
            using (var fs = new FileStream(filePath, FileMode.CreateNew))
            {
                ms.CopyTo(fs);
                fs.Flush();
            }

            Assert.IsTrue(File.Exists(filePath));
        }
    }
}