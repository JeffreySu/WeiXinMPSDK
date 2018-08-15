using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.CoreSample.Controllers.NeuChar
{
    /// <summary>
    /// NeuChar 处理程序
    /// </summary>
    public class NeuCharController : Controller
    {
        [HttpPost]
        public IActionResult Sync(/*string json*/)
        {
            //TODO:json可加密



            var stream = Request.GetRequestMemoryStream();
            var postSr = new StreamReader(stream);
            string json = postSr.ReadToEnd();


            //TODO：进行保存
            var dir = Server.GetMapPath(string.Format("~/App_Data/NeuChar"));
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
