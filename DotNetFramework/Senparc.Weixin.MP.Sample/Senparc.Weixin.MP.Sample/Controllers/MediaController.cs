/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：MediaController.cs
    文件功能描述：根据mediaId获取语音
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

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
            var accessToken = CommonAPIs.AccessTokenContainer.TryGetAccessToken(appId, appSecret);

            MemoryStream ms = new MemoryStream();
            AdvancedAPIs.MediaApi.Get(accessToken, mediaId, ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "audio/amr","voice.amr");
        }
    }
}
