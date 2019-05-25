/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：MediaController.cs
    文件功能描述：根据mediaId获取语音
    
    
    创建标识：Senparc - 20150312
    
    修改标识：Senparc - 20150312
    修改描述：TestUploadMediaFile() 方法专为Senparc.Weixin.MP.Test/HttpUtility/RequestUtilityTest.cs/HttpPostTest 提供上传测试目标
----------------------------------------------------------------*/

using Senparc.CO2NET.Utilities;
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
        private string appId = Config.SenparcWeixinSetting.WeixinAppId;

        public FileResult GetVoice(string mediaId)
        {
            MemoryStream ms = new MemoryStream();
            AdvancedAPIs.MediaApi.Get(appId, mediaId, ms);
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
            var inputStream = Request.InputStream;
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
            using (FileStream ms = new FileStream(ServerUtility.ContentRootMapPath("~/TestUploadMediaFile.jpg"), FileMode.OpenOrCreate))
            {
                inputStream.CopyTo(ms, 256);
            }

            return Content("{\"type\":\"image\",\"media_id\":\"MEDIA_ID\",\"created_at\":123456789}");
        }
    }
}
