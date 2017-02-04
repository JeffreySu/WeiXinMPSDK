using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.WxOpen.Containers;

namespace Senparc.WeixinTests.Cache
{
    [TestClass]
    public class SerializeTest : CommonApiTest
    {
        /// <summary>
        /// 不同的序列化方式比较
        /// </summary>
        [TestMethod]
        public void SerializeDataTest()
        {
            var sessionBag = SessionContainer.UpdateSession(null, "OpenId", "SessionKey");
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sessionBag);
            Console.WriteLine(jsonString);
        }

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
                    CacheTime = DateTime.Now,
                    ExpireTime = DateTime.Now,
                    OpenId = "OpenId"
                };
                return sessionBag;
            };

            var testCycle = 100000;
            //使用 Newtonsoft.Json 进行 10 万次序列化并计算时间
            DateTime dt1 = DateTime.Now;
            for (int i = 0; i < testCycle; i++)
            {
                //获取一个 SessionBag 对象
                var sessionBag = getNewEntity();
                //序列化
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sessionBag);
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionBag>(jsonString);
            }
            DateTime dt2 = DateTime.Now;

            //使用 二进制方式序列化
            for (int i = 0; i < testCycle; i++)
            {
                //获取一个 SessionBag 对象
                var sessionBag = getNewEntity();
                //序列化
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream memoryStream = new MemoryStream();
                {
                    binaryFormatter.Serialize(memoryStream, sessionBag);
                    byte[] objectDataAsStream = memoryStream.ToArray();
                    //反序列化
                    var obj = StackExchangeRedisExtensions.Deserialize<SessionBag>(objectDataAsStream);
                }
            }
            DateTime dt3 = DateTime.Now;

            Console.WriteLine("JSON 序列化 {0} 次，耗时：{1}ms", testCycle, (dt2 - dt1).TotalMilliseconds);
            Console.WriteLine("二进制 序列化 {0} 次，耗时：{1}ms", testCycle, (dt3 - dt2).TotalMilliseconds);

            //结果：Newtonsoft.JSON 效率更高
        }
    }
}
