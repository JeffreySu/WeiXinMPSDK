using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Senparc.Weixin.MCP.Server.Tests
{
    [TestClass]
    public class InvokeWeixinApiHelperTests
    {
        private global::InvokeWeixinApiHelper _helper;

        [TestInitialize]
        public void Setup()
        {
            _helper = new global::InvokeWeixinApiHelper();
        }

        [TestMethod]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange & Act
            var helper = new global::InvokeWeixinApiHelper();

            // Assert
            Assert.IsNotNull(helper);
            Assert.IsInstanceOfType(helper, typeof(global::InvokeWeixinApiHelper));
        }

        [TestMethod]
        public void Invoke_WithValidMethod_ShouldCallMethod()
        {
            // Arrange
            var testHelper = new TestInvokeWeixinApiHelper();
            var methodName = "TestMethod";
            var parameters = new object[] { "test parameter" };

            // Act
            testHelper.Invoke(methodName, parameters);

            // Assert
            Assert.IsTrue(testHelper.TestMethodCalled);
            Assert.AreEqual("test parameter", testHelper.LastParameter);
        }

        [TestMethod]
        public void Invoke_WithInvalidMethod_ShouldNotThrowException()
        {
            // Arrange
            var methodName = "NonExistentMethod";
            var parameters = new object[] { };

            // Act & Assert - Should not throw exception
            try
            {
                _helper.Invoke(methodName, parameters);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected no exception, but got: {ex.Message}");
            }
        }

        [TestMethod]
        public void Invoke_WithNullMethodName_ShouldHandleGracefully()
        {
            // Arrange
            string? methodName = null;
            var parameters = new object[] { };

            // Act & Assert - Should handle null gracefully (may throw ArgumentNullException)
            try
            {
                _helper.Invoke(methodName!, parameters);
                // If no exception is thrown, that's also acceptable
                Assert.IsTrue(true);
            }
            catch (ArgumentNullException)
            {
                // ArgumentNullException is expected behavior for null method name
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected ArgumentNullException or no exception, but got: {ex.GetType().Name} - {ex.Message}");
            }
        }

        [TestMethod]
        public void Invoke_WithEmptyMethodName_ShouldNotThrowException()
        {
            // Arrange
            var methodName = "";
            var parameters = new object[] { };

            // Act & Assert - Should not throw exception
            try
            {
                _helper.Invoke(methodName, parameters);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected no exception, but got: {ex.Message}");
            }
        }

        [TestMethod]
        public void Invoke_WithNullParameters_ShouldHandleGracefully()
        {
            // Arrange
            var testHelper = new TestInvokeWeixinApiHelper();
            var methodName = "TestMethodWithoutParameters";
            object[]? parameters = null;

            // Act & Assert - Should not throw exception
            try
            {
                testHelper.Invoke(methodName, parameters!);
                Assert.IsTrue(testHelper.TestMethodWithoutParametersCalled);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected no exception, but got: {ex.Message}");
            }
        }

        [TestMethod]
        public void Invoke_WithMultipleParameters_ShouldPassAllParameters()
        {
            // Arrange
            var testHelper = new TestInvokeWeixinApiHelper();
            var methodName = "TestMethodWithMultipleParameters";
            var parameters = new object[] { "param1", 42, true };

            // Act
            testHelper.Invoke(methodName, parameters);

            // Assert
            Assert.IsTrue(testHelper.TestMethodWithMultipleParametersCalled);
            Assert.AreEqual("param1", testHelper.StringParam);
            Assert.AreEqual(42, testHelper.IntParam);
            Assert.AreEqual(true, testHelper.BoolParam);
        }

        [TestMethod]
        public void Invoke_WithPrivateMethod_ShouldNotInvokePrivateMethod()
        {
            // Arrange
            var testHelper = new TestInvokeWeixinApiHelper();
            var methodName = "PrivateTestMethod";
            var parameters = new object[] { };

            // Act
            testHelper.Invoke(methodName, parameters);

            // Assert
            Assert.IsFalse(testHelper.PrivateMethodCalled);
        }
    }

    /// <summary>
    /// Test helper class that extends InvokeWeixinApiHelper for testing purposes
    /// </summary>
    public class TestInvokeWeixinApiHelper : global::InvokeWeixinApiHelper
    {
        public bool TestMethodCalled { get; private set; }
        public string? LastParameter { get; private set; }
        public bool TestMethodWithoutParametersCalled { get; private set; }
        public bool TestMethodWithMultipleParametersCalled { get; private set; }
        public string? StringParam { get; private set; }
        public int IntParam { get; private set; }
        public bool BoolParam { get; private set; }
        public bool PrivateMethodCalled { get; private set; }

        public void TestMethod(string parameter)
        {
            TestMethodCalled = true;
            LastParameter = parameter;
        }

        public void TestMethodWithoutParameters()
        {
            TestMethodWithoutParametersCalled = true;
        }

        public void TestMethodWithMultipleParameters(string stringParam, int intParam, bool boolParam)
        {
            TestMethodWithMultipleParametersCalled = true;
            StringParam = stringParam;
            IntParam = intParam;
            BoolParam = boolParam;
        }

        private void PrivateTestMethod()
        {
            PrivateMethodCalled = true;
        }
    }
}
