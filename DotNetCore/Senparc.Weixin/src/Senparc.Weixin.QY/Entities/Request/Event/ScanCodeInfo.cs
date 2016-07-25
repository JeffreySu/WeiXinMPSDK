/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ScanCodeInfo.cs
    文件功能描述：扫码事件中的ScanCodeInfo
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 扫码事件中的ScanCodeInfo
    /// </summary>
    public class ScanCodeInfo
    {
        public string ScanType { get; set; }
        public string ScanResult { get; set; }
    }
}
