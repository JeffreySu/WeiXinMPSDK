using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(Server.MapPath("~/bin/Senparc.Weixin.MP.dll"));
            TempData["Version"] = string.Format("{0}.{1}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart); //Regex.Match(fileVersionInfo.FileVersion, @"\d+\.\d+");
            return View();
        }
    }
}
