/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：BaiduMapHelper.cs
    文件功能描述：百度地图静态图片API
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

/*
     文档：http://api.map.baidu.com/lbsapi/cloud/staticimg.htm
 */

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities.BaiduMap;

namespace Senparc.Weixin.MP.Helpers
{
    /// <summary>
    /// 百度地图静态图片API，文档：http://api.map.baidu.com/lbsapi/cloud/staticimg.htm
    /// </summary>
    public static class BaiduMapHelper
    {
        /// <summary>
        /// 获取百度地图静态图片
        /// </summary>
        /// <param name="lng">中心点经度</param>
        /// <param name="lat">中心点维度</param>
        /// <param name="scale">返回图片大小会根据此标志调整。取值范围为1或2：
        ///1表示返回的图片大小为size= width * height;
        ///2表示返回图片为(width*2)*(height *2)，且zoom加1
        ///注：如果zoom为最大级别，则返回图片为（width*2）*（height*2），zoom不变。</param>
        /// <param name="zoom">地图级别。高清图范围[3, 18]；低清图范围[3,19]</param>
        /// <param name="markersList">标记列表，如果为null则不输出标记</param>
        /// <param name="width">图片宽度。取值范围：(0, 1024]。</param>
        /// <param name="height">图片高度。取值范围：(0, 1024]。</param>
        /// <returns></returns>
        public static string GetBaiduStaticMap(double lng, double lat, int scale, int zoom, IList<BaiduMarkers> markersList, int width = 400, int height = 300)
        {
            var url = new StringBuilder();
            url.Append("http://api.map.baidu.com/staticimage?");

            url.AppendFormat("center={0},{1}", lng, lat);
            url.AppendFormat("&width={0}", width);
            url.AppendFormat("&height={0}", height);
            url.AppendFormat("&scale={0}", scale);
            url.AppendFormat("&zoom={0}", zoom);

            if (markersList != null && markersList.Count > 0)
            {
                url.AppendFormat("&markers={0}", string.Join("|", markersList.Select(z => string.Format("{0},{1}", z.Longitude, z.Latitude)).ToArray()));
                url.AppendFormat("&markerStyles={0}", string.Join("|", markersList.Select(z => string.Format("{0},{1},{2}", z.Size.ToString(), z.Label, z.Color)).ToArray()));
            }

            return url.ToString();
        }
    }
}
