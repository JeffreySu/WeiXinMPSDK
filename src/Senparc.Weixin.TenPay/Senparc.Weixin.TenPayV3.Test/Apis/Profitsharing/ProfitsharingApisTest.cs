using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.Profitsharing;
using Senparc.Weixin.TenPayV3.Apis.Profitsharing.Entities.RequestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.net6.Apis
{
    public class ProfitsharingApisTest : BaseTenPayTest
    {
        #region 分账接口

        CreateProfitsharingReturnJson createProfitsharingResult = null;
        ReturnProfitsharingReturnJson returnProfitsharingResult = null;

        /// <summary>
        /// 请求分账接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_1.shtml
        /// </summary>
        [TestMethod()]
        public void CreateBusifavorStockAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var transaction_id = "{transaction_id}";// TODO:此处输入待分账的订单号
            var receivers_merchant_id = "{receivers_merchant_id}";//TODO: 接收方receivers_merchant_id
            var receivers = new CreateProfitsharingRequestData.Receiver("MERCHANT_ID", receivers_merchant_id, null, 1, "Senparc分账接口单元测试");
            // TODO:流水号?这样是否有效?
            var out_order_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));
            var requestData = new CreateProfitsharingRequestData(TenPayV3Info.AppId, transaction_id, out_order_no, new CreateProfitsharingRequestData.Receiver[] { receivers }, true);

            var profitsharingApis = new ProfitsharingApis();

            try
            {
                // receiver.type为MERCHANT_ID时,receiver.name必填
                createProfitsharingResult = profitsharingApis.CreateProfitsharingAsync(requestData).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TenpayApiRequestException));
                Console.WriteLine(ex.Message);
                Assert.IsTrue(ex.Message.Contains("必填"));
            }

            requestData.receivers.First().name = "{name}";// TODO: 此处填入接受商户的名称

            createProfitsharingResult = profitsharingApis.CreateProfitsharingAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 请求分账接口结果：" + createProfitsharingResult.ToJson(true));

            Assert.IsNotNull(createProfitsharingResult);
            Assert.IsTrue(createProfitsharingResult.ResultCode.Success);
            Assert.IsTrue(createProfitsharingResult.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询分账结果接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_2.shtml
        /// </summary>
        [TestMethod()]
        public void QueryProfitsharingAsyncTest()
        {
            var marketingApis = new ProfitsharingApis();
            var result = marketingApis.QueryProfitsharingAsync(createProfitsharingResult.transaction_id, createProfitsharingResult.out_order_no).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询分账结果接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 请求分账回退接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_3.shtml
        /// </summary>
        [TestMethod()]
        public void ReturnProfitsharingAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // TODO:流水号?这样是否有效?
            var out_return_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));
            var requestData = new ReturnProfitsharingRequestData(createProfitsharingResult.order_id, createProfitsharingResult.out_order_no, out_return_no, createProfitsharingResult.receivers.First().account, 1, "分账退款接口单元测试");

            var profitsharingApis = new ProfitsharingApis();
            returnProfitsharingResult = profitsharingApis.ReturnProfitsharingAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 请求分账回退接口测试结果：" + returnProfitsharingResult.ToJson(true));

            Assert.IsNotNull(returnProfitsharingResult);
            Assert.IsTrue(returnProfitsharingResult.ResultCode.Success);
            Assert.IsTrue(returnProfitsharingResult.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询分账回退结果接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_4.shtml
        /// </summary>
        [TestMethod()]
        public void QueryReturnProfitsharingAsyncTest()
        {
            var profitsharingApis = new ProfitsharingApis();
            var result = profitsharingApis.QueryReturnProfitsharingAsync(returnProfitsharingResult.out_return_no, returnProfitsharingResult.out_order_no).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询分账回退结果接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 解冻剩余资金接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_5.shtml
        /// </summary>
        [TestMethod()]
        public void UnfreezeProfitsharingAsyncTest()
        {
            var requestData = new UnfreezeProfitsharingRequestData(createProfitsharingResult.transaction_id, createProfitsharingResult.out_order_no, "分账解冻剩余资金接口单元测试");

            var profitsharingApis = new ProfitsharingApis();
            var result = profitsharingApis.UnfreezeProfitsharingAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 解冻剩余资金接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询剩余待分金额接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_6.shtml
        /// </summary>
        [TestMethod()]
        public void QueryProfitsharingAmountsAsyncTest()
        {
            var profitsharingApis = new ProfitsharingApis();
            var result = profitsharingApis.QueryProfitsharingAmountsAsync(createProfitsharingResult.transaction_id).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询剩余待分金额接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 添加分账接收方接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_8.shtml
        /// </summary>
        [TestMethod()]
        public void AddProfitsharingReceiverAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var openId = "{openId}";//TODO: 此处填入分账接受个人的openid
            var requestData = new AddProfitsharingReceiverRequestData(TenPayV3Info.AppId, "MERCHANT_ID", openId, null, "CUSTOM", "SenparcUnitTest");

            var profitsharingApis = new ProfitsharingApis();

            try
            {
                // receiver.type为MERCHANT_ID时,receiver.name必填
                _ = profitsharingApis.AddProfitsharingReceiverAsync(requestData).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TenpayApiRequestException));
                Console.WriteLine(ex.Message);
                Assert.IsTrue(ex.Message.Contains("必填"));
            }

            // 修改数据
            requestData.type = "PERSONAL_OPENID";

            var result = profitsharingApis.AddProfitsharingReceiverAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 添加分账接收方接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 删除分账接收方接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_9.shtml
        /// </summary>
        [TestMethod()]
        public void DeleteProfitsharingAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var openId = "{openId}";//TODO: 此处填入分账接受个人的openid
            var requestData = new DeleteProfitsharingReceiverRequestData(TenPayV3Info.AppId, "PERSONAL_OPENID", openId);

            var profitsharingApis = new ProfitsharingApis();
            var result = profitsharingApis.DeleteProfitsharingAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 删除分账接收方接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        #endregion
    }
}
