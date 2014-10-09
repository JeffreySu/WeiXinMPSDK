using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.CommonAPIs;

namespace Senparc.Weixin.QY.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class DepartmentTest
    {
        protected string _corpId = "wx7618c0a6d9358622"; //换成你的信息
        protected string _corpSecret = "PKrd-r76fDCNjbUY5-9I1vhOkMqBly038Sc8zcODscmu202dqCtUWkxK7nrCGUR4"; //换成你的信息

        public DepartmentTest()
        {
            //全局只需注册一次
            AccessTokenContainer.Register(_corpId, _corpSecret);
        }

        //[TestMethod]
        public int CreateDepartmentTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Department.CreateDepartment(accessToken,"ceshi", 1);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.id > 0);
            return result.id;
        }

        //[TestMethod]
        public void UpdateDepartmentTest(string id)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Department.UpdateDepartment(accessToken,id, "更新");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode ==ReturnCode.请求成功);
        }

        //[TestMethod]
        public void DeleteDepartmentTest(string id)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Department.DeleteDepartment(accessToken, id);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void GetDepartmentListTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Department.GetDepartmentList(accessToken);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        [TestMethod]
        public void TestAllSet()
        {
            string id = CreateDepartmentTest().ToString();
            UpdateDepartmentTest(id);
            DeleteDepartmentTest(id);
            GetDepartmentListTest();
        }
    }
}
