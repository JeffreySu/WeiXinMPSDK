#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using Senparc.CO2NET.Threads;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.RegisterServices;
using Senparc.CO2NET;
using Senparc.Weixin.Entities;
#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.AspNetCore.Hosting;
#endif
using Moq;
using Senparc.WeixinTests;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{
    //已通过测试
    //[TestClass]
    public partial class CommonApiTest : BaseTest
    {
        private dynamic _appConfig;
        protected dynamic AppConfig
        {
            get
            {
                if (_appConfig == null)
                {
#if NETCOREAPP2_0 || NETCOREAPP2_1
                    var filePath = "../../../Config/test.config";
#else
                    var filePath = "../../Config/test.config";
#endif
                    if (File.Exists(filePath))
                    {
#if NETCOREAPP2_0 || NETCOREAPP2_1
                        var stream = new FileStream(filePath, FileMode.Open);
                        var doc = XDocument.Load(stream);
                        stream.Dispose();
#else
                        var doc = XDocument.Load(filePath);
#endif
                        _appConfig = new
                        {
                            AppId = doc.Root.Element("AppId").Value,
                            Secret = doc.Root.Element("Secret").Value,
                            WxOpenAppId = doc.Root.Element("WxOpenAppId").Value,
                            WxOpenSecret = doc.Root.Element("WxOpenSecret").Value,
                            MchId = doc.Root.Element("MchId").Value,
                            TenPayKey = doc.Root.Element("TenPayKey").Value,
                            TenPayCertPath = doc.Root.Element("TenPayCertPath").Value,

                            //WxOpenAppId= doc.Root.Element("WxOpenAppId").Value,
                            //WxOpenSecret = doc.Root.Element("WxOpenSecret").Value
                        };
                    }
                    else
                    {
                        _appConfig = new
                        {
                            AppId = "YourAppId", //换成你的信息
                            Secret = "YourSecret",//换成你的信息
                            WxOpenAppId ="YourWxOpenAppId",//换成你的信息
                            WxOpenSecret = "YourWxOpenSecret",//换成你的信息
                            MchId = "YourMchId",//换成你的信息
                            TenPayKey = "YourTenPayKey",//换成你的信息
                            TenPayCertPath = "YourTenPayCertPath",//换成你的信息
                            //WxOpenAppId="YourWxOpenAppId",//换成你的小程序AppId
                            //WxOpenSecret= "YourWxOpenSecret",//换成你的小程序Secret
                        };
                    }
                }
                return _appConfig;
            }
        }

        protected string _appId
        {
            get { return AppConfig.AppId; }
        }

        protected string _appSecret
        {
            get { return AppConfig.Secret; }
        }


        protected string _wxOpenAppId
        {
            get { return AppConfig.WxOpenAppId; }
        }

        protected string _wxOpenAppSecret
        {
            get { return AppConfig.WxOpenSecret; }
        }

        protected string _mchId
        {
            get { return AppConfig.MchId; }
        }

        protected string _tenPayKey
        {
            get { return AppConfig.TenPayKey; }
        }

        protected string _tenPayCertPath
        {
            get { return AppConfig.TenPayCertPath; }
        }

        //protected string _wxOpenAppId
        //{
        //    get { return AppConfig.WxOpenAppId; }
        //}

        //protected string _wxOpenSecret
        //{
        //    get { return AppConfig.WxOpenSecret; }
        //}

        protected readonly bool _useRedis = false;//是否使用Reids

        /* 由于获取accessToken有次数限制，为了节约请求，
        * 可以到 http://sdk.weixin.senparc.com/Menu 获取Token之后填入下方，
        * 使用当前可用Token直接进行测试。
        */
        private string _access_token = null;

        protected string _testOpenId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//换成实际关注者的OpenId

        /// <summary>
        /// 自动获取Openid
        /// </summary>
        /// <param name="getNew">是否从服务器上强制获取一个</param>
        /// <returns></returns>
        protected string getTestOpenId(bool getNew)
        {
            if (getNew || string.IsNullOrEmpty(_testOpenId))
            {
                var accessToken = AccessTokenContainer.GetAccessToken(_appId);
                var openIdResult = UserApi.Get(accessToken, null);
                _testOpenId = openIdResult.data.openid.First();
            }
            return _testOpenId;
        }

        protected Dictionary<Thread, bool> AsyncThreadsCollection = new Dictionary<Thread, bool>();

        /// <summary>
        /// 异步多线程测试方法
        /// </summary>
        /// <param name="maxThreadsCount"></param>
        /// <param name="openId"></param>
        /// <param name="threadAction"></param>
        protected void TestAyncMethod(int maxThreadsCount, string openId, ThreadStart threadAction)
        {
            //int finishThreadsCount = 0;
            for (int i = 0; i < maxThreadsCount; i++)
            {
                Thread thread = new Thread(threadAction);
                AsyncThreadsCollection.Add(thread, false);
            }

            AsyncThreadsCollection.Keys.ToList().ForEach(z => z.Start());

            while (AsyncThreadsCollection.Count > 0)
            {
                Thread.Sleep(100);
            }
        }


        public CommonApiTest()
        {
            if (_useRedis)
            {
                var redisConfiguration = "localhost:6379";
                RedisManager.ConfigurationOption = redisConfiguration;
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);//Redis
            }

            //全局只需注册一次
            AccessTokenContainer.Register(_appId, _appSecret);

            ////注册小程序
            //if (!string.IsNullOrEmpty(_wxOpenAppId))
            //{
            //    AccessTokenContainer.Register(_wxOpenAppId, _wxOpenSecret);
            //}

            //ThreadUtility.Register();

            //v13.3.0之后，JsApiTicketContainer已经合并入AccessTokenContainer，已经不需要单独注册
            ////全局只需注册一次
            //JsApiTicketContainer.Register(_appId, _appSecret);
        }

        [TestMethod]
        public void GetTokenTest()
        {
            var tokenResult = CommonApi.GetToken(_appId, _appSecret);
            Assert.IsNotNull(tokenResult);
            Assert.IsTrue(tokenResult.access_token.Length > 0);
            Assert.IsTrue(tokenResult.expires_in > 0);
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
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.不合法的APPID);
            }
        }

        [TestMethod]
        public void GetUserInfoTest()
        {
            try
            {
                var accessToken = AccessTokenContainer.GetAccessToken(_appId);
                var result = CommonApi.GetUserInfo(accessToken, _testOpenId);
                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                //如果不参加内测，只是“服务号”，这类接口仍然不能使用，会抛出异常：错误代码：45009：api freq out of limit
            }
        }

        [TestMethod]
        public void GetTicketTest()
        {
            var tokenResult = CommonApi.GetTicket(_appId, _appSecret);
            Assert.IsNotNull(tokenResult);
            Assert.IsTrue(tokenResult.ticket.Length > 0);
            Assert.IsTrue(tokenResult.expires_in > 0);
        }
    }
}
