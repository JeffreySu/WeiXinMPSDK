using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.MP.Test
{
    using Senparc.Weixin.MP.Entities.BaiduMap;
    using Senparc.Weixin.MP.Helpers;
    using Senparc.Weixin.MP.Entities.GoogleMap;

    [TestClass]
    public class BaiduMapHelperTest
    {
        [TestMethod]
        public void GetBaiduStaticMapTes()
        {
            //var markersList = new List<BaiduMarkers>();
            ////markersList.Add(new BaiduMarkers()
            ////                    {
            ////                        Longitude = 22.97992,
            ////                        Latitude = 113.36825,
            ////                        Color = "red",
            ////                        Label = "O",
            ////                        Size = BaiduMarkerSize.Default,
            ////                    });
            ////markersList.Add(new BaiduMarkers()
            ////                    {
            ////                        Longitude = 31.289774,
            ////                        Latitude = 120.597910,
            ////                        Color = "blue",
            ////                        Label = "T",
            ////                        Size = BaiduMarkerSize.Default,
            ////                    });

            //var url = BaiduMapHelper.GetBaiduStaticMap(116.403874, 39.914889, 2, 16, markersList);
            //Console.WriteLine(url);
            //Assert.IsNotNull(url);


            var markersList = new List<GoogleMapMarkers>();
            markersList.Add(new GoogleMapMarkers()
            {
                X = 113.36825,
                Y = 22.97992,
                Color = "red",
                //Label = "测试",
                Size = GoogleMapMarkerSize.Default,
            });
            var mapSize = "480x600";
            var mapUrl = GoogleMapHelper.GetBaiduStaticMap(1 /*requestMessage.Scale*//*微信和GoogleMap的Scale不一致，这里建议使用固定值*/,
                                                            markersList, mapSize);
        }
    }
}
