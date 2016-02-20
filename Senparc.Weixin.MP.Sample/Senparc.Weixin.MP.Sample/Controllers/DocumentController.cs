using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Sample.CommonService.Download;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class DocumentController : AsyncController
    {
        private string appId = ConfigurationManager.AppSettings["WeixinAppId"];

        public ActionResult Index()
        {
            var guid = Guid.NewGuid().ToString("n");
            ViewData["Guid"] = guid;

            var configHelper = new ConfigHelper(this.HttpContext);
            var qrCodeId = configHelper.GetQrCodeId();
            var qrResult = AdvancedAPIs.QrCodeApi.Create(appId, 10000, qrCodeId);
            ViewData["QrCodeUrl"] = qrResult.url;

            ConfigHelper.CodeCollection[guid] = new CodeRecord()
            {
                Key = guid,
                QrCodeId = qrCodeId,
                QrCodeTicket = qrResult
            };//添加对应关系

            return View();
        }

        public Task<ActionResult> GetQrCode(string guid)
        {
            return Task.Factory.StartNew<ActionResult>(() =>
            {

                return Content(qrResult.url);
            });

        }

        /// <summary>
        /// 检查是否可下载
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ActionResult> CheckDownload(string guid)
        {
            return Task.Factory.StartNew<ActionResult>(() =>
            {
                var success = ConfigHelper.CodeCollection.ContainsKey(guid) && !ConfigHelper.CodeCollection[guid].Used;
                return Json(new { success = true });
            });
        }

        /// <summary>
        /// 尝试下载
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Task<FileResult> Download(string guid)
        {
            return Task.Factory.StartNew<FileResult>(() =>
            {
                var success = ConfigHelper.CodeCollection.ContainsKey(guid) && !ConfigHelper.CodeCollection[guid].Used;
                if (!success)
                {
                    var file = File(Encoding.UTF8.GetBytes("未通过审核，或此二维码已过期"), "text/plain");
                    file.FileDownloadName = "下载失败.txt";
                    return file;
                }
                else
                {
                    var codeRecord = ConfigHelper.CodeCollection[guid];
                    codeRecord.Used = true;
                    var configHelper = new ConfigHelper(this.HttpContext);
                    var fileStream = configHelper.Download(codeRecord.Version);
                    var file = File(fileStream, "application/octet-stream");
                    file.FileDownloadName = string.Format("Senparc.Weixin-v{0}.rar", codeRecord.Version);
                    return file;
                }
            });
        }
    }
}