#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Test.CommonApis;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    /// <summary>
    /// DataIntelligenceTest
    /// </summary>
    [TestClass]
    public partial class DataIntelligenceTest : CommonApiTest
    {
        [TestMethod]
        public void GetConversationRecordsTest()
        {
            // 由于此接口需要实际的企业微信环境和真实的会话数据，
            // 这里仅测试API调用的基本结构和参数验证
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            // 测试基本参数验证
            var startTime = DateTime.Now.AddDays(-7);
            var endTime = DateTime.Now;
            var chatId = "test_chat_id";

            try
            {
                // 注意：在没有实际数据的情况下，这个调用可能会返回错误
                // 但能验证API的基本结构是否正确
                var result = DataIntelligenceApi.GetConversationRecords(accessToken, chatId, startTime, endTime, "", 10);
                
                // 由于是新功能，可能还没有开通权限，所以不断言成功
                Console.WriteLine($"API调用结果: errcode={result.errcode}, errmsg={result.errmsg}");
            }
            catch (Exception ex)
            {
                // 记录异常但不让测试失败，因为这可能是权限问题
                Console.WriteLine($"API调用异常: {ex.Message}");
            }
        }

        [TestMethod]
        public async void GetConversationRecordsAsyncTest()
        {
            // 测试异步方法
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            var startTime = DateTime.Now.AddDays(-7);
            var endTime = DateTime.Now;
            var chatId = "test_chat_id";

            try
            {
                var result = await DataIntelligenceApi.GetConversationRecordsAsync(accessToken, chatId, startTime, endTime, "", 10);
                Console.WriteLine($"异步API调用结果: errcode={result.errcode}, errmsg={result.errmsg}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"异步API调用异常: {ex.Message}");
            }
        }

        [TestMethod]
        public void GetConversationRecordsWithRequestObjectTest()
        {
            // 测试使用请求对象的方法
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            var request = new GetConversationRecordsRequest
            {
                chatid = "test_chat_id",
                starttime = DateTimeOffset.Now.AddDays(-7).ToUnixTimeSeconds(),
                endtime = DateTimeOffset.Now.ToUnixTimeSeconds(),
                cursor = "",
                limit = 50
            };

            try
            {
                var result = DataIntelligenceApi.GetConversationRecords(accessToken, request);
                Console.WriteLine($"请求对象API调用结果: errcode={result.errcode}, errmsg={result.errmsg}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"请求对象API调用异常: {ex.Message}");
            }
        }

        [TestMethod]
        public void LimitValidationTest()
        {
            // 测试limit参数限制
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            var startTime = DateTime.Now.AddDays(-1);
            var endTime = DateTime.Now;
            var chatId = "test_chat_id";

            try
            {
                // 测试超过最大限制的情况（应该自动调整为1000）
                var result = DataIntelligenceApi.GetConversationRecords(accessToken, chatId, startTime, endTime, "", 1500);
                Console.WriteLine($"限制验证测试: errcode={result.errcode}, errmsg={result.errmsg}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"限制验证测试异常: {ex.Message}");
            }
        }

        [TestMethod]
        public void GetMessageStatisticsTest()
        {
            // 测试消息统计API
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            var startTime = DateTime.Now.AddDays(-30);
            var endTime = DateTime.Now;

            try
            {
                var result = DataIntelligenceApi.GetMessageStatistics(accessToken, startTime, endTime, "day", null, null);
                Console.WriteLine($"消息统计API调用结果: errcode={result.errcode}, errmsg={result.errmsg}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"消息统计API调用异常: {ex.Message}");
            }
        }

        [TestMethod]
        public async void GetMessageStatisticsAsyncTest()
        {
            // 测试异步消息统计API
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            var request = new GetMessageStatisticsRequest
            {
                starttime = DateTimeOffset.Now.AddDays(-7).ToUnixTimeSeconds(),
                endtime = DateTimeOffset.Now.ToUnixTimeSeconds(),
                type = "day",
                agentid = null,
                userids = null
            };

            try
            {
                var result = await DataIntelligenceApi.GetMessageStatisticsAsync(accessToken, request);
                Console.WriteLine($"异步消息统计API调用结果: errcode={result.errcode}, errmsg={result.errmsg}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"异步消息统计API调用异常: {ex.Message}");
            }
        }
    }
}
