/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：GetPerformanceDataJsonResult.cs
    文件功能描述：GetPerformanceDataJsonResult 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.DataCube
{
    /// <summary>
    /// PerformanceDataTime 微信接口数据模型。
    /// </summary>
    public class PerformanceDataTime
    {
        public long begin_timestamp { get; set; }
        public long end_timestamp { get; set; }
    }

    /// <summary>
    /// PerformanceDataQuery 微信接口数据模型。
    /// </summary>
    public class PerformanceDataQuery
    {
        public string field { get; set; }
        public string value { get; set; }
    }

    /// <summary>
    /// GetPerformanceData 接口返回结果。
    /// </summary>
    public class GetPerformanceDataJsonResult : WxJsonResult
    {
        public PerformanceData data { get; set; }
    }

    /// <summary>
    /// Performance 数据。
    /// </summary>
    public class PerformanceData
    {
        public PerformanceDataBody body { get; set; }
    }

    /// <summary>
    /// PerformanceDataBody 微信接口数据模型。
    /// </summary>
    public class PerformanceDataBody
    {
        public List<PerformanceDataTable> tables { get; set; }
        public int count { get; set; }
    }

    /// <summary>
    /// PerformanceDataTable 微信接口数据模型。
    /// </summary>
    public class PerformanceDataTable
    {
        public string id { get; set; }
        public List<PerformanceDataLine> lines { get; set; }
        public string zh { get; set; }
    }

    /// <summary>
    /// PerformanceDataLine 微信接口数据模型。
    /// </summary>
    public class PerformanceDataLine
    {
        public List<PerformanceDataField> fields { get; set; }
    }

    /// <summary>
    /// PerformanceDataField 微信接口数据模型。
    /// </summary>
    public class PerformanceDataField
    {
        public string refdate { get; set; }
        public string value { get; set; }
    }
}
