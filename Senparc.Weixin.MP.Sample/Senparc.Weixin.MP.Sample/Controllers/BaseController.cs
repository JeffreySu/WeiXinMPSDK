using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var mpFileVersionInfo = FileVersionInfo.GetVersionInfo(Server.MapPath("~/bin/Senparc.Weixin.MP.dll"));
            var extensionFileVersionInfo = FileVersionInfo.GetVersionInfo(Server.MapPath("~/bin/Senparc.Weixin.MP.MvcExtension.dll"));
            TempData["MpVersion"] = string.Format("{0}.{1}", mpFileVersionInfo.FileMajorPart, mpFileVersionInfo.FileMinorPart); //Regex.Match(fileVersionInfo.FileVersion, @"\d+\.\d+");
            TempData["ExtensionVersion"] = string.Format("{0}.{1}", extensionFileVersionInfo.FileMajorPart, extensionFileVersionInfo.FileMinorPart); //Regex.Match(fileVersionInfo.FileVersion, @"\d+\.\d+");

            base.OnResultExecuting(filterContext);
        }

    }
}
