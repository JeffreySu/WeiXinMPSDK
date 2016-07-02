using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs.Analysis;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
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
                    EndDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                };
            
            

            return View(vd);
        }

        [HttpPost]
        public ActionResult Index(Analysis_IndexVD vd_Form)
        {
            IBaseAnalysisResult result1 = null;

            //if (ModelState.IsValid)
            //{

            //    var strongResult = new AnalysisResultJson<ArticleSummaryItem>();
            //    result1 = strongResult;//yin yong
            //    (result1 as AnalysisResultJson<ArticleSummaryItem>).list.Add(new ArticleSummaryItem());
            //}

            //if (result1 is AnalysisResultJson<ArticleSummaryItem>)
            //{
            //    var Strong = result1 as AnalysisResultJson<ArticleSummaryItem>;
            //   // Strong.list.First().ori_page_read_count
            //}




            IBaseAnalysisResult result = null;

            var accessToken = CommonAPIs.AccessTokenContainer.TryGetToken(vd_Form.AppId, vd_Form.AppSecret);

            switch (vd_Form.AnalysisType)
            {
                case AnalysisType.图文群发每日数据:
                    //var strongResult = new AnalysisResultJson<ArticleSummaryItem>();
                    //result = strongResult;
                    result = AnalysisApi.GetArticleSummary(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                    vd_Form.Result = (result as AnalysisResultJson<ArticleSummaryItem>).list.ToString();
                    break;
                //case AnalysisType.图文群发总数据:
                //    result = new AnalysisResultJson<ArticleTotalItem>();
                //    result = AnalysisApi.GetArticleTotal(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 2:
                //    result = result as UserReadResultJson;
                //    result = AnalysisApi.GetUserRead(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 3:
                //    result = result as UserReadHourResultJson;
                //    result = AnalysisApi.GetUserReadHour(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 4:
                //    result = result as UserShareResultJson;
                //    result = AnalysisApi.GetUserShare(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 5:
                //    result = result as UserShareHourResultJson;
                //    result = AnalysisApi.GetUserShareHour(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 6:
                //    result = result as UserSummaryResultJson;
                //    result = AnalysisApi.GetInterfaceSummary(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 7:
                //    result = result as UserCumulateResultJson;
                //    result = AnalysisApi.GetInterfaceSummaryHour(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 8:
                //    result = result as UpStreamMsgResultJson;
                //    result = AnalysisApi.GetUpStreamMsg(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 9:
                //    result = result as UpStreamMsgHourResultJson;
                //    result = AnalysisApi.GetUpStreamMsgHour(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 10:
                //    result = result as UpStreamMsgWeekResultJson;
                //    result = AnalysisApi.GetUpStreamMsgWeek(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 11:
                //    result = result as UpStreamMsgMonthResultJson;
                //    result = AnalysisApi.GetUpStreamMsgMonth(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 12:
                //    result = result as UpStreamMsgDistResultJson;
                //    result = AnalysisApi.GetUpStreamMsgDist(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 13:
                //    result = result as UpStreamMsgDistWeekResultJson;
                //    result = AnalysisApi.GetUpStreamMsgDistWeek(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 14:
                //    result = result as UpStreamMsgDistMonthResultJson;
                //    result = AnalysisApi.GetUpStreamMsgDistMonth(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 15:
                //    result = result as UserSummaryResultJson;
                //    result = AnalysisApi.GetUserSummary(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                //case 16:
                //    result = result as UserCumulateResultJson;
                //    result = AnalysisApi.GetUserCumulate(accessToken, vd_Form.StartDate, vd_Form.EndDate);
                //    break;
                default:
                    break;
            }

            //result.ListObj;

            return View(vd_Form);
        }
    }
}
