/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ShakearoundUserShake.cs
    文件功能描述：系统拍照发图中的SendPicsInfo
    
    
    创建标识：Senparc - 20150914
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// Beacon的参数以及距离
    /// </summary>
    public class BaseBeaconItem
    {
        public string Uuid { get; set; }
        public long Major { get; set; }
        public long Minor { get; set; }
        /// <summary>
        /// 设备与用户的距离（浮点数；单位：米）
        /// </summary>
        public double Distance { get; set; }
    }

    public class ChosenBeacon : BaseBeaconItem
    {
        
    }

    public class AroundBeacon : BaseBeaconItem
    {

    }
}
