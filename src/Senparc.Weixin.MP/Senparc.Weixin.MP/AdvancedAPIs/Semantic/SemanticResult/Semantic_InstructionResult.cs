/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_InstructionResult.cs
    文件功能描述：语意理解接口通用指令（instruction）[beta]返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 通用指令（instruction）[beta]
    /// </summary>
    public class Semantic_InstructionResult : BaseSemanticResultJson
    {
        public Semantic_Instruction semantic { get; set; }
    }

    public class Semantic_Instruction : BaseSemanticIntent
    {
        public Semantic_Details_Instruction details { get; set; }
    }

    public class Semantic_Details_Instruction
    {
        /// <summary>
        /// 数字，根据intent有不同的含义，例如：把声音调到20；
        /// </summary>
        public int number { get; set; }
        /// <summary>
        /// 操作值：STANDARD(标准模式)，MUTE(静音模式)，VIBRA(振动模式)，INAIR(飞行模式)，RING(铃声)，WALLPAPER(壁纸)，TIME(时间)，WIFI(无线网络)，BLUETOOTH(蓝牙)，GPS(GPS)，NET(移动网络)，SPACE(存储空间)，INPUT(输入法设置)，LANGUAGE(语言设置)，PERSONAL(个性化设置)，SCREEN(屏幕保护)，FACTORY_SETTINGS (出厂设置)，SI(系统信息)，UPDATE(系统更新)，PLAY(播放)，OPEN_MUSIC(开机音乐)，TIME_ON(定时开机)，TIME_OFF(定时关机)
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 操作值：OPEN (打开)，CLOSE (关闭)，MIN (最小)，MAX (最大)，UP (变大)，DOWN (变小)
        /// </summary>
        public string @operator { get; set; }
    }
}
