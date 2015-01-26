using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    //接口详见：http://mp.weixin.qq.com/wiki/8/30ed81ae38cf4f977194bf1a5db73668.html
    
    /// <summary>
    /// 接口分析数据接口
    /// 最大时间跨度是指一次接口调用时最大可获取数据的时间范围，如最大时间跨度为7是指最多一次性获取7天的数据。
    /// 注意：所有的日期请使用【yyyy-MM-dd】的格式
    /// </summary>
    public static class InterfaceAnalysis
    {
        /// <summary>
        /// 获取接口分析数据（getinterfacesummary）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UserSummaryResultJson GetInterfaceSummary(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getinterfacesummary?access_token={0}";

            var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

            return CommonJsonSend.Send<UserSummaryResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取接口分析分时数据（getinterfacesummaryhour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UserCumulateResultJson GetInterfaceSummaryHour(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getinterfacesummaryhour?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UserCumulateResultJson>(accessToken, urlFormat, data);
        }
    }
}
