/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_CouponResult.cs
    文件功能描述：语意理解接口团购服务（coupon）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 团购服务（coupon）
    /// </summary>
    public class Semantic_CouponResult : BaseSemanticResultJson
    {
        public Semantic_Coupon semantic { get; set; }
    }

    public class Semantic_Coupon : BaseSemanticIntent
    {
        public Semantic_Details_Coupon details { get; set; }
        /// <summary>
        /// SEARCH 普通查询
        /// </summary>
        public string intent { get; set; }
    }

    public class Semantic_Details_Coupon
    {
        /// <summary>
        /// 地点
        /// </summary>
        public Semantic_Location location { get; set; }
        /// <summary>
        /// 价格（单位元）
        /// </summary>
        public Semantic_Number price { get; set; }
        /// <summary>
        /// 距离（单位米）
        /// </summary>
        public Semantic_Number radius { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 优惠信息：0所有（默认），1优惠券，2团购
        /// </summary>
        public int coupon { get; set; }
        /// <summary>
        /// 排序类型：0距离（默认），1价格高到低，2价格低到高
        /// </summary>
        public int sort { get; set; }
    }
}
