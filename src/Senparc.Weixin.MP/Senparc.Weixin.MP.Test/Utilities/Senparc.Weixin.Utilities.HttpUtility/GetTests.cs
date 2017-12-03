using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.Utilities
{
    [TestClass]
    public class GetTests : CommonApiTest
    {

        [TestMethod]
        public void DownloadToDirTest()
        {
            ////下载图片   
            //var file = string.Format("qr-{0}.jpg", DateTime.Now.Ticks);
            //using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            //{
            //    Get.Download(url, fs);//下载
            //    fs.Flush();//直接保存，无需处理指针
            //}

            var fileName = @"E:\Senparc项目\WeiXinMPSDK\src\Senparc.Weixin.MP\Senparc.Weixin.MP.Test\qr.jpg";

            //上传素材
            var result = MediaApi.UploadTemporaryMedia(base._appId, UploadMediaFileType.image, fileName);
            Console.WriteLine("MediaId：" + result.media_id);
    
            //下载
            var url = "http://sdk.weixin.senparc.com/images/v2/ewm_01.png";

           var downloadResult =  Senparc.Weixin.HttpUtility.Get.Download(url, "/");
            Console.WriteLine(downloadResult);
        }


    }
}
