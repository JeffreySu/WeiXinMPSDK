using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.Sample.NetCore3.Controllers;
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.Sample.Net6.Controllers
{
    public class TenPayRealV3Controller : BaseController
    {
        private IServiceProvider _serviceProvider;
        private static TenPayV3Info _tenPayV3Info;

        public static TenPayV3Info TenPayV3Info
        {
            get
            {
                if (_tenPayV3Info == null)
                {
                    var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

                    _tenPayV3Info =
                        TenPayV3InfoCollection.Data[key];
                }
                return _tenPayV3Info;
            }
        }

        public TenPayRealV3Controller(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IActionResult Js()
        {
            var openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";
           var sp_billno = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
                        TenPayV3Util.BuildRandomStr(6));
            var goodsTags = "";
            var attach = "附加数据";
            JsApiRequestData.Detail detail = null;
            JsApiRequestData.Scene_Info scene = null;
            JsApiRequestData.Settle_Info settle = null;
            JsApiRequestData jsApiRequestData = new(new TenpayDateTime(DateTime.Now),new JsApiRequestData.Amount() { currency="CNY", total=100 }, 
                TenPayV3Info.MchId,"测试购买", TenPayV3Info.TenPayV3Notify,new JsApiRequestData.Payer() {  openid= openId },sp_billno, goodsTags, TenPayV3Info.AppId,
                attach, detail, scene, settle);
            var result = BasePayApis.JsApi(_serviceProvider, jsApiRequestData);
            return Json(result);
        }
    }
}
