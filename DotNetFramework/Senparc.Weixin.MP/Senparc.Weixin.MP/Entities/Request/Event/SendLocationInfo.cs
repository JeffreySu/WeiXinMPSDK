/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SendLocationInfo.cs
    文件功能描述：弹出地理位置选择器的事件推送中的SendLocationInfo
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 弹出地理位置选择器的事件推送中的SendLocationInfo
    /// </summary>
    public class SendLocationInfo
    {
        /// <summary>
        /// X坐标信息
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// Y坐标信息
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// 精度，可理解为精度或者比例尺、越精细的话 scale越高
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置的字符串信息
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 朋友圈POI的名字，可能为空
        /// </summary>
        public string Poiname { get; set; }
    }
}
