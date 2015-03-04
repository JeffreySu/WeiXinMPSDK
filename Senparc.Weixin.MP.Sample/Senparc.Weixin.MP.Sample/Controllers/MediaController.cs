using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class MediaController : Controller
    {
        private string appId = WebConfigurationManager.AppSettings["WeixinAppId"];
        private string appSecret = WebConfigurationManager.AppSettings["WeixinAppSecret"];

        public FileResult GetVoice(string mediaId)
        {
            var accessToken = CommonAPIs.AccessTokenContainer.TryGetToken(appId, appSecret);

            MemoryStream ms = new MemoryStream();
            AdvancedAPIs.Media.MediaApi.Get(accessToken, mediaId, ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "audio/amr","voice.amr");
        }
    }
}
