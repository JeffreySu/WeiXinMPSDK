/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：DrivingJsonResult.cs
    文件功能描述：OCR 行驶证识别返回结果
    
    
    创建标识：yaofeng - 20231204

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    /// <summary>
    /// 行驶证识别
    /// </summary>
    public class DrivingJsonResult : WxJsonResult
    {
        /// <summary>
        /// 车牌号码 粤xxxxx
        /// </summary>
        public string plate_num { get; set; }

        /// <summary>
        /// 车辆类型 小型普通客车
        /// </summary>
        public string vehicle_type { get; set; }

        /// <summary>
        /// 所有人
        /// </summary>
        public string owner { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string addr { get; set; }

        /// <summary>
        /// 使用性质 非营运
        /// </summary>
        public string use_character { get; set; }

        /// <summary>
        /// 品牌型号 江淮牌HFCxxxxxxx
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// 车辆识别代号
        /// </summary>
        public string vin { get; set; }

        /// <summary>
        /// 发动机号码
        /// </summary>
        public string engine_num { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        public string register_date { get; set; }

        /// <summary>
        /// 发证日期
        /// </summary>
        public string issue_date { get; set; }

        /// <summary>
        /// 车牌号码
        /// </summary>
        public string plate_num_b { get; set; }

        /// <summary>
        /// 号牌
        /// </summary>
        public string record { get; set; }

        /// <summary>
        /// 核定载人数
        /// </summary>
        public string passengers_num { get; set; }

        /// <summary>
        /// 总质量
        /// </summary>
        public string total_quality { get; set; }

        /// <summary>
        /// 整备质量
        /// </summary>
        public string prepare_quality { get; set; }

        /// <summary>
        /// 外廓尺寸
        /// </summary>
        public string overall_size { get; set; }

        /// <summary>
        /// 卡片正面位置（检测到卡片正面才会返回）
        /// </summary>
        public Position card_position_front { get; set; }

        /// <summary>
        /// 卡片反面位置（检测到卡片反面才会返回）
        /// </summary>
        public Position card_position_back { get; set; }

        /// <summary>
        /// 图片大小
        /// </summary>
        public ImgSize img_size { get; set; }
    }
}
