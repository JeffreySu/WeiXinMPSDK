using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs.Analysis;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Sample.Models.VD;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public enum AnalysisType
    {
        图文群发每日数据,
        图文群发总数据,
        图文统计数据,
        图文统计分时数据,
        图文分享转发数据,
        图文分享转发分时数据,
        接口分析数据,
        接口分析分时数据,
        消息发送概况数据,
        消息分送分时数据,
        消息发送周数据,
        消息发送月数据,
        消息发送分布数据,
        消息发送分布周数据,
        消息发送分布月数据,
        用户增减数据,
        累计用户数据,
    }

    public class AnalysisController : BaseController
    {
        public ActionResult Index()
        {
            Analysis_IndexVD vd = new Analysis_IndexVD()
                {
                    StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                    EndDate = DateTime.Now.ToString("yyyy-MM-dd"),
                };

            return View(vd);
        }

        [HttpPost]
        public ActionResult Index(Analysis_IndexVD vd_Form)
        {
            WxJsonResult result = null;

            var accessToken = CommonAPIs.AccessTokenContainer.TryGetToken(vd_Form.AppId, vd_Form.AppSecret);

            switch (vd_Form.AnalysisType)
            {
                case AnalysisType.图文群发每日数据:
                    result = result as ArticleSummaryResultJson;
                    result = AnalysisApi.GetArticleSummary(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.图文群发总数据:
                    result = result as ArticleTotalResultJson;
                    result = AnalysisApi.GetArticleTotal(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.图文统计数据:
                    result = result as UserReadResultJson;
                    result = AnalysisApi.GetUserRead(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.图文统计分时数据:
                    result = result as UserReadHourResultJson;
                    result = AnalysisApi.GetUserReadHour(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.图文分享转发数据:
                    result = result as UserShareResultJson;
                    result = AnalysisApi.GetUserShare(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.图文分享转发分时数据:
                    result = result as UserShareHourResultJson;
                    result = AnalysisApi.GetUserShareHour(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.接口分析数据:
                    result = result as UserSummaryResultJson;
                    result = AnalysisApi.GetInterfaceSummary(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.接口分析分时数据:
                    result = result as UserCumulateResultJson;
                    result = AnalysisApi.GetInterfaceSummaryHour(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.消息发送概况数据:
                    result = result as UpStreamMsgResultJson;
                    result = AnalysisApi.GetUpStreamMsg(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.消息分送分时数据:
                    result = result as UpStreamMsgHourResultJson;
                    result = AnalysisApi.GetUpStreamMsgHour(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.消息发送周数据:
                    result = result as UpStreamMsgWeekResultJson;
                    result = AnalysisApi.GetUpStreamMsgWeek(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.消息发送月数据:
                    result = result as UpStreamMsgMonthResultJson;
                    result = AnalysisApi.GetUpStreamMsgMonth(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.消息发送分布数据:
                    result = result as UpStreamMsgDistResultJson;
                    result = AnalysisApi.GetUpStreamMsgDist(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.消息发送分布周数据:
                    result = result as UpStreamMsgDistWeekResultJson;
                    result = AnalysisApi.GetUpStreamMsgDistWeek(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.消息发送分布月数据:
                    result = result as UpStreamMsgDistMonthResultJson;
                    result = AnalysisApi.GetUpStreamMsgDistMonth(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.用户增减数据:
                    result = result as UserSummaryResultJson;
                    result = AnalysisApi.GetUserSummary(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                case AnalysisType.累计用户数据:
                    result = result as UserCumulateResultJson;
                    result = AnalysisApi.GetUserCumulate(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    break;
                default:
                    break;
            }
            string s = result.ToString();

            vd_Form.Result = s;
            return View(vd_Form);
        }
    }
}
