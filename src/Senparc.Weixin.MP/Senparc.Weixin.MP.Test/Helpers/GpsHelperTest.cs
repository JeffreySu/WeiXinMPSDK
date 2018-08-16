#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.Test.Helpers
{
    [TestClass]
    public class GpsHelperTest
    {
        [TestMethod]
        public void DistanceTest()
        {
            var result = GpsHelper.Distance(31.3131, 120.5815, 31.2751, 120.6497);
            Assert.IsTrue(result>0);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void GetLatitudeDifferenceTest()
        {
            var result = GpsHelper.GetLatitudeDifference(10);
            Assert.IsTrue(result>0);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void GetLongitudeDifferenceTest()
        {
            var result = GpsHelper.GetLongitudeDifference(10);
            Assert.IsTrue(result > 0);
            Console.WriteLine(result);
        }
    }
}
