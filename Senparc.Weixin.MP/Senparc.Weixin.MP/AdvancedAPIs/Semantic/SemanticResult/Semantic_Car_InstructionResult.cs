/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_Car_InstructionResult.cs
    文件功能描述：语意理解接口车载指令（car_instruction）[beta]返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 车载指令（car_instruction）[beta]
    /// </summary>
    public class Semantic_Car_InstructionResult : BaseSemanticResultJson
    {
        public Semantic_Car_Instruction semantic { get; set; }
    }

    public class Semantic_Car_Instruction : BaseSemanticIntent
    {
        public Semantic_Details_Car_Instruction details { get; set; }
    }

    public class Semantic_Details_Car_Instruction
    {
        /// <summary>
        /// 数字，根据intent有不同的含义，例如：把温度调到20度
        /// </summary>
        public int number { get; set; }
        /// <summary>
        /// 窗户位置：1(司机)，2(副驾驶)，3(司机后面)，4(副驾驶后面)，5(天窗)
        /// </summary>
        public int position { get; set; }
        /// <summary>
        /// 操作值：OPEN (打开)，CLOSE (关闭)，MIN (最小)，MAX (最大)，UP (变大)，DOWN (变小)
        /// </summary>
        public string @operator { get; set; }
    }
}
