#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.WxOpen.Containers;
using Senparc.Weixin.WxOpen.Tests;

namespace Senparc.WeixinTests.Cache
{
    [TestClass]
    public class SerializeTest : WxOpenBaseTest
    {
        /// <summary>
        /// 不同的序列化方式比较
        /// </summary>
        [TestMethod]
        public void SerializeDataTest()
        {
            var unionId = "";
            var sessionBag = SessionContainer.UpdateSession(null, "OpenId", "SessionKey", unionId);
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sessionBag);
            Console.WriteLine(jsonString);
        }

#if !NETCOREAPP2_1 || NETCOREAPP2_2
        /// <summary>
        /// 对比序列化到JSON以及二进制序列化的效率
        /// </summary>
        [TestMethod]
        public void SerializeCompareTest()
        {
            //var sessionBag = SessionContainer.UpdateSession(null, "OpenId", "SessionKey");

            Func<SessionBag> getNewEntity = () =>
            {
                var sessionBag = new SessionBag()
                {
                    Key = Guid.NewGuid().ToString(),
                    Name = "Jeffrey",
                    SessionKey = "SessionKey",
                    CacheTime = SystemTime.Now,
                    ExpireTime = SystemTime.Now,
                    OpenId = "OpenId"
                };
                return sessionBag;
            };

            var testCycle = 50;
            //使用 Newtonsoft.Json 进行 1 万次序列化并计算时间
            var dt1 = SystemTime.Now;
            for (int i = 0; i < testCycle; i++)
            {
                //获取一个 SessionBag 对象
                var sessionBag = getNewEntity();
                //序列化
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sessionBag);
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionBag>(jsonString);

                if (i==0)
                {
                    Console.WriteLine("Newtonsoft.JSON:");
                    Console.WriteLine(jsonString);//输出字符串
                    Console.WriteLine(obj.CacheTime);//输出反序列化后的参数
                    Console.WriteLine("==============");
                    dt1=SystemTime.Now;//过滤启动时间，Newtonsoft启动时间需要200多ms
                }
            }
            var dt2 = SystemTime.Now;

            var dt3 = SystemTime.Now;
            //使用 Newtonsoft.Json 进行 1 万次序列化并计算时间
            for (int i = 0; i < testCycle; i++)
            {
                //获取一个 SessionBag 对象
                var sessionBag = getNewEntity();
                //序列化
                var jsonString = SerializerHelper.GetJsonString(sessionBag);
                var obj = SerializerHelper.GetObject<SessionBag>(jsonString);

                if (i == 0)
                {
                    Console.WriteLine(".NET Serializer:");
                    Console.WriteLine(jsonString);//输出字符串
                    Console.WriteLine(obj.CacheTime);//输出反序列化后的参数
                    Console.WriteLine("==============");
                    dt3=SystemTime.Now;//过滤启动时间
                }
            }
            var dt4 = SystemTime.Now;

            var dt5 = SystemTime.Now;
            //使用 .NET 内置 JSON 序列化
            for (int i = 0; i < testCycle; i++)
            {
                //获取一个 SessionBag 对象
                var sessionBag = getNewEntity();
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                MemoryStream memoryStream = new MemoryStream();
                {
                    //序列化
                    binaryFormatter.Serialize(memoryStream, sessionBag);
                    byte[] objectDataAsStream = memoryStream.ToArray();

                    //反序列化
                    var obj = StackExchangeRedisExtensions.Deserialize<SessionBag>(objectDataAsStream);

                    if (i == 0)
                    {
                        Console.WriteLine(".NET Serializer:");
                        Console.WriteLine(Encoding.UTF8.GetString(objectDataAsStream));//输出字符串
                        Console.WriteLine(obj.CacheTime);//输出反序列化后的参数
                    Console.WriteLine("==============");
                    dt5=SystemTime.Now;//过滤启动时间
                    }
                }
            }
            var dt6 = SystemTime.Now;

            Console.WriteLine("Newtonsoft JSON 序列化 {0} 次，耗时：{1}ms", testCycle, (dt2 - dt1).TotalMilliseconds);
            Console.WriteLine(".NET 内置 JSON 序列化 {0} 次，耗时：{1}ms", testCycle, (dt4 - dt3).TotalMilliseconds);
            Console.WriteLine("二进制 序列化 {0} 次，耗时：{1}ms", testCycle, (dt6 - dt5).TotalMilliseconds);

            //结果：Newtonsoft.JSON 效率更高，三个结果时间基本上1:2:3
        }
#endif
    }
}
