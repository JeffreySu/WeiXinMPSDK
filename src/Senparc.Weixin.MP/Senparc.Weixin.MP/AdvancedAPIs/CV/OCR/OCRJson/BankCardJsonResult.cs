/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：BankCardJsonResult.cs
    文件功能描述：OCR 银行卡识别返回结果
    
    
    创建标识：yaofeng - 20231204

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    /// <summary>
    /// 银行卡识别
    /// </summary>
    public class BankCardJsonResult : WxJsonResult
    {
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string number { get; set; }
    }
}
