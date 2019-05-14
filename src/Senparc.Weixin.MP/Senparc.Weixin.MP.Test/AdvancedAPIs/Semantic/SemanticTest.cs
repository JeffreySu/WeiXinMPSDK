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


using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Semantic;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class SemanticTest : CommonApiTest
    {
        protected SemanticPostData SemanticPostData = new SemanticPostData()
            {
                query = "查一下明天从北京到上海的南航机票",
                category = "flight",
                city = "北京",
                appid = "wxbe855a981c34aa3f",
                uid = "123456"
            };

        [TestMethod]
        public void SemanticUnderStandTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = SemanticApi.SemanticSend<Semantic_RestaurantResult>(accessToken, SemanticPostData);
            
            Assert.IsNotNull(result.query);
            Assert.AreEqual("附近有什么川菜馆", result.query);
        }

        [TestMethod]
        public void RestaurantResultTest()
        {
            string returnText = "{\"res\":0,\"query\":\" 附近有什么川菜馆\",\"type\":\"restaurant\",\"semantic\":{\"details\":{\"category\":\"川菜\"},\"intent\":\"SEARCH\"}}";
            var result = Senparc.Weixin.HttpUtility.Post.GetResult<Semantic_RestaurantResult>(returnText);
            Assert.IsNotNull(result.semantic);
            Assert.AreEqual(" 附近有什么川菜馆", result.query);
            Assert.AreEqual("SEARCH", result.semantic.intent);
            Assert.AreEqual("川菜", result.semantic.details.category);
        }

    }
}
