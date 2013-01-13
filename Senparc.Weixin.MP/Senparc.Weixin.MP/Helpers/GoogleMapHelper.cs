using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Helpers
{
    public static class GoogleMapHelper
    {
        /// <summary>
        /// 获取谷歌今天静态地图Url。API介绍：https://developers.google.com/maps/documentation/staticmaps/?hl=zh-CN
        /// </summary>
        /// <returns></returns>
        public static string GetGoogleStaticMap(double x, double y, int scale, string label, string size = "640x640", string color = "red")
        {
            string parameters = string.Format("center=&zoom=&size={0}&maptype=roadmap&format=jpg&sensor=false", size);
            string url = "https://maps.googleapis.com/maps/api/staticmap?" + parameters;
            return url;
        }
    }
}
