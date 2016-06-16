using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.AdvancedAPIs.ScanProduct;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        }

        [TestMethod]
        public void Test2()
        {
            AFoo a = new AFoo();
            a.FooBarBuzz = "A";
            a.A = "Hello World";

            BFoo b = new BFoo();
            b.FooBarBuzz = "B";
            b.B = "Hello World";

            List<BaseFoo> allFoos = new List<BaseFoo>();
            allFoos.Add(a);
            allFoos.Add(b);

            var result = JsonConvert.SerializeObject(allFoos);

            // issue here:
            //Additional information: Could not create an instance of type ConsoleApplication6.BaseFoo. Type is an interface or abstract class and cannot be instantiated. Path '[0].A', line 1, position 6.
            //var test = JsonConvert.DeserializeObject<List<BaseFoo>>(result);
            //JsonConverter[] converters = { new BaseConverter() };

            //var test = JsonConvert.DeserializeObject<List<BaseFoo>>(result, new JsonSerializerSettings() { Converters = converters });
            var test = JsonConvert.DeserializeObject<List<BaseFoo>>(result);

        }

        public class FooConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(BaseFoo));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JObject jo = JObject.Load(reader);
                if (jo["FooBarBuzz"].Value<string>() == "A")
                    return jo.ToObject<AFoo>(serializer);

                if (jo["FooBarBuzz"].Value<string>() == "B")
                    return jo.ToObject<BFoo>(serializer);

                return null;
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        public class BaseFoo
        {
            public string FooBarBuzz { get; set; }
        }

        [JsonConverter(typeof(FooConverter))]
        public class AFoo : BaseFoo
        {
            public string A { get; set; }
        }

        [JsonConverter(typeof(FooConverter))]
        public class BFoo : BaseFoo
        {
            public string B { get; set; }
        }
    }
}
