#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Containers;
using Senparc.Weixin.QY.Test.CommonApis;

namespace Senparc.Weixin.QY.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class DepartmentTest : CommonApiTest
    {
        //[TestMethod]
        public int CreateDepartmentTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.CreateDepartment(accessToken, "test", 1, id: 3);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.id > 0);
            return result.id;
        }

        //[TestMethod]
        public void UpdateDepartmentTest(string id)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.UpdateDepartment(accessToken, id, "更新test", 1);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode ==ReturnCode_QY.请求成功);
        }

        //[TestMethod]
        public void DeleteDepartmentTest(string id)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.DeleteDepartment(accessToken, id);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }

        //[TestMethod]
        public void GetDepartmentListTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.GetDepartmentList(accessToken, 4);
            var result1 = MailListApi.GetDepartmentList(accessToken);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
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
