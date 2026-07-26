/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ImgSize.cs
    文件功能描述：ImgSize 相关功能


    创建标识：Senparc - 20231204

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/


namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    /// <summary>
    /// 微信图像接口返回的原图尺寸。
    /// </summary>
    public class ImgSize
    {
        /// <summary>
        /// 图片宽度，单位为像素。
        /// </summary>
        public int w { get; set; }

        /// <summary>
        /// 图片高度，单位为像素。
        /// </summary>
        public int h { get; set; }
    }
}
