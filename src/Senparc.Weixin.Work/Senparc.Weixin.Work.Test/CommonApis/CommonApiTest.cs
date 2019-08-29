#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.Cache;
using Moq;
using Senparc.CO2NET.RegisterServices;
using Senparc.CO2NET;
using Senparc.Weixin.Entities;
using Senparc.WeixinTests;

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
using Microsoft.AspNetCore.Hosting;
#endif


namespace Senparc.Weixin.Work.Test.CommonApis
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class CommonApiTest: BaseTest
    {
        private dynamic _appConfig;
        protected dynamic AppConfig
        {
            get
            {
                if (_appConfig == null)
                {
#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
                    var filePath = "../../../Config/test.config";
#else
                    var filePath = "../../Config/test.config";
#endif
                    if (File.Exists(filePath))
                    {
#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
                        var stream = new FileStream(filePath, FileMode.Open);
                        var doc = XDocument.Load(stream);
                        stream.Dispose();
#else
                        var doc = XDocument.Load(filePath);
#endif

                        _appConfig = new
                        {
                            CorpId = doc.Root.Element("CorpId").Value,
                            CorpSecret = doc.Root.Element("CorpSecret").Value,
                        };
                    }
                    else
                    {
                        _appConfig = new
                        {
                            CorpId = "YourAppId", //换成你的信息
                            CorpSecret = "YourSecret",//换成你的信息
                        };
                    }
                }
                return _appConfig;
            }
        }

        protected string _corpId
        {
            get { return AppConfig.CorpId; }
        }

        protected string _corpSecret
        {
            get { return AppConfig.CorpSecret; }
        }


        protected readonly bool _userRedis = true;//是否使用Reids

        public CommonApiTest()
        {
            if (_userRedis)
            {
                var redisConfiguration = "localhost:6379,defaultDatabase=2";
                RedisManager.ConfigurationOption = redisConfiguration;
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);//Redis
                Senparc.CO2NET.Cache.Redis.Register.UseKeyValueRedisNow();//键值对缓存策略（推荐）

                Senparc.Weixin.Cache.Redis.Register.RegisterDomainCache();//进行领域缓存注册
            }


            //全局只需注册一次
            AccessTokenContainer.Register(_corpId, _corpSecret);
        }


        [TestMethod]
        public void GetTokenTest()
        {
            var tokenResult = CommonApi.GetToken(_corpId, _corpSecret);
            Assert.IsNotNull(tokenResult);
            Assert.IsTrue(tokenResult.access_token.Length > 0);
        }

        [TestMethod]
        public void GetTokenFailTest()
        {
            try
            {
                var result = CommonApi.GetToken("appid", "secret");
                Assert.Fail();//上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode_Work.不合法的corpid);
            }
        }

        [TestMethod]
        public void GetCallBackIpTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, _corpSecret);

            var result = CommonApi.GetCallBackIp(accessToken);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }
    }
}
