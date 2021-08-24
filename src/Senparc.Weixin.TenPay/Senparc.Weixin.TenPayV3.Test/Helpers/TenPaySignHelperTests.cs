using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.Helpers.Tests
{
    [TestClass]
    public class TenPaySignHelperTests : BaseTenPayTest
    {
        string privateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCwbkFymtSqLLv+LgQPe2JeYIhwkaccbGtJ+k5XWDENouA/2YhsExuHmHxL5TCf0R96WxWQDYLXxL1YNMYbjACerAQr/NaLjXWWz7bCGEmtMfkSSy8Vav+XYGFgbeEixhqhBKnwafVcGkwXABvdVgQPj1KZWFEfB86nFYxlwhBd/lMN64oE87b8Q8gQSSKavr8rupNk/+dJ9MxfbLB8Ocbx3oQc46qttIPwxw7tsO263YgSnHz/pWFKTM9HVcehKm3yLq4Ka31NXZNx7yY3FJvkTxk00HFoKIOkJ7oHVSSz1d/BcFV0J0j4o2aiJ8dV4Y8jVXyCm8daoROIUJ06pLUdAgMBAAECggEBAIQKgEDdT7vsruWUWsWbegtYA8T7vS2wlrtO8cK85jlmZJ5kl40K39ejb6L9bF6vi/duo7yj4ADL5UlWdc24Ad6+roCdQZQ/0nmECytIMhvFAruGOT62TYNlRnt8wLnduaA4dVbQdijwJXIxSqlj7rYWdaL+TfgDR84UenbNvqIYrkK2/ygesBx4FyZ0PPxBCNLtom6AgRcPm5paqN5D9gErYnmH9tDhyWaVNlR4Yl3fTu5OAji1oOo7IPK6c3h8onu/2oVSTT2hnyViGOnb9PfdF8HLpBYn1jDhY02G7dHSXE9nFZsfdEMtlv4UsKtDEeQWDHWZFLG7Jj6WkYP9qgECgYEA4NL1C543+0ygWtqSUpEraFyvmPr0kR0jOFFzQxRQ04GHNQTPUqKL6q9Wzz0k6MXpb6naDh6Zt4NazMhY6gXA+JvMFT9jO8Flga5ugbCDoSkh7GZHF2ILRmBnzbiq5OcaZWT2uGZ5hF7n+93EX2yI+Oajmwl58sMVYQrusS3gI40CgYEAyOVhIFT0+mK5fwFLO9imJOQNv28/kwOvs3xiNzP7t+lBnnyHgKADFzqDrlTmwlMvkjiXFrL9lVFiRbuZKgDgRxIlLyrrGQDDhELZTCbBQRw0US5jaEty5d4exMVc5sYtBQU7KeGvMNuHQPOCr9sL2yMkHqAPh59wLkjJrsgrK9ECgYAXyJldukYn0opgMiEqlOrnpm55G4tF0rRIH+22me6XAWvhQvn/vuxYTmY7lDdUXKN+SZSky73tOPshEMy/LQ6l/i1Pzh3cU3A+kMpl89to8CYDkSpSIfAZaNdyXufNQVl8gnrLNSQDydp2vZYplhzDX/rMyurYsKSRWIu5uKWufQKBgBUpDATyzeb9tRoqkbkZBc8G8mfCmQisL7qRFGtKH25stEu0RDWJJSoyMKKRMMkxJ+aX7VonukwbCWXc77Ib64Ow5pfERoUYrn9k5yx9PIfWJ7CRWUkoAU8zCTyiqTlpkF5iAaxUqDZWOTXfMFnDkckgN3K+W0Rmx9MoOY4eaWWxAoGBAIjVLqW+Dg9T9eUj6xqILDg6w0vYX9gm0pMQ5dPmC6XFHiYb2zbM2MonKAgYxPDQpBLWqNjQLjNRUeqeo2XEyFmMVYM4/Bx5PxTyhxzbiYI4eohVdCJaw73tFtVa/gTq2lmqQTPwaW7mof2JPbGOOY2vZ+HZhsuwwVA7nIW/NCqa";

        string package = "prepay_id=wx201410272009395522657a690389285100";

        [TestMethod]
        public void CreatePaySignTest()
        {
            var appId = "wx8888888888888888";
            var timestamp = "1414561699";
            var nonceStr = "5K8264ILTKCH16CQ2502SI8ZNMTM67VS";

            var separcWeixinSetting = Config.SenparcWeixinSetting with
                                        {
                                            TenPayV3_AppId = appId,
                                            TenPayV3_PrivateKey = privateKey
                                        };

            var result = TenPaySignHelper.CreatePaySign(timestamp, nonceStr, package, separcWeixinSetting);
            Assert.IsNotNull(result);

            var exceptedResult = "POmTZCzk7fj+FeSwbU4rNghygFOzwpoaQt9SBW8blDAPZCVJ7wVnDVisx6t1ryyBpB3NmOwiNaT+hHi7YthYZzr0kvL5kWKSnpssyWBofnjqbFWBSV8JaFx7Ia2qnsgdVYALisYjLBr+bj69YXuyWiBxYFx+JylH6wW4w55Rziatoa4rwrdlrpgE2yRTxDu9wSZ4VCdUYSMj2ctyAy2fOiCcP00VGjihJWGCXXjeVm2YQyFZXB7KqGPhncdHaFmJzIvL8SbWKSc36cUKSuHhZ5n+oZVU8Vf+lb/eJibzTWxBIAJbtQplKojG48ukd7QFtRUd3b2EkOjzmeJ26zMlfA==";

            Assert.AreEqual(exceptedResult, result);
        }


        [TestMethod()]
        public void CreatePaySign_ByPrivateKeyTest()
        {
            var appId = "wx8888888888888888";
            var timestamp = "1414561699";
            var nonceStr = "5K8264ILTKCH16CQ2502SI8ZNMTM67VS";

            var result = TenPaySignHelper.CreatePaySign(timestamp, nonceStr, package, appId, privateKey);
            Assert.IsNotNull(result);
            Console.WriteLine(result);

            var exceptedResult = "POmTZCzk7fj+FeSwbU4rNghygFOzwpoaQt9SBW8blDAPZCVJ7wVnDVisx6t1ryyBpB3NmOwiNaT+hHi7YthYZzr0kvL5kWKSnpssyWBofnjqbFWBSV8JaFx7Ia2qnsgdVYALisYjLBr+bj69YXuyWiBxYFx+JylH6wW4w55Rziatoa4rwrdlrpgE2yRTxDu9wSZ4VCdUYSMj2ctyAy2fOiCcP00VGjihJWGCXXjeVm2YQyFZXB7KqGPhncdHaFmJzIvL8SbWKSc36cUKSuHhZ5n+oZVU8Vf+lb/eJibzTWxBIAJbtQplKojG48ukd7QFtRUd3b2EkOjzmeJ26zMlfA==";
            Assert.AreEqual(exceptedResult, result);
        }
    }
}
