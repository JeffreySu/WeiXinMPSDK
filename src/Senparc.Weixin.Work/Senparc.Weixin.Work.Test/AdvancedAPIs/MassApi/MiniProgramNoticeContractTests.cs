/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MiniProgramNoticeContractTests.cs
    文件功能描述：小程序通知消息请求契约测试

----------------------------------------------------------------*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Mass;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.MassApi
{
    [TestClass]
    public class MiniProgramNoticeContractTests
    {
        [TestMethod]
        public void SerializesAgentId()
        {
            var request = new SendMiniProgramNoticeData
            {
                agentid = 1000002
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(request);

            StringAssert.Contains(json, "\"agentid\":1000002");
        }
    }
}
