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
    
    文件名：GoogleMapMarkers.cs
    文件功能描述：谷歌地图
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities.GoogleMap
{
    /// <summary>
    /// 标记大小
    /// </summary>
    public enum GoogleMapMarkerSize
    {
        Default = mid,
        tiny = 0, mid = 1, small = 2
    }

    /// <summary>
    /// 谷歌地图标记
    /// </summary>
    public class GoogleMapMarkers
    {
        /// <summary>
        /// （可选）指定集合 {tiny, mid, small} 中的标记大小。如果未设置 size 参数，标记将以其默认（常规）大小显示。
        /// </summary>
        public GoogleMapMarkerSize Size { get; set; }
        /// <summary>
        /// （可选）指定 24 位颜色（例如 color=0xFFFFCC）或集合 {black, brown, green, purple, yellow, blue, gray, orange, red, white} 中预定义的一种颜色。
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// （可选）指定集合 {A-Z, 0-9} 中的一个大写字母数字字符。
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 经度longitude
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// 纬度latitude
        /// </summary>
        public double Y { get; set; }
    }
}
