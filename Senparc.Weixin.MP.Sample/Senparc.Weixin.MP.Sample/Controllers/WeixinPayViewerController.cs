using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.WeixinPayLib;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class WeixinPayViewerController : Controller
    {
        //
        // GET: /WeixinPayView/

        public ActionResult Index()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("WeixinPay_PartnerId:{0}<br />", WeixinPayUtil.PartnerId);

            return Content(sb.ToString());
        }

    }
}
