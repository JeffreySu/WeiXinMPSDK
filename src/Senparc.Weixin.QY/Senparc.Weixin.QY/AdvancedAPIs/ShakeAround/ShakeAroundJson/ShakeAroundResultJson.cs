/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ShakeAroundResultJson.cs
    文件功能描述：摇一摇周边返回结果
    
    
    创建标识：Senparc - 20150921
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 获取设备及用户信息返回结果
    /// </summary>
    public class GetShakeInfoResult : QyJsonResult
    {
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
        public BeaconInfo beacon_info { get; set; }

        /// <summary>
        /// 企业号成员的userid
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 企业号父号下用户的openid，非企业关注成员的标识，对当前企业号唯一
        /// </summary>
        public string openid { get; set; }
    }

    public class BeaconInfo
    {
        /// <summary>
        /// Beacon信号与手机的距离，单位为米
        /// </summary>
        public double distance { get; set; }

        public long major { get; set; }
        public long minor { get; set; }

        /// <summary>
        /// UUID
        /// </summary>
        public string uuid { get; set; }
    }
}