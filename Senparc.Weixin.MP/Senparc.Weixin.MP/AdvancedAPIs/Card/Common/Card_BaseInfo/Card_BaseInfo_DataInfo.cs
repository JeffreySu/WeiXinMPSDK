/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Card_BaseInfo_Sku.cs
    文件功能描述：基本的卡券使用日期，有效期的信息，所有卡券通用。
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 使用日期，有效期的信息
    /// </summary>
    public class Card_BaseInfo_DateInfo
    {
        /// <summary>
        /// 使用时间的类型 1：固定日期区间，2：固定时长（自领取后按天算）
        /// 必填
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 固定日期区间专用，表示起用时间。从1970 年1 月1 日00:00:00 至起用时间的秒数，最终需转换为字符串形态传入，下同。（单位为秒）
        /// 必填
        /// </summary>
        public long begin_timestamp { get; set; }
        /// <summary>
        /// 固定日期区间专用，表示结束时间。（单位为秒）
        /// 必填
        /// </summary>
        public long end_timestamp { get; set; }
        /// <summary>
        /// 固定时长专用，表示自领取后多少天内有效。（单位为天）
        /// 必填
        /// </summary>
        public int fixed_term { get; set; }
        /// <summary>
        /// 固定时长专用，表示自领取后多少天开始生效。（单位为天）
        /// 必填
        /// </summary>
        public int fixed_begin_term { get; set; }
    }

}
