/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_TV_InstructionResult.cs
    文件功能描述：语意理解接口电视指令（tv_instruction）[beta]返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 电视指令（tv_instruction）[beta]
    /// </summary>
    public class Semantic_TV_InstructionResult : BaseSemanticResultJson
    {
        public Semantic_TV_Instruction semantic { get; set; }
    }

    public class Semantic_TV_Instruction : BaseSemanticIntent
    {
        public Semantic_Details_TV_Instruction details { get; set; }
    }

    public class Semantic_Details_TV_Instruction
    {
        /// <summary>
        /// 电视台名称
        /// </summary>
        public string tv_name { get; set; }
        /// <summary>
        /// 电视频道名称
        /// </summary>
        public string tv_channel { get; set; }
        /// <summary>
        /// 节目类型
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 数字，根据intent有不同的含义，例如：把声音调到20；换到5台。
        /// </summary>
        public int number { get; set; }
        /// <summary>
        /// 操作值：3D，AV(视频)，AV1(视频1)，AV2(视频2)，HDMI，HDMI1，HDMI2，HDMI3，DTV，ATV，YPBPR，DVI，VGA，USB，ANALOG(模拟电视)，DIGITAL(数字电视)，IMAGE(图像设置)，SCREEN(屏幕比例)，SOUND(声音模式) ，IMAGE_MODEL(图像模式)
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 操作值：OPEN (打开)，CLOSE (关闭)
        /// </summary>
        public string @operator { get; set; }
        /// <summary>
        /// 设备：U(U盘)，CLOUD(云存储)
        /// </summary>
        public string device { get; set; }
        /// <summary>
        /// 文件类型：VIDEO(视频)，TEXT(文本)，APP(安装包)，PIC(照片)，MUSIC(音乐)
        /// </summary>
        public string file_type { get; set; }
    }
}
