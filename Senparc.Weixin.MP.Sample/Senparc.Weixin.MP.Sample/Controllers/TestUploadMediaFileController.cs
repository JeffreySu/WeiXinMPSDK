using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class TestUploadMediaFileController : Controller
    {
        [HttpPost]
        public ActionResult Index(string token, UploadMediaFileType type, int contentLength /*, HttpPostedFileBase postedFile*/)
        {
            var inputStream = Request.InputStream;
            if (contentLength != inputStream.Length)
            {
                return Content("ContentLength不正确，可能接收错误！");
            }

            if (token!="TOKEN")
            {
                return Content("TOKEN不正确！");
            }

            if (type!= UploadMediaFileType.image)
            {
                return Content("UploadMediaFileType不正确！");
            }

            //储存文件，对比是否上传成功
            using (FileStream ms =new FileStream(Server.MapPath("~/TestUploadMediaFile.jpg"), FileMode.OpenOrCreate))
            {
                inputStream.CopyTo(ms,256);
            }

            return Content("OK");
        }
    }
}