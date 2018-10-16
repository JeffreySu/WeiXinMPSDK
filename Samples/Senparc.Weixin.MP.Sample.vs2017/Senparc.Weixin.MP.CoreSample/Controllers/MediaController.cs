/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：MediaController.cs
    文件功能描述：根据mediaId获取语音
    
    
    创建标识：Senparc - 20150312
    
    修改标识：Senparc - 20150312
    修改描述：TestUploadMediaFile() 方法专为Senparc.Weixin.MP.Test/HttpUtility/RequestUtilityTest.cs/HttpPostTest 提供上传测试目标
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    public class MediaController : Controller
    {
        public readonly string appId = Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
        private readonly string appSecret = Config.SenparcWeixinSetting.WeixinAppSecret;//与微信公众账号后台的AppId设置保持一致，区分大小写。

        public FileResult GetVoice(string mediaId)
        {
            var accessToken = Containers.AccessTokenContainer.TryGetAccessToken(appId, appSecret);

            MemoryStream ms = new MemoryStream();
            AdvancedAPIs.MediaApi.Get(accessToken, mediaId, ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "audio/amr","voice.amr");
        }

        /// <summary>
        /// 这个方法专为Senparc.Weixin.MP.Test/HttpUtility/RequestUtilityTest.cs/HttpPostTest() 提供上传测试目标
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type"></param>
        /// <param name="contentLength"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TestUploadMediaFile(string token, UploadMediaFileType type, int contentLength /*, HttpPostedFileBase postedFile*/)
        {
            var inputStream = Request.Body;
            if (contentLength != inputStream.Length)
            {
                return Content("ContentLength不正确，可能接收错误！");
            }

            if (token != "TOKEN")
            {
                return Content("TOKEN不正确！");
            }

            if (type != UploadMediaFileType.image)
            {
                return Content("UploadMediaFileType不正确！");
            }

            //储存文件，对比是否上传成功
            using (FileStream ms = new FileStream(Server.GetMapPath("~/TestUploadMediaFile.jpg"), FileMode.OpenOrCreate))
            {
                inputStream.CopyTo(ms, 256);
            }

            return Content("{\"type\":\"image\",\"media_id\":\"MEDIA_ID\",\"created_at\":123456789}");
        }
    }
}
