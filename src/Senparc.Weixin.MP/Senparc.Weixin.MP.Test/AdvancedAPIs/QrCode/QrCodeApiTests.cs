#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs.Tests
{
    [TestClass()]
    public class QrCodeApiTests : CommonApiTest
    {

        #region 异步方法


        [TestMethod()]
        public void CreateAsyncTest()
        {
            var openId = base._testOpenId;

            base.TestAyncMethod(1, openId, () =>
            {
                var result = QrCodeApi.CreateAsync(base._appId, 100, 999999, QrCode_ActionName.QR_SCENE, "QrTest").Result;
                //Assert.AreEqual(ReturnCode.请求成功,result.errcode);

                Console.WriteLine("Result（T-{0}）：{1}", Thread.CurrentThread.GetHashCode(), result.ToString());
                
                //发送消息通知生成状态
                var testData = new //TestTemplateData()
                {
                    first = new TemplateDataItem(string.Format("【测试-{0}】QrCode单元测试完成一个线程。", SystemTime.Now.ToString("T"))),
                    keyword1 = new TemplateDataItem(openId),
                    keyword2 = new TemplateDataItem("QrCode测试"),
                    keyword3 = new TemplateDataItem(SystemTime.Now.Ticks.ToString("O")),
                    remark = new TemplateDataItem("结果：" + result.errcode.ToString())
                };

                var tmResult = TemplateApi.SendTemplateMessageAsync(base._appId, openId, "cCh2CTTJIbVZkcycDF08n96FP-oBwyMVrro8C2nfVo4",
                    (result.url)
                    , testData).Result;

                if (result.errcode == ReturnCode.请求成功)
                {
                    //下载并获得二维码
                    try
                    {

                        var fileName = Path.Combine(System.Environment.CurrentDirectory, "..\\", string.Format("qrcode-{0}.jpg", SystemTime.Now.ToString("yyyy-MM-dd HH_mm_ss")));
                        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                        {
                            QrCodeApi.ShowQrCode(result.ticket, fs);//下载二维码
                        }

                        //下载之后，文件储存在\src\Senparc.Weixin.MP\Senparc.Weixin.MP.Test\bin目录下，
                        //打开.jpg文件之后，使用微信扫一扫查看效果。
                    }
                    catch
                    {
                    }
                }

                base.AsyncThreadsCollection.Remove(Thread.CurrentThread);//必须要加

            });
        }

        #endregion

    }
}