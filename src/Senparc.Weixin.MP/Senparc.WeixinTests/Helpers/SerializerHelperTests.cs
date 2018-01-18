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
using Senparc.Weixin.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Helpers.Tests
{
    [TestClass()]
    public class SerializerHelperTests
    {
        #region 微信JSON的忽略null值测试
        [TestMethod()]
        public void GetJsonStringTest_Null()
        {
            var obj =
                new
                {
                    X =
                        new RootClass()
                        {
                            A = "Jeffrey",
                            B = 31,
                            C = null,
                            ElementClassA = new ElementClass() { A = "Jeffrey", B = null },
                            ElementClassB = null
                        },
                    Y = new
                    {
                        O = "0",
                        Z = (string)null
                    }
                };

            var obj2 = new RootClass()
            {
                A = "Jeffrey",
                B = 31,
                C = null,
                ElementClassA = new ElementClass() { A = "Jeffrey", B = null },
                ElementClassB = null
            };

            var obj3 = new RootClass()
            {
                A = "Jeffrey",
                B = 31,
                C = null,
                ElementClassA = new ElementClass() { A = "Jeffrey", B = null },
                ElementClassB = null,
                ElementClass2 = new ElementClass2()
                {
                    A = "A"
                }
            };

            DateTime dt1 = DateTime.Now;
            SerializerHelper js = new SerializerHelper();

            var json = js.GetJsonString(obj, new JsonSetting(true, new List<string>(new[] { "Z", "C" })));
            Console.WriteLine(json);

            var json2 = js.GetJsonString(obj2, new JsonSetting(true, new List<string>(new[] { "B" })));
            Console.WriteLine(json2);

            var json3 = js.GetJsonString(obj3, new JsonSetting(true, null, new List<Type>(new[] { typeof(ElementClass2) })));
            Console.WriteLine(json3);

            Console.WriteLine((DateTime.Now - dt1).TotalMilliseconds);
        }

        public class RootClass : JsonIgnoreNull, IJsonIgnoreNull
        {
            private string _private { get; set; }
            public string A { get; set; }
            public int B { get; set; }
            public int? C { get; set; }
            public ElementClass ElementClassA { get; set; }
            public ElementClass ElementClassB { get; set; }
            public ElementClass2 ElementClass2 { get; set; }

            public RootClass()
            {
                _private = "Private";
            }
        }

        public class ElementClass : JsonIgnoreNull, IJsonIgnoreNull
        {
            public string A { get; set; }
            public string B { get; set; }
            public RootClass RootClass { get; set; }
        }

        public class ElementClass2
        {
            public string A { get; set; }
            public string B { get; set; }
            public RootClass RootClass { get; set; }
        }
        #endregion


        #region ExpandoObject类型转换测试

        [TestMethod()]
        public void GetJsonStringTest_Expando()
        {
            dynamic test = new ExpandoObject();
            test.x = "Senparc.Weixin SDK";
            test.y = DateTime.Now;

            DateTime dt1 = DateTime.Now;
            SerializerHelper js = new SerializerHelper();

            var json = js.GetJsonString(test);
            Console.WriteLine(json);

            Console.WriteLine((DateTime.Now - dt1).TotalMilliseconds);

        }

        #endregion


        #region 常规序列化、反序列化

        [Serializable]
        public class Data
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [TestMethod()]
        public void GetJsonStringTest()
        {
            var data = new Data()
            {
                Id = 1,
                Name = "Senparc"
            };
            SerializerHelper js = new SerializerHelper();
            string json = js.GetJsonString(data);
            Assert.AreEqual("{\"Id\":1,\"Name\":\"Senparc\"}", json);
            Console.WriteLine(json);
        }


        #endregion

        [TestMethod()]
        public void GetObjectTest()
        {
            string json = "{\"Id\":1,\"Name\":\"Senparc\"}";
            SerializerHelper js = new SerializerHelper();
            Data data = js.GetObject<Data>(json);

            Assert.AreEqual(1, data.Id);
            Assert.AreEqual("Senparc", data.Name);
        }

        #region JsonSetting 测试

        [Serializable]
        public class WeixinData
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Note { get; set; }
            public string Sign { get; set; }
            public Sex Sex { get; set; }
        }


        [TestMethod]
        public void JsonSettingTest()
        {
            var weixinData = new WeixinData()
            {
                Id = 1,
                UserName = "JeffreySu",
                Note = null,
                Sign = null,
                Sex = Sex.男
            };

            SerializerHelper js = new SerializerHelper();
            //string json = js.GetJsonString(weixinData);
            //Console.WriteLine(json);

            //JsonSetting jsonSetting = new JsonSetting(true);
            //string json2 = js.GetJsonString(weixinData, jsonSetting);
            //Console.WriteLine(json2);

JsonSetting jsonSetting3 = new JsonSetting(true, new List<string>() { "Note" });
string json3 = js.GetJsonString(weixinData, jsonSetting3);
Console.WriteLine(json3);
        }

        #endregion
    }
}