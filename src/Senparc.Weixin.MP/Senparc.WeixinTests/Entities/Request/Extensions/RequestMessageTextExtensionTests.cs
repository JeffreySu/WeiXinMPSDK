using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.NeuChar.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Entities;
using Senparc.CO2NET.Extensions;

namespace Senparc.NeuChar.Entities.Request.Tests
{
    [TestClass()]
    public class RequestMessageTextExtensionTests
    {
        [TestMethod()]
        public void KeywordTest()
        {
            var requestMessage = new RequestMessageText();
            var result = false;

            {
                //大小写不敏感
                requestMessage.Content = "Case1";
                result = requestMessage.StartHandler(false)
                   .Keyword("CASe1", () => null)
                   .MatchSuccessed;
                Assert.IsTrue(result);

                requestMessage.Content = "Case1";
                result = requestMessage.StartHandler(false)
                   .Keywords(new[] { "CaSe1", "case2" }, () => null)
                   .MatchSuccessed;
                Assert.IsTrue(result);

                result = requestMessage.StartHandler(false)
                .Regex(@"case\d+", () => null)
                .MatchSuccessed;
                Assert.IsTrue(result);
            }


            {
                //大小写敏感
                requestMessage.Content = "Case1";
                result = requestMessage.StartHandler(true)
                   .Keyword("CASe1", () => null)
                   .MatchSuccessed;
                Assert.IsFalse(result);

                requestMessage.Content = "Case1";
                result = requestMessage.StartHandler(true)
                   .Keywords(new[] { "CaSe1", "case2" }, () => null)
                   .MatchSuccessed;
                Assert.IsFalse(result);

                result = requestMessage.StartHandler(true)
                .Regex(@"case\d+", () => null)
                .MatchSuccessed;
                Assert.IsFalse(result);
            }

            {
                //异步方法
                requestMessage.Content = "case123";
                var handler = requestMessage.StartHandler(true)
                 .RegexAsync(@"case\d+", async () =>
                 {
                     Console.WriteLine(SystemTime.Now.ToString("mm:ss.ffff"));
                     await Task.Delay(10);
                     Console.WriteLine(SystemTime.Now.ToString("mm:ss.ffff"));
                     var responseMessage = new ResponseMessageText();
                     responseMessage.Content = @"case\d+";
                     return responseMessage;
                 }).Result
                 .KeywordAsync("CaseA", async () =>
                 {
                     var responseMessage = new ResponseMessageText();
                     responseMessage.Content = @"CaseA";
                     Console.WriteLine(responseMessage.ToJson(true));
                     return responseMessage;
                 }).Result;
                result = handler.MatchSuccessed;
                Assert.IsTrue(result);
            }
        }
    }
}