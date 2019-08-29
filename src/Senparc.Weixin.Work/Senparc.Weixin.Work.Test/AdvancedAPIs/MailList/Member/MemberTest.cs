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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Test.CommonApis;
using Senparc.Weixin.Work.AdvancedAPIs.MailList.Member;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class MemberTest : CommonApiTest
    {
        //[TestMethod]
        public void CreateMemberTest(string userId)
        {
            Extattr extattr = new Extattr()
            {
                attrs = new List<Attr>()
                        {
                            new Attr(){ name = "员工角色",value = "123"}
                        }
            };
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            var memberCreateRequest = new  MemberCreateRequest()
            {
                userid = userId,
                name= string.Format("单元测试生成-{0}", SystemTime.Now.ToString("yyMMdd-HH:mm:ss")),
                english_name = "english name",
                department = new long[] { 2 },
                gender = "1",
                email = "xxx@qq.com",
                extattr = extattr
            };

            var result = MailListApi.CreateMember(accessToken, memberCreateRequest);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        //[TestMethod]
        public void UpdateMemberTest(string userId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            var memberUpdateRequest = new MemberUpdateRequest()
            {
                userid = userId,
                english_name = "new english name",
                department = new long[] { 2 },
                gender = "1",
                email = "498977166@qq.com"
            };

            var result = MailListApi.UpdateMember(accessToken, memberUpdateRequest);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);

            //校验更新成功
            var newMemberData = MailListApi.GetMember(accessToken, userId);
            Assert.AreEqual(memberUpdateRequest.english_name, newMemberData.english_name);
            Assert.AreEqual(memberUpdateRequest.email, newMemberData.email);

            Console.WriteLine("new user info:"+newMemberData.ToJson());
        }

        //[TestMethod]
        public void DeleteMemberTest(string userId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.DeleteMember(accessToken, userId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        //[TestMethod]
        public void BatchDeleteMemberTest(string[] userIds)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.BatchDeleteMember(accessToken, userIds);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        //[TestMethod]
        public void GetMember(string userId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.GetMember(accessToken, userId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        //[TestMethod]
        public void GetDepartmentMemberTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.GetDepartmentMember(accessToken, 2, 0/*, 0*/);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        //[TestMethod]
        public void GetDepartmentMemberInfoTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.GetDepartmentMemberInfo(accessToken, 2, 0/*, 0*/);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        //[TestMethod]
        public void InviteTest(string userId)
        {
            var invateData = new InviteMemberData() {
                 user = new[] { userId},
            };

            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.Invite(accessToken, invateData);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        [TestMethod]
        public void MemberTestAllSet()
        {
            string userId = "33";


            //var userIds = new string[] { "ceshi1", "ceshi2" };
            // BatchDeleteMemberTest(userIds);

            CreateMemberTest(userId);
            Console.WriteLine("完成 CreateMemberTest");

            UpdateMemberTest(userId);
            Console.WriteLine("完成 UpdateMemberTest");

            InviteTest(userId);
            Console.WriteLine("完成 InviteMemberTest");

            GetMember(userId);
            Console.WriteLine("完成 GetMember");

            GetDepartmentMemberTest();
            Console.WriteLine("完成 GetDepartmentMemberTest");

            GetDepartmentMemberInfoTest();
            Console.WriteLine("完成 GetDepartmentMemberInfoTest");

            DeleteMemberTest(userId);
            Console.WriteLine("完成 DeleteMemberTest");

        }
    }
}
