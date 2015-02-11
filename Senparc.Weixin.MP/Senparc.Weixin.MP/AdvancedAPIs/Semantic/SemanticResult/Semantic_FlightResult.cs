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
        public Details_Flight details { get; set; }
    }

    public class Details_Flight
    {
        public string flight_no { get; set; }//航班号
        public Semantic_Location start_loc { get; set; }//出发地
        public Semantic_Location end_loc { get; set; }//目的地
        //public Semantic_DateTime start_date { get; set; }//出发日期
        //public Semantic_DateTime end_date { get; set; }//返回日期
        public string airline { get; set; }//航空公司
        public string seat { get; set; }//座位级别（默认无限制）：ECONOMY（经济舱）BIZ（商务舱）FIRST（头等舱）
        public int sort { get; set; }//排序类型：0排序无要求（默认），1价格升序，2价格降序，3时间升序，4时间降序
    }
}
