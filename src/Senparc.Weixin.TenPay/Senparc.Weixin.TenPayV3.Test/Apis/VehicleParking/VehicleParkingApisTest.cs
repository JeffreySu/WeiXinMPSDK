using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.VehicleParking;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.net6.Apis.VehicleParking
{
    // 测试前请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter3_7_2.shtml
    public class VehicleParkingApisTest : BaseTenPayTest
    {
        string openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//"olPjZjiGtsfaqOhUbOd2puy1wVvc";//换成测试人的 OpenId
        string notify_url = "{notify_url}";//通知回调url 只接收https
        CreateParkingReturnJson createParkingResult = null;
        PayParkingReturnJson payParkingResult = null;

        #region 微信支付分停车服务

        /// <summary>
        /// 查询车牌服务开通信息测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_1.shtml
        /// </summary>
        [TestMethod()]
        public void QueryServiceAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 车牌号
            var plate_number = "{plate_number}";
            // 车牌颜色
            var plate_color = "{plate_color}";

            var vehicleParkingApis = new VehicleParkingApis();
            var result = vehicleParkingApis.QueryServiceAsync(TenPayV3Info.AppId, plate_number, plate_color, openId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询车牌服务开通信息测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 创建停车入场接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_2.shtml
        /// </summary>
        [TestMethod()]
        public void CreateParkingAsyncTest()
        {
            // TODO:需填入数据
            //商户侧入场标识id，在同一个商户号下唯一
            var out_parking_no = "{out_parking_no}";
            //车牌号
            var plate_number = "{plate_number}";
            //车牌颜色
            var plate_color = "{plate_color}";

            var requestData = new CreateParkingRequestData(out_parking_no, plate_number, plate_color, notify_url, new TenpayDateTime(DateTime.Now), "Senparc接口单元测试停车场", 3600);

            var vehicleParkingApis = new VehicleParkingApis();
            var createParkingResult = vehicleParkingApis.CreateParkingAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 创建停车入场接口测试结果：" + createParkingResult.ToJson(true));

            Assert.IsNotNull(createParkingResult);
            Assert.IsTrue(createParkingResult.ResultCode.Success);
            Assert.IsTrue(createParkingResult.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 扣费受理接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_3.shtml
        /// </summary>
        [TestMethod()]
        public void PayParkingAsyncTest()
        {
            // 若没有创建停车入场, 则创建停车入场
            if (createParkingResult is null)
            {
                CreateParkingAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var out_order_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            var parking_info = new PayParkingRequestData.Parking_Info(createParkingResult);
            // 停车场设备id
            parking_info.device_id = "{device_id}";
            // 每小时收费
            var price = 1;
            // 根据停车时间计算收费
            var amount = new PayParkingRequestData.Amount(price * parking_info.charging_duration, "CNY");
            var requestData = new PayParkingRequestData(TenPayV3Info.AppId, "Senparc单元测试停车场扣费", null, out_order_no, "PARKING", null, notify_url, null, amount, parking_info);

            var vehicleParkingApis = new VehicleParkingApis();
            payParkingResult = vehicleParkingApis.PayParkingAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 扣费受理接口测试结果：" + payParkingResult.ToJson(true));

            Assert.IsNotNull(payParkingResult);
            Assert.IsTrue(payParkingResult.ResultCode.Success);
            Assert.IsTrue(payParkingResult.VerifySignSuccess == true);//通过验证
        }


        /// <summary>
        /// 查询订单接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_4.shtml
        /// </summary>
        [TestMethod()]
        public void PQueryParkingAsyncTest()
        {
            // 若没有扣费受理, 则扣费受理
            if (payParkingResult is null)
            {
                PayParkingAsyncTest();
            }

            var vehicleParkingApis = new VehicleParkingApis();
            var result = vehicleParkingApis.QueryParkingAsync(payParkingResult.out_trade_no).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询订单接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        #endregion
    }
}
