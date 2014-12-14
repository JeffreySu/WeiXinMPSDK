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
        [TestMethod]
        public void SemanticUnderStandTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = Semantic.SemanticUnderStand(accessToken, "百度一下明天从北京到上海的南航机票", "flight,search", "北京", _appId);
            SearchResultJson json = new SearchResultJson();
            
            Assert.IsNotNull(result.query);
            Assert.AreEqual("百度一下明天从北京到上海的南航机票", result.query);
        }
    }
}
