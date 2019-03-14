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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
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

            //改成自己公众号的OpenId
            var openids = new[] { "oxRg0uLsnpHjb8o93uVnwMK_WAVw","oxRg0uFnf66iXoS_ScybtgjUgK28", "oxRg0uKDWyD8yxgLEFuJFRsI_LQ0" };

            foreach (var item in openids)
            {
                data.Add(new BatchGetUserInfoData()
                {
                    openid = item,
                    lang = Language.zh_CN.ToString()
                });
            }

            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = UserApi.BatchGetUserInfo(accessToken, data);

            Assert.AreEqual(result.errcode, ReturnCode.请求成功);

            var result2 = UserApi.BatchGetUserInfo(_appId, data);

            Assert.AreEqual(result2.errcode, ReturnCode.请求成功);

        }
    }
}
