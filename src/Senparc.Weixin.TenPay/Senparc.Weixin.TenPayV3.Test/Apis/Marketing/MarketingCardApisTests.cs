using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis.Marketing;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Tests
{
    public partial class MarketingApisTests
    {
        #region 消费卡接口

        /// <summary>
        /// 发放消费卡接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_6_1.shtml
        /// </summary>
        [TestMethod()]
        public void SendCardAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建建立合作关系 则建立合作关系
            if (buildPartnershipsResult is null)
            {
                BuildPartnershipsAsyncTest();
            }
            var card_id = "pIJMr5MMiIkO_93VtPyIiEk2DZ4w";//TODO: 消费卡ID，即card_id。card_id获取方法请参见《接入前准备》配置应用中的创建消费卡。https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter5_6_2.shtml#part-6

            // TODO:流水号?这样是否有效?
            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            var requestData = new SendCardRequestData(card_id, TenPayV3Info.AppId, openId, out_request_no, new TenpayDateTime(DateTime.Now));

            var marketingApis = new MarketingApis();
            var result = marketingApis.SendCardAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 发放消费卡接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        #endregion
    }
}
