using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Apis.Marketing;
using Senparc.Weixin.TenPayV3.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using Senparc.Weixin.TenPayV3.Test;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.TenPayV3.Apis.Tests
{
    [TestClass()]
    public partial class MarketingApisTests : BaseTenPayTest
    {
        string openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//"olPjZjiGtsfaqOhUbOd2puy1wVvc";//换成测试人的 OpenId
    }
}