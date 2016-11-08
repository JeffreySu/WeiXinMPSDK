using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
using ZXing.Aztec.Internal;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class AsyncMethodsController : AsyncController
    {
        private string appId = WebConfigurationManager.AppSettings["WeixinAppId"];
        private string appSecret = WebConfigurationManager.AppSettings["WeixinAppSecret"];

        public ActionResult Index()
        {
            return View();
        }



        public Task<ActionResult> QrCodeTest()
        {
            return Task.Factory.StartNew<ActionResult>(() =>
            {
                var ticks = DateTime.Now.Ticks.ToString();
                var sceneId = int.Parse(ticks.Substring(ticks.Length - 7, 7));

                var qrResult = QrCodeApi.CreateAsync(appId, 100, sceneId, QrCode_ActionName.QR_SCENE, "QrTest").Result;
                var qrCodeUrl = QrCodeApi.GetShowQrCodeUrl(qrResult.ticket);

                return Redirect(qrCodeUrl);

            }).ContinueWith<ActionResult>(task => task.Result);
        }

        public Task<ActionResult> TemplateMessageTest()
        {
            return Task.Factory.StartNew<ActionResult>(() =>
            {
                var ticks = DateTime.Now.Ticks.ToString();
                var sceneId = int.Parse(ticks.Substring(ticks.Length - 7, 7));

                var qrResult = QrCodeApi.CreateAsync(appId, 100, sceneId, QrCode_ActionName.QR_SCENE, "QrTest").Result;
                var qrCodeUrl = QrCodeApi.GetShowQrCodeUrl(qrResult.ticket);

                return Redirect(qrCodeUrl);

            }).ContinueWith<ActionResult>(task => task.Result);
        }
    }
}