/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：Position.cs
    文件功能描述：Position 相关功能


    创建标识：Senparc - 20231204

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/


namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    /// <summary>
    /// 图像中的像素坐标点。
    /// </summary>
    public class Pos_Location
    {
        /// <summary>
        /// 横坐标。
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// 纵坐标。
        /// </summary>
        public int y { get; set; }
    }

    /// <summary>
    /// 识别对象在图片中的四边形位置。
    /// </summary>
    public class Pos
    {
        /// <summary>
        /// 左上角坐标。
        /// </summary>
        public Pos_Location left_top { get; set; }
        /// <summary>
        /// 右上角坐标。
        /// </summary>
        public Pos_Location right_top { get; set; }
        /// <summary>
        /// 右下角坐标。
        /// </summary>
        public Pos_Location right_bottom { get; set; }
        /// <summary>
        /// 左下角坐标。
        /// </summary>
        public Pos_Location left_bottom { get; set; }
    }

    /// <summary>
    /// OCR 卡片位置包装对象。
    /// </summary>
    public class Position
    {
        /// <summary>
        /// 卡片在图片中的四边形位置。
        /// </summary>
        public Pos pos { get; set; }
    }
}
