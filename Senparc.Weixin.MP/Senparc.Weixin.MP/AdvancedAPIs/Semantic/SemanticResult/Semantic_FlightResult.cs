using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 航班服务（flight）
    /// </summary>
    public class Semantic_FlightResult : BaseSemanticResultJson
    {
        public Semantic_Flight semantic { get; set; }
    }

    public class Semantic_Flight : BaseSemanticIntent
    {
        public Semantic_Details_Flight details { get; set; }
    }

    public class Semantic_Details_Flight
    {
        /// <summary>
        /// 航班号
        /// </summary>
        public string flight_no { get; set; }
        /// <summary>
        /// 出发地
        /// </summary>
        public Semantic_Location start_loc { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        public Semantic_Location end_loc { get; set; }
        /// <summary>
        /// 出发日期
        /// </summary>
        public Semantic_DateTime start_date { get; set; }
        /// <summary>
        /// 返回日期
        /// </summary>
        public Semantic_DateTime end_date { get; set; }
        /// <summary>
        /// 航空公司
        /// </summary>
        public string airline { get; set; }
        /// <summary>
        /// 座位级别（默认无限制）：ECONOMY（经济舱）BIZ（商务舱）FIRST（头等舱）
        /// </summary>
        public string seat { get; set; }
        /// <summary>
        /// 排序类型：0排序无要求（默认），1价格升序，2价格降序，3时间升序，4时间降序
        /// </summary>
        public int sort { get; set; }
    }
}
