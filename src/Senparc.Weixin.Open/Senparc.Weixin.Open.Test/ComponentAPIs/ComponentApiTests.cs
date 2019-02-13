using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Open.ComponentAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Open.CommonAPIs;
using Senparc.Weixin.Open.Containers;
using Senparc.Weixin.Open.Test;

namespace Senparc.Weixin.Open.ComponentAPIs.Tests
{
    [TestClass()]
    public class ComponentApiTests : OpenBaseTest
    {
        [TestMethod()]
        public void RefreshAuthorizerTokenTest()
        {
            var componentAccessTokenResult = ComponentContainer.GetComponentAccessTokenResult(base._appId, base._ticket);

            Assert.IsNotNull(componentAccessTokenResult.component_access_token);

            var authorizerId = "wxd7a61edcdce336b0";
            var authorizerInfo = ComponentApi.GetAuthorizerInfo(componentAccessTokenResult.component_access_token, base._appId, authorizerId);

            var authorizer_access_token = authorizerInfo.authorization_info.authorizer_access_token;
            var authorizer_refresh_token = authorizerInfo.authorization_info.authorizer_refresh_token;

            Assert.IsNotNull(authorizerInfo.authorization_info.authorizer_access_token);
            Assert.IsNotNull(authorizerInfo.authorization_info.authorizer_refresh_token);

            Console.WriteLine("authorizer_access_token：" + authorizer_access_token);
            Console.WriteLine("authorizer_refresh_token：" + authorizer_refresh_token);

            var result = ComponentApi.ApiAuthorizerToken(componentAccessTokenResult.component_access_token,
                base._appId, authorizerId, authorizerInfo.authorization_info.authorizer_refresh_token);

            Console.WriteLine("authorizer_access_token：" + result.authorizer_access_token);
            Console.WriteLine("authorizer_access_token：" + result.authorizer_refresh_token);
        }
    }
}