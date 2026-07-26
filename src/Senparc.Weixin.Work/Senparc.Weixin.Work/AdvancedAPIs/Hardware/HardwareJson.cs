/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：HardwareJson.cs
    文件功能描述：企业微信硬件设备特征强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐硬件设备特征查询模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Hardware
{
    /// <summary>
    /// 获取硬件设备特征请求。
    /// </summary>
    public class HardwareGetDeviceFeatureRequest
    {
        /// <summary>
        /// 设备序列号。
        /// </summary>
        public string device_sn { get; set; }
    }

    /// <summary>
    /// 获取硬件设备特征结果。
    /// </summary>
    public class HardwareGetDeviceFeatureResult : WorkJsonResult
    {
        /// <summary>
        /// 设备厂商自定义的特征串；内容通常为 JSON，但结构由设备厂商定义，因此按原始字符串保留。
        /// </summary>
        public string device_feature { get; set; }
    }
}
