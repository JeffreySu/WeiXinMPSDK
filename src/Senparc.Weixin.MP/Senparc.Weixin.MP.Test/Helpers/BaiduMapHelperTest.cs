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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.MP.Test
{
    using Senparc.CO2NET.Helpers.BaiduMap;
    using Senparc.CO2NET.Helpers;

    [TestClass]
    public class BaiduMapHelperTest
    {
        [TestMethod]
        public void GetBaiduStaticMapTest()
        {
            var markersList = new List<BaiduMarkers>();
            markersList.Add(new BaiduMarkers()
                                {
                                    Longitude = 31.285774,
                                    Latitude = 120.597610,
                                    Color="red",
                                    Label="O",
                                    Size=  BaiduMarkerSize.Default,
                                });
            markersList.Add(new BaiduMarkers()
                                {
                                    Longitude = 31.289774,
                                    Latitude = 120.597910,
                                    Color = "blue",
                                    Label = "T",
                                    Size = BaiduMarkerSize.Default,
                                });

            var url = BaiduMapHelper.GetBaiduStaticMap(31.285774, 120.597610,2,16, markersList);
            Console.WriteLine(url);
            Assert.IsNotNull(url);
        }
    }
}
