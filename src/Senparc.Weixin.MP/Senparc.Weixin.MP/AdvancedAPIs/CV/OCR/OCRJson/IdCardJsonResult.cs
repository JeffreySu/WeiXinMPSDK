/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：IdCardJsonResult.cs
    文件功能描述：OCR 身份证识别返回结果
    
    
    创建标识：yaofeng - 20231204

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    /// <summary>
    /// 身份证OCR识别
    /// </summary>
    public class IdCardJsonResult : WxJsonResult
    {
        /// <summary>
        /// Front / Back
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string addr { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string nationality { get; set; }

        /// <summary>
        /// 有效期 国徽面特有
        /// </summary>
        public string valid_date { get; set; }
    }
}
