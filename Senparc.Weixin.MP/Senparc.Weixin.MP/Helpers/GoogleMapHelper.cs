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
        public static string GetGoogleStaticMap(int scale,  IList<GoogleMapMarkers> markersList, string size = "640x640")
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
                                             size,markersStr.ToString());
            string url = "http://maps.googleapis.com/maps/api/staticmap?" + parameters;
            return url;
        }
    }
}
