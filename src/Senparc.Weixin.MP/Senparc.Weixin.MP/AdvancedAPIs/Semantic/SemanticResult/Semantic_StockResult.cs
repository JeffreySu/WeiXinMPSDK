/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_StockResult.cs
    文件功能描述：语意理解接口股票服务（stock）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 股票服务（stock）
    /// 注：1、币种单位同上市所在地；2、如果用户输入的时间是未来的时间，那么结果展示的是当前时间的信息。
    /// </summary>
    public class Semantic_StockResult : BaseSemanticResultJson
    {
        public Semantic_Stock semantic { get; set; }
    }

    public class Semantic_Stock : BaseSemanticIntent
    {
        public Semantic_Details_Stock details { get; set; }
        public List<Semantic_Stock_Result> result { get; set; }
    }

    public class Semantic_Details_Stock
    {
        /// <summary>
        /// 股票名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 标准股票代码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 市场：sz,sh,hk,us
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public Semantic_DateTime datetime { get; set; }
    }

    public class Semantic_Stock_Result
    {
        /// <summary>
        /// 股票代码
        /// </summary>
        public string cd { get; set; }
        /// <summary>
        /// 当前价
        /// </summary>
        public string np { get; set; }
        /// <summary>
        /// 涨幅
        /// </summary>
        public string ap { get; set; }
        /// <summary>
        /// 涨幅比例
        /// </summary>
        public string apn { get; set; }
        /// <summary>
        /// 最高价
        /// </summary>
        public string tp_max { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public string tp_min { get; set; }
        /// <summary>
        /// 成交量(单位：万)
        /// </summary>
        public string dn { get; set; }
        /// <summary>
        /// 成交额(单位：亿)
        /// </summary>
        public string de { get; set; }
        /// <summary>
        /// 市盈率
        /// </summary>
        public string pe { get; set; }
        /// <summary>
        /// 市值(单位：亿)
        /// </summary>
        public string sz { get; set; }
    }
}
