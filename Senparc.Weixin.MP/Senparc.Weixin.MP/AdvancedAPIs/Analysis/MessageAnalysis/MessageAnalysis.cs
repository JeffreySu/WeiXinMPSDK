using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    //接口详见：http://mp.weixin.qq.com/wiki/12/32d42ad542f2e4fc8a8aa60e1bce9838.html
    
    /// <summary>
    /// 消息分析数据接口
    /// 最大时间跨度是指一次接口调用时最大可获取数据的时间范围，如最大时间跨度为7是指最多一次性获取7天的数据。
    /// 注意：所有的日期请使用【yyyy-MM-dd】的格式
    /// </summary>
    public static class MessageAnalysis
    {
        /// <summary>
        /// 获取消息发送概况数据（getupstreammsg）
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UpStreamMsgResultJson GetUpStreamMsg(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsg?access_token={0}";

            var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

            return CommonJsonSend.Send<UpStreamMsgResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取消息分送分时数据（getupstreammsghour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UpStreamMsgHourResultJson GetUpStreamMsgHour(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsghour?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UpStreamMsgHourResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取消息发送周数据（getupstreammsgweek）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UpStreamMsgWeekResultJson GetUpStreamMsgWeek(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsgweek?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UpStreamMsgWeekResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取消息发送月数据（getupstreammsgmonth）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UpStreamMsgMonthResultJson GetUpStreamMsgMonth(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsgmonth?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UpStreamMsgMonthResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取消息发送分布数据（getupstreammsgdist）
        /// 最大时间跨度 15
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UpStreamMsgDistResultJson GetUpStreamMsgDist(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsgdist?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UpStreamMsgDistResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取消息发送分布周数据（getupstreammsgdistweek）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UpStreamMsgDistWeekResultJson GetUpStreamMsgDistWeek(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsgdistweek?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UpStreamMsgDistWeekResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取消息发送分布月数据（getupstreammsgdistmonth）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UpStreamMsgDistMonthResultJson GetUpStreamMsgDistMonth(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsgdistmonth?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UpStreamMsgDistMonthResultJson>(accessToken, urlFormat, data);
        }
    }
}
