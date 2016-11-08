using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
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

        public Task<ActionResult> TemplateMessageTest(string checkcode)
        {
            return Task.Factory.StartNew<ActionResult>(() =>
            {
                var openId = CustomMessageHandler.TemplateMessageCollection.ContainsKey(checkcode)
                    ? CustomMessageHandler.TemplateMessageCollection[checkcode]
                    : null;



                if (openId == null)
                {
                    return Content("验证码已过期或不存在！请在“盛派网络小助手”公众号输入“tm”获取验证码。");
                }
                else
                {
                    CustomMessageHandler.TemplateMessageCollection.Remove(checkcode);


                       var templateId = "cCh2CTTJIbVZkcycDF08n96FP-oBwyMVrro8C2nfVo4";
                    var testData = new //TestTemplateData()
                    {
                        first = new TemplateDataItem("【异步模板消息测试】"),
                        keyword1 = new TemplateDataItem(openId),
                        keyword2 = new TemplateDataItem("网页测试"),
                        keyword3 = new TemplateDataItem(DateTime.Now.ToString("O")),
                        remark = new TemplateDataItem("更详细信息，请到Senparc.Weixin SDK官方网站（http://sdk.weixin.senparc.com）查看！")
                    };

                    var result = TemplateApi.SendTemplateMessageAsync(appId, openId, templateId, null, testData).Result;
                    return Content("异步模板消息已经发送到【盛派网络小助手】公众号，请查看。此前的验证码已失效，如需继续测试，请重新获取验证码。");
                }
            }).ContinueWith<ActionResult>(task => task.Result);
        }
    }
}