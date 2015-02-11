using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已测试通过
    [TestClass]
    public class SemanticTest : CommonApiTest
    {
        protected SemanticPostData SemanticPostData = new SemanticPostData()
            {
                query = "附近有什么饭馆",
                category = "restaurant",
                latitude = 31,
                longitude = 120
            };

        [TestMethod]
        public void SemanticUnderStandTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = Semantic.SemanticSend(accessToken, SemanticPostData);
            SearchResultJson json = new SearchResultJson();
            
            Assert.IsNotNull(result.query);
            Assert.AreEqual("附近有什么川菜馆", result.query);
        }
    }
}
