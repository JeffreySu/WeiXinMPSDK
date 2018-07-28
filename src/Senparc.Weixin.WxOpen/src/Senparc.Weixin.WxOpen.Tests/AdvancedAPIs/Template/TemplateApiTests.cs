#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using Senparc.Weixin.WxOpen.AdvancedAPIs.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.WxOpen.Tests;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Template.Tests
{
    public class WxOpenTemplateMessage_PaySuccessNotice : TemplateMessageBase
    {

        public string keyword1 { get; set; }
        public string keyword2 { get; set; }
        public string keyword3 { get; set; }
        public string keyword4 { get; set; }
        public string keyword5 { get; set; }
        public string keyword6 { get; set; }

        /// <summary>
        /// “购买成功通知”模板消息数据
        /// </summary>
        /// <param name="payAddress">购买地点</param>
        /// <param name="payTime">购买时间</param>
        /// <param name="productName">物品名称</param>
        /// <param name="orderNumber">交易单号</param>
        /// <param name="orderPrice">购买价格</param>
        /// <param name="hotLine">售后电话</param>
        /// <param name="url"></param>
        /// <param name="templateId"></param>
        public WxOpenTemplateMessage_PaySuccessNotice(
            string payAddress, DateTime payTime, string productName,
            string orderNumber, decimal orderPrice, string hotLine,
            string url, string templateId = "PZfsad7ijpwmqS1f9UDHW8ZBzXT69mKdzLR9zCFBD-E")
            : base(templateId, url, "购买成功通知")
        {
            /* 
                关键词
                购买地点 {{keyword1.DATA}}
                购买时间 {{keyword2.DATA}}
                物品名称 {{keyword3.DATA}}
                交易单号 {{keyword4.DATA}}
                购买价格 {{keyword5.DATA}}
                售后电话 {{keyword6.DATA}}
                */

            keyword1 = payAddress;
            keyword2 = payTime.ToString();
            keyword3 = productName;
            keyword4 = orderNumber;
            keyword5 = orderPrice.ToString("C");
            keyword6 = hotLine;
        }
    }


    [TestClass()]
    public class TemplateApiTests: WxOpenBaseTest
    {
        [TestMethod()]
        public void SendTemplateMessageTest()
        {
            var openId = "onh7q0DGM1dctSDbdByIHvX4imxA";
            var data = new WxOpenTemplateMessage_PaySuccessNotice(
                "在线购买",DateTime.Now,"图书众筹","1234567890",
                100, "400-9939-858","http://sdk.senparc.weixin.com");

           var result = TemplateApi.SendTemplateMessage(_wxOpenAppId, openId, data.TemplateId, data, "formSubmit", "pages/websocket",
                "keyword3");

            Assert.AreEqual(ReturnCode.请求成功,result.errcode);
        }
    }
}