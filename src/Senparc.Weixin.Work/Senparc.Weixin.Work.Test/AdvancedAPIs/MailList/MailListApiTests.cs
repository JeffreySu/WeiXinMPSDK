﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Work.Test.CommonApis;
using Senparc.Weixin.Work.Containers;

namespace Senparc.Weixin.Work.AdvancedAPIs.Tests
{
    [TestClass()]
    public class MailListApiTests : CommonApiTest
    {
        [TestMethod()]
        public void GetDepartmentMemberInfoTest()
        {
            var appKey = AccessTokenContainer.BuildingKey(_corpId, _corpSecret);
            var result = MailListApi.GetDepartmentMemberInfo(appKey, 1, 1/*, Int32.MaxValue*/);
            Assert.IsNotNull(result);
        }
    }
}