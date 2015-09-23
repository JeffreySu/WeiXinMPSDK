/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：GoogleMapHelper.cs
    文件功能描述：获取谷歌今天静态地图Url
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

/*
     API介绍：https://developers.google.com/maps/documentation/staticmaps/?hl=zh-CN
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities.GoogleMap;

namespace Senparc.Weixin.MP.Helpers
{
    public static class GoogleMapHelper
    {
        /// <summary>
        /// 获取谷歌今天静态地图Url。API介绍：https://developers.google.com/maps/documentation/staticmaps/?hl=zh-CN
        /// </summary>
        /// <returns></returns>
        public static string GetGoogleStaticMap(int scale, IList<GoogleMapMarkers> markersList, string size = "640x640")
        {
            markersList = markersList ?? new List<GoogleMapMarkers>();
            StringBuilder markersStr = new StringBuilder();
            foreach (var markers in markersList)
            {
                markersStr.Append("&markers=");
                if (markers.Size != GoogleMapMarkerSize.mid)
                {
                    markersStr.AppendFormat("size={0}%7C", markers.Size);
                }
                if (!string.IsNullOrEmpty(markers.Color))
                {
                    markersStr.AppendFormat("color:{0}%7C", markers.Color);
                }
                markersStr.Append("label:");
                if (!string.IsNullOrEmpty(markers.Label))
                {
                    markersStr.AppendFormat("{0}%7C", markers.Label);
                }
                markersStr.AppendFormat("{0},{1}", markers.X, markers.Y);
            }
            string parameters = string.Format("center=&zoom=&size={0}&maptype=roadmap&format=jpg&sensor=false&language=zh&{1}",
                                             size, markersStr.ToString());
            string url = "http://maps.googleapis.com/maps/api/staticmap?" + parameters;
            return url;
        }

        /// <summary>color
        /// 获取百度今天静态地图Url。API介绍：http://api.map.baidu.com/staticimage?
        /// </summary>
        /// <returns></returns>
        public static string GetBaiduStaticMap(int scale, IList<GoogleMapMarkers> markersList, string size = "640x640")
        {
            markersList = markersList ?? new List<GoogleMapMarkers>();
            StringBuilder markersStr = new StringBuilder();
            foreach (var markers in markersList)
            {
                markersStr.AppendFormat("markers={0},{1}", markers.X, markers.Y);
                if (markers.Size != GoogleMapMarkerSize.mid)
                {
                    markersStr.AppendFormat("&size={0}", markers.Size);
                }
                if (!string.IsNullOrEmpty(markers.Color))
                {
                    markersStr.AppendFormat("&color={0}", markers.Color);
                }
                markersStr.Append("&labels=");
                if (!string.IsNullOrEmpty(markers.Label))
                {
                    markersStr.AppendFormat("{0}|{1},{2}", markers.Label, markers.X, markers.Y);
                }
                markersStr.AppendFormat("&center={0},{1}", markers.X, markers.Y);
            }

            string parameters = string.Format("?zoom={0}&size={1}&{2}", scale,
                                             size, markersStr.ToString());
            string url = "http://api.map.baidu.com/staticimage" + parameters;
            return url;
        }
    }
}
