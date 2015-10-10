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
    }

}