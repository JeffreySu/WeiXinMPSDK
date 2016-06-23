using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.AdvancedAPIs.ScanProduct;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class DeserializerTest
    {
        public static readonly string json = @"{
    'keystandard': 'qrcode',
    'keystr': 'wxscandemo',
    'brand_info': {
        'base_info': {
            'title': '扫一扫动态主页 demo',
            'thumb_url': 'http://mmbiz.qpic.cn/mmbiz/AhrnkhhK7rWevHib2pmq1phtply6JicADNrX6Yrvd7LzKERyic3kn3Vd SsmFr5F5ibXzj9Al65yWFudmjqcWic1Qe9g/0',
            'brand_tag': '小耿哥 8',
            'category_id': 0,
            'store_mgr_type': 'auto',
            'store_vendorid_list': [],
            'color': 'auto'
        },
        'detail_info': {
            'banner_list': [
                {
                    'link': 'http://mmbiz.qpic.cn/mmbiz/AhrnkhhK7rWevHib2pmq1phtply6JicADNic0LvlkCw7s6mZpicib7ict5Mhoi aL3gPrYXpibnibOpViaYJFpic12nx4bNZcQ/0'
                },
                {
                    'link': 'http://mmbiz.qpic.cn/mmbiz/AhrnkhhK7rWevHib2pmq1phtply6JicADNbTfwJmlVXp9k1A80UCFL1a9ic wdthmSLh0RuJ5iaKcZBwdXbOicktkwPQ/0'
                },
                {
                    'link': 'http://mmbiz.qpic.cn/mmbiz/AhrnkhhK7rWevHib2pmq1phtply6JicADNW4FD74oXjEyqHicE9U3H0nTC dLHibo7rRia2TFBQ6tx2Pvic92ica8Wns4Q/0'
                }
            ],
            'detail_list': [
                {
                    'title': '产品名称',
                    'desc': '微信相框 moment'
                },
                {
                    'title': '设计团队',
                    'desc': '微信团队'
                },
                {
                    'title': '设计初衷',
                    'desc': '做一个简单纯粹的电子相框'
                },
                {
                    'title': '产品诉求',
                    'desc': '以相框为纽带,增加子女与父母长辈的沟通,用照片通过微信传递感情交流'
                }
            ]
        },
        'action_info': {
            'action_list': [
                {
                    'type': 'price',
                    'retail_price': '12.00'
                },
                {
                    'type': 'link',
                    'name': 'banner',
                    'link': 'http://mp.weixin.qq.com',
                    'image': 'http://mmbiz.qpic.cn/mmbiz/AhrnkhhK7rWevHib2pmq1phtply6JicADNgjXTKn0j4TlfXjUOPYBDicVO mG0sdNfUOg9Lzia2g9cbjyTXmOiaB6L1g/0',
                    'showtype': 'banner'
                },
                {
                    'type': 'link',
                    'name': '自定义活动 1',
                    'link': 'http://p.url.cn/wxscan.php'
                },
                {
                    'type': 'link',
                    'name': '自定义活动 2',
                    'link': 'http://p.url.cn/wxscan.php'
                },
                {
                    'type': 'user',
                    'appid': 'wx307e399609946068'
                },
                {
                    'type': 'text',
                    'text': '此处可根据品牌商需要,用于简单描述商品或活动。'
                }
            ]
        },
        'module_info': {
            'module_list': [
                {
                    'type': 'anti_fake',
                    'native_show': 'true'
                }
            ]
        }
    }
}";

        private static readonly string json2 = "{'keystandard':'ean13','keystr':'6954496900556','brand_info':{'base_info':{'title':'稚优泉','thumb_url':'http://mmbiz.qpic.cn/mmbiz/hXn4njvzgZUT3A4VeW98Msv5lFP8ZKeg4l0D1WtKlZo1ibJasouW6cGOa18MGqw2X8cryNSIiarekty2tGiaKn7SA/0?wx_fmt=jpeg','color':'auto','brand_tag':'稚优泉','category_id':538112978,'store_mgr_type':'custom','store_vendorid_list':[],'status':'off'},'detail_info':{'banner_list':[{'link':'http://mmbiz.qpic.cn/mmbiz/hXn4njvzgZUT3A4VeW98Msv5lFP8ZKeg4YWvsqtZzmKPp3rN9v1K56qQ5B8VEYAEkuXgnOAvEKMVqwNfawM6rQ/0?wx_fmt=jpeg'},{'link':'http://mmbiz.qpic.cn/mmbiz/hXn4njvzgZUT3A4VeW98Msv5lFP8ZKegnw0UMjyiaZW3pCrtKZCyhRvKXZw01iafXDAYNCUKO8HFcJia7EZxaULKw/0?wx_fmt=jpeg'},{'link':'http://mmbiz.qpic.cn/mmbiz/hXn4njvzgZUT3A4VeW98Msv5lFP8ZKegVV2ll5ARtBib50woqT1ibdomE1wlAJu6Z3icmPTBodmicOYQ17XWONHGGg/0?wx_fmt=jpeg'},{'link':'http://mmbiz.qpic.cn/mmbiz/hXn4njvzgZUT3A4VeW98Msv5lFP8ZKegMoRFKluibnLSenpicL5RSUCYS3NQCcqyAdURicutHq741IR1EXGO2EEtw/0?wx_fmt=jpeg'},{'link':'http://mmbiz.qpic.cn/mmbiz/hXn4njvzgZUT3A4VeW98Msv5lFP8ZKeg02UEqFAc6ZHWt0JtIZEzSHpcu0PQcKcB5MCUaKnJDxIibJiaNy0JWEBA/0?wx_fmt=jpeg'},{'link':'http://mmbiz.qpic.cn/mmbiz/hXn4njvzgZUT3A4VeW98Msv5lFP8ZKegW6QHbdcWDiaWba5W9TlGPibXibocrsjLVbmzv4O4fLbZkHgNVtHrN1rcg/0?wx_fmt=jpeg'}],'detail_list':[{'title':'容量','desc':'50ml'}]},'action_info':{'action_list':[{'type':'price','retail_price':'51.00'},{'type':'user','name':'查看公众号','appid':'wx0e2ceaf763dd6e7a'},{'type':'text','name':'商品简介','text':'50ml 男女面部隔离紫外线，spf50,PA+++'}]}}}";
        [TestMethod]
        public void TestMethod1()
        {
            //JsonConverter[] converters = { new BaseConverter() };

            //var test = JsonConvert.DeserializeObject<ProductModel>(json, new JsonSerializerSettings() { Converters = converters });

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new ScanProductActionListConverter() }
            };
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new ScanProductActionListConverter());

            //var serializer = JsonSerializer.CreateDefault();
            //serializer.Converters.Add(new BaseConverter());

            var s = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(json);

            var ss = Newtonsoft.Json.JsonConvert.SerializeObject(s);

            var s2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(json2);

            var ss2 = Newtonsoft.Json.JsonConvert.SerializeObject(s2);

        }

        [TestMethod]
        public void Test2()
        {
            var ss = new Senparc.Weixin.MP.Entities.ResponseMessageScanProduct()
            {
                CreateTime = DateTime.Now,
                FromUserName = "342",
                ToUserName = "243",
                ScanProduct = new MP.Entities.ResponseMessageScanProduct_ScanProduct
                {
                    AntiFake = new MP.Entities.ResponseMessageScanProduct_AntiFake
                    {
                        CodeResult = "ok"
                    },
                    ExtInfo = "123",
                    KeyStandard = ProductKeyStandardOptions.ean13.ToString(),
                    KeyStr = "614234234",
                }
            };

            var ssStr = EntityHelper.ConvertEntityToXmlString<ResponseMessageScanProduct>(ss);

        }

    }
}
