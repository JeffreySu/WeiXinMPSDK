using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample.Controllers.NeuChar
{
    /// <summary>
    /// NeuChar 处理程序
    /// </summary>
    public class NeuCharController : Controller
    {
        [HttpPost]
        public ActionResult Sync(/*string json*/)
        {
            //TODO:json可加密



            var stream = Request.InputStream;
            var postSr = new StreamReader(stream);
            string json = postSr.ReadToEnd();


            //TODO：进行保存
            var dir = Server.MapPath(string.Format("~/App_Data/NeuChar"));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var fileName = Path.Combine(dir, "json.json");
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            using (var fs = new FileStream(fileName, FileMode.CreateNew))
            {
                using (var sr = new StreamWriter(fs))
                {
                    sr.Write(json);
                    sr.Flush();
                }
            }

            return Json(new { msg = "ok", json = json });
        }
    }
}