using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    [TestClass]
    public class UserTest : CommonApiTest
    {

        [TestMethod]
        public void InfoTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            //获取OpenId
            getTestOpenId(true);

            var result = UserApi.Info(accessToken, _testOpenId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);


            var result = UserApi.Get(accessToken, null);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.total > 0);
            Assert.IsTrue(result.data == null || result.data.openid.Count > 0);
        }

        /// <summary>
        /// 获取所有用户详细资料
        /// </summary>
        [TestMethod]
        public void GetFullUserTest()
        {
            var max = 100;
            string nextOpenId = null;
            var stop = false;
            List<UserInfoJson> userInfoList = new List<UserInfoJson>();
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            while (!stop)
            {
                var result = UserApi.Get(accessToken, nextOpenId);
                nextOpenId = result.next_openid;

                foreach (var id in result.data.openid)
                {
                    var userInfoResult = UserApi.Info(accessToken, id);
                    userInfoList.Add(userInfoResult);
                    Console.WriteLine(userInfoList.Count + ".添加：" + userInfoResult.nickname);

                    if (userInfoList.Count >= max)
                    {
                        stop = true;
                        break;
                    }
                }

                if (nextOpenId == null)
                {
                    stop = true;
                }
            }
        }

        [TestMethod]
        public void BatchGetUserInfoTest()
        {
            List<BatchGetUserInfoData> data = new List<BatchGetUserInfoData>();

            data.Add(new BatchGetUserInfoData()
            {
                openid = "",
                lang = Language.zh_CN
            });

            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = UserApi.BatchGetUserInfo(accessToken, data);

            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}
