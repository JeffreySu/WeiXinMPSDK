using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Test.CommonAPIs;
using System.Threading;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class AsyncTest : CommonApiTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var d1 = DateTime.Now;
            Console.WriteLine("1. Start");

            bool finished = false;
            Thread thread = new Thread(()=> {
                Task.Factory.StartNew(async () =>
                {
                    var d2 = DateTime.Now;
                    var tagJsonResult = await Senparc.Weixin.MP.AdvancedAPIs.UserTagApi.GetAsync(base._appId);
                    Console.WriteLine("3. tagJsonResult 1：" + string.Join(",", tagJsonResult.tags.Select(z => z.name)));
                    Console.WriteLine("4. 用时：" + (DateTime.Now-d2).TotalMilliseconds+" ms");

                    return tagJsonResult;
                }).ContinueWith(async task =>
                {
                    var tagJsonResult = await task.Result;
                    Console.WriteLine("5. tagJsonResult 2：" + string.Join(",", tagJsonResult.tags.Select(z => z.name)));
                    finished = true;
                    //TODO：继续操作tagJsonResult
                });

                Console.WriteLine("2. StartNew Finished");

            });

            thread.Start();

            while (!finished)
            {
                Thread.Sleep(5);
            }

            Console.WriteLine("6. End："+(DateTime.Now -d1).TotalMilliseconds+" ms");

        }
    }
}
