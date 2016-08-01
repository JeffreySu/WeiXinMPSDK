using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities.Request;

namespace Senparc.Weixin.MP.Test.Entities.Request
{
    [TestClass]
   public class PostModelTest
    {
        [TestMethod]
        public void SetSecretInfoTest()
        {
            var postModel = new PostModel();
            postModel.SetSecretInfo("A","B","C");
            Assert.AreEqual("A", postModel.Token);
            Assert.AreEqual("B", postModel.EncodingAESKey);
            Assert.AreEqual("C", postModel.AppId);
        }
    }
}
