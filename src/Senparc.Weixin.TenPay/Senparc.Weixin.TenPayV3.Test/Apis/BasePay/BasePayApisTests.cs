using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.Tests
{
    [TestClass()]
    public class BasePayApisTests
    {
        [TestMethod()]
        public void JsAPiTest()
        {
			var json = @$"{{
	""time_expire"": ""2018-06-08T10:34:56+08:00"",
	""amount"": {{
		""total"": 100,
		""currency"": ""CNY""
	}}}},
	""mchid"": ""{{}}"",
	""description"": ""Image形象店-深圳腾大-QQ公仔"",
	""notify_url"": ""http://sdk.weixin.senparc.com/TenpayV3/PayNotifyUrl"",
	""payer"": {{
		""openid"": ""oUpF8uMuAJO_M2pxb1Q9zNjWeS6o""
	}},
	""out_trade_no"": ""121775250120140v7033233368018"",
	""goods_tag"": ""WXG"",
	""appid"": ""wxd678efh567hg6787"",
	""attach"": ""自定义数据说明"",
	""detail"": {{
		""invoice_id"": ""wx123"",
		""goods_detail"": [{{
			""goods_name"": ""iPhoneX 256G"",
			""wechatpay_goods_id"": ""1001"",
			""quantity"": 1,
			""merchant_goods_id"": ""商品编码"",
			""unit_price"": 828800
		}}, {{
			""goods_name"": ""iPhoneX 256G"",
			""wechatpay_goods_id"": ""1001"",
			""quantity"": 1,
			""merchant_goods_id"": ""商品编码"",
			""unit_price"": 828800
		}}],
		""cost_price"": 608800
	}},
	""scene_info"": {{
		""store_info"": {{
			""address"": ""广东省深圳市南山区科技中一道10000号"",
			""area_code"": ""440305"",
			""name"": ""腾讯大厦分店"",
			""id"": ""0001""
		}},
		""device_id"": ""013467007045764"",
		""payer_client_ip"": ""14.23.150.211""
	}}
}}";
            JsApiRequestData data = Senparc.CO2NET.Helpers.SerializerHelper.GetObject<JsApiRequestData>(json);
            

        }
    }
}