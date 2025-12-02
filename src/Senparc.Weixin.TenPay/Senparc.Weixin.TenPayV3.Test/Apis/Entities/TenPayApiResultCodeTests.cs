using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.Apis.Entities
{
    [TestClass]
    public class TenPayApiResultCodeTests
    {
        [TestMethod]
        public void TryGetCodeTest()
        {
            var result = TenPayApiResultCode.TryGetCode(System.Net.HttpStatusCode.BadRequest, "{\"code\":\"RESOURCE_ALREADY_EXISTS\",\"message\":\"创建请求重入，但本次请求与上次请求信息不一致\"}");
            Assert.IsNotNull(result);
            Assert.AreEqual("400", result.StateCode);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("RESOURCE_ALREADY_EXISTS", result.ErrorCode);
            Assert.AreEqual("创建请求重入，但本次请求与上次请求信息不一致", result.Solution);
        }

        [TestMethod]
        public void TryGetCode_204Test()
        {
            var result = TenPayApiResultCode.TryGetCode(System.Net.HttpStatusCode.NoContent, null);
            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson(true));
            Assert.AreEqual("204", result.StateCode);
            Assert.IsTrue(result.Success);
        }

        /// <summary>
        /// Test that detail.value can handle string array values
        /// 测试 detail.value 可以处理字符串数组值
        /// </summary>
        [TestMethod]
        public void TryGetCode_WithDetailValueAsStringArray_Test()
        {
            // This is the format mentioned in the comment in TenPayApiResultCode.cs
            var json = "{\"code\":\"PARAM_ERROR\",\"detail\":{\"location\":null,\"value\":[\"/body/payer/openid\"]},\"message\":\"请求中含有未在API文档中定义的参数\"}";
            var result = TenPayApiResultCode.TryGetCode(System.Net.HttpStatusCode.BadRequest, json);
            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson(true));
            Assert.AreEqual("400", result.StateCode);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("PARAM_ERROR", result.ErrorCode);
        }

        /// <summary>
        /// Test that detail.value can handle integer values
        /// 测试 detail.value 可以处理整数值 (Issue #xxxx)
        /// </summary>
        [TestMethod]
        public void TryGetCode_WithDetailValueAsInteger_Test()
        {
            // This is the case reported in the issue where value is an integer like 135
            var json = "{\"code\":\"PARAM_ERROR\",\"detail\":{\"location\":null,\"value\":135},\"message\":\"参数错误\"}";
            var result = TenPayApiResultCode.TryGetCode(System.Net.HttpStatusCode.BadRequest, json);
            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson(true));
            Assert.AreEqual("400", result.StateCode);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("PARAM_ERROR", result.ErrorCode);
        }

        /// <summary>
        /// Test that detail.value can handle string values
        /// 测试 detail.value 可以处理字符串值
        /// </summary>
        [TestMethod]
        public void TryGetCode_WithDetailValueAsString_Test()
        {
            var json = "{\"code\":\"PARAM_ERROR\",\"detail\":{\"location\":null,\"value\":\"some_value\"},\"message\":\"参数错误\"}";
            var result = TenPayApiResultCode.TryGetCode(System.Net.HttpStatusCode.BadRequest, json);
            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson(true));
            Assert.AreEqual("400", result.StateCode);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("PARAM_ERROR", result.ErrorCode);
        }

        /// <summary>
        /// Test that detail.value can handle null values
        /// 测试 detail.value 可以处理 null 值
        /// </summary>
        [TestMethod]
        public void TryGetCode_WithDetailValueAsNull_Test()
        {
            var json = "{\"code\":\"PARAM_ERROR\",\"detail\":{\"location\":null,\"value\":null},\"message\":\"参数错误\"}";
            var result = TenPayApiResultCode.TryGetCode(System.Net.HttpStatusCode.BadRequest, json);
            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson(true));
            Assert.AreEqual("400", result.StateCode);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("PARAM_ERROR", result.ErrorCode);
        }
    }
}
