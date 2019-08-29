using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Senparc.CO2NET.Extensions;
//using Senparc.CO2NET.Helpers.Extensions;
using Senparc.Weixin.MP.AdvancedAPIs.WiFi;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Sample.CommonService.Download;
//using Senparc.Weixin.MP.Sample.CommonService.Download;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class DocumentController : BaseController
    {
        private string appId = Config.SenparcWeixinSetting.WeixinAppId;

        private bool CheckCanDownload(string guid)
        {
            return ConfigHelper.CodeCollection.ContainsKey(guid) && ConfigHelper.CodeCollection[guid].AllowDownload;
        }

        public ActionResult Index()
        {
            var guid = Guid.NewGuid().ToString("n");
            ViewData["Guid"] = guid;

            var configHelper = new ConfigHelper();

            try
            {
                //chm二维码
                var qrCodeId = configHelper.GetQrCodeId();
                var qrResult = AdvancedAPIs.QrCodeApi.Create(appId, 10000, qrCodeId, QrCode_ActionName.QR_SCENE);

                var qrCodeUrl = AdvancedAPIs.QrCodeApi.GetShowQrCodeUrl(qrResult.ticket);
                ViewData["QrCodeUrl"] = qrCodeUrl;

                ConfigHelper.CodeCollection[guid] = new CodeRecord()
                {
                    Key = guid,
                    QrCodeId = qrCodeId,
                    QrCodeTicket = qrResult
                };//添加对应关系

                //下载版本
                var config = configHelper.GetConfig();
                ViewData["Versions"] = config.Versions;
                ViewData["WebVersions"] = config.WebVersions;
                ViewData["DownloadCount"] = config.DownloadCount.ToString("##,###");

                return View();
            }
            catch (Exception e)
            {
                WeixinTrace.SendCustomLog("Document发生appsecret错误！",e.ToString());
                var accessTokenBags = AccessTokenContainer.GetAllItems();

                WeixinTrace.SendCustomLog("当前AccessToken信息", accessTokenBags.ToJson());

                throw;
            }
           
        }

        /// <summary>
        /// 检查是否可下载
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ActionResult> CheckDownload(string guid, string version)
        {
            return Task.Factory.StartNew<ActionResult>(() =>
            {
                var success = CheckCanDownload(guid);
                var realVersion = version.StartsWith("W") ? version.Substring(1, version.Length - 1) : version;
                if (success)
                {
                    var codeRecord = ConfigHelper.CodeCollection[guid];
                    codeRecord.Version = realVersion;
                }
                else if (ConfigHelper.CodeCollection.ContainsKey(guid))
                {
                    if (version.StartsWith("W"))
                    {
                        ConfigHelper.CodeCollection[guid].IsWebVersion = true;//下载网页版
                    }
                    ConfigHelper.CodeCollection[guid].Version = realVersion;
                }
                return Json(new { success = success });
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
                var success = CheckCanDownload(guid);
                if (!success)
                {
                    string message = null;
                    var guidNotFound = !ConfigHelper.CodeCollection.ContainsKey(guid);
                    if (guidNotFound)
                    {
                        message = "审核失败，请从官方下载页面进入！";
                    }
                    else
                    {
                        var codeRecord = ConfigHelper.CodeCollection[guid];
                        if (!codeRecord.AllowDownload)
                        {
                            message = string.Format("审核失败，文件不允许下载，或已经下载过！如需重新下载请刷新浏览器！（101 - {0}）", guid);
                        }
                    }

                    message = message ?? string.Format("未通过审核，或此二维码已过期，请刷新网页后重新操作！（102 - {0}）", guid);

                    var file = File(Encoding.UTF8.GetBytes(message), "text/plain");
                    file.FileDownloadName = "下载失败.txt";
                    return file;
                }
                else
                {
                    var codeRecord = ConfigHelper.CodeCollection[guid];
                    codeRecord.Used = true;
                    //codeRecord.AllowDownload = false;//这里如果只允许一次下载，有的浏览器插件或者防护软件会自动访问页面上的链接，导致用户真实的下载时效
                    var configHelper = new ConfigHelper();
                    var filePath = configHelper.Download(codeRecord.Version, codeRecord.IsWebVersion);
                    var file = File(filePath, "application/octet-stream");


                    file.FileDownloadName = string.Format("Senparc.Weixin{0}-v{1}.rar",
                        codeRecord.IsWebVersion ? "-Web" : "",
                        codeRecord.Version);
                    return file;
                }
            });
        }
    }
}