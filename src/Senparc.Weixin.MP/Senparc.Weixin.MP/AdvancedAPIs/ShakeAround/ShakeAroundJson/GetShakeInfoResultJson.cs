/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：GetShakeInfoResultJson.cs
    文件功能描述：获取摇周边的设备及用户信息返回结果
    
    
    创建标识：Senparc - 20150512
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 获取摇周边的设备及用户信息返回结果
    /// </summary>
    public class GetShakeInfoResultJson : WxJsonResult
    {
        /// <summary>
        /// 获取摇周边的设备及用户信息返回数据
        /// </summary>
        public ShakeInfo_Data data { get; set; }
    }

    public class ShakeInfo_Data
    {
        /// <summary>
        /// 摇周边页面唯一ID
        /// </summary>
        public string page_id { get; set; }
        /// <summary>
        /// 设备信息，包括UUID、major、minor，以及距离
        /// </summary>
        public ShakeInfo_Data_Beacon_Info beacon_info { get; set; }
        /// <summary>
        /// 商户AppID下用户的唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 门店ID，有的话则返回，反之不会在JSON格式内
        /// </summary>
        public long poi_id { get; set; }
    }

    public class ShakeInfo_Data_Beacon_Info
    {
        /// <summary>
        /// Beacon信号与手机的距离，单位为米
        /// </summary>
        public double distance { get; set; }
        public long major { get; set; }
        public long minor { get; set; }
        public string uuid { get; set; }
    }
}