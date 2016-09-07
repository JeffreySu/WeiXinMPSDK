/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_HotelResult.cs
    文件功能描述：语意理解接口酒店服务（hotel）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 酒店服务（hotel）
    /// </summary>
    public class Semantic_HotelResult : BaseSemanticResultJson
    {
        public Semantic_Hotel semantic { get; set; }
    }

    public class Semantic_Hotel : BaseSemanticIntent
    {
        public Semantic_Details_Hotel details { get; set; }
    }

    public class Semantic_Details_Hotel
    {
        /// <summary>
        /// 地点
        /// </summary>
        public Semantic_Location location { get; set; }
        /// <summary>
        /// 入住时间
        /// </summary>
        public Semantic_DateTime start_date { get; set; }
        /// <summary>
        /// 离开时间
        /// </summary>
        public Semantic_DateTime end_date { get; set; }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 价格（单位元）
        /// </summary>
        public Semantic_Number price { get; set; }
        /// <summary>
        /// 距离（单位米）
        /// </summary>
        public Semantic_Number radius { get; set; }
        /// <summary>
        /// 等级：五星级酒店，四星级酒店，三星级酒店，青年旅社，经济型酒店，公寓式酒店
        /// </summary>
        public string level { get; set; }
        /// <summary>
        /// 1（有wifi），0（无wifi）
        /// </summary>
        public int wifi { get; set; }
        /// <summary>
        /// 房间类型：标准间，单人间，双人间，三人间
        /// </summary>
        public string roomtype { get; set; }
        /// <summary>
        /// 优惠信息：0无（默认），1优惠券，2团购
        /// </summary>
        public int coupon { get; set; }
        /// <summary>
        /// 排序类型：0距离（默认），1点评高优先级，2服务质量高优先级，3环境高优先级，4价格高到低，5价格低到高
        /// </summary>
        public int sort { get; set; }
    }
}
