/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SemanticAgreement.cs
    文件功能描述：语意理解相关协议
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    #region 语义输入协议

    /// <summary>
    /// 语义输入协议
    /// </summary>
    public class SemanticPostData
    {
        /// <summary>
        /// 输入文本串
        /// 必填
        /// </summary>
        public string query { get; set; }
        /// <summary>
        /// 需要使用的服务类别，多个用,隔开，不能为空
        /// 必填
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 纬度坐标，与经度同时传入；与城市二选一传入
        /// 见说明，选填
        /// </summary>
        public float latitude { get; set; }
        /// <summary>
        /// 经度坐标，与纬度同时传入；与城市二选一传入
        /// 见说明，选填
        /// </summary>
        public float longitude { get; set; }
        /// <summary>
        /// 城市名称，与经纬度二选一传入
        /// 见说明，选填
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区域名称，在城市存在的情况下可省；与经纬度二选一传入`
        /// 见说明，选填
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// App id，开发者的唯一标识，用于区分开放者，如果为空，则没法使用上下文理解功能。
        /// 非必填
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 用户唯一id（并非开发者id），用于区分该开发者下不同用户，如果为空，则没法使用上下文理解功能。appid和uid同时存在的情况下，才可以使用上下文理解功能。
        /// 非必填
        /// </summary>
        public string uid { get; set; }
    }

    #endregion

    #region 时间相关协议

    /// <summary>
    /// 时间相关协议datetime
    /// </summary>
    public class Semantic_DateTime
    {
        /// <summary>
        /// 单时间的描述协议类型：“DT_SINGLE”。DT_SINGLE又细分为两个类别：DT_ORI和DT_INFER。DT_ORI是字面时间，比如：“上午九点”；DT_INFER是推理时间，比如：“提前5分钟”
        /// 时间段的描述协议类型：“DT_INTERVAL”
        /// 重复时间的描述协议类型：“DT_REPEAT”  DT_ REPEAT又细分为两个类别：DT_RORI和DT_RINFER。DT_RORI是字面时间，比如：“每天上午九点”；DT_RINFER是推理时间，比如：“工作日除外”
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 24小时制，格式：HH:MM:SS，默认为00:00:00
        /// </summary>
        public string time { get; set; }
    }

    /// <summary>
    /// 单时间的描述协议datetime
    /// </summary>
    public class Semantic_SingleDateTime : Semantic_DateTime
    {
        /// <summary>
        /// 格式：YYYY-MM-DD，默认是当天时间
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// date的原始字符串
        /// </summary>
        public string date_ori { get; set; }
        /// <summary>
        /// Time的原始字符串
        /// </summary>
        public string time_ori { get; set; }
    }

    /// <summary>
    /// 时间段的描述协议datetime
    /// </summary>
    public class Semantic_IntervalDateTime : Semantic_DateTime
    {
        /// <summary>
        /// 格式：YYYY-MM-DD，默认是当天时间
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// date的原始字符串
        /// </summary>
        public string date_ori { get; set; }
        /// <summary>
        /// time的原始字符串
        /// </summary>
        public string time_ori { get; set; }
        /// <summary>
        /// 格式：YYYY-MM-DD，默认是当前时间
        /// </summary>
        public string end_date { get; set; }
        /// <summary>
        /// date的原始字符串
        /// </summary>
        public string end_date_ori { get; set; }
        /// <summary>
        /// 24小时制，格式：HH:MM:SS
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// Time的原始字符串
        /// </summary>
        public string end_time_ori { get; set; }

    }

    /// <summary>
    /// 重复时间的描述协议datetime
    /// </summary>
    public class Semantic_RepeatDateTime : Semantic_DateTime
    {
        /// <summary>
        /// time的原始字符串
        /// </summary>
        public string time_ori { get; set; }
        /// <summary>
        /// 重复标记：0000000 注：依次代表周日，周六，…，周一；1表示该天要重复，0表示不重复
        /// </summary>
        public string repeat { get; set; }
        /// <summary>
        /// date的原始字符串
        /// </summary>
        public string repeat_ori { get; set; }
    }

    #endregion

    #region 地点相关协议

    /// <summary>
    /// 地点相关协议
    /// </summary>
    public class Semantic_Location
    {
        /// <summary>
        /// 大类型：“LOC”  LOC又细分为如下类别：LOC_COUNTRY、LOC_PROVINCE、LOC_CITY、LOC_TOWN、LOC_POI、NORMAL_POI。
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 省全称，例如：广东省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 省简称，例如：广东|粤
        /// </summary>
        public string province_simple { get; set; }
        /// <summary>
        /// 市全称，例如：北京市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 市简称，例如：北京
        /// </summary>
        public string city_simple { get; set; }
        /// <summary>
        /// 县区全称，例如：海淀区
        /// </summary>
        public string town { get; set; }
        /// <summary>
        /// 县区简称，例如：海淀
        /// </summary>
        public string town_simple { get; set; }
        /// <summary>
        /// poi详细地址
        /// </summary>
        public string poi { get; set; }
        /// <summary>
        /// 原始地名串
        /// </summary>
        public string loc_ori { get; set; }
    }

    #endregion

    #region 数字相关协议

    /// <summary>
    /// 说明：begin或end，如果为“-1”表示无上限或者下限，如果为“-2”，表示该字段无信息。
    ///NUM_PRICE：价格相关，例：200元左右
    ///NUM_RADIUS：距离相关，例：200米以内
    ///NUM_DISCOUNT：折扣相关，例：八折
    ///NUM_SEASON：部，季相关，例：第一部
    ///NUM_EPI：集相关，例：第一集
    /// </summary>
    public class Semantic_Number
    {
        /// <summary>
        /// 大类型：“NUMBER”  NUMBER又细分为如下类别：NUM_PRICE、NUM_PADIUS、NUM_DISCOUNT、NUM_SEASON、NUM_EPI、NUM_CHAPTER。
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 开始
        /// </summary>
        public string begin { get; set; }
        /// <summary>
        /// 结束
        /// </summary>
        public string end { get; set; }
    }

    #endregion
}
