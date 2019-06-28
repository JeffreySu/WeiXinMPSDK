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
            //var file = string.Format("qr-{0}.jpg", SystemTime.Now.Ticks);
            //using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            //{
            //    Get.Download(url, fs);//下载
            //    fs.Flush();//直接保存，无需处理指针
            //}

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\qr.jpg");
#else
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\qr.jpg");
#endif

            //上传素材
            var result = MediaApi.UploadTemporaryMedia(base._appId, UploadMediaFileType.image, fileName);
            Console.WriteLine("MediaId：" + result.media_id);

            //下载
            var url = "https://sdk.weixin.senparc.com/images/v2/ewm_01.png";


#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\");
#else
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\");
#endif

            var downloadResult = Senparc.CO2NET.HttpUtility.Get.Download(url, filePath);
            Console.WriteLine(downloadResult);

            Assert.IsTrue(File.Exists(downloadResult));

            //完成之后通常需要强制修改文件名
#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
            File.Move(downloadResult, downloadResult + "core20.renamed.jpg");
#else
            File.Move(downloadResult, downloadResult + ".net45.renamed.jpg");
#endif
        }


    }
}
