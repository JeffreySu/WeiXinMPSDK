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

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：BaiduMapMarkers.cs
    文件功能描述：百度地图
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities.BaiduMap
{
    /// <summary>
    /// 标记大小
    /// </summary>
    public enum BaiduMarkerSize
    {
        Default = m,
        s = 0, m = 1, l = 2
    }

    /// <summary>
    /// 百度地图标记
    /// </summary>
    public class BaiduMarkers
    {
        /// <summary>
        /// （可选）有大中小三个值，分别为s、m、l。
        /// </summary>
        public BaiduMarkerSize Size { get; set; }
        /// <summary>
        /// （可选）Color = [0x000000, 0xffffff]或使用css定义的颜色表。
        /// black 0x000000 
        /// silver 0xC0C0C0 
        /// gray 0x808080 
        /// white 0xFFFFFF 
        /// maroon 0x800000
        /// red 0xFF0000 
        /// purple 0x800080 
        /// fuchsia 0xFF00FF 
        /// green 0x008000
        /// lime 0x00FF00 
        /// olive 0x808000 
        /// yellow 0xFFFF00 
        /// navy 0x000080 
        /// blue 0x0000FF
        /// teal 0x008080 
        /// aqua 0x00FFFF
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// （可选）指定集合 {A-Z, 0-9} 中的一个大写字母数字字符。不指定时显示A。
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 自定义icon的地址，图片格式目前仅支持png32的。设置自定义图标标注时，忽略Size、Color、Label三个属性，只设置该属性且该属性前增加-1，如markerStyles=-1, http://api.map.baidu.com/images/marker_red.png，图标大小需小于5k，超过该值会导致加载不上图标的情况发生。
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 经度longitude（对应GoogleMap的X）
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度latitude（对应GoogleMap的Y）
        /// </summary>
        public double Latitude { get; set; }
    }
}
